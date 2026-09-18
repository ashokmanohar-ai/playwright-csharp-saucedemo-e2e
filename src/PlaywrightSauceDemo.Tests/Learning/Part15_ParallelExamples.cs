using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightSauceDemo.Tests.Learning;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
[Category("Learning")]
public class Part15_ParallelExamples : PageTest
{
    [TestCase("standard_user")]
    [TestCase("problem_user")]
    public async Task IndependentUsers_RunWithIsolatedContexts(string username)
    {
        await Page.GotoAsync("https://www.saucedemo.com");
        await Page.GetByPlaceholder("Username").FillAsync(username);
        await Page.GetByPlaceholder("Password").FillAsync("secret_sauce");
        await Page.Locator("[data-test='login-button']").ClickAsync();

        await Expect(Page.Locator(".title")).ToHaveTextAsync("Products");
    }
}
