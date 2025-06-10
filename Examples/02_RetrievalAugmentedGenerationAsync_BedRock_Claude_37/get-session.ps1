# PowerShell script to get temporary AWS session credentials using MFA

# Set-PSDebug -Trace 1
# Set-PSDebug -Off


############################################################################
###
### You must have a profile called "mfa-base" configured with your 
### long-term AWS credentials.
###
############################################################################


$BaseProfile = "mfa-base"  # Name of your AWS CLI profile with long-term credentials

# Validate base profile exists
$check = aws configure get aws_access_key_id --profile $BaseProfile
if (-not $check) {
    Write-Error "Base profile '$BaseProfile' not found or incomplete. Please run 'aws configure --profile $BaseProfile' first."
    exit 1
}


# Look up variables dynamically
$AccountId = aws sts get-caller-identity --profile $BaseProfile --query Account --output text
$Username  = aws sts get-caller-identity --profile $BaseProfile --query Arn --output text |
             ForEach-Object { ($_ -split "/")[-1] }

# You must configure your MFA device name in your AWS config (eg. ~/.aws/config)
$MfaDeviceName = aws configure get mfa_device_name --profile $BaseProfile
$MfaArn = "arn:aws:iam::${AccountId}:mfa/${MfaDeviceName}"


# Prompt for MFA token
$MfaCode = Read-Host "Enter current MFA token for $Username"


# Call AWS STS to get session token using the base profile
$SessionJson = aws sts get-session-token `
    --serial-number $MfaArn `
    --token-code $MfaCode `
    --profile $BaseProfile `
    --output json
if (-not $SessionJson) {
    Write-Error "AWS CLI did not return session credentials."
    exit 1
}


# Parse credentials
$Session = $SessionJson | ConvertFrom-Json

$AccessKey = $Session.Credentials.AccessKeyId
$SecretKey = $Session.Credentials.SecretAccessKey
$SessionToken = $Session.Credentials.SessionToken
$Expiration = $Session.Credentials.Expiration


Write-Host "Session expires at: $Expiration"
Write-Host "Setting environment variables..."


# Set environment variables for current PowerShell session
$env:AWS_ACCESS_KEY_ID = $AccessKey
$env:AWS_SECRET_ACCESS_KEY = $SecretKey
$env:AWS_SESSION_TOKEN = $SessionToken


Write-Host "`n✅ AWS environment variables set for this session."
