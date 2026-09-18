namespace PlaywrightSauceDemo.Tests.Core;

public sealed record ApiTestConfig(string BaseUrl)
{
    public static ApiTestConfig Load() => new(
        Environment.GetEnvironmentVariable("API_BASE_URL")
        ?? "https://jsonplaceholder.typicode.com"
    );
}
