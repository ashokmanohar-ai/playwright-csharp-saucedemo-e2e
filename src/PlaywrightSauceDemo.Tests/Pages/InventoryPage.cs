using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightSauceDemo.Tests.Pages;

public sealed class InventoryPage
{
    private readonly IPage _page;

    public InventoryPage(IPage page) => _page = page;

    private ILocator Title => _page.Locator(".title");
    private ILocator CartLink => _page.Locator(".shopping_cart_link");
    private ILocator CartBadge => _page.Locator(".shopping_cart_badge");
    private ILocator SortDropdown => _page.Locator("[data-test='product-sort-container']");

    public async Task AssertLoadedAsync()
    {
        await Assertions.Expect(Title).ToHaveTextAsync("Products");
        await Assertions.Expect(_page).ToHaveURLAsync(new System.Text.RegularExpressions.Regex("inventory\\.html"));
    }

    public async Task AddProductAsync(string productSlug)
    {
        await _page.Locator($"[data-test='add-to-cart-{productSlug}']").ClickAsync();
    }

    public async Task RemoveProductAsync(string productSlug)
    {
        await _page.Locator($"[data-test='remove-{productSlug}']").ClickAsync();
    }

    public async Task OpenCartAsync() => await CartLink.ClickAsync();

    public async Task AssertCartCountAsync(int expected)
    {
        if (expected == 0)
        {
            await Assertions.Expect(CartBadge).ToHaveCountAsync(0);
            return;
        }

        await Assertions.Expect(CartBadge).ToHaveTextAsync(expected.ToString());
    }

    public async Task SortByAsync(string value) => await SortDropdown.SelectOptionAsync(value);

    public async Task<IReadOnlyList<string>> GetPricesAsync()
        => await _page.Locator(".inventory_item_price").AllTextContentsAsync();
}
