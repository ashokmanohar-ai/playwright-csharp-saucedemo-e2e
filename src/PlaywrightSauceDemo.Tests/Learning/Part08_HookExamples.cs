using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightSauceDemo.Tests.Learning;

[TestFixture]
[Category("Learning")]
public class Part08_HookExamples : PageTest
{
    [OneTimeSetUp]
    public void BeforeFixture()
    {
        TestContext.Progress.WriteLine("Part 8: fixture setup runs once.");
    }

    [SetUp]
    public async Task BeforeEachTest()
    {
        await Page.GotoAsync("https://www.saucedemo.com");
    }

    [TearDown]
    public void AfterEachTest()
    {
        TestContext.Progress.WriteLine(
            $"Completed: {TestContext.CurrentContext.Test.Name}");
    }

    [Test]
    public async Task HookPreparedPage_IsReadyForTest()
    {
        await Expect(Page).ToHaveTitleAsync("Swag Labs");
    }

    [OneTimeTearDown]
    public void AfterFixture()
    {
        TestContext.Progress.WriteLine("Part 8: fixture teardown runs once.");
    }
}
