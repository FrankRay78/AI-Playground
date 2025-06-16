# C# .NET AWS BedRock Claude 3.7 Assistant for Local File Search

<br />

> [!IMPORTANT]  
>  This example is actually using Claude 3 Sonnet, not Claude 3.7 Sonnet, for the reasons outlined [here](https://github.com/FrankRay78/AI-Playground/issues/2). 

<br />

A small sample project that shows you how to:

- Upload a local PDF file to AWS BedRock
- Ask a natural language question about the file

Presented as a single Program.cs C# .NET 9.0 Console app.

## Pre-requisites

* You must have a profile called "mfa-base" configured with your long-term AWS credentials.
* You must have your MFA device configured in your AWS config file.
* Run `get-session.ps1` to generate temporary session credentials.
* The script will prompt you for your MFA code.

## Usage

Executing this example will:

- Upload [Japan.pdf](https://github.com/FrankRay78/AI-Playground/blob/main/Examples/02_RetrievalAugmentedGenerationAsync_BedRock_Claude_37/Japan.pdf) to Amazon BedRock.
- Ask the question: "Tell me a little known fact about Japan."

![Example 02 screenshot](https://github.com/user-attachments/assets/e1e7222c-66c2-4514-ad06-63ad00360de8)

## Notes

* AWS has a reference example here similar to this, [here](https://github.com/awsdocs/aws-doc-sdk-examples/blob/main/dotnetv4/Bedrock-runtime/Models/AnthropicClaude/Converse/Converse.cs).

## References

- **[Credential and profile resolution – AWS SDK for .NET V4](https://docs.aws.amazon.com/sdk-for-net/v4/developer-guide/creds-assign.html)**  
  Explains how the SDK locates credentials using environment variables, shared profiles, and SDK store :contentReference[oaicite:1]{index=1}.

- **[STS GetSessionToken – AWS CLI Reference](https://docs.aws.amazon.com/cli/latest/reference/sts/get-session-token.html)**  
  Shows how to generate temporary session credentials using `aws sts get-session-token` with MFA :contentReference[oaicite:2]{index=2}.

- **[GetSessionToken with MFA example – IAM User Guide](https://docs.aws.amazon.com/IAM/latest/UserGuide/sts_example_sts_GetSessionToken_section.html)**  
  Provides guidance and examples on using `GetSessionToken` with MFA in scripts :contentReference[oaicite:3]{index=3}.
