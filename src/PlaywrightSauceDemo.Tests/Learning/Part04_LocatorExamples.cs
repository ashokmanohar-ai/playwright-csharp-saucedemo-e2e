using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightSauceDemo.Tests.Learning;

[TestFixture]
[Category("Learning")]
public class Part04_LocatorExamples : PageTest
{
    [Test]
    public async Task DemonstrateRecommendedLocatorStyles()
    {
        await Page.GotoAsync("https://www.saucedemo.com");

        var byPlaceholder = Page.GetByPlaceholder("Username");
        var byStableAttribute = Page.Locator("[data-test='password']");
        var byRole = Page.GetByRole(AriaRole.Button, new() { Name = "Login" });

        await Expect(byPlaceholder).ToBeVisibleAsync();
        await Expect(byStableAttribute).ToBeVisibleAsync();
        await Expect(byRole).ToBeEnabledAsync();
    }
}
