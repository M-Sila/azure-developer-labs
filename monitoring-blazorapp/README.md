# Exercise: Monitor an application with autoinstrumentation

This project follows the Microsoft Learn exercise:
- https://learn.microsoft.com/en-us/training/modules/monitor-app-performance/6a-monitor-application-instrumentation

## Objective
Create an Azure App Service web app with Application Insights enabled, deploy this Blazor app, and monitor requests, failures, and traces without changing app code.

## Project details
- Target framework: `.NET 10`
- App type: `Blazor Web App (Interactive Server)`
- Entry point: `Program.cs`

## Prerequisites
- Azure subscription
- Azure CLI installed and signed in
- .NET 10 SDK

## 1) Create Azure resources
Run in PowerShell (replace values as needed):

```powershell
az login

$RESOURCE_GROUP="rg-monitoring-lab"
$LOCATION="eastus"
$PLAN="plan-monitoring-lab"
$WEBAPP="monitoring-blazorapp-$((Get-Random -Maximum 99999))"
$INSIGHTS="appi-monitoring-lab-$((Get-Random -Maximum 99999))"

az group create --name $RESOURCE_GROUP --location $LOCATION
az appservice plan create --name $PLAN --resource-group $RESOURCE_GROUP --sku B1 --is-linux
az monitor app-insights component create --app $INSIGHTS --location $LOCATION --resource-group $RESOURCE_GROUP --application-type web
az webapp create --name $WEBAPP --resource-group $RESOURCE_GROUP --plan $PLAN --runtime "DOTNETCORE|9.0"
```

## 2) Configure Application Insights autoinstrumentation

```powershell
$CONNECTION_STRING=$(az monitor app-insights component show --app $INSIGHTS --resource-group $RESOURCE_GROUP --query connectionString -o tsv)

az webapp config appsettings set --name $WEBAPP --resource-group $RESOURCE_GROUP --settings `
  APPLICATIONINSIGHTS_CONNECTION_STRING=$CONNECTION_STRING `
  ApplicationInsightsAgent_EXTENSION_VERSION=~3
```

## 3) Publish and deploy the Blazor app
From the `monitoring-blazorapp` folder:

```powershell
dotnet publish -c Release -o .\publish
Compress-Archive -Path .\publish\* -DestinationPath .\publish\app.zip -Force
az webapp deploy --name $WEBAPP --resource-group $RESOURCE_GROUP --src-path .\publish\app.zip --type zip
```

## 4) Generate traffic and failures
- Open the app URL:

```powershell
$APP_URL="https://$WEBAPP.azurewebsites.net"
Write-Host $APP_URL
```

- Browse pages such as `/`, `/counter`, and `/weather`.
- To generate a 404 for testing status code monitoring, browse:
  - `https://<webapp-name>.azurewebsites.net/does-not-exist`

## 5) View telemetry in Application Insights
In Azure Portal, open the Application Insights resource and review:
- `Live Metrics`
- `Transaction search`
- `Failures`
- `Application map`

Optional KQL checks (Logs):

```kusto
requests
| where timestamp > ago(15m)
| order by timestamp desc
```

```kusto
exceptions
| where timestamp > ago(15m)
| order by timestamp desc
```

## 6) Clean up resources

```powershell
az group delete --name $RESOURCE_GROUP --yes --no-wait
```
