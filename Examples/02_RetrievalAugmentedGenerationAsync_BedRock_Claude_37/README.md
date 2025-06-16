# C# .NET AWS BedRock Claude 3.7 Assistant for Local File Search

This is small sample project that shows you how to:

- Upload a local PDF file to AWS BedRock using the AWS BedRock API
- Use the File Search tool to ask natural language questions about the file
- Keep the conversation thread active for follow-up questions
- Clean up resources when complete

Presented as a single Program.cs C# .NET 9.0 Console app.

## Usage

* You must have a profile called "mfa-base" configured with your long-term AWS credentials.
* You must have your MFA device configured in your AWS config file.
* Run `get-session.ps1` to generate temporary session credentials.
* The first argument of the console app must be a local PDF file
* [Japan.pdf](https://github.com/FrankRay78/AI-Playground/blob/main/Examples/02_RetrievalAugmentedGenerationAsync_BedRock_Claude_37/Japan.pdf) has been provided to help you get started

## Notes

* AWS has a reference example here similar to this, [here](https://github.com/awsdocs/aws-doc-sdk-examples/blob/main/dotnetv4/Bedrock-runtime/Models/AnthropicClaude/Converse/Converse.cs).

## References

- **[Credential and profile resolution – AWS SDK for .NET V4](https://docs.aws.amazon.com/sdk-for-net/v4/developer-guide/creds-assign.html)**  
  Explains how the SDK locates credentials using environment variables, shared profiles, and SDK store :contentReference[oaicite:1]{index=1}.

- **[STS GetSessionToken – AWS CLI Reference](https://docs.aws.amazon.com/cli/latest/reference/sts/get-session-token.html)**  
  Shows how to generate temporary session credentials using `aws sts get-session-token` with MFA :contentReference[oaicite:2]{index=2}.

- **[GetSessionToken with MFA example – IAM User Guide](https://docs.aws.amazon.com/IAM/latest/UserGuide/sts_example_sts_GetSessionToken_section.html)**  
  Provides guidance and examples on using `GetSessionToken` with MFA in scripts :contentReference[oaicite:3]{index=3}.