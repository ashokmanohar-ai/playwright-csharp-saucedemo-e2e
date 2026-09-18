using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace PlaywrightSauceDemo.Tests.Learning;

[TestFixture]
[Category("Learning")]
public class Part06_AssertionExamples : PageTest
{
    [Test]
    public async Task DemonstrateWebFirstAssertions()
    {
        await Page.GotoAsync("https://www.saucedemo.com");

        await Expect(Page).ToHaveTitleAsync("Swag Labs");
        await Expect(Page.GetByPlaceholder("Username")).ToBeVisibleAsync();
        await Expect(Page.GetByPlaceholder("Password")).ToBeEditableAsync();
        await Expect(Page.Locator("[data-test='login-button']")).ToBeEnabledAsync();
        await Expect(Page).ToHaveURLAsync(new Regex("saucedemo\\.com"));
    }
}
