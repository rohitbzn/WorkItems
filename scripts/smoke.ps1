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

# Wait for the service to start up (adjust delay if needed)
Start-Sleep -Seconds 5

$healthUrl = "http://localhost:8085/api/v1/health"
Write-Host "Calling health endpoint: $healthUrl"
try {
    $response = Invoke-WebRequest -Uri $healthUrl -UseBasicParsing -TimeoutSec 10
    Write-Host "Health endpoint response:"
    Write-Host $response.Content
} catch {
    Write-Error "Failed to call health endpoint: $_"
}

Write-Host "Stopping the service..."
Stop-Process -Id $serviceProcess.Id -Force

Write-Host "Smoke test complete."