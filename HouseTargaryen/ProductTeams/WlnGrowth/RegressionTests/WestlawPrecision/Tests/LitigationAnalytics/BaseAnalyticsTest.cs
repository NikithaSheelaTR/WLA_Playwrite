namespace WestlawPrecision.Tests.LitigationAnalytics
{
    using Framework.Common.UI.DataModel;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.LitigationAnalytics;
    using Framework.Common.UI.Products.WestLawNext.Utils;
    using Framework.Common.UI.Tests;
    using Framework.Common.UI.Utils;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Security;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.DataModel.Security.Specialized;
    using Framework.Core.Utils;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium.Chrome;
    using System;
    using System.IO;

    [DeploymentItem(@"Resources\RP.config.json")]
    public class BaseAnalyticsTest : BaseWebUiTest
    {
        protected const string FolderToSaveConst = @"C:\Temp\LitigationAnalytics";
        protected const string CurrentTestCategory = "LitigationAnalytics";
        protected const string SmokeTestCategory = "LitigationAnalyticsSmoke";
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAnalyticsTest"/> class.
        /// </summary>
        public BaseAnalyticsTest()
        {
            UiExecutionSettings = UiExecutionSettings.SetFlags(
                UiExecutionFlags.AllowApiPostconditionRoutines,
                UiExecutionFlags.AllowUiPreconditionRoutines,
                UiExecutionFlags.AllowSuperDeleteOnCleanUp,
                UiExecutionFlags.AllowSuperDeleteOnSetUp,
                UiExecutionFlags.AllowContextLoggingOnSetUp,
                UiExecutionFlags.AllowScreenshotOnFailedQualityCheckReportPortal);

            DefaultCobaltProduct =
                TestConfigurationRepository.DefaultInstance.FindProduct(CobaltProductId.WestlawEdge);
            Settings.Append(
                EnvironmentConstants.PasswordPoolName,
                "IndigoPremium",
                SettingUpdateOption.Overwrite);

            FolderingManager = new FolderingUiManager(TestExecutionContext, TestConfigurationRepository.DefaultInstance.FindProduct(CobaltProductId.WestlawNext));

        }

        /// <summary>
        /// The on manage credential.
        /// </summary>
        protected override void OnManageCredential()
        {
            var userCredential = new UserDbCredential(
                       this.TestContext,
                       PasswordVertical.WlnGrowth,
                       this.GetUserPool())
            { ClientId = "Litigation Analytics" };

            CredentialPool.RegisterUser(userCredential);

            this.DefaultSignOnContext = new WlnSignOnContext<IUserInfo>(
                                            this.TestExecutionContext,
                                            userCredential.ToWlnUserInfo());

            //  this.DefaultSignOnContext.RoutingSettingsInfo.RoutingTextboxSettings[RoutingSettingTextbox.NextLAIndexVersionUSOverride] = "181";
        }

        /// <summary>
        /// Get UserInfo
        /// </summary>
        /// <returns></returns>
        protected WlnUserInfo UserInfo => DefaultSignOnContext.UserInfo as WlnUserInfo;

        /// <summary>
        /// Retrieves Google Chrome browser options.
        /// </summary>
        /// <param name="pathToBrowserExecutable">The path to the browser executable file.</param>
        /// <returns>Google Chrome browser options.</returns>
        protected override ChromeOptions GetChromeOptions(string pathToBrowserExecutable)
        {
            ChromeOptions browserOptions = base.GetChromeOptions(pathToBrowserExecutable);

            // disable "Do you want to save password for this site" dialog
            browserOptions.AddUserProfilePreference("credentials_enable_service", false);
            browserOptions.AddUserProfilePreference("profile.password_manager_enabled", false);

            browserOptions.AddUserProfilePreference("download.default_directory", FolderToSave);
            browserOptions.AddUserProfilePreference("disable-popup-blocking", "true");        
            
            return browserOptions;
        }

        /// <summary>
        /// The folder to save.
        /// </summary>
        /// <value>
        /// The folder to save items.
        /// </value>
        protected string FolderToSave => Path.Combine(FolderToSaveConst, TestContext.TestName);

        /// <summary>
        /// Gets the foldering manager.
        /// </summary>
        protected FolderingUiManager FolderingManager { get; private set; }

        // Turn the tooltip off
        protected override void PerformUiPreconditionRoutines()
        {
            string executionDir = Path.GetDirectoryName(Path.GetDirectoryName(
            TestContext.DeploymentDirectory));
            string query = File.ReadAllText(Environment.CurrentDirectory + @"\RP.config.json");
            File.WriteAllText(executionDir + @"\ReportPortal.config.json", query);
            Logger.LogInfo($"Report portal config json was updated {executionDir}");

            BrowserPool.CurrentBrowser.Refresh<PrecisionHomePage>();
        }

        protected LitigationAnalyticsSearchPage OpenLitigationAnalyticsPage()
        {
            const string LitigationAnalytics = "Litigation Analytics";

            var homePage = this.GetHomePage<PrecisionHomePage>();

            var litigationAnalyticsPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(LitigationAnalytics)
                                                      .Click<LitigationAnalyticsSearchPage>();

            return litigationAnalyticsPage;
        }

        /// <summary>
        /// Specifies the UI post-condition operations to perform after the test on UI, but before signing off the application.
        /// </summary>
        protected override void PerformUiPostconditionRoutines()
        {
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed && !BrowserPool.CurrentBrowser.Driver.ToString().Contains("null"))
            {
                ScreenshotTaker.TakeScreenshotRp();
            }

            if (Directory.Exists(FolderToSave))
            {
                Directory.Delete(FolderToSave, true);
            }

        }

        private string GetUserPool()
        {
            return this.TestContext.Properties["Pool"] != null
                       ? this.TestContext.Properties["Pool"].ToString()
                       : this.GetPasswordPool();
        }
    }
}