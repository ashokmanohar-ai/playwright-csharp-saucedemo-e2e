namespace PlaywrightSauceDemo.Tests.Core;

public sealed record TestConfig(string BaseUrl, string Username, string Password)
{
    public static TestConfig Load() => new(
        Environment.GetEnvironmentVariable("SAUCE_BASE_URL") ?? "https://www.saucedemo.com",
        Environment.GetEnvironmentVariable("SAUCE_USERNAME") ?? "standard_user",
        Environment.GetEnvironmentVariable("SAUCE_PASSWORD") ?? "secret_sauce"
    );
}
