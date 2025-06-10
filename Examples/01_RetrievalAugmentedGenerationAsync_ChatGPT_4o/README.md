# C# .NET OpenAI GPT-4 Assistant for Local File Search

This is small sample project that shows you how to:

- Upload a local PDF file to the OpenAI GPT-4 Assistant Service using the OpenAI API
- Use the File Search tool to ask natural language questions about the file
- Keep the conversation thread active for follow-up questions
- Clean up resources when complete

Presented as a single Program.cs C# .NET 9.0 Console app.

![01_Screenshot](https://github.com/user-attachments/assets/93e3d956-7788-48bf-89f0-66a293043d93)

## Usage

* Your OpenAI API key should be stored in an environment variable called `OPENAI_API_KEY`
* The environment variable should be accessible to the running console app
* The first argument of the console app must be a local PDF file
* [Japan.pdf](https://github.com/FrankRay78/AI-Playground/blob/main/Examples/01_RetrievalAugmentedGenerationAsync_ChatGPT_4o/Japan.pdf) has been provided to help you get started

## Notes

* OpenAI has a reference example here similar to this, [here](https://github.com/openai/openai-dotnet/blob/main/examples/Assistants/Example01_RetrievalAugmentedGenerationAsync.cs).

## Credits

Many thanks to [Ed Andersen](https://github.com/edandersen), who provided the original [tutorial](https://www.youtube.com/watch?v=FDDpkm9vCAI) and [codebase](https://github.com/edandersen/csharp-openai-assistants-dotnet-console). 

My contribution was removing the Azure Nuget and reworking the codebase to run entirely against the [OpenAI dotnet client](https://github.com/openai/openai-dotnet).
