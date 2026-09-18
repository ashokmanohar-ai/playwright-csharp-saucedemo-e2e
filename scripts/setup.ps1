$ErrorActionPreference = "Stop"

Write-Host "Checking .NET SDK..."
dotnet --version

Write-Host "Restoring packages..."
dotnet restore PlaywrightSauceDemo.sln

Write-Host "Building solution..."
dotnet build PlaywrightSauceDemo.sln

$playwright = "src/PlaywrightSauceDemo.Tests/bin/Debug/net8.0/playwright.ps1"
if (-not (Test-Path $playwright)) { throw "Playwright installer was not generated at $playwright" }

Write-Host "Installing Playwright browsers..."
pwsh $playwright install

Write-Host "Setup complete."
