param([string]$Filter = "Name~StandardUser_CanCompletePurchase")
$ErrorActionPreference = "Stop"
$env:PWDEBUG = "1"
dotnet test PlaywrightSauceDemo.sln --settings runsettings/headed.runsettings --filter $Filter
