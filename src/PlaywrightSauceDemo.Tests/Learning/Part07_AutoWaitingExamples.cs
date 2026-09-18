using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightSauceDemo.Tests.Learning;

[TestFixture]
[Category("Learning")]
public class Part07_AutoWaitingExamples : PageTest
{
    [Test]
    public async Task LoginWithoutThreadSleep()
    {
        await Page.GotoAsync("https://www.saucedemo.com");

        await Page.GetByPlaceholder("Username").FillAsync("standard_user");
        await Page.GetByPlaceholder("Password").FillAsync("secret_sauce");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();

        // No Thread.Sleep is needed. Playwright waits for actionability and
        // the web-first assertion retries until the expected state is ready.
        await Expect(Page.Locator(".title")).ToHaveTextAsync("Products");
    }
}
