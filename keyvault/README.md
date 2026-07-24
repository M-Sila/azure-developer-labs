# Azure Key Vault .NET Exercise

This project is a small .NET 10 console app based on the Microsoft Learn exercise for creating and retrieving secrets from Azure Key Vault.

Source exercise: https://microsoftlearning.github.io/mslearn-azure-developer/instructions/azure-secure-solutions/01-key-vault-store-retrieve.html

## What it does

- Connects to Azure Key Vault with `DefaultAzureCredential`
- Creates secrets from a simple console menu
- Lists existing secrets and their values

## Prerequisites

- .NET 10 SDK
- An Azure subscription
- Azure CLI installed and signed in, or Visual Studio signed in to Azure
- An Azure Key Vault you can access

## Azure setup

Before running the app, complete the Azure setup from the Microsoft Learn exercise:

1. Create a resource group.
2. Create an Azure Key Vault.
3. Assign yourself the **Key Vault Secrets Officer** role on the vault.
4. Optionally create a test secret with Azure CLI.

Example Azure CLI commands:

```bash
az group create --name myResourceGroup --location eastus
az keyvault create --name <your-key-vault-name> --resource-group myResourceGroup --location eastus
az keyvault secret set --vault-name <your-key-vault-name> --name MySecret --value "My secret value"
```

## Configuration

Update `keyvault/Program.cs` and replace the placeholder value in `KeyVaultUrl`:

```csharp
string KeyVaultUrl = "https://YOUR-KEYVAULT-NAME.vault.azure.net/";
```

Use your real vault name, for example:

```csharp
string KeyVaultUrl = "https://mykeyvault123.vault.azure.net/";
```

## Run

From the repository root:

```bash
dotnet run --project keyvault/keyvault.csproj
```

Or from the project folder:

```bash
cd keyvault
dotnet run
```

## Authentication notes

The app uses `DefaultAzureCredential` and excludes environment and managed identity credentials. For local development, the simplest options are:

- Sign in with Azure CLI using `az login`
- Sign in to Azure from Visual Studio

Make sure the signed-in identity has permission to set and list secrets in the vault.

## Packages used

- `Azure.Identity`
- `Azure.Security.KeyVault.Secrets`

## Troubleshooting

- If authentication fails, sign in again with `az login` or verify your Visual Studio Azure sign-in.
- If you get authorization errors, confirm your account has the correct Key Vault RBAC role.
- If the app cannot find the vault, verify the `KeyVaultUrl` value matches your vault name.
