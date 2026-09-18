using System.Collections.Concurrent;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace PlaywrightSauceDemo.Tests.Core;

public abstract class SauceDemoTestBase : PageTest
{
    private readonly ConcurrentQueue<string> _consoleLog = new();
    private readonly ConcurrentQueue<string> _networkLog = new();
    private bool _traceStarted;

    protected TestConfig Config { get; private set; } = TestConfig.Load();

    public override BrowserNewContextOptions ContextOptions()
    {
        var options = base.ContextOptions();

        if (IsEnabled("PW_VIDEO"))
        {
            var videoDirectory = Path.Combine(
                TestContext.CurrentContext.WorkDirectory,
                "evidence",
                "videos");

            Directory.CreateDirectory(videoDirectory);
            options.RecordVideoDir = videoDirectory;
            options.RecordVideoSize = new RecordVideoSize
            {
                Width = 1280,
                Height = 720
            };
        }

        return options;
    }

    [SetUp]
    public async Task BaseSetUp()
    {
        Config = TestConfig.Load();

        Page.Console += (_, message) =>
            _consoleLog.Enqueue($"[{DateTimeOffset.UtcNow:O}] {message.Type}: {message.Text}");

        Page.PageError += (_, error) =>
            _consoleLog.Enqueue($"[{DateTimeOffset.UtcNow:O}] PAGE ERROR: {error}");

        Page.Request += (_, request) =>
            _networkLog.Enqueue(
                $"[{DateTimeOffset.UtcNow:O}] REQUEST {request.Method} {request.Url}");

        Page.Response += (_, response) =>
            _networkLog.Enqueue(
                $"[{DateTimeOffset.UtcNow:O}] RESPONSE {response.Status} {response.Url}");

        await Context.Tracing.StartAsync(new()
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
        _traceStarted = true;

        await Page.GotoAsync(Config.BaseUrl);
    }

    [TearDown]
    public async Task CaptureEvidence()
    {
        var failed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed;
        var evidenceDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "evidence");

        Directory.CreateDirectory(evidenceDir);

        var safeName = string.Concat(
            TestContext.CurrentContext.Test.Name.Select(
                c => char.IsLetterOrDigit(c) ? c : '_'));

        var prefix = $"{safeName}_{DateTimeOffset.UtcNow:yyyyMMdd_HHmmssfff}";

        try
        {
            if (failed && !Page.IsClosed)
            {
                var screenshotPath = Path.Combine(evidenceDir, $"{prefix}.png");
                await Page.ScreenshotAsync(new()
                {
                    Path = screenshotPath,
                    FullPage = true
                });
                TestContext.AddTestAttachment(screenshotPath, "Failure screenshot");
            }

            if (failed)
            {
                var consolePath = Path.Combine(evidenceDir, $"{prefix}_console.log");
                await File.WriteAllLinesAsync(consolePath, _consoleLog);
                TestContext.AddTestAttachment(consolePath, "Browser console log");

                var networkPath = Path.Combine(evidenceDir, $"{prefix}_network.log");
                await File.WriteAllLinesAsync(networkPath, _networkLog);
                TestContext.AddTestAttachment(networkPath, "Network request/response log");
            }
        }
        finally
        {
            if (_traceStarted)
            {
                if (failed)
                {
                    var tracePath = Path.Combine(evidenceDir, $"{prefix}_trace.zip");
                    await Context.Tracing.StopAsync(new() { Path = tracePath });
                    TestContext.AddTestAttachment(tracePath, "Playwright trace");
                }
                else
                {
                    await Context.Tracing.StopAsync();
                }

                _traceStarted = false;
            }
        }
    }

    private static bool IsEnabled(string variableName)
    {
        var value = Environment.GetEnvironmentVariable(variableName);
        return string.Equals(value, "1", StringComparison.OrdinalIgnoreCase)
            || string.Equals(value, "true", StringComparison.OrdinalIgnoreCase)
            || string.Equals(value, "yes", StringComparison.OrdinalIgnoreCase);
    }
}
