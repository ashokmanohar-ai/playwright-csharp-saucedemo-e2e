#!/usr/bin/env bash
set -euo pipefail

dotnet build PlaywrightSauceDemo.sln
PLAYWRIGHT_SCRIPT="src/PlaywrightSauceDemo.Tests/bin/Debug/net8.0/playwright.ps1"
if [[ ! -f "$PLAYWRIGHT_SCRIPT" ]]; then
  echo "Playwright installer not found: $PLAYWRIGHT_SCRIPT" >&2
  exit 1
fi
pwsh "$PLAYWRIGHT_SCRIPT" codegen https://www.saucedemo.com
