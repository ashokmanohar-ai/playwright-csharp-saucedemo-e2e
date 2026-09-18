using NUnit.Framework;
using PlaywrightSauceDemo.Tests.Core;
using PlaywrightSauceDemo.Tests.Pages;

namespace PlaywrightSauceDemo.Tests.Tests;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CartTests : SauceDemoTestBase
{
    [Test]
    [Category("Regression")]
    public async Task AddedProducts_AppearInCart()
    {
        var login = new LoginPage(Page);
        var inventory = new InventoryPage(Page);
        var cart = new CartPage(Page);

        await login.LoginAsync(Config.Username, Config.Password);
        await inventory.AddProductAsync("sauce-labs-backpack");
        await inventory.AddProductAsync("sauce-labs-bike-light");
        await inventory.OpenCartAsync();

        await cart.AssertLoadedAsync();
        await cart.AssertItemCountAsync(2);
        await cart.AssertContainsProductAsync("Sauce Labs Backpack");
        await cart.AssertContainsProductAsync("Sauce Labs Bike Light");
    }
}
