using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightSauceDemo.Tests.Learning;

[TestFixture]
[Category("Learning")]
public class Part03_FirstTestExamples : PageTest
{
    [Test]
    public async Task OpenSauceDemo_AndVerifyTitle()
    {
        await Page.GotoAsync("https://www.saucedemo.com");

        await Expect(Page).ToHaveTitleAsync("Swag Labs");
    }
}
