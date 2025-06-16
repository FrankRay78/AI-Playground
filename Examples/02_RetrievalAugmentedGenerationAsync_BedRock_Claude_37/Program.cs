using Amazon;
using Amazon.BedrockRuntime;
using Amazon.BedrockRuntime.Model;

// Create a Bedrock Runtime client in the AWS Region you want to use.
var client = new AmazonBedrockRuntimeClient(RegionEndpoint.EUWest1);

// Set the model ID, e.g., Claude 3 Haiku.
//var modelId = "anthropic.claude-3-7-sonnet-20250219-v1:0";   // Arrgh. This model requires provisioned throughput and inference profile.
var modelId = "anthropic.claude-3-sonnet-20240229-v1:0";

// Define the user message.
var userPrompt = "Tell me a little known fact about Japan.";
var fileAttachment = new MemoryStream(File.ReadAllBytes("Japan.pdf"));

// Create a request with the model ID, the user message, and an inference configuration.
var request = new ConverseRequest
{
    ModelId = modelId,
    Messages = new List<Message>
    {
        new Message
        {
            Role = ConversationRole.User,
            Content = new List<ContentBlock>
            {
                new ContentBlock
                {
                    Text = userPrompt,
                },
                new ContentBlock
                {
                    Document = new DocumentBlock
                    {
                        Format = DocumentFormat.Pdf,
                        Name = "Japan (PDF)",
                        Source = new DocumentSource
                        {
                            Bytes = fileAttachment,
                        }
                    }
                }
            },
        }
    },
    InferenceConfig = new InferenceConfiguration()
    {
        MaxTokens = 512,
        Temperature = 0.5F,
        TopP = 0.9F
    }
};

try
{
    // Send the request to the Bedrock Runtime and wait for the result.
    var response = await client.ConverseAsync(request);

    // Extract and print the response text.
    string responseText = response?.Output?.Message?.Content?[0]?.Text ?? "";
    Console.WriteLine(responseText);
}
catch (AmazonBedrockRuntimeException e)
{
    Console.WriteLine($"ERROR: Can't invoke '{modelId}'. Reason: {e.Message}");
    throw;
}