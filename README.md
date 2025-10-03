# WorkItems Solution

A layered Windows ecosystem for managing Work Items, featuring:
- OWIN self-hosted Web/API (.NET 8) with Entity Framework 6 and Serilog
- WinForms client (.NET 8) with Serilog
- Shared DTOs and types (.NET Standard 2.0)
- Automated smoke test script

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb) (default, or update connection string for SQL Server Express)
- Windows OS (for WinForms client and service)
- PowerShell (for running the smoke test script)

---

## Build Instructions

1. **Clone the repository:**

2. **Restore dependencies and build:**

---

## Configuration

### Service

- The service uses `App.config` for its connection string:
- API Key is set in `ApiKeyMiddleware.cs` (update `"YourSecretApiKey"` as needed).

### Client

- The client uses `appsettings.json`:
- Update `ApiBaseUrl` and `ApiKey` as needed.

---

## Run Instructions

### Service

1. **Start the service:**

The service will listen on `http://localhost:8085/`.

---

## Smoke Test

A PowerShell script is provided to validate the build and service health endpoint.

1. **Run the smoke test:** This will:
- Restore and build the solution
- Start the service
- Call the health endpoint (`http://localhost:8085/api/v1/health`)
- Print the result
- Stop the service

---

## Notes

- If you change the API key, update it in both the service and client configuration.
- If LocalDB is not available, update the connection string in `App.config` to use SQL Server Express or another SQL Server instance.
- Logs are written to the `logs` directory in both service and client.

---

## Troubleshooting

- Ensure no other process is using port 8085.
- If the service fails to start due to permissions, run the terminal as Administrator or reserve the URL ACL:

---

## License

MIT