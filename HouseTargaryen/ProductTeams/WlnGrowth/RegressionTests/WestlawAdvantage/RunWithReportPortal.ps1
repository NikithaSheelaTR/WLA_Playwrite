# Report Portal Test Runner for WestlawAdvantage
# Works on both Local and EC2 environments
# Usage: .\RunWithReportPortal.ps1 [-TestName "TestMethodName"] [-TestCategory "CategoryName"]
# Examples:
#   .\RunWithReportPortal.ps1 -TestName "AjsIncludeRelatedFedCopyLinkTest"
#   .\RunWithReportPortal.ps1 -TestCategory "WestlawAdvantage"
#   .\RunWithReportPortal.ps1  # Runs all tests

param(
    [string]$TestName = "",
    [string]$TestCategory = "",
    [string]$TestDll = "WestlawAdvantage.dll"
)

$ErrorActionPreference = "Stop"

# Get script directory
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$BinDir = Join-Path $ScriptDir "bin\Debug"

# Find vstest.console.exe
$VsTestPaths = @(
    "C:\Program Files\Microsoft Visual Studio\2022\Enterprise\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe",
    "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe",
    "C:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe",
    "C:\Program Files\Microsoft Visual Studio\18\Enterprise\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe",
    "C:\Program Files\Microsoft Visual Studio\18\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe",
    "C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"
)

$VsTestExe = $null
foreach ($path in $VsTestPaths) {
    if (Test-Path $path) {
        $VsTestExe = $path
        break
    }
}

# Check PATH
if (-not $VsTestExe) {
    $VsTestExe = Get-Command vstest.console.exe -ErrorAction SilentlyContinue | Select-Object -ExpandProperty Source
}

if (-not $VsTestExe) {
    Write-Error "Could not find vstest.console.exe. Please ensure Visual Studio or Build Tools are installed."
    exit 1
}

# Change to bin directory
Set-Location $BinDir

Write-Host "============================================" -ForegroundColor Cyan
Write-Host "Report Portal Test Runner" -ForegroundColor Cyan
Write-Host "============================================" -ForegroundColor Cyan
Write-Host "Environment: $env:COMPUTERNAME"
Write-Host "VSTest: $VsTestExe"
Write-Host "Test DLL: $TestDll"
Write-Host ""
Write-Host "Report Portal Configuration:" -ForegroundColor Yellow
Write-Host "  Project: WLR_WESTLAW_REGRESSION"
Write-Host "  Launch: WL_Advantage_Playwright"
Write-Host "  URL: https://reportportal-cr.1129.aws-int.thomsonreuters.com"
Write-Host "============================================" -ForegroundColor Cyan
Write-Host ""

# Build arguments
$Args = @($TestDll, "/logger:ReportPortal", "/TestAdapterPath:.")

if ($TestName) {
    Write-Host "Running test: $TestName" -ForegroundColor Green
    $Args += "/Tests:$TestName"
}
elseif ($TestCategory) {
    Write-Host "Running tests in category: $TestCategory" -ForegroundColor Green
    $Args += "/TestCaseFilter:TestCategory=$TestCategory"
}
else {
    Write-Host "Running ALL tests..." -ForegroundColor Green
}

# Run tests
& $VsTestExe $Args
$ExitCode = $LASTEXITCODE

Write-Host ""
Write-Host "============================================" -ForegroundColor Cyan
Write-Host "Test execution complete! Exit code: $ExitCode" -ForegroundColor $(if ($ExitCode -eq 0) { "Green" } else { "Red" })
Write-Host "Check Report Portal for results at:" -ForegroundColor Yellow
Write-Host "https://reportportal-cr.1129.aws-int.thomsonreuters.com/ui/#wlr_westlaw_regression/launches/all"
Write-Host "============================================" -ForegroundColor Cyan

exit $ExitCode
