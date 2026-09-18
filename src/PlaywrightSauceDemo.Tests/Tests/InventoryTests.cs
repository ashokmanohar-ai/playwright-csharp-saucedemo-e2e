using System.Globalization;
using NUnit.Framework;
using PlaywrightSauceDemo.Tests.Core;
using PlaywrightSauceDemo.Tests.Pages;

namespace PlaywrightSauceDemo.Tests.Tests;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class InventoryTests : SauceDemoTestBase
{
    [SetUp]
    public async Task Login()
    {
        var login = new LoginPage(Page);
        await login.LoginAsync(Config.Username, Config.Password);
        await login.AssertLoginSucceededAsync();
    }

    [Test]
    [Category("Regression")]
    public async Task User_CanAddAndRemoveProductFromInventory()
    {
        var inventory = new InventoryPage(Page);

        await inventory.AddProductAsync("sauce-labs-backpack");
        await inventory.AssertCartCountAsync(1);

        await inventory.RemoveProductAsync("sauce-labs-backpack");
        await inventory.AssertCartCountAsync(0);
    }

    [Test]
    [Category("Regression")]
    public async Task User_CanSortProductsByPriceLowToHigh()
    {
        var inventory = new InventoryPage(Page);

        await inventory.SortByAsync("lohi");
        var priceTexts = await inventory.GetPricesAsync();
        var prices = priceTexts
            .Select(x => decimal.Parse(x.Replace("$", string.Empty), CultureInfo.InvariantCulture))
            .ToArray();

        Assert.That(prices, Is.Ordered.Ascending);
    }
}
