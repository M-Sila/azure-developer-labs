# Azure Cosmos DB .NET Exercise

This project is a small .NET 10 console app that connects to Azure Cosmos DB, creates a database and container if needed, and inserts a sample item.

## Prerequisites

- .NET 10 SDK
- An Azure Cosmos DB account (SQL API)
- Cosmos DB endpoint URI and primary key

## Configuration

Create a `.env` file in the `cosmosdb` project folder (`cosmosdb/.env`) with:

```env
DOCUMENT_ENDPOINT=https://<your-account>.documents.azure.com:443/
ACCOUNT_KEY=<your-primary-key>
```

> Do not commit `.env` or secrets to source control.

## Run

From repository root:

```bash
dotnet run --project cosmosdb/cosmosdb.csproj
```

Or from the project folder:

```bash
cd cosmosdb
dotnet run
```

## What it does

- Loads values from `.env`
- Connects to Cosmos DB
- Creates database: `myDatabase`
- Creates container: `myContainer` with partition key `/id`
- Inserts one sample `Product` document

## Troubleshooting

- If environment values are missing, verify `cosmosdb/.env` exists and contains both keys.
- If authentication fails, verify endpoint/key are correct and not expired.
