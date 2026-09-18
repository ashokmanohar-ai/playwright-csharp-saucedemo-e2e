param([string]$Filter = "TestCategory=Smoke")
$ErrorActionPreference = "Stop"
dotnet test PlaywrightSauceDemo.sln --settings runsettings/headed.runsettings --filter $Filter
