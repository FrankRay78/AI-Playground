# C# .NET OpenAI GPT-4 Assistant for Local File Search

This is small sample project that shows you how to:

- Upload a local PDF file to the OpenAI GPT-4 Assistant Service using the OpenAI API
- Use the File Search tool to ask natural language questions about the file
- Keep the conversation thread active for follow-up questions
- Clean up resources when complete

Presented as a single Program.cs C# .NET 9.0 Console app.

## Usage

* Your OpenAI API key should be stored in an environment variable called `OPENAI_API_KEY`, which is accessible to the running console app
* The first argument of the console app must be a local PDF file

## Notes

* OpenAI has a reference example here similar to this, [here](https://github.com/openai/openai-dotnet/blob/main/examples/Assistants/Example01_RetrievalAugmentedGenerationAsync.cs).

## Credits

Many thanks to Ed Andersen, who provided the original [tutorial](https://www.youtube.com/watch?v=FDDpkm9vCAI) and [codebase](https://github.com/edandersen/csharp-openai-assistants-dotnet-console). 

My contribution was entirely removing the Azure Nuget and reworking the codebase to run against the [OpenAI dotnet client](https://github.com/openai/openai-dotnet).
