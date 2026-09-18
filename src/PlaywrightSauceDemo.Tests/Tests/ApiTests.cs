using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightSauceDemo.Tests.Core;

namespace PlaywrightSauceDemo.Tests.Tests;

[TestFixture]
[Category("API")]
public class ApiTests : PlaywrightTest
{
    private IAPIRequestContext _api = null!;

    [SetUp]
    public async Task CreateApiContext()
    {
        var config = ApiTestConfig.Load();
        _api = await Playwright.APIRequest.NewContextAsync(new()
        {
            BaseURL = config.BaseUrl,
            ExtraHTTPHeaders = new Dictionary<string, string>
            {
                ["Accept"] = "application/json"
            }
        });
    }

    [TearDown]
    public async Task DisposeApiContext()
    {
        await _api.DisposeAsync();
    }

    [Test]
    public async Task GetUser_ReturnsExpectedUser()
    {
        var response = await _api.GetAsync("/users/1");

        Assert.That(response.Ok, Is.True, "GET /users/1 should return a 2xx response.");

        var body = await response.JsonAsync();
        Assert.That(body.HasValue, Is.True);
        Assert.That(body!.Value.GetProperty("id").GetInt32(), Is.EqualTo(1));
        Assert.That(body.Value.GetProperty("email").GetString(), Does.Contain("@"));
    }

    [Test]
    public async Task CreatePost_ReturnsCreatedResource()
    {
        var response = await _api.PostAsync("/posts", new()
        {
            DataObject = new
            {
                title = "Playwright C# API test",
                body = "Created through APIRequestContext",
                userId = 1
            }
        });

        Assert.That(response.Status, Is.EqualTo(201));

        var body = await response.JsonAsync();
        Assert.That(body.HasValue, Is.True);
        Assert.That(body!.Value.GetProperty("title").GetString(),
            Is.EqualTo("Playwright C# API test"));
        Assert.That(body.Value.GetProperty("id").GetInt32(), Is.GreaterThan(0));
    }
}
