using NUnit.Framework;
using PlaywrightSauceDemo.Tests.Core;
using PlaywrightSauceDemo.Tests.Models;
using PlaywrightSauceDemo.Tests.Pages;

namespace PlaywrightSauceDemo.Tests.Tests;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CheckoutTests : SauceDemoTestBase
{
    [Test]
    [Category("Smoke")]
    [Category("E2E")]
    public async Task StandardUser_CanCompletePurchase()
    {
        var login = new LoginPage(Page);
        var inventory = new InventoryPage(Page);
        var cart = new CartPage(Page);
        var checkout = new CheckoutPage(Page);

        await login.LoginAsync(Config.Username, Config.Password);
        await login.AssertLoginSucceededAsync();

        await inventory.AddProductAsync("sauce-labs-backpack");
        await inventory.AddProductAsync("sauce-labs-bike-light");
        await inventory.AssertCartCountAsync(2);
        await inventory.OpenCartAsync();

        await cart.AssertLoadedAsync();
        await cart.AssertItemCountAsync(2);
        await cart.CheckoutAsync();

        await checkout.FillCustomerInfoAsync(CheckoutInfo.Default);
        await checkout.AssertOverviewLoadedAsync();
        await checkout.FinishAsync();
        await checkout.AssertOrderCompleteAsync();
    }
}
