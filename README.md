# Playwright C# SauceDemo E2E Automation Demo

A practical end-to-end automation repository built with **Playwright for .NET, C#, NUnit, and .NET 8** against the public **SauceDemo** training application.

The project is intentionally simple enough for Playwright C# beginners, but structured like a maintainable automation framework: Page Objects, reusable configuration, web-first assertions, isolated browser contexts, cross-browser execution, failure evidence, categories, parallel-ready tests, and GitHub Actions.

> Target: https://www.saucedemo.com  
> Public demo user: standard_user  
> Public demo password: secret_sauce

These credentials belong to SauceDemo's public training site only. Never commit real passwords, API keys, PATs, customer secrets, or private certificates.

---

## 1. What the repository demonstrates

The main E2E test automates this complete business flow:

    Open SauceDemo
        ↓
    Login
        ↓
    Verify Products page
        ↓
    Add two products
        ↓
    Validate cart count
        ↓
    Open cart
        ↓
    Verify products
        ↓
    Start checkout
        ↓
    Enter customer details
        ↓
    Validate checkout overview
        ↓
    Finish purchase
        ↓
    Verify "Thank you for your order!"

Additional scenarios cover:

- Valid login
- Invalid password
- Add/remove inventory item
- Sort products low-to-high by price
- Multi-product cart validation
- Complete checkout
- Chromium, Firefox and WebKit
- Failure screenshot capture
- NUnit categories
- Parallel-ready fixtures
- CI/CD execution on GitHub Actions

---

## 2. Technology stack

| Layer | Technology | Purpose |
|---|---|---|
| Language | C# | Test code |
| Runtime | .NET 8 | Execution platform |
| Browser automation | Microsoft.Playwright.NUnit | Playwright + NUnit integration |
| Test framework | NUnit | Tests, setup/teardown, categories |
| Test adapter | NUnit3TestAdapter | dotnet test discovery |
| Architecture | Page Object Model | Separates test intent from selectors |
| CI/CD | GitHub Actions | Cross-browser automated execution |
| Evidence | Playwright screenshots | Failure diagnosis |

The project currently references:

- Microsoft.Playwright.NUnit 1.62.0
- NUnit 4.6.1
- NUnit3TestAdapter 6.3.0
- NUnit.Analyzers 4.15.0
- Microsoft.NET.Test.Sdk 18.10.1

---

## 3. Repository structure

    playwright-csharp-saucedemo-e2e/
    │
    ├── .github/
    │   └── workflows/
    │       └── playwright.yml
    │
    ├── runsettings/
    │   ├── chromium.runsettings
    │   ├── firefox.runsettings
    │   └── webkit.runsettings
    │
    ├── src/
    │   └── PlaywrightSauceDemo.Tests/
    │       ├── Core/
    │       │   ├── SauceDemoTestBase.cs
    │       │   └── TestConfig.cs
    │       ├── Models/
    │       │   └── CheckoutInfo.cs
    │       ├── Pages/
    │       │   ├── LoginPage.cs
    │       │   ├── InventoryPage.cs
    │       │   ├── CartPage.cs
    │       │   └── CheckoutPage.cs
    │       ├── Tests/
    │       │   ├── LoginTests.cs
    │       │   ├── InventoryTests.cs
    │       │   ├── CartTests.cs
    │       │   └── CheckoutTests.cs
    │       └── PlaywrightSauceDemo.Tests.csproj
    │
    ├── .env.example
    ├── .gitignore
    ├── global.json
    ├── LICENSE
    ├── PlaywrightSauceDemo.sln
    └── README.md

### Layer responsibilities

**Tests** describe user/business scenarios and assertions.

**Pages** contain selectors and reusable interactions.

**Core** contains shared configuration and the common test lifecycle.

**Models** contain structured test data.

**runsettings** chooses the Playwright browser engine and shared execution options.

**GitHub Actions** runs the suite automatically.

---

## 4. Prerequisites

Install:

- .NET 8 SDK
- Git
- PowerShell 7 (pwsh)
- Visual Studio 2022 or VS Code
- Internet access for NuGet and Playwright browser downloads

Verify:

    dotnet --version
    git --version
    pwsh --version

---

## 5. Clone and open

    git clone https://github.com/ashokmanohar-ai/playwright-csharp-saucedemo-e2e.git
    cd playwright-csharp-saucedemo-e2e

Open in VS Code:

    code .

Or open PlaywrightSauceDemo.sln in Visual Studio.

---

## 6. Restore and build

Restore dependencies:

    dotnet restore PlaywrightSauceDemo.sln

Build:

    dotnet build PlaywrightSauceDemo.sln

The build creates the Playwright PowerShell script under the project's output directory.

---

## 7. Install browsers

Install all Playwright browsers:

    pwsh src/PlaywrightSauceDemo.Tests/bin/Debug/net8.0/playwright.ps1 install

Install browsers plus Linux dependencies:

    pwsh src/PlaywrightSauceDemo.Tests/bin/Debug/net8.0/playwright.ps1 install --with-deps

Install Chromium only:

    pwsh src/PlaywrightSauceDemo.Tests/bin/Debug/net8.0/playwright.ps1 install chromium

---

## 8. Run all tests by browser

Chromium:

    dotnet test PlaywrightSauceDemo.sln --settings runsettings/chromium.runsettings

Firefox:

    dotnet test PlaywrightSauceDemo.sln --settings runsettings/firefox.runsettings

WebKit:

    dotnet test PlaywrightSauceDemo.sln --settings runsettings/webkit.runsettings

---

## 9. Run selected tests

Smoke:

    dotnet test --filter "TestCategory=Smoke"

Regression:

    dotnet test --filter "TestCategory=Regression"

E2E:

    dotnet test --filter "TestCategory=E2E"

Negative:

    dotnet test --filter "TestCategory=Negative"

One method:

    dotnet test --filter "Name~StandardUser_CanCompletePurchase"

One fixture:

    dotnet test --filter "LoginTests"

---

## 10. Configuration

Core/TestConfig.cs reads:

| Environment variable | Default |
|---|---|
| SAUCE_BASE_URL | https://www.saucedemo.com |
| SAUCE_USERNAME | standard_user |
| SAUCE_PASSWORD | secret_sauce |

PowerShell:

    $env:SAUCE_BASE_URL="https://www.saucedemo.com"
    $env:SAUCE_USERNAME="standard_user"
    $env:SAUCE_PASSWORD="secret_sauce"
    dotnet test

Bash:

    export SAUCE_BASE_URL="https://www.saucedemo.com"
    export SAUCE_USERNAME="standard_user"
    export SAUCE_PASSWORD="secret_sauce"
    dotnet test

For a real system, remove credential defaults and supply them from a secret manager or CI secret store.

---

## 11. Included scenarios

### LoginTests

**StandardUser_CanLogin** — Smoke

Confirms valid credentials navigate to the Products page.

**InvalidPassword_ShowsError** — Negative

Confirms an invalid password is rejected and a useful error is shown.

### InventoryTests

**User_CanAddAndRemoveProductFromInventory** — Regression

Adds the Sauce Labs Backpack, verifies cart count 1, removes it, and confirms the cart badge disappears.

**User_CanSortProductsByPriceLowToHigh** — Regression

Selects SauceDemo's lohi sort option, reads displayed prices, converts them to decimals, and asserts ascending order.

### CartTests

**AddedProducts_AppearInCart** — Regression

Adds Sauce Labs Backpack and Sauce Labs Bike Light, opens the cart, asserts two cart items, and verifies both names.

### CheckoutTests

**StandardUser_CanCompletePurchase** — Smoke + E2E

Validates the complete purchase journey through the order confirmation page.

---

## 12. E2E flow in code

Login:

    await login.LoginAsync(Config.Username, Config.Password);
    await login.AssertLoginSucceededAsync();

Add products:

    await inventory.AddProductAsync("sauce-labs-backpack");
    await inventory.AddProductAsync("sauce-labs-bike-light");
    await inventory.AssertCartCountAsync(2);

Open cart:

    await inventory.OpenCartAsync();
    await cart.AssertLoadedAsync();
    await cart.AssertItemCountAsync(2);

Checkout:

    await cart.CheckoutAsync();
    await checkout.FillCustomerInfoAsync(CheckoutInfo.Default);
    await checkout.AssertOverviewLoadedAsync();
    await checkout.FinishAsync();
    await checkout.AssertOrderCompleteAsync();

The final validation checks:

    Thank you for your order!

and verifies checkout-complete.html.

---

## 13. Page Object Model

The tests intentionally avoid duplicating selectors.

Instead of putting this everywhere:

    await Page.Locator("[data-test='username']").FillAsync("standard_user");
    await Page.Locator("[data-test='password']").FillAsync("secret_sauce");
    await Page.Locator("[data-test='login-button']").ClickAsync();

tests call:

    await login.LoginAsync(Config.Username, Config.Password);

This improves:

- Readability
- Reuse
- Maintainability
- Locator ownership
- Separation of concerns

Page objects should model meaningful interactions. Avoid giant page classes and avoid wrapping every tiny click without a clear purpose.

---

## 14. Locator strategy

SauceDemo exposes stable data-test attributes. The project uses selectors such as:

    [data-test='username']
    [data-test='password']
    [data-test='login-button']
    [data-test='product-sort-container']
    [data-test='checkout']
    [data-test='finish']

Recommended priority when adding future locators:

1. Role
2. Label
3. Stable test attribute / test ID
4. Visible text
5. Stable CSS
6. XPath only when necessary

Avoid fragile selectors based on layout position, auto-generated IDs, or deeply nested DOM structure.

---

## 15. Assertions and auto-waiting

Playwright's web-first assertions retry until the expected condition becomes true or the timeout expires.

Examples:

    await Assertions.Expect(Title).ToHaveTextAsync("Products");

    await Assertions.Expect(CartItems).ToHaveCountAsync(2);

    await Assertions.Expect(_page)
        .ToHaveURLAsync(new Regex("inventory\\.html"));

The framework deliberately does not use Thread.Sleep.

Use Playwright actions, locators and assertions for synchronization. Add explicit condition-based waits only when genuinely necessary.

---

## 16. Test isolation

SauceDemoTestBase inherits from PageTest.

Playwright's NUnit integration manages an isolated browser context/page for each test.

This isolates:

- Cookies
- Local storage
- Session storage
- Authentication state
- Page state

Isolation helps tests run independently and supports parallel execution.

---

## 17. Setup and teardown

SauceDemoTestBase contains a SetUp that:

- Reloads environment configuration
- Navigates to the configured base URL

Its TearDown:

- Checks NUnit result status
- Captures a full-page screenshot only for failures
- Writes the image to evidence/
- Attaches the screenshot to the NUnit result

This provides useful debugging evidence while avoiding unnecessary screenshots for every passing test.

---

## 18. Parallel execution

Fixtures use:

    [Parallelizable(ParallelScope.Self)]

Example with four NUnit workers:

    dotnet test -- NUnit.NumberOfTestWorkers=4

Parallel tests must stay independent.

Avoid:

- Shared mutable static data
- Shared customer/order records
- Execution-order assumptions
- Identical output filenames
- Reusing accounts when workflows mutate server-side state

---

## 19. GitHub Actions

Workflow:

    .github/workflows/playwright.yml

Triggers:

- Push to main
- Pull request to main
- Manual workflow dispatch

Browser matrix:

    chromium
    firefox
    webkit

Each browser job performs:

    Checkout
        ↓
    Setup .NET 8
        ↓
    Restore
        ↓
    Build Release
        ↓
    Install selected Playwright browser
        ↓
    Execute tests
        ↓
    Upload TRX + failure evidence

fail-fast is disabled so a failure in one browser does not cancel the other browser jobs.

---

## 20. Secrets in real projects

The SauceDemo username/password are public training credentials. Real projects should use GitHub Actions secrets.

Suggested secret names:

    APP_BASE_URL
    APP_USERNAME
    APP_PASSWORD

Path:

    Repository
      → Settings
      → Secrets and variables
      → Actions

Never commit real:

- Passwords
- API keys
- GitHub PATs
- OAuth secrets
- Database credentials
- Private keys

---

## 21. Debugging workflow

When a test fails:

1. Read the NUnit failure.
2. Inspect the captured screenshot.
3. Run only the failing test.
4. Validate the selector.
5. Inspect the current page state.
6. Use headed mode/Inspector when required.
7. Check console/network behavior when relevant.
8. Fix the real issue instead of adding a fixed delay.

Run one test:

    dotnet test --filter "Name~StandardUser_CanCompletePurchase"

Temporary debugging:

    await Page.PauseAsync();

Remove debug pauses before committing.

---

## 22. Add another scenario

Example: add and then remove Sauce Labs Fleece Jacket.

Recommended approach:

1. Reuse LoginPage.
2. Reuse or extend InventoryPage.
3. Reuse or extend CartPage.
4. Add a test under Tests.
5. Use meaningful assertions.
6. Run the new test alone.
7. Run the relevant suite.
8. Run all browser engines.

Keep selectors inside page objects rather than copying them into test methods.

---

## 23. VS Code + GitHub Copilot Agent Mode prompts

Understand the repository:

    Analyze this Playwright C# NUnit repository.
    Explain its architecture, Page Object Model, test lifecycle,
    configuration, locators, assertions, browser isolation,
    parallel execution and GitHub Actions.
    Do not modify files.

Run Smoke:

    Restore and build the solution.
    Ensure Chromium is installed.
    Run only the Smoke category.
    Explain any failures before changing code.

Add a scenario:

    Add an E2E test that logs in, adds Sauce Labs Fleece Jacket,
    validates the cart, removes the product, and confirms the cart is empty.
    Follow the current Page Object Model.
    Do not use Thread.Sleep.
    Run the relevant tests after implementation.

Debug:

    Run StandardUser_CanCompletePurchase.
    If it fails, inspect the failure message and existing selectors.
    Identify the root cause and apply the smallest safe fix.
    Do not hide the issue with arbitrary waits.

---

## 24. Enterprise evolution ideas

Framework:

- Dependency injection
- Environment profiles
- Strongly typed settings
- Component objects
- Data builders
- JSON/CSV data
- APIRequestContext
- Authentication-state reuse

Quality engineering:

- Accessibility checks
- Visual regression
- API contract validation
- Negative checkout scenarios
- Boundary tests
- Console-error validation

Evidence/reporting:

- Trace capture
- Video on failure
- Browser console/network logs
- Allure
- HTML dashboards
- Trend analytics

CI/CD:

- Separate Smoke and Regression jobs
- Nightly regression
- PR quality gates
- Windows/Linux runners
- Azure DevOps
- Jenkins

---

## 25. Troubleshooting

**playwright.ps1 missing**

Build first:

    dotnet build PlaywrightSauceDemo.sln

**Browser executable missing**

Install browsers:

    pwsh src/PlaywrightSauceDemo.Tests/bin/Debug/net8.0/playwright.ps1 install

**pwsh missing**

Install PowerShell 7 and put pwsh on PATH.

**SauceDemo unreachable**

Check DNS, internet, proxy, VPN, firewall, and corporate filtering.

**Works locally, fails in CI**

Check:

- Browser dependencies
- Environment variables
- Parallel data collisions
- Target site restrictions
- TRX output
- Failure screenshots

---

## 26. Command cheat sheet

Restore:

    dotnet restore PlaywrightSauceDemo.sln

Build:

    dotnet build PlaywrightSauceDemo.sln

Install all browsers:

    pwsh src/PlaywrightSauceDemo.Tests/bin/Debug/net8.0/playwright.ps1 install

Chromium:

    dotnet test --settings runsettings/chromium.runsettings

Firefox:

    dotnet test --settings runsettings/firefox.runsettings

WebKit:

    dotnet test --settings runsettings/webkit.runsettings

Smoke:

    dotnet test --filter "TestCategory=Smoke"

Regression:

    dotnet test --filter "TestCategory=Regression"

E2E:

    dotnet test --filter "TestCategory=E2E"

Parallel workers:

    dotnet test -- NUnit.NumberOfTestWorkers=4

---

## 27. Learning path

A practical order:

1. Run LoginTests.
2. Understand PageTest.
3. Read LoginPage.
4. Learn locator strategy.
5. Run InventoryTests.
6. Learn actions and assertions.
7. Run CartTests.
8. Run CheckoutTests.
9. Understand browser context isolation.
10. Trigger a failure and inspect evidence.
11. Run Firefox and WebKit.
12. Study GitHub Actions.
13. Add a new scenario.
14. Refactor repeated behavior.

This maps to the broader Playwright C# learning series:

    Introduction
      → Setup
      → First test
      → Locators
      → Actions
      → Assertions
      → Auto-waiting
      → Hooks
      → Browser contexts
      → Evidence
      → Debugging
      → Codegen
      → Page Object Model
      → API testing
      → Parallel execution
      → Reporting & CI/CD

---

## 28. Expected outcome

A successful E2E run finishes with:

    Thank you for your order!

The scenario validates:

    Authentication
      + Inventory
      + Product selection
      + Cart
      + Checkout data
      + Order overview
      + Purchase completion

---

## 29. References

- Playwright for .NET: https://playwright.dev/dotnet/
- Installation: https://playwright.dev/dotnet/docs/intro
- Locators: https://playwright.dev/dotnet/docs/locators
- Assertions: https://playwright.dev/dotnet/docs/test-assertions
- Running tests: https://playwright.dev/dotnet/docs/running-tests
- SauceDemo: https://www.saucedemo.com

---

## 30. Disclaimer

SauceDemo is a third-party demonstration application used for test-automation practice. This repository is an educational/demo project and is not affiliated with or endorsed by Sauce Labs.

## License

MIT — see LICENSE.

---

## 31. 16-Part Implementation Map

The repository now maps the complete Playwright C# basics series to executable examples, framework behavior, tooling, or CI/CD.

| Part | Topic | Implementation |
|---|---|---|
| 1 | Introduction | This README + architecture/stack overview |
| 2 | Setup & Installation | scripts/setup.ps1 and scripts/setup.sh |
| 3 | First Test | Learning/Part03_FirstTestExamples.cs |
| 4 | Locators | Learning/Part04_LocatorExamples.cs |
| 5 | Actions | Learning/Part05_ActionExamples.cs |
| 6 | Assertions | Learning/Part06_AssertionExamples.cs |
| 7 | Auto-Waiting | Learning/Part07_AutoWaitingExamples.cs |
| 8 | Test Hooks | Learning/Part08_HookExamples.cs + SauceDemoTestBase.cs |
| 9 | Browser Context | Learning/Part09_BrowserContextExamples.cs |
| 10 | Evidence | Screenshot + trace + console/network logs + optional video |
| 11 | Debugging | scripts/debug-test.ps1 + headed runsettings |
| 12 | Codegen | scripts/codegen-saucedemo.ps1 + codegen-saucedemo.sh |
| 13 | Page Object Model | Pages/ + production tests |
| 14 | API Testing | Tests/ApiTests.cs using Playwright APIRequestContext |
| 15 | Parallel Execution | parallel.runsettings + Part15_ParallelExamples.cs |
| 16 | Reporting & CI/CD | GitHub Actions UI/browser matrix + API job + TRX/artifacts |

Detailed learning guide:

    docs/PLAYWRIGHT_CSHARP_16_PART_GUIDE.md

## 32. Learning-mode commands

Run all dedicated learning examples in Chromium:

    dotnet test --filter "TestCategory=Learning" --settings runsettings/chromium.runsettings

Run only the production-style SauceDemo UI suite:

    dotnet test --filter "TestCategory!=Learning&TestCategory!=API" --settings runsettings/chromium.runsettings

Run APIRequestContext tests:

    dotnet test --filter "TestCategory=API"

Run with four NUnit workers:

    dotnet test --settings runsettings/parallel.runsettings

Run a headed smoke test:

    ./scripts/run-headed.ps1

Launch Playwright Codegen:

    ./scripts/codegen-saucedemo.ps1

## 33. Advanced failure evidence

SauceDemoTestBase now starts Playwright tracing for every production-style UI test. Successful tests discard the trace; failed tests retain a diagnostic bundle.

On failure, the framework captures:

    evidence/
      <test>_<timestamp>.png
      <test>_<timestamp>_trace.zip
      <test>_<timestamp>_console.log
      <test>_<timestamp>_network.log

This gives four complementary views:

- screenshot: visual state at failure
- trace: timeline, DOM snapshots, source and actions
- console log: browser console and page errors
- network log: request/response sequence and status codes

Optional browser video is available without changing test code:

PowerShell:

    $env:PW_VIDEO="1"
    dotnet test --filter "TestCategory=E2E"

Bash:

    PW_VIDEO=1 dotnet test --filter "TestCategory=E2E"

Open a trace locally:

    pwsh src/PlaywrightSauceDemo.Tests/bin/Debug/net8.0/playwright.ps1 show-trace <trace.zip>

## 34. Real Playwright API testing

The API module uses Playwright APIRequestContext directly rather than introducing a separate HTTP testing library.

It includes:

- GET /users/1
- HTTP success validation
- JSON field validation
- POST /posts
- request payload creation
- status 201 validation
- response-content validation

The default public training API is:

    https://jsonplaceholder.typicode.com

It is deliberately separate from SauceDemo because SauceDemo does not expose a supported public API intended for this learning exercise.

Override the target with:

    API_BASE_URL=https://your-api.example.com dotnet test --filter "TestCategory=API"

## 35. Browser-context and authentication-state learning

Part 9 demonstrates a key Playwright capability beyond ordinary Selenium-style browser automation:

    Browser
       |
       +-- Context A -> login as a user
       |       |
       |       +-- capture storage state
       |
       +-- Context B -> reuse storage state
               |
               +-- open authenticated inventory page

The example keeps sessions isolated while showing how authentication state can be reused in a fresh context.

## 36. Updated CI/CD architecture

The GitHub Actions pipeline now has two independent quality paths.

UI + learning:

    Chromium
    Firefox
    WebKit
       |
       +-- restore
       +-- build
       +-- install selected browser
       +-- run every non-API test
       +-- publish TRX
       +-- publish screenshots/traces/logs if present
       +-- add GitHub job summary

API:

    APIRequestContext
       |
       +-- restore
       +-- build
       +-- run API category
       +-- publish API TRX
       +-- add GitHub job summary

Workflow concurrency cancels superseded runs on the same branch, reducing wasted CI time while the repository evolves.

## 37. Recommended portfolio walkthrough

For a technical demo, present the repository in this order:

1. README and architecture.
2. Simple Part 3 test.
3. Locator/action/assertion examples.
4. SauceDemo Page Object Model.
5. Complete checkout E2E flow.
6. Browser-context/auth-state example.
7. Failure screenshot + trace + logs.
8. APIRequestContext tests.
9. Parallel runsettings.
10. Cross-browser GitHub Actions pipeline.

This progression shows both learning depth and framework engineering rather than only a collection of UI scripts.
