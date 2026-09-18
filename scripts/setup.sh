#!/usr/bin/env bash
set -euo pipefail

echo "Checking .NET SDK..."
dotnet --version

echo "Restoring packages..."
dotnet restore PlaywrightSauceDemo.sln

echo "Building solution..."
dotnet build PlaywrightSauceDemo.sln

PLAYWRIGHT_SCRIPT="src/PlaywrightSauceDemo.Tests/bin/Debug/net8.0/playwright.ps1"
if [[ ! -f "$PLAYWRIGHT_SCRIPT" ]]; then
  echo "Playwright installer not found: $PLAYWRIGHT_SCRIPT" >&2
  exit 1
fi

pwsh "$PLAYWRIGHT_SCRIPT" install
echo "Setup complete."
