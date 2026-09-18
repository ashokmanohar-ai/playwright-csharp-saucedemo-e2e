using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightSauceDemo.Tests.Models;

namespace PlaywrightSauceDemo.Tests.Pages;

public sealed class CheckoutPage
{
    private readonly IPage _page;

    public CheckoutPage(IPage page) => _page = page;

    public async Task FillCustomerInfoAsync(CheckoutInfo info)
    {
        await _page.Locator("[data-test='firstName']").FillAsync(info.FirstName);
        await _page.Locator("[data-test='lastName']").FillAsync(info.LastName);
        await _page.Locator("[data-test='postalCode']").FillAsync(info.PostalCode);
        await _page.Locator("[data-test='continue']").ClickAsync();
    }

    public async Task AssertOverviewLoadedAsync()
    {
        await Assertions.Expect(_page.Locator(".title")).ToHaveTextAsync("Checkout: Overview");
        await Assertions.Expect(_page.Locator("[data-test='finish']")).ToBeVisibleAsync();
    }

    public async Task FinishAsync() => await _page.Locator("[data-test='finish']").ClickAsync();

    public async Task AssertOrderCompleteAsync()
    {
        await Assertions.Expect(_page.Locator(".complete-header")).ToHaveTextAsync("Thank you for your order!");
        await Assertions.Expect(_page).ToHaveURLAsync(new System.Text.RegularExpressions.Regex("checkout-complete\\.html"));
    }
}
