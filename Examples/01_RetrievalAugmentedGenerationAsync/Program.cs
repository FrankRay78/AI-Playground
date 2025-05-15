using OpenAI;
using OpenAI.Assistants;
using OpenAI.Files;


var filename = (args.Length == 0 ? "" : args[0]);

if (string.IsNullOrWhiteSpace(filename) || !filename.ToLower().EndsWith(".pdf") || !File.Exists(filename))
{
    Console.WriteLine($"Please specify a PDF file to chat about.");
    return;
}

// Cache the default foreground colour, so we can always get back to it.
var defaultForegroundColor = Console.ForegroundColor;


// Assistants is a beta API and subject to change; acknowledge its experimental status by suppressing the matching warning.
#pragma warning disable OPENAI001

OpenAIClient openAIClient = new(Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
OpenAIFileClient fileClient = openAIClient.GetOpenAIFileClient();
AssistantClient assistantClient = openAIClient.GetAssistantClient();


// Upload the PDF file for the thread.
OpenAIFile fileUploadResponse = fileClient.UploadFile(filename, FileUploadPurpose.Assistants);
Console.WriteLine($"Uploaded: {fileUploadResponse.Filename}" + Environment.NewLine);


// Create the assistant.
var assistantOptions = new AssistantCreationOptions
{
    Name = "File question answerer",
    Instructions = "Answer questions from the user about the provided file.",
    Tools = { ToolDefinition.CreateFileSearch() },
};
var assistant = await assistantClient.CreateAssistantAsync("gpt-4o", assistantOptions);


// Ask the user to pose a question.
Console.WriteLine("Ask a question about the file (empty response + enter to quit):");

Console.ForegroundColor = ConsoleColor.Yellow;
var question = Console.ReadLine();
Console.ForegroundColor = defaultForegroundColor;


var thread = await assistantClient.CreateThreadAsync();

while (!string.IsNullOrWhiteSpace(question))
{
    // Create a new message with the question and uploaded file Id.
    var messageCreationOptions = new MessageCreationOptions();
    messageCreationOptions.Attachments.Add(new MessageCreationAttachment(fileUploadResponse.Id, new List<ToolDefinition>() {ToolDefinition.CreateFileSearch()}));
    await assistantClient.CreateMessageAsync(thread.Value.Id, MessageRole.User, new List<MessageContent>() { MessageContent.FromText(question)}, messageCreationOptions);

    await foreach (StreamingUpdate streamingUpdate in assistantClient.CreateRunStreamingAsync(thread.Value.Id, assistant.Value.Id, new RunCreationOptions()))
    {
        if (streamingUpdate.UpdateKind == StreamingUpdateReason.RunCreated)
        {
            Console.WriteLine(Environment.NewLine + $"--- Thinking ---" + Environment.NewLine);
        }
        else if (streamingUpdate is MessageContentUpdate contentUpdate)
        {
            if (contentUpdate?.TextAnnotation?.InputFileId == fileUploadResponse.Id)
            {
                // Remarks:
                //
                // 1. contentUpdate?.TextAnnotation.TextToReplace
                // seems to contain annotations that come through contentUpdate?.Text below
                // (which we've already written to the console given it's streamed, not buffered)
                //
                // 2. As we only support one file upload, I've commented out writing the Filename.

                // Console.Write(" (From: " + fileUploadResponse.Filename + ")");
            }
            else
            {
                // Lets avoid printing annotations for niceness of console output.
                // These look something like: 【9:1†Japan.pdf】

                if ((contentUpdate?.Text ?? "").Split(['【', '】']).Length == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(contentUpdate?.Text);
                    Console.ForegroundColor = defaultForegroundColor;
                }
            }
        }
    }

    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine("Your response (empty response + enter to quit):");

    Console.ForegroundColor = ConsoleColor.Yellow;
    question = Console.ReadLine();
    Console.ForegroundColor = defaultForegroundColor;
}


// Clean up the resources
Console.WriteLine("Cleaning up and exiting...");
await assistantClient.DeleteThreadAsync(thread.Value.Id);
await assistantClient.DeleteAssistantAsync(assistant.Value.Id);
await fileClient.DeleteFileAsync(fileUploadResponse.Id);
