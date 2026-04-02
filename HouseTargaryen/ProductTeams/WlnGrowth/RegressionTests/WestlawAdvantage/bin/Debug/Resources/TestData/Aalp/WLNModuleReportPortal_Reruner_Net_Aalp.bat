@ECHO ON
@ECHO ---------------------------------------------------------
@ECHO STEP 1. Initialisation.
@ECHO STEP 1.1. Initialising script variables.

:: location of the DLLs and where to push the results to
@IF NOT DEFINED TEST_SOURCE_DIR SET "TEST_SOURCE_DIR=s3://a207951-cobaltcm-testtools-prod-use1/QED/WLNR/SevenKingdoms/HouseTargaryen/TestFiles/"

:: reporting variables
@SET QREPORT_SUBJECT=%TEST_SUITE%_%TEST_ENVIRONMENT%_%TEST_BROWSER%
@IF NOT DEFINED BUSINESS_CASE_NAME SET BUSINESS_CASE_NAME=Module Regression Testing

:: PARALLEL_TEST_COUNT defines the number of the parallel test runs will be launched
:: options are in the range - 0 <= n <= number of cores
:: where 0 = Auto configure: all free cores will be used
@IF NOT DEFINED PARALLEL_TEST_COUNT SET PARALLEL_TEST_COUNT=1

:: Define unique GUID for test run
for /f %%i in ('echo %random%%random%%random%') do set TEST_RUN_GUID=%%i

@IF NOT DEFINED REPORT_PORTAL_TAGS SET REPORT_PORTAL_TAGS=

@SET LIST_OF_CONTAINERS=
@SET TEST_RESULT_FILENAME=TestResults
@IF NOT DEFINED TEST_EXECUTION_DIR SET TEST_EXECUTION_DIR=C:\Temp\TestExecution\%RANDOM%
@SET SELENIUM_DRIVERS_DIR=C:\SeleniumDrivers\WLG_Chromedriver
@SET TEST_SETTINGS_FILE_LOCATION=%TEST_EXECUTION_DIR%\Local.testsettings
@SET RUN_SETTINGS_FILE_LOCATION=%TEST_EXECUTION_DIR%\Local.runsettings
@SET HH=%time:~0,2%
@IF "%HH:~0,1%"==" " SET HH=0%HH:~1,1%
@SET TODAYS_DATETIME=%date:~10,4%-%date:~4,2%-%date:~7,2%_%HH%%time:~3,2%
@SET TEST_RESULT_DIR=%TEST_RESULT_ROOT_DIR%\%TODAYS_DATETIME%
@IF NOT DEFINED STACK SET STACK=.NET
@SET RERUNER_BIN=%TEST_EXECUTION_DIR%\Resources\Rerunner\ReportPortalReruner.bin
@SET RERUNER_EXE=%TEST_EXECUTION_DIR%\Resources\Rerunner\ReportPortalReruner.exe
:: set QyalityTestRun and Trx files name
@IF NOT DEFINED QUALITY_TEST_RUN_FILE_NAME SET QUALITY_TEST_RUN_FILE_NAME=QualityTestRun.xml
@IF NOT DEFINED TRX_FILE_NAME SET TRX_FILE_NAME=%TEST_RESULT_FILENAME%.trx

:: set the location of the test Runner (VsTest.Console.exe)
@ECHO STEP 1.2. Initialising a test runner.
@GOTO BeginTestRunnerInitBlock

:EndTestRunnerInitBlock
@ECHO ---------------------------------------------------------
@ECHO STEP 2. Preparation.
@ECHO STEP 2.1. Preparing file system objects.

:: if the additional results folder is defined, set the directory
@IF DEFINED ADDITIONAL_RESULTS_FOLDER SET ADDITIONAL_RESULTS_DIR=%TEST_RESULT_DIR%\%ADDITIONAL_RESULTS_FOLDER%\

@IF EXIST "%TEST_EXECUTION_DIR%"\ RMDIR /S /Q "%TEST_EXECUTION_DIR%" & MKDIR "%TEST_EXECUTION_DIR%"

:: create additional results directory
@IF DEFINED ADDITIONAL_RESULTS_DIR IF NOT EXIST "%ADDITIONAL_RESULTS_DIR%"\ MKDIR "%ADDITIONAL_RESULTS_DIR%"\

:: copy regression tests locally
@ECHO STEP 2.2. Copying test artefacts to %COMPUTERNAME%:
::robocopy "%TEST_SOURCE_DIR%" "%TEST_EXECUTION_DIR%" /s /njh /njs /ndl /nc /ns /np /r:2 /w:2 
aws s3 cp "%TEST_SOURCE_DIR%" "%TEST_EXECUTION_DIR%" --recursive


:: Copying Selenium drivers to known location
@ECHO STEP 2.3. Copying browser drivers artefacts to "%SELENIUM_DRIVERS_DIR%" on %COMPUTERNAME%:
@SET DRIVERS_TO_COPY=chromedriver.exe IEDriverServer.exe geckodriver.exe MicrosoftWebDriver.exe
@IF NOT EXIST %SELENIUM_DRIVERS_DIR% MKDIR %SELENIUM_DRIVERS_DIR%
FOR %%x in (%DRIVERS_TO_COPY%) DO COPY /Y %TEST_EXECUTION_DIR%\Resources\DriverExecutables\%%x %SELENIUM_DRIVERS_DIR%\%%x

::Downloading chromedriver from NAS, S3 bucket, or dynamically/automatically

:: From NAS. This seems to work. TODO: 1)How to get Dahl folder and contents to S3 bucket 2)How to make future driver updates
::@SET CHROME_SOURCE_DIR=\\cobalttesttools.int.thomsonreuters.com\cobalttesttools$\QED\WLNR\LTPCTexScripts\Dahl\SeleniumDrivers
::robocopy "%CHROME_SOURCE_DIR%" "%SELENIUM_DRIVERS_DIR%" /s /njh /njs /ndl /nc /ns /np /r:0 /w:0

:: From S3 bucket. Driver seems downloaded but version doesn't seem to match with browser version
::@SET CHROME_SOURCE_DIR=s3://a207951-cobaltcm-testtools-prod-use1/QED/WLNR/_tools and utilities/Webdriver_Updater/Chromedriver
::aws s3 cp "%CHROME_SOURCE_DIR%" "%SELENIUM_DRIVERS_DIR%" --recursive

:: Download real time
@ECHO OFF

set "jsonFile=output.json"
set "extractedFilePath=%SCRIPT_EXECUTION_DIR%\chromedriver-win32" 
::@SET CHROME_SOURCE_DIR=C:\Temp\WlnGrowthScripts\chromedriver-win32

@ECHO STEP 1. Getting the browser version
set "Version="&for /F "tokens=2delims==" %%a in ('wmic datafile where "Name='C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe'" get version /format:list') do set "Version=%%a"
IF DEFINED %Version% goto driverextraction

:chromeSourceDir
set "Version="&for /F "tokens=2delims==" %%a in ('wmic datafile where "Name='C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe'" get version /format:list') do set "Version=%%a"

:Driverextraction
set Versionvalue=%Version:~0,3%
echo %Version%
echo %Versionvalue%

@ECHO STEP 2. Identifying the stable release
set "version="&for /F %%I in ('curl https://googlechromelabs.github.io/chrome-for-testing/LATEST_RELEASE_%Versionvalue%') do set version=%%I

@ECHO STEP 2.1. Downloading and extracting the chrome-driver
curl https://storage.googleapis.com/chrome-for-testing-public/%version%/win32/chromedriver-win32.zip -o driver.zip
tar -xf driver.zip
cd chromedriver-win32
dir
@ECHO STEP 2.2. Copying browser drivers artefacts to "%SELENIUM_DRIVERS_DIR%" on %COMPUTERNAME%:
robocopy "%extractedFilePath%" "%SELENIUM_DRIVERS_DIR%" /s /njh /njs /ndl /nc /ns /np







cd %SELENIUM_DRIVERS_DIR%
CALL java -jar %CHROME_SCRIPT% chrome

GOTO VerifictionTestSettingsFile

:UpdatingTestSettingsFile
@ECHO STEP 2.4. Updating the content of the "%TEST_SETTINGS_FILE_LOCATION%" file:
@attrib -R "%TEST_SETTINGS_FILE_LOCATION%"
@CD "%TEST_EXECUTION_DIR%"
%TEST_EXECUTION_DIR%\TestSettingsUtility.exe "%TEST_SETTINGS_FILE_LOCATION%" "%TEST_SETTINGS_FILE_LOCATION%" "%QREPORT_SUBJECT%" "%PARALLEL_TEST_COUNT%"

@ECHO STEP 2.5. Updating the content of the "%RUN_SETTINGS_FILE_LOCATION%" file:

Echo STEP XXX. Displaying MaxCpuCount value found in the runsettings file:
FindStr /C:"<MaxCpuCount>" "%RUN_SETTINGS_FILE_LOCATION%"

@IF NOT "%NODE_LABELS:windows-ec2=%"=="%NODE_LABELS%" (
    @ECHO Modifying the ForcedLegacyMode tag in Local.runsettings file...
    PowerShell -Command "(Get-Content %RUN_SETTINGS_FILE_LOCATION%) -replace 'ForcedLegacyMode>true', 'ForcedLegacyMode>false' | Set-Content %RUN_SETTINGS_FILE_LOCATION%"
)

@ECHO OFF
ECHO ---------------------------------------------------------
ECHO STEP 3. Execution.
GOTO BeginVerificationBeforLaunch

:EndVerificationBeforLaunch
:: execute the tests
ECHO STEP 3.1. Running automation tests from the "%LIST_OF_CONTAINERS%" assembly:

SET TEST_RUN_COMMAND=%VSTEST_LOCATION% %LIST_OF_CONTAINERS% /Settings:"%RUN_SETTINGS_FILE_LOCATION%" /Logger:trx /Logger:ReportPortal;launch.name="%TEST_SUITE% %TEST_PRODUCTS% %TEST_ENVIRONMENT%";launch.attributes=run_id:"%TEST_RUN_GUID%";launch.tags="%TEST_PRODUCTS%,%TEST_ENVIRONMENT%,%TEST_BROWSER%,%TEST_SUITE%,%STACK%,%REPORT_PORTAL_TAGS%";launch.description="\\%TEST_RESULT_DIR%"
IF NOT "%SKIP_TEST_SETTINGS%" == "" SET TEST_RUN_COMMAND=%VSTEST_LOCATION% %LIST_OF_CONTAINERS% /Logger:trx /Logger:ReportPortal;launch.name="%TEST_SUITE% %TEST_PRODUCTS% %TEST_ENVIRONMENT%";launch.attributes=run_id:"%TEST_RUN_GUID%";launch.tags="%TEST_PRODUCTS%,%TEST_ENVIRONMENT%,%TEST_BROWSER%,%TEST_SUITE%,%STACK%,%REPORT_PORTAL_TAGS%";launch.description="\\%TEST_RESULT_DIR%"
IF DEFINED TEST_LOCATION SET TEST_RUN_COMMAND=%TEST_RUN_COMMAND%;server.project="%TEST_LOCATION%"
IF NOT "%CATEGORY_NAME%" == "" SET TEST_RUN_COMMAND=%TEST_RUN_COMMAND% /TestCaseFilter:"%CATEGORY_NAME%"


PushD %TEST_EXECUTION_DIR%
%TEST_RUN_COMMAND%

COPY /y "%RERUNER_BIN%" "%RERUNER_EXE%"
@SET RERUN_COMMAND=%RERUNER_EXE% %VSTEST_LOCATION% %LIST_OF_CONTAINERS% %TEST_RUN_GUID% %TEST_ENVIRONMENT% %TEST_BROWSER%
ECHO RERUN COMMAND: "%RERUN_COMMAND%"
@ECHO ON
PushD %TEST_EXECUTION_DIR%
%RERUN_COMMAND%

RENAME *.trx %TEST_RESULT_FILENAME%.trx
MOVE %TEST_RESULT_FILENAME%.trx .
PopD

IF NOT EXIST "%TEST_EXECUTION_DIR%\%TEST_RESULT_FILENAME%.trx" (
  SET ERRORLEVEL=-1
  SET EXIT_MSG=ERROR: %TEST_RESULT_FILENAME%.trx was not found in %TEST_EXECUTION_DIR%. The test run might have failed or not started. 
  GOTO TerminateScriptBlock
)

::@ECHO ON
::@ECHO ---------------------------------------------------------
::@ECHO STEP 4. Test Result Reporting.
:: copy the results to a network location
::@ECHO STEP 4.1. Copying the results to NAS drive:
::@IF NOT EXIST "%TEST_RESULT_DIR%"\ MKDIR "%TEST_RESULT_DIR%"
::COPY /y "%TEST_EXECUTION_DIR%\*.trx" "%TEST_RESULT_DIR%\*.trx"
::COPY /y "%TEST_EXECUTION_DIR%\ReportPortal.Shared.*.log" "%TEST_RESULT_DIR%\ReportPortal.Shared.*.log"
::COPY /y "%TEST_EXECUTION_DIR%\ReportPortal.VSTest.TestLogger.*.log" "%TEST_RESULT_DIR%\ReportPortal.VSTest.TestLogger.*.log"
::COPY /y "%TEST_EXECUTION_DIR%\%TEST_SUITE%_%TEST_ENVIRONMENT%_%TEST_BROWSER%\Out\%QUALITY_TEST_RUN_FILE_NAME%" "%TEST_RESULT_DIR%\%QUALITY_TEST_RUN_FILE_NAME%"
::robocopy "%TEST_EXECUTION_DIR%" "%TEST_RESULT_DIR%" "*config.xml" "*.jpg" /XD In /s /njh /njs /ndl /nc /ns /np

:: copy additional results to network location
@IF DEFINED ADDITIONAL_RESULTS_DIR robocopy "%TEST_EXECUTION_DIR%\%ADDITIONAL_RESULTS_FOLDER%" "%ADDITIONAL_RESULTS_DIR%" /s

:CleanUpScriptBlock
@ECHO ---------------------------------------------------------
@ECHO STEP 5. Termintation.
:: kill Chrome, Chrome driver, Firefox, IE and IE driver after running the test
@ECHO STEP 5.1. Terminating browser and Web Driver processes, if they are still running
@SET PROC2KILL=chrome.exe chromedriver.exe firefox.exe iexplore.exe IEDriverServer.exe geckodriver.exe
FOR %%x in (%PROC2KILL%) DO TASKKILL /F /FI "STATUS ne UNKNOWN" /IM %%x

@ECHO STEP 5.2. Deleting any downloaded files:
@SET DOWNLOAD_DIR=%USERPROFILE%\Downloads
FOR %%p IN ("%DOWNLOAD_DIR%\*.*") DO DEL "%%p" /F /S /Q

@ECHO OFF
@ECHO STEP 5.3. Deleting any temp files created by webdrivers and browsers:
@SET CLEANUP_LOG_FILE="%TEST_EXECUTION_DIR%\cleanup.log"
CALL :CLEANUP "%temp%" "scoped_dir*"
if not %temp%==%tmp% CALL :CLEANUP "%tmp%" "scoped_dir*"
CALL :CLEANUP "%temp%" "IE*.tmp"
if not %temp%==%tmp% CALL :CLEANUP "%tmp%" "IE*.tmp"
@ECHO Clean up has been completed. 
IF EXIST %CLEANUP_LOG_FILE% COPY /y %CLEANUP_LOG_FILE% "%TEST_RESULT_DIR%"
@ECHO Refer to the cleanup.log file in %TEST_RESULT_DIR% to view clean up output.

@GOTO TerminateScriptBlock

:: ----------------------------------------------------------------------------
:: ----- ROUTINES--------------------------------------------------------------
:: -----------Test Runner Initialisation---------------------------------------
:BeginTestRunnerInitBlock
@ECHO OFF
SET TEST_RUNNER_EXE_NAME=vstest.console.exe
SET TEST_RUNNER_VERSIONS=" 14.0" "\2017\Professional" "\2017\TestAgent" "\2017\Community" "\2019\Professional" "\2019\TestAgent" "\2022\Professional"
SET TEST_RUNNER_PATH_PREFIX=%PROGRAMFILES(x86)%\Microsoft Visual Studio
SET TEST_RUNNER_PATH_POSTFIX=Common7\IDE\CommonExtensions\Microsoft\TestWindow\%TEST_RUNNER_EXE_NAME%
SET DETECTED_TEST_RUNNER_VERSION=UNKNOWN
SET VSTEST_LOCATION=UNKNOWN
ECHO Determining a path to the test runner (%TEST_RUNNER_EXE_NAME%).

FOR %%v IN (%TEST_RUNNER_VERSIONS%) DO FOR %%v IN (%TEST_RUNNER_VERSIONS%) DO call :CHECK_VSTEST_LOCATION %%v
goto CHECK_VSTEST_LOCATION_END

:CHECK_VSTEST_LOCATION
set vstest_version=%1
set vstest_version=%vstest_version:"=%
IF EXIST "%TEST_RUNNER_PATH_PREFIX%%vstest_version%\%TEST_RUNNER_PATH_POSTFIX%" (
    SET DETECTED_TEST_RUNNER_VERSION=%vstest_version%
    SET VSTEST_LOCATION="%TEST_RUNNER_PATH_PREFIX%%vstest_version%\%TEST_RUNNER_PATH_POSTFIX%"
  )
goto :eof
:CHECK_VSTEST_LOCATION_END

IF NOT EXIST %VSTEST_LOCATION% (
  SET ERRORLEVEL=-1
  SET EXIT_MSG=ERROR: No %TEST_RUNNER_EXE_NAME% was found on %COMPUTERNAME%. MS Visual Studio 2012.1 or better is required.
  GOTO TerminateScriptBlock
) ELSE ECHO + %TEST_RUNNER_EXE_NAME% v%DETECTED_TEST_RUNNER_VERSION% was found.

@ECHO ON
@GOTO EndTestRunnerInitBlock
:: -----------Perform Verification of Parameters before Launching Tests--------
:BeginVerificationBeforLaunch
@ECHO OFF
SETLOCAL ENABLEDELAYEDEXPANSION
FOR %%i IN (%TEST_CONTAINER%) DO (
SET LIST_OF_CONTAINERS=!LIST_OF_CONTAINERS!"%TEST_EXECUTION_DIR%
SET LIST_OF_CONTAINERS=!LIST_OF_CONTAINERS!\%%i.dll" 
)
SETLOCAL DISABLEDELAYEDEXPANSION

FOR %%a IN (%LIST_OF_CONTAINERS%) DO (
IF NOT EXIST %%a (
  SET ERRORLEVEL=-1
  SET EXIT_MSG=ERROR: %%a.dll assembly was not found in %TEST_EXECUTION_DIR%. The tests cannot be launched.
  GOTO TerminateScriptBlock
)
)

IF NOT EXIST "%RUN_SETTINGS_FILE_LOCATION%" (
  SET ERRORLEVEL=-1
  SET EXIT_MSG=ERROR: %RUN_SETTINGS_FILE_LOCATION% run settings file was not found. The tests cannot be launched.
  GOTO TerminateScriptBlock
)

GOTO EndVerificationBeforLaunch

:VerifictionTestSettingsFile
IF NOT EXIST "%TEST_SETTINGS_FILE_LOCATION%" (
  SET ERRORLEVEL=-1
  SET EXIT_MSG=ERROR: %TEST_SETTINGS_FILE_LOCATION% test settings file was not found. The tests cannot be launched.
  GOTO TerminateScriptBlock
)
GOTO UpdatingTestSettingsFile

:: -----------Program Termination Section--------------------------------------
:TerminateScriptBlock
@ECHO OFF
IF %ERRORLEVEL% NEQ 0 ( ECHO The script has terminated with the following error code: %ERRORLEVEL%.) ELSE ( ECHO The script has terminated. )
IF DEFINED EXIT_MSG ECHO Termination message is: %EXIT_MSG%.
GOTO :eof
:: ----------------------------------------------------------------------------

:: --- CLEANUP Func ---
:CLEANUP
:: Clean-up temp folder
@SETLOCAL
@SET CLEANUP_DIR=%~1
@SET MASK=%~2
echo ------ Attempt to delete all files from %CLEANUP_DIR%\%MASK% ------

FOR %%G IN (TRUE, FALSE) DO (forfiles /P %CLEANUP_DIR% /M %MASK% /C "cmd /c if @isdir==%%G del /s/q @file " >>%CLEANUP_LOG_FILE% 2>&1)

echo Ok...
echo ------ Attempt to delete all folders from %CLEANUP_DIR% with mask: %MASK%------
for /d %%p in (%CLEANUP_DIR%\%MASK%) Do rd /Q /S "%%p" >>%CLEANUP_LOG_FILE% 2>&1
echo Ok...
@ENDLOCAL
:: --- /CLEANUP Func ---