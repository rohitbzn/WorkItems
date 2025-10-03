# Ensure script stops on error
$ErrorActionPreference = "Stop"

Write-Host "Restoring NuGet packages..."
dotnet restore ..

Write-Host "Building the solution..."
dotnet build .. --configuration Release

# Path to the service executable (adjust if needed)
$serviceExe = "..\WorkItems.Service\bin\Release\net8.0\WorkItems.Service.exe"

if (!(Test-Path $serviceExe)) {
    Write-Error "Service executable not found at $serviceExe"
    exit 1
}

Write-Host "Starting the service..."
$serviceProcess = Start-Process -FilePath $serviceExe -PassThru
Write-Host "Service started with PID: $($serviceProcess.Id)"

# Wait for the service to start up
Start-Sleep -Seconds 10

# Confirm service is still running
if (!(Get-Process -Id $serviceProcess.Id -ErrorAction SilentlyContinue)) {
    Write-Error "Service process exited prematurely."
    exit 1
}

$healthUrl = "http://127.0.0.1:9876/api/v1/health"
Write-Host "Calling health endpoint: $healthUrl"

try {
    $response = Invoke-WebRequest -Uri $healthUrl -UseBasicParsing -TimeoutSec 10
    Write-Host "Health endpoint response:"
    Write-Host $response.Content
} catch {
    Write-Error "Failed to call health endpoint: $_"
    Stop-Process -Id $serviceProcess.Id -Force
    exit 1
}

Write-Host "Stopping the service..."
Stop-Process -Id $serviceProcess.Id -Force

Write-Host "Smoke test complete."