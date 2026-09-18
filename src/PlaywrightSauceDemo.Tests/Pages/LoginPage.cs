using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightSauceDemo.Tests.Pages;

public sealed class LoginPage
{
    private readonly IPage _page;

    public LoginPage(IPage page) => _page = page;

    private ILocator Username => _page.Locator("[data-test='username']");
    private ILocator Password => _page.Locator("[data-test='password']");
    private ILocator LoginButton => _page.Locator("[data-test='login-button']");
    private ILocator ErrorMessage => _page.Locator("[data-test='error']");

    public async Task LoginAsync(string username, string password)
    {
        await Username.FillAsync(username);
        await Password.FillAsync(password);
        await LoginButton.ClickAsync();
    }

    public async Task AssertLoginSucceededAsync()
    {
        await Assertions.Expect(_page).ToHaveURLAsync(new System.Text.RegularExpressions.Regex("inventory\\.html"));
        await Assertions.Expect(_page.GetByText("Products", new() { Exact = true })).ToBeVisibleAsync();
    }

    public async Task AssertErrorAsync(string messageFragment)
    {
        await Assertions.Expect(ErrorMessage).ToBeVisibleAsync();
        await Assertions.Expect(ErrorMessage).ToContainTextAsync(messageFragment);
    }
}
