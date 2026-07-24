# Graph App

A simple .NET 10 console app that signs in a user with Microsoft Entra ID and reads the signed-in user's profile from Microsoft Graph (`/me`).

## Prerequisites

- .NET 10 SDK
- An Azure/Microsoft Entra ID app registration
- A user account in the tenant

## App registration setup

1. Register an application in Microsoft Entra ID.
2. Add a **Public client / mobile & desktop** redirect URI:
   - `http://localhost`
3. Add Microsoft Graph delegated permission:
   - `User.Read`
4. Grant admin consent if required by your tenant policies.

## Configuration

Create or update `graphapp/.env`:

```env
CLIENT_ID=<your-app-client-id>
TENANT_ID=<your-tenant-id>
```

## Run

From the `graphapp` folder:

```powershell
dotnet restore
dotnet run
```

The app opens an interactive browser sign-in flow and then prints:

- Display Name
- Principal Name (UPN)
- User Id

## Packages used

- `Azure.Identity`
- `Microsoft.Graph`
- `dotenv.net`
