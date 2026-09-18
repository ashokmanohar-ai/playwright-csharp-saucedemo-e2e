using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightSauceDemo.Tests.Learning;

[TestFixture]
[Category("Learning")]
public class Part09_BrowserContextExamples : BrowserTest
{
    [Test]
    public async Task AuthenticationState_CanBeReusedInAnotherContext()
    {
        await using var firstContext = await Browser.NewContextAsync();
        var loginPage = await firstContext.NewPageAsync();

        await loginPage.GotoAsync("https://www.saucedemo.com");
        await loginPage.GetByPlaceholder("Username").FillAsync("standard_user");
        await loginPage.GetByPlaceholder("Password").FillAsync("secret_sauce");
        await loginPage.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();

        await Assertions.Expect(loginPage.Locator(".title")).ToHaveTextAsync("Products");

        var storageState = await firstContext.StorageStateAsync();

        await using var secondContext = await Browser.NewContextAsync(new()
        {
            StorageState = storageState
        });

        var reusedSessionPage = await secondContext.NewPageAsync();
        await reusedSessionPage.GotoAsync("https://www.saucedemo.com/inventory.html");

        await Assertions.Expect(reusedSessionPage.Locator(".title"))
            .ToHaveTextAsync("Products");
    }
}
