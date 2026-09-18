# Playwright C# — 16-Part Learning Guide

This repository maps the complete 16-part learning series to runnable code, framework features, scripts, or CI/CD implementation.

| Part | Topic | Repository implementation |
|---|---|---|
| 1 | Introduction | README architecture, stack and browser overview |
| 2 | Setup & Installation | scripts/setup.ps1, scripts/setup.sh, solution and runsettings |
| 3 | First Test | Learning/Part03_FirstTestExamples.cs |
| 4 | Locators | Learning/Part04_LocatorExamples.cs |
| 5 | Actions | Learning/Part05_ActionExamples.cs |
| 6 | Assertions | Learning/Part06_AssertionExamples.cs |
| 7 | Auto-Waiting | Learning/Part07_AutoWaitingExamples.cs |
| 8 | NUnit Hooks | Learning/Part08_HookExamples.cs and SauceDemoTestBase.cs |
| 9 | Browser Context | Learning/Part09_BrowserContextExamples.cs |
| 10 | Evidence | failure screenshot, trace, console/network logs and optional video |
| 11 | Debugging | scripts/debug-test.ps1 and headed runsettings |
| 12 | Codegen | scripts/codegen-saucedemo.ps1 and codegen-saucedemo.sh |
| 13 | Page Object Model | Pages folder and production E2E tests |
| 14 | API Testing | Tests/ApiTests.cs using Playwright APIRequestContext |
| 15 | Parallel Execution | parallel.runsettings plus parallel fixtures/examples |
| 16 | Reporting & CI/CD | GitHub Actions browser matrix, TRX, evidence artifacts and summaries |

## Run the learning examples

    dotnet test --filter "TestCategory=Learning" --settings runsettings/chromium.runsettings

## Run the production-style SauceDemo UI tests

    dotnet test --filter "TestCategory!=Learning&TestCategory!=API" --settings runsettings/chromium.runsettings

## Run API tests

    dotnet test --filter "TestCategory=API"

## Part 10 — Evidence

For tests derived from SauceDemoTestBase, failures automatically capture:

- full-page screenshot
- Playwright trace ZIP
- browser console log
- request/response network log

Optional video is enabled with PW_VIDEO=1.

PowerShell:

    $env:PW_VIDEO="1"
    dotnet test --filter "TestCategory=E2E"

Bash:

    PW_VIDEO=1 dotnet test --filter "TestCategory=E2E"

Open a saved trace with:

    pwsh src/PlaywrightSauceDemo.Tests/bin/Debug/net8.0/playwright.ps1 show-trace path/to/trace.zip

## Part 11 — Debugging

Headed smoke run:

    ./scripts/run-headed.ps1

Debug the checkout scenario with Playwright Inspector:

    ./scripts/debug-test.ps1 -Filter "Name~StandardUser_CanCompletePurchase"

## Part 12 — Codegen

PowerShell:

    ./scripts/codegen-saucedemo.ps1

macOS/Linux:

    ./scripts/codegen-saucedemo.sh

Codegen output should be treated as starter code. Review selectors, refactor into Page Objects, remove noise and add meaningful assertions before committing.

## Part 14 — API testing

The API learning module deliberately uses JSONPlaceholder rather than inventing unsupported SauceDemo APIs.

Default API target:

    https://jsonplaceholder.typicode.com

Override it with API_BASE_URL:

    API_BASE_URL=https://your-api.example.com dotnet test --filter "TestCategory=API"

The API suite demonstrates GET and POST with Playwright APIRequestContext and JSON response validation.

## Part 15 — Parallel execution

Run Chromium with four NUnit workers:

    dotnet test --settings runsettings/parallel.runsettings

Parallel-safe rules:

- no shared mutable static state
- no execution-order dependency
- unique evidence/output names
- isolated browser contexts
- independent test data

## Part 16 — CI/CD

GitHub Actions runs non-API tests across Chromium, Firefox and WebKit. The API suite runs in a separate job. Browser jobs publish TRX results and any failure evidence as downloadable artifacts.
