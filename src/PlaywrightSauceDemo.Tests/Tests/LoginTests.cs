using NUnit.Framework;
using PlaywrightSauceDemo.Tests.Core;
using PlaywrightSauceDemo.Tests.Pages;

namespace PlaywrightSauceDemo.Tests.Tests;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class LoginTests : SauceDemoTestBase
{
    [Test]
    [Category("Smoke")]
    public async Task StandardUser_CanLogin()
    {
        var login = new LoginPage(Page);

        await login.LoginAsync(Config.Username, Config.Password);
        await login.AssertLoginSucceededAsync();
    }

    [Test]
    [Category("Negative")]
    public async Task InvalidPassword_ShowsError()
    {
        var login = new LoginPage(Page);

        await login.LoginAsync(Config.Username, "wrong-password");
        await login.AssertErrorAsync("Username and password do not match");
    }
}
