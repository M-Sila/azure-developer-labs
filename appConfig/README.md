# Exercise: Retrieve configuration settings from Azure App Configuration

This project follows the Microsoft Learn exercise:
- https://learn.microsoft.com/en-us/training/modules/implement-azure-app-configuration/5a-retrieve-configuration-settings

## Objective
Create an Azure App Configuration resource, store a configuration value, and retrieve it from this .NET console app.

## Project details
- Target framework: `.NET 10`
- Main file: `Program.cs`
- Packages:
  - `Azure.Identity`
  - `Microsoft.Extensions.Configuration.AzureAppConfiguration`

## Prerequisites
- Azure subscription
- Azure CLI installed and signed in
- .NET 10 SDK

## 1) Create Azure resources
Run these commands in PowerShell (replace values as needed):

```powershell
az login

$RESOURCE_GROUP="rg-appconfig-lab"
$LOCATION="eastus"
$APP_CONFIG_NAME="appconfig$((Get-Random -Maximum 99999))"

az group create --name $RESOURCE_GROUP --location $LOCATION
az appconfig create --name $APP_CONFIG_NAME --resource-group $RESOURCE_GROUP --location $LOCATION --sku Standard
```

## 2) Add a configuration setting
Add the key used by this app:

```powershell
az appconfig kv set --name $APP_CONFIG_NAME --key "Dev:conStr" --value "Server=tcp:demo.database.windows.net,1433;Database=DemoDb;" --yes
```

## 3) Update the endpoint in code
In `Program.cs`, replace:
- `https://YOUR_APP_CONFIGURATION_NAME.azconfig.io`

with:
- `https://<your-app-config-name>.azconfig.io`

## 4) Run the app
From the `appConfig` folder:

```powershell
dotnet run
```

Expected output is the value stored in `Dev:conStr`.

## Authentication notes
This app uses `DefaultAzureCredential` with these options disabled:
- `EnvironmentCredential`
- `ManagedIdentityCredential`

So local interactive/developer sign-in methods are used.

## 5) Clean up resources
When done:

```powershell
az group delete --name $RESOURCE_GROUP --yes --no-wait
```
