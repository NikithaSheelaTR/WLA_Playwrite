@echo off
REM Report Portal Test Runner for WestlawAdvantage
REM Works on both Local and EC2 environments
REM Usage: RunWithReportPortal.bat [TestName]
REM Example: RunWithReportPortal.bat AjsIncludeRelatedFedCopyLinkTest

setlocal EnableDelayedExpansion

set TEST_DLL=WestlawAdvantage.dll
set BIN_DIR=%~dp0bin\Debug

REM Try to find vstest.console.exe in multiple locations
set VSTEST=

REM Check Visual Studio 2022/2026 Community
if exist "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" (
    set VSTEST="C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"
    goto :found
)

REM Check Visual Studio 2022/2026 Enterprise (common on EC2)
if exist "C:\Program Files\Microsoft Visual Studio\2022\Enterprise\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" (
    set VSTEST="C:\Program Files\Microsoft Visual Studio\2022\Enterprise\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"
    goto :found
)

REM Check Visual Studio 2022/2026 Professional
if exist "C:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" (
    set VSTEST="C:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"
    goto :found
)

REM Check VS 18 (2026) Community
if exist "C:\Program Files\Microsoft Visual Studio\18\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" (
    set VSTEST="C:\Program Files\Microsoft Visual Studio\18\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"
    goto :found
)

REM Check VS 18 (2026) Enterprise
if exist "C:\Program Files\Microsoft Visual Studio\18\Enterprise\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" (
    set VSTEST="C:\Program Files\Microsoft Visual Studio\18\Enterprise\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"
    goto :found
)

REM Check if vstest.console.exe is in PATH (for EC2 with tools in PATH)
where vstest.console.exe >nul 2>&1
if %ERRORLEVEL% EQU 0 (
    set VSTEST=vstest.console.exe
    goto :found
)

REM Check Visual Studio Build Tools (common on CI/CD servers)
if exist "C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" (
    set VSTEST="C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"
    goto :found
)

echo ERROR: Could not find vstest.console.exe
echo Please ensure Visual Studio or Build Tools are installed
exit /b 1

:found
cd /d "%BIN_DIR%"

echo ============================================
echo Report Portal Test Runner
echo ============================================
echo Environment: %COMPUTERNAME%
echo VSTest: %VSTEST%
echo Test DLL: %TEST_DLL%
echo.
echo Report Portal Configuration:
echo   Project: WLR_WESTLAW_REGRESSION
echo   Launch: WL_Advantage_Playwright
echo   URL: https://reportportal-cr.1129.aws-int.thomsonreuters.com
echo ============================================
echo.

if "%1"=="" (
    echo Running ALL tests with ReportPortal logging...
    %VSTEST% %TEST_DLL% /logger:ReportPortal /TestAdapterPath:.
) else (
    echo Running test: %1
    %VSTEST% %TEST_DLL% /logger:ReportPortal /Tests:%1 /TestAdapterPath:.
)

set EXIT_CODE=%ERRORLEVEL%

echo.
echo ============================================
echo Test execution complete! Exit code: %EXIT_CODE%
echo Check Report Portal for results at:
echo https://reportportal-cr.1129.aws-int.thomsonreuters.com/ui/#wlr_westlaw_regression/launches/all
echo ============================================

endlocal
exit /b %EXIT_CODE%
