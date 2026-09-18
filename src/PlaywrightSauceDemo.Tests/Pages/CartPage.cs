using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightSauceDemo.Tests.Pages;

public sealed class CartPage
{
    private readonly IPage _page;

    public CartPage(IPage page) => _page = page;

    private ILocator CartItems => _page.Locator(".cart_item");
    private ILocator CheckoutButton => _page.Locator("[data-test='checkout']");

    public async Task AssertLoadedAsync()
    {
        await Assertions.Expect(_page.Locator(".title")).ToHaveTextAsync("Your Cart");
        await Assertions.Expect(_page).ToHaveURLAsync(new System.Text.RegularExpressions.Regex("cart\\.html"));
    }

    public async Task AssertItemCountAsync(int expected)
        => await Assertions.Expect(CartItems).ToHaveCountAsync(expected);

    public async Task AssertContainsProductAsync(string productName)
        => await Assertions.Expect(_page.GetByText(productName, new() { Exact = true })).ToBeVisibleAsync();

    public async Task CheckoutAsync() => await CheckoutButton.ClickAsync();
}
