# Azure Queue Storage - Send and Receive Messages

This project demonstrates how to send and receive messages using Azure Queue Storage in .NET.

## Module Reference

This implementation is based on the Microsoft Learn module: [Send and receive messages by using Azure Queue Storage](https://learn.microsoft.com/en-us/training/modules/discover-azure-message-queue/8a-send-receive-messages-queue-storage)

## Prerequisites

- Azure subscription
- Azure Storage Account with Queue Storage enabled
- .NET 10 SDK or later
- Visual Studio, Visual Studio Code, or another code editor

## Setup

### 1. Create an Azure Storage Account

If you don't have a storage account, create one:

```bash
az storage account create \
  --name <YOUR-STORAGE-ACCT-NAME> \
  --resource-group <YOUR-RESOURCE-GROUP> \
  --location eastus
```

### 2. Configure Credentials

Update the `Program.cs` file with your storage account name:

```csharp
string storageAccountName = "<YOUR-STORAGE-ACCT-NAME>";
```

The application uses `DefaultAzureCredential` for authentication, which supports:
- Environment variables
- Visual Studio authentication
- Azure CLI authentication
- Other credential chains

**Note:** The current configuration excludes environment credentials and managed identity credentials. Modify the `DefaultAzureCredentialOptions` if needed for your environment.

### 3. Install Dependencies

Ensure the required NuGet packages are installed:

```bash
dotnet add package Azure.Storage.Queues
dotnet add package Azure.Identity
```

## Running the Application

1. Build the project:
```bash
dotnet build
```

2. Run the application:
```bash
dotnet run
```

## What the Application Does

The application demonstrates the following Queue Storage operations:

### 1. **Create a Queue**
Creates a new queue with a unique name based on a GUID.

### 2. **Send Messages**
Sends three messages to the queue:
- Message 1
- Message 2
- Message 3 (with receipt saved for later use)

### 3. **Peek at Messages**
Views messages in the queue without removing them. This is useful for inspecting message content without consuming them.

### 4. **Update a Message**
Updates an existing message using the receipt obtained from the send operation. This demonstrates how to modify message content.

### 5. **Receive and Delete Messages**
- Receives messages from the queue
- Processes each message (in this case, just displays it)
- Deletes messages after processing is complete

### 6. **Delete the Queue**
Cleans up by deleting the queue.

## Key Concepts

### QueueClient
The `QueueClient` is the primary class for interacting with Azure Queue Storage. It provides methods for:
- Creating and deleting queues
- Sending, receiving, peeking, and updating messages
- Managing queue properties

### Message Receipt
When sending a message, the application receives a `SendReceipt` containing:
- `MessageId`: Unique identifier for the message
- `PopReceipt`: Receipt required for updating or deleting messages

### Peeking vs. Receiving
- **Peek:** View messages without removing them from the queue
- **Receive:** Remove messages from the queue (after deleting them)

## Interactive Workflow

The application is interactive and waits for user input at key points, allowing you to:
1. Review actions before proceeding
2. Observe the state of the queue after each operation
3. Understand the complete message lifecycle

## Troubleshooting

### Authentication Issues
- Ensure you're signed in with `az login` or have credentials configured in Visual Studio
- Verify your Azure subscription has access to the storage account

### Queue Not Found
- Confirm the storage account name is correct
- Ensure the storage account exists in your subscription

### Access Denied
- Verify your user account has the "Storage Queue Data Contributor" or equivalent role on the storage account

## Next Steps

- Explore queue triggers in Azure Functions
- Implement message batching for better performance
- Add error handling and retry logic
- Integrate with background job processing

## Resources

- [Azure Queue Storage Documentation](https://learn.microsoft.com/en-us/azure/storage/queues/storage-queues-introduction)
- [Azure.Storage.Queues NuGet Package](https://www.nuget.org/packages/Azure.Storage.Queues/)
- [Azure.Identity NuGet Package](https://www.nuget.org/packages/Azure.Identity/)
- [Azure Storage Code Samples (.NET)](https://learn.microsoft.com/en-us/samples/browse/?products=azure-storage)
