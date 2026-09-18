using Microsoft.Playwright.NUnit;
using NUnit.Framework.Interfaces;

namespace PlaywrightSauceDemo.Tests.Core;

public abstract class SauceDemoTestBase : PageTest
{
    protected TestConfig Config { get; private set; } = TestConfig.Load();

    [SetUp]
    public async Task BaseSetUp()
    {
        Config = TestConfig.Load();
        await Page.GotoAsync(Config.BaseUrl);
    }

    [TearDown]
    public async Task CaptureEvidenceOnFailure()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed)
            return;

        var evidenceDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "evidence");
        Directory.CreateDirectory(evidenceDir);

        var safeName = string.Concat(
            TestContext.CurrentContext.Test.Name.Select(c => char.IsLetterOrDigit(c) ? c : '_'));

        var path = Path.Combine(evidenceDir, $"{safeName}.png");

        await Page.ScreenshotAsync(new()
        {
            Path = path,
            FullPage = true
        });

        TestContext.AddTestAttachment(path, "Failure screenshot");
    }
}
