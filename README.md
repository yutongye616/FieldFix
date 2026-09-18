# FieldFix

FieldFix is a small service-request tracking application designed for transit and facility operations. It demonstrates a .NET/C# web implementation and a Microsoft Power Platform deployment model.

## .NET / C# web app

The project is in the `FieldFix` folder and uses ASP.NET Core Razor Pages.

### Run locally

```bash
export PATH="$HOME/.dotnet:$PATH"
cd /Users/yutongye/Desktop/demo/FieldFix/FieldFix
dotnet restore
dotnet run --urls http://localhost:5007
```

Then open: http://localhost:5007

### Build for deployment

```bash
export PATH="$HOME/.dotnet:$PATH"
cd /Users/yutongye/Desktop/demo/FieldFix/FieldFix
dotnet publish -c Release -o ./publish
```

The published app can be deployed to Azure App Service, IIS, or any ASP.NET Core host.

## Microsoft Power Platform version

See the blueprint in `PowerPlatform/FieldFix-PowerApps-Blueprint.md`.

It describes:
- Dataverse tables for service requests, assets, technicians, and work orders
- Canvas app screens for dashboard, request form, and dispatch queue
- Power Automate flows for notifications and escalations
- Security and deployment steps for Microsoft Power Platform

## Project purpose

This version is tailored for a field operations workflow such as:
- track maintenance requests
- assign work to technicians
- monitor priority and status
- act on operational issues quickly
