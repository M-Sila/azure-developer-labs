# MSAL Auth App

A .NET 10 console application that demonstrates Azure AD authentication using the Microsoft Authentication Library (MSAL). This app authenticates users and retrieves access tokens for Microsoft Graph API.

## Overview

This application showcases how to:
- Set up MSAL public client application
- Configure Azure AD authentication with tenant and client IDs
- Acquire access tokens either silently from cache or interactively from the user
- Handle authentication errors and retry with interactive authentication

## Prerequisites

- .NET 10 SDK or later
- Azure AD application registration with:
  - Application (Client) ID
  - Directory (Tenant) ID
  - Default redirect URI configured
- An Azure AD tenant with user accounts

## Project Structure

```
msal-authapp/
├── Program.cs          # Main application logic
├── msal-authapp.csproj # Project configuration
├── .env               # Environment variables (CLIENT_ID and TENANT_ID)
└── README.md          # This file
```

## Configuration

The application uses environment variables stored in the `.env` file:

```
CLIENT_ID="your-azure-ad-client-id"
TENANT_ID="your-azure-ad-tenant-id"
```

### Setting Up Azure AD

1. Go to [Azure Portal](https://portal.azure.com)
2. Navigate to Azure Active Directory → App registrations
3. Create a new application registration
4. Copy the **Application (Client) ID** and **Directory (Tenant) ID**
5. Add these values to the `.env` file

## Dependencies

- **Microsoft.Identity.Client** (v4.86.1) - For Azure AD authentication
- **dotenv.net** (v4.0.2) - For loading environment variables from `.env` file

## Usage

### Running the Application

```bash
dotnet run
```

### Authentication Flow

1. **Silent Authentication**: The app first attempts to acquire a token silently from the MSAL cache for any cached account
2. **Interactive Authentication**: If silent acquisition fails (e.g., for first-time users), the app prompts an interactive browser-based login
3. **Token Output**: The acquired access token is displayed in the console

### Sample Output

```
Access Token:
eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IjVaQnVy...
```

## Scopes

The application requests the following Microsoft Graph scopes:
- `User.Read` - Read signed-in user's profile

To request additional scopes, modify the `_scopes` array in `Program.cs`:

```csharp
string[] _scopes = { "User.Read", "Mail.Read" };
```

## Error Handling

The application handles authentication errors gracefully:
- **MsalUiRequiredException**: Triggered when silent token acquisition fails; switches to interactive authentication
- The user is prompted to sign in interactively when needed

## Security Considerations

- ⚠️ **Never commit the `.env` file** with real credentials to version control
- Add `.env` to `.gitignore` in production environments
- Use Azure Key Vault or managed identities for production deployments
- Ensure the application registration has appropriate permissions configured

## Building

```bash
dotnet build
```

## Further Reading

- [MSAL Documentation](https://learn.microsoft.com/en-us/entra/msal/dotnet/)
- [Microsoft Graph API](https://learn.microsoft.com/en-us/graph/overview)
- [Azure AD Authentication](https://learn.microsoft.com/en-us/entra/identity-platform/authentication-scenarios)
