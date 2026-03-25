namespace Framework.Common.UI.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Framework.Common.Api.Endpoints;
    using Framework.Common.Api.Enums;
    using Framework.Common.UI.DataModel;
    using Framework.Common.UI.DataModel.Configuration;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Utils;
    using Framework.Common.UI.Products.WestLawNextMobile.Pages;
    using Framework.Common.UI.Utils;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Cobalt.Passwords;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.DataModel.Security;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.DataModel.Security.Specialized;
    using Framework.Core.Tests;
    using Framework.Core.Utils;
    using Framework.Core.Utils.Configuration;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.Extensions;
    using Framework.Core.Utils.Verification;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenQA.Selenium.Chrome;
    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// The base Web UI Test class
    /// </summary>
    [TestClass]
    [DeploymentItem("Resources/Rerunner/ReportPortalReruner.exe")]
    [DeploymentItem("ReportPortal.config.json")]
    public class BaseWebUiTest : BaseContextualTest<UiTestExecutionContext>
    {
        private ScreenshotTaker screenshotTaker;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseWebUiRegressionTest"/> class. 
        /// </summary>
        public BaseWebUiTest()
        {
            this.UiExecutionSettings = this.UiExecutionSettings.SetFlags(
                UiExecutionFlags.AllowCredentialManagement,
                UiExecutionFlags.AllowContextLoggingOnSetUp,
                UiExecutionFlags.AllowUiPreconditions,
                UiExecutionFlags.AllowAutoSignOn,
                UiExecutionFlags.AllowContextLoggingOnCleanUp,
                UiExecutionFlags.AllowApiPostconditions,
                UiExecutionFlags.AllowSuperDeleteOnCleanUp,
                UiExecutionFlags.AllowUiPostconditions,
                UiExecutionFlags.AllowUiPostconditionRoutines);

            this.DefaultCobaltProduct = this.TestExecutionContext.Products.FirstOrDefault();

            string isFedRamp = this.Settings.GetValue(EnvironmentConstants.IsFedRamp);
            this.DefaultCobaltProduct = isFedRamp != null && isFedRamp.ToLower().Equals("yes")
                                            ? TestConfigurationRepository.FedRampInstance.FindProduct(
                                                CobaltProductId.WestlawNext)
                                            : this.DefaultCobaltProduct;
        }

        /// <summary>
        /// Gets the default screenshot taker to capture browser screens.
        /// </summary>
        protected ScreenshotTaker ScreenshotTaker
        {
            get
            {
                return this.screenshotTaker = this.screenshotTaker ?? new ScreenshotTaker(
                                                  this.TestContext,
                                                  Thread.CurrentThread.ManagedThreadId,
                                                  this.UiExecutionSettings.HasFlag(
                                                      UiExecutionFlags
                                                          .AllowScreenshotOnFailedQualityCheckReportPortal));
            }

            private set
            {
                this.screenshotTaker = value;
            }
        }

        /// <summary>
        /// Cobalt product under test
        /// </summary>
        protected CobaltProductInfo DefaultCobaltProduct { get; set; }

        /// <summary>
        /// Gets or sets the default sign on context.
        /// </summary>
        protected ISignOnContext<IUserInfo> DefaultSignOnContext { get; set; }

        /// <summary>
        /// Gets or sets the default sign-on manager.
        /// </summary>
        protected ISignOnManager DefaultSignOnManager { get; set; }

        /// <summary>
        /// Test Case Verify
        /// </summary>
        protected TestCaseVerify TestCaseVerify { get; set; }

        /// <summary>
        /// Test Case Assert
        /// </summary>
        protected TestCaseAssert TestCaseAssert { get; set; }

        /// <summary>
        /// Gets or sets the UI execution settings.
        /// </summary>
        protected UiExecutionFlags UiExecutionSettings { get; set; }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        protected IOnePassUserInfo CurrentUser { get; set; }

        /// <summary>
        /// Cleans up the test case.
        /// </summary>
        [TestCleanup]
        public override void CleanupTestCase()
        {
            var handlers = new List<Action>();

            // Logging block
            if (this.UiExecutionSettings.IsFlagDependencyMet(UiFlagDependencyMap.AllowContextLoggingOnCleanUp))
            {
                handlers.Add(this.LogExecutionEnd);
            }

            // UI post-condition block
            if (this.UiExecutionSettings.IsFlagDependencyMet(UiFlagDependencyMap.AllowUiPostconditions))
            {
                handlers.Add(this.PerformUiPostconditions);
            }

            // Test client finalization block
            handlers.Add(BrowserPool.DisposeOfBrowsers);

            // API post-condition block
            if (this.UiExecutionSettings.IsFlagDependencyMet(UiFlagDependencyMap.AllowApiPostconditions))
            {
                handlers.Add(this.PerformApiPostconditions);
            }

            // User account finalization block
            handlers.Add(CredentialPool.DisposeOfUsers);

            // Execution of the chained post-condition steps and recording the results.
            this.ProcessEnvironmentPreparationHandlers(handlers, false);

            this.TestCaseVerify.CleanUp();
            this.TestCaseAssert.CleanUp();

            // Superior clean-up call block. Finalize the checks
            base.CleanupTestCase();
        }

        /// <summary>
        /// Initialize method for Module Regression tests
        /// </summary>
        [TestInitialize]
        public override sealed void InitializeTestCase()
        {
            var handlers = new List<Action>();

            this.Settings.UpdateFromTestProperties(this.TestContext.Properties);

            // Superior initialize call
            this.TestCaseVerify = new TestCaseVerify(this.TakeScreenshot);
            this.TestCaseAssert = new TestCaseAssert(this.TakeScreenshot);
            base.InitializeTestCase();

            // Test client initialization block,
            handlers.Add(this.InitializeTestClient);
            handlers.Add(this.InitializeRoutingPageSettings);

            // User account management block
            if (this.UiExecutionSettings.IsFlagDependencyMet(UiFlagDependencyMap.AllowCredentialManagement))
            {
                handlers.Add(this.OnManageCredential);
            }

            // Logging block
            if (this.UiExecutionSettings.IsFlagDependencyMet(UiFlagDependencyMap.AllowContextLoggingOnSetUp))
            {
                handlers.Add(this.LogExecutionStart);
            }

            // API precondition block
            if (this.UiExecutionSettings.IsFlagDependencyMet(UiFlagDependencyMap.AllowApiPreconditions))
            {
                handlers.Add(this.PerformApiPreconditions);
            }

            // UI precondition block
            if (this.UiExecutionSettings.IsFlagDependencyMet(UiFlagDependencyMap.AllowUiPreconditions))
            {
                handlers.Add(this.PerformUiPreconditions);
            }

            // Execution of the chained pre-condition steps.
            ExecutionResultType execResult = this.ProcessEnvironmentPreparationHandlers(handlers, true);

            this.ScreenshotTaker = null;

            // Terminating a test if at least one preparation procedure has failed unexpectedly.
            if (execResult != ExecutionResultType.Success)
            {
                this.CleanupTestCase();
            }
        }

        /// <summary>
        /// Return to home page.
        /// </summary>
        /// <typeparam name="T">Home page</typeparam>
        /// <returns>The <see cref="CommonSearchHomePage"/></returns>
        public T ReturnToHomePage<T>() where T : ICommonSearchHomePage
        {
            var url = new Uri(BrowserPool.CurrentBrowser.Url);
            BrowserPool.CurrentBrowser.GoToUrl(url.GetLeftPart(UriPartial.Authority));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Prints out current password info for checked out passwords to confirm that no concurrency issues have occurred.
        /// </summary>
        protected void DisplayPasswordInfo()
        {
            foreach (IUserInfo userInfo in CredentialPool.GetAllRegisteredUsers())
            {
                var userDbInfo = userInfo as UserDbCredential;
                if (userDbInfo != null)
                {
                    Logger.LogInfo("Checked out password:");
                    BaseWebUiTest.PrintPasswordInfo(userDbInfo.PasswordInfo);
                }
                else
                {
                    Logger.LogInfo("There is no required info for user: " + userInfo.UserName);
                }
            }
        }

        /// <summary>
        /// Retrieves browser options for the specified version of a browser
        /// </summary>
        /// <returns>The <see cref="object"/>.</returns>
        protected object GetBrowserOptions()
        {
            object browserOptions;
            string pathToExe = this.TestExecutionContext.TestClient.PathToExecutable;

            switch (this.TestExecutionContext.TestClient.Id)
            {
                case TestClientId.Chrome:
                    browserOptions = this.GetChromeOptions(pathToExe);
                    break;
                case TestClientId.ChromeCanary:
                    browserOptions = this.GetChromeCanaryOptions(pathToExe);
                    break;
                default:
                    browserOptions = DriverFactory.GetBrowserOptions(this.TestExecutionContext.TestClient);
                    break;
            }

            return browserOptions;
        }

        /// <summary>
        /// Retrieves Google Chrome Canary browser options.
        /// </summary>
        /// <param name="pathToBrowserExecutable">The path to the browser executable file.</param>
        /// <returns>Google Chrome Canary browser options</returns>
        protected virtual ChromeOptions GetChromeCanaryOptions(string pathToBrowserExecutable)
        {
            return DriverFactory.GetChromeCanaryOptions(pathToBrowserExecutable);
        }

        /// <summary>
        /// Retrieves Google Chrome Canary browser options.
        /// </summary>
        /// <param name="pathToBrowserExecutable">The path to the browser executable file.</param>
        /// <returns>Google Chrome Canary browser options</returns>
        protected virtual ChromeOptions GetChromeOptions(string pathToBrowserExecutable)
        {
            return DriverFactory.GetChromeOptions(pathToBrowserExecutable);
        }

        /// <summary>
        /// Gets the default test settings.
        /// </summary>
        /// <returns>The <see cref="IDictionary{TKey, TValue}"/>.</returns>
        protected override IDictionary<string, string> GetDefaultTestSettings()
        {
            IDictionary<string, string> result = base.GetDefaultTestSettings();

            result.Add(EnvironmentConstants.FeatureAccessControlsOn, string.Empty);
            result.Add(EnvironmentConstants.FeatureAccessControlsOff, string.Empty);
            result.Add(EnvironmentConstants.SupportedFeaturesOn, string.Empty);
            result.Add(EnvironmentConstants.SupportedFeaturesOff, string.Empty);
            result.Add(EnvironmentConstants.InfrastructureAccessControlsOn, null);
            result.Add(EnvironmentConstants.InfrastructureAccessControlsOff, null);
            result.Add(EnvironmentConstants.CategoryPageCollectionSet, null);
            result.Add(EnvironmentConstants.DataRoom, null);
            result.Add(EnvironmentConstants.DataRoomBulk, null);
            result.Add(EnvironmentConstants.IsDataRoomRegionEnabled, null);
            result.Add(EnvironmentConstants.EnableCiam, null);
            result.Add(EnvironmentConstants.BlockCiam, null);
            result.Add(EnvironmentConstants.PmdVersion, null);
            result.Add(EnvironmentConstants.OnlineCharges, null);
            result.Add(EnvironmentConstants.PasswordPoolName, string.Empty);
            result.Add(EnvironmentConstants.OverrideOnlineCharges, string.Empty);
            result.Add(EnvironmentConstants.EnableGovCiam, null);
            return result;
        }

        /// <summary>
        /// Gets the landing page for the product under test.
        /// </summary>
        /// <typeparam name="T">The class of the Homepage</typeparam>
        /// <returns>The homepage page-object</returns>
        protected T GetHomePage<T>() where T : ICreatablePageObject
        {
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Returns the password pool.
        /// </summary>
        /// <returns>The password pool.</returns>
        protected string GetPasswordPool()
        {
            string passwordPool = this.Settings.GetValue(EnvironmentConstants.PasswordPoolName);
            return !string.IsNullOrEmpty(passwordPool)
                       ? passwordPool
                       : (this.TestExecutionContext.TestEnvironment.IsLower
                              ? PasswordPool.WlnrGeneralPurposePreProdPool.GetEnumTextValue()
                              : PasswordPool.WlnrGeneralPurposeProdPool.GetEnumTextValue());
        }

        /// <summary>
        /// Instantiates a default sign-on context.
        /// </summary>
        /// <param name="userInfo">The user info.</param>
        /// <typeparam name="T">The type of the sign-on context.</typeparam>
        /// <returns>An instance of the specified type of the sign-on context.</returns>
        protected T GetSignOnContext<T>(IUserInfo userInfo) where T : ISignOnContext<IUserInfo>
        {
            return (T)Activator.CreateInstance(typeof(T), this.TestExecutionContext, userInfo);
        }

        /// <summary>
        /// Initialize the routing page setting part of the test execution context with default values.
        /// </summary>
        protected virtual void InitializeRoutingPageSettings()
        {
            this.Settings.AppendValues(
                    EnvironmentConstants.FeatureAccessControlsOff,
                    SettingUpdateOption.Append,
                    FeatureAccessControl.WelcomeToWLNBox)
                .AppendValues(
                    EnvironmentConstants.FeatureAccessControlsOn,
                    SettingUpdateOption.Append,
                    FeatureAccessControl.IgnoreAuthorizationBlocks)
                .Append(
                    EnvironmentConstants.WlnDataRoomFoldersReadWriteDestination,
                    RoutingSettingDropdownOption.SingleDataRoom.ToString(),
                    SettingUpdateOption.UpdateIfUnset);
        }

        /// <summary>
        /// Set user preferencies from the top of the test
        /// </summary>
        protected void SetUserPreferencies()
        {
            var allTestProperties = this.TestContext.Properties;

            var userPreferencies = Enum.GetValues(typeof(PreferenceName))
                .Cast<PreferenceName>()
                .Select(v => v.ToString())
                .ToList();

            foreach (var userPreference in userPreferencies)
            {
                if (allTestProperties[userPreference]?.ToString() != null)
                {
                    WebsiteManager.SetUserSettings(
                        this.CurrentUser,
                        this.DefaultCobaltProduct,
                        this.TestExecutionContext.TestEnvironment,
                        BrowserPool.CurrentBrowser.Driver.GetCookies().GetCookieContainerFromCookies(),
                        (PreferenceName)Enum.Parse(typeof(PreferenceName), userPreference),
                        allTestProperties[userPreference].ToString());
                }
            }
        }

        /// <summary>
        /// Logs the test execution info
        /// </summary>
        protected virtual void LogExecutionEnd()
        {
            // TODO: revise the logic for taking session Id in SignOnManager
            this.PrintSessionId();
        }

        /// <summary>
        /// Logs the test execution context  
        /// </summary>
        protected virtual void LogExecutionStart()
        {
            this.TestExecutionContext.PrintTestContext();

            // Logs info about users that are reserved by the test
            if (this.UiExecutionSettings.HasFlag(UiExecutionFlags.AllowCredentialManagement))
            {
                SafeMethodExecutor.Execute(this.DisplayPasswordInfo).LogDetails();
            }
        }

        /// <summary>
        /// Navigates selenium to the Drafting assistant page
        /// </summary>
        /// <typeparam name="T">the type of the sign on page to return</typeparam>
        /// <returns>a page object representing the drafting assistant page</returns>
        protected T NavigateToDraftingAssistantPage<T>() where T : DraftingAssistantPage
        {
            BrowserPool.CurrentBrowser.GoToUrl(
                this.TestExecutionContext.TestEnvironment.Id.GetUrlForDraftingAssistant());
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Specifies how to reserve a user (users) for the test
        /// </summary>
        protected virtual void OnManageCredential()
        {
            var userCredential = new UserDbCredential(
                this.TestContext,
                PasswordVertical.WlnModuleAndFeatureRegression,
                this.GetPasswordPool());

            CredentialPool.RegisterUser(userCredential);
            this.DefaultSignOnContext = new WlnSignOnContext<IUserInfo>(
                this.TestExecutionContext,
                userCredential.ToWlnUserInfo());
        }

        /// <summary>
        /// Specifies the API post-condition calls to perform after the test,
        /// for example, clearing some test data via API calls.
        /// </summary>
        protected virtual void PerformApiPostconditionRoutines()
        {
        }

        /// <summary>
        /// Specifies the type of API post-condition operations to perform after the test.
        /// </summary>
        protected virtual void PerformApiPostconditions()
        {
            if (this.UiExecutionSettings.IsFlagDependencyMet(UiFlagDependencyMap.AllowSuperDeleteOnCleanUp)
                && this.TestExecutionContext.TestEnvironment.IsLower
                && FolderingManager.IsFolderingAvailableFor(this.DefaultCobaltProduct?.Id))
            {
                SafeMethodExecutor.Execute(this.CleanUpFolders).LogDetails();
            }

            if (this.UiExecutionSettings.IsFlagDependencyMet(UiFlagDependencyMap.AllowApiPostconditionRoutines))
            {
                this.PerformApiPostconditionRoutines();
            }
        }

        /// <summary>
        /// Performs common API preconditions that are needed for the test,
        /// for example, creation of folders via API calls.
        /// </summary>
        protected virtual void PerformApiPreconditionRoutines()
        {
        }

        /// <summary>
        /// Specifies the type of API precondition operations to perform before the test.
        /// </summary>
        protected void PerformApiPreconditions()
        {
            if (this.UiExecutionSettings.IsFlagDependencyMet(UiFlagDependencyMap.AllowSuperDeleteOnSetUp)
                && this.TestExecutionContext.TestEnvironment.IsLower
                && FolderingManager.IsFolderingAvailableFor(this.DefaultCobaltProduct?.Id))
            {
                SafeMethodExecutor.Execute(this.CleanUpFolders).LogDetails();
            }

            if (this.UiExecutionSettings.IsFlagDependencyMet(UiFlagDependencyMap.AllowApiPreconditionRoutines))
            {
                this.PerformApiPreconditionRoutines();
            }
        }

        /// <summary>
        /// Performs the sign-off operation after the test execution. The block terminates the user's session in a correct way.
        /// </summary>
        protected virtual void PerformAutoSignOff()
        {
            (this.DefaultSignOnManager ?? SignOnManagerFactory.Retrieve(this.DefaultCobaltProduct)).SignOff();
        }

        /// <summary>
        /// Performs a sign-on operation. Generally, a test will start from a landing page of the product under test.
        /// </summary>
        protected virtual void PerformAutoSignOn()
        {
            this.DefaultSignOnManager = SignOnManagerFactory.Retrieve(this.DefaultCobaltProduct);
            this.DefaultSignOnManager.SignOn(this.DefaultSignOnContext);
        }

        /// <summary>
        /// Specifies the UI post-condition operations to perform after the test on UI, but before signing off the application.
        /// </summary>
        protected virtual void PerformUiPostconditionRoutines()
        {
        }

        /// <summary>
        /// Performs common UI preconditions that are needed for the test after the sign-on operation.
        /// For example, all Company Investigator tests should start from the Company Investigator page, 
        /// Setting the Company Investigator page as a starting point for the tests should be described in this method.
        /// </summary>
        protected virtual void PerformUiPreconditionRoutines()
        {
        }

        /// <summary>
        /// Prints the current test's session id and session info tool URL.
        /// </summary>
        protected void PrintSessionId()
        {
            var userInfo = CredentialPool.GetFirstOrDefaultUser<IOnePassUserInfo>();
            string sessionId = string.Empty;
            Logger.LogInfo(BrowserPool.CurrentBrowser.Url);

            if (userInfo != null)
            {
                if (!string.IsNullOrEmpty(userInfo.UniqueKey))
                {
                    SafeMethodExecutor.Execute(
                        () =>
                            sessionId =
                                new CobaltSessionManager(
                                    this.TestExecutionContext.TestEnvironment,
                                    this.DefaultCobaltProduct,
                                    userInfo).GetSessionInfo(DriverExtensions.GetSiteCookieValue).SessionId);
                }
                else
                {
                    Logger.LogError($"Unable to retrieve session, Prism GUID not set for user '{userInfo.UserName}'");
                }
            }
            else
            {
                Logger.LogError("User is not defined.'");
            }

            if (!string.IsNullOrEmpty(sessionId))
            {
                Logger.LogInfo("Session ID: " + sessionId);
                Logger.LogInfo(
                    this.TestExecutionContext.TestEnvironment.Id.GetSessionInfoUrl(sessionId) + Environment.NewLine);
            }
        }

        /// <summary>
        /// Prints out password info.
        /// </summary>
        /// <param name="passwordInfo">The password info.</param>
        private static void PrintPasswordInfo(PasswordInfo passwordInfo)
        {
            if (passwordInfo == null)
            {
                Logger.LogInfo("Password information is undefined.");
            }
            else
            {
                Logger.LogInfo("OnePass Username:\t" + passwordInfo.OnePassUsername);
                Logger.LogInfo("Available:\t\t\t" + passwordInfo.Available);
                Logger.LogInfo("Checked out by:\t\t" + passwordInfo.CheckedOutBy);
                Logger.LogInfo("Checkout Date Time:\t" + passwordInfo.CheckoutDateTime);
                Logger.LogInfo("Expiration Date Time:\t" + passwordInfo.ExpirationDateTime);
                Logger.LogInfo("Test Run ID:\t\t\t" + passwordInfo.TestRunId);
                Logger.LogInfo("Test Using Password:\t" + passwordInfo.TestUsingPwd);
            }
        }

        /// <summary>
        /// Call SuperDelete endpoint for the user: clean up all folders and sharing invitations.
        /// </summary>
        private void CleanUpFolders()
        {
            foreach (IOnePassUserInfo user in CredentialPool.GetAllRegisteredUsers<IOnePassUserInfo>())
            {
                if (string.IsNullOrEmpty(user.PrismGuid))
                {
                    throw new ArgumentException(
                        $"The user info for user: '{user.UserName}' is not complete: PrismGUID filed is empty");
                }

                FolderingManager.PerformSuperDelete(
                    user,
                    this.TestExecutionContext.TestEnvironment,
                    this.DefaultCobaltProduct);
            }
        }

        /// <summary>
        ///  Sets up the Selenium WebDriver based the browser being used.
        /// </summary>
        private void InitializeTestClient()
        {
            // Initialize the Selenium WebDriver
            BrowserPool.RegisterAndMakeCurrentBrowser(
                new Browser(
                    this.TestExecutionContext.TestClient,
                    this.GetBrowserOptions(),
                    this.Settings.GetValue(EnvironmentConstants.DriverLocation)));
            SafeMethodExecutor.Execute(BrowserPool.CurrentBrowser.Maximize).LogDetails();

            // Add an implicit wait to the driver that will cause the driver to wait
            // when findElement function is called
            DriverExtensions.SetTimeout(0);
        }

        /// <summary>
        /// Specifies the type of UI post-condition operations to perform after the test.
        /// </summary>
        private void PerformUiPostconditions()
        {
            if (this.UiExecutionSettings.IsFlagDependencyMet(UiFlagDependencyMap.AllowUiPostconditionRoutines))
            {
                this.PerformUiPostconditionRoutines();
            }

            if (this.UiExecutionSettings.IsFlagDependencyMet(UiFlagDependencyMap.AllowAutoSignOff))
            {
                this.PerformAutoSignOff();
            }
        }

        /// <summary>
        /// Specifies the type of UI precondition operations to perform before the test.
        /// </summary>
        private void PerformUiPreconditions()
        {
            if (this.UiExecutionSettings.IsFlagDependencyMet(UiFlagDependencyMap.AllowAutoSignOn))
            {
                this.PerformAutoSignOn();
            }

            if (this.UiExecutionSettings.IsFlagDependencyMet(UiFlagDependencyMap.AllowUiPreconditionRoutines))
            {
                this.PerformUiPreconditionRoutines();
            }
        }

        /// <summary>
        /// Processes the chained test environment handlers in pre-condition and post-condition blocks. Failures detected are embedded into virtual quality checks.
        /// </summary>
        /// <param name="handlers">The chain of handlers to invoke.</param>
        /// <param name="stopExecutionOnFailure">Instructs whether to stop execution of the chain of handlers on a failure or to proceed.</param>
        /// <returns>The <see cref="ExecutionResultType"/>.</returns>
        private ExecutionResultType ProcessEnvironmentPreparationHandlers(
            IList<Action> handlers,
            bool stopExecutionOnFailure)
        {
            var execResult = ExecutionResultType.Success;

            for (int i = 0;
                 (!stopExecutionOnFailure || execResult == ExecutionResultType.Success) && i < handlers.Count;
                 i++)
            {
                ExecutionResult intermExecResullt = SafeMethodExecutor.Execute(handlers[i]);

                intermExecResullt.LogDetails();

                if (intermExecResullt.ResultType != ExecutionResultType.Success)
                {
                    // ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
                    execResult |= intermExecResullt.ResultType;
                }
            }

            return execResult;
        }

        private void TakeScreenshot() => this.ScreenshotTaker.TakeScreenshotRp();
    }
}
