using Microsoft.Identity.Client;
using dotenv.net;

// Load environment variables from .env file
var envVars = DotEnv.Read(options: new DotEnvOptions(
    probeForEnv: true,
    probeLevelsToSearch: 8,
    ignoreExceptions: false,
    trimValues: true
));

// Retrieve Azure AD Application ID and tenant ID from environment variables
envVars.TryGetValue("CLIENT_ID", out var _clientId);
envVars.TryGetValue("TENANT_ID", out var _tenantId);

// Define the scopes required for authentication
string[] _scopes = { "User.Read" };

// Build the MSAL public client application with authority and redirect URI
var app = PublicClientApplicationBuilder.Create(_clientId)
    .WithAuthority(AzureCloudInstance.AzurePublic, _tenantId)
    .WithDefaultRedirectUri()
    .Build();

// Attempt to acquire an access token silently or interactively
AuthenticationResult result;
try
{
    // Try to acquire token silently from cache for the first available account
    var accounts = await app.GetAccountsAsync();
    result = await app.AcquireTokenSilent(_scopes, accounts.FirstOrDefault())
                .ExecuteAsync();
}
catch (MsalUiRequiredException)
{
    // If silent token acquisition fails, prompt the user interactively
    result = await app.AcquireTokenInteractive(_scopes)
                .ExecuteAsync();
}

// Output the acquired access token to the console
Console.WriteLine($"Access Token:\n{result.AccessToken}");