@echo off
::the name of the dll file and the categories to run (.NET suites)
@SET TEST_SUITE=WL_Advantage_Playwright
@SET CATEGORY_NAME=TestCategory=WestlawAdvantage
@SET TEST_CONTAINER=WestlawAdvantage

::@SET ACCEPTABLE_PERCENTAGE_FAILED_TESTS=50

::update below to change config file values
@SET TEST_BROWSER=CH
@SET TEST_ENVIRONMENT=%TEST_ENVIRONMENT%
@SET TEST_PRODUCTS=WestlawAdvantage
@SET TEST_MODULES=Website
@SET ACCEPTABLE_FAILURE_PERCENTAGE=100
@SET RERUN_COUNT=1

@SET ENABLE_CIAM=True
@SET BLOCK_CIAM=False

@SET EXECUTOR_NUMBER=%EXECUTOR_NUMBER%
@SET BUILD_URL=%BUILD_URL%

@SET RP_TAGS=Env:%TEST_ENVIRONMENT%;Host:VDI;Browser:CH;Product:WestlawAdvantage;Category:Playwright;Owner:6114358
@SET FREQUENCY=Weekly 
@SET RP_TAGS_SCRIPT=WLNR_RP_Tags_Injector.bat

::update below to change routing page settings in WLN
@SET IACS_ON=IAC-STATUS-PAGE
@SET IACS_OFF=

::update below to change other reporting tool settings
@SET QRT_RESULTS_EMAIL=WestlawRegression-Results@thomson.com

::update this to change the name of the bat file used by this run
@SET TEX_SCRIPT=WLNModuleRegressionReportPortal_Net_Novus.bat

::DO NOT CHANGE THE BELOW SECTION
@SET SCRIPT_SOURCE_DIR=s3://a207951-cobaltcm-comm-testtools-prod-use1/QED/WLNR/LTPCTexScripts
@SET SCRIPT_EXECUTION_DIR=C:\Temp\TexScript\%EXECUTOR_NUMBER%
@IF EXIST "%SCRIPT_EXECUTION_DIR%" RMDIR "%SCRIPT_EXECUTION_DIR%"
MKDIR "%SCRIPT_EXECUTION_DIR%"

aws s3 cp "%SCRIPT_SOURCE_DIR%" "%SCRIPT_EXECUTION_DIR%" --recursive

:: Change directory to where the scripts have been copied
CD "%SCRIPT_EXECUTION_DIR%"

:: Assuming %TEX_SCRIPT% is a variable that points to the script you want to execute
CALL %TEX_SCRIPT%
