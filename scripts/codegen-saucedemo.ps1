$ErrorActionPreference = "Stop"
dotnet build PlaywrightSauceDemo.sln
$playwright = "src/PlaywrightSauceDemo.Tests/bin/Debug/net8.0/playwright.ps1"
if (-not (Test-Path $playwright)) { throw "Playwright installer was not generated at $playwright" }
pwsh $playwright codegen https://www.saucedemo.com
