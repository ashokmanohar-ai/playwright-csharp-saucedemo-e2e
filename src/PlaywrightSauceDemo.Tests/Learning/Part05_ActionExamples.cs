using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightSauceDemo.Tests.Learning;

[TestFixture]
[Category("Learning")]
public class Part05_ActionExamples : PageTest
{
    [Test]
    public async Task DemonstrateFillClickSelectAndHover()
    {
        await Page.GotoAsync("https://www.saucedemo.com");

        await Page.GetByPlaceholder("Username").FillAsync("standard_user");
        await Page.GetByPlaceholder("Password").FillAsync("secret_sauce");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();

        var sort = Page.Locator("[data-test='product-sort-container']");
        await sort.SelectOptionAsync("lohi");

        var backpack = Page.GetByText("Sauce Labs Backpack", new() { Exact = true });
        await backpack.HoverAsync();

        await Expect(Page.Locator(".title")).ToHaveTextAsync("Products");
    }
}
