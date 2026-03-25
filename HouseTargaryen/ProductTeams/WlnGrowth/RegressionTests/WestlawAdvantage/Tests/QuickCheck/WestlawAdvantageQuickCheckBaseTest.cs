namespace WestlawAdvantage.Tests.QuickCheck
{
    using System;
    using System.IO;

    using Framework.Common.Api.Endpoints;
    using Framework.Common.Api.Enums;
    using Framework.Common.UI.DataModel;
    using Framework.Common.UI.Products.WestLawNext.Utils;
    using Framework.Common.UI.Tests;
    using Framework.Common.UI.Utils;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.DataModel.Security;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.DataModel.Security.Specialized;
    using Framework.Core.Utils.Extensions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenQA.Selenium.Chrome;
    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Extensions;

    /// <summary>
    /// The document analyzer base test.
    /// </summary>
    [TestClass]
    [DeploymentItem("TestData/QuickCheckDoc")]
    [DeploymentItem("TestData/CoCounsel")]
    public class WestlawAdvantageQuickCheckBaseTest : WlaBaseTest
    {
        protected const string GeneralTestCategory = "WestlawAdvantageQuickCheck";
        protected const string TeamMatzekCategory = "AalpMatzekTests";
        protected const string AllRecommendationsContentType = "All recommendations";
        protected const string CasesContentType = "Cases";
        protected const string BriefsAndMemorandaContentType = "Briefs & Memoranda";
        protected const string SecondarySourcesContentType = "Secondary Sources";
        protected const string DocumentTab = "Document page";

        protected new const string FolderToSaveConst = @"C:\Temp\DocAnalyzer";               
        protected const string LandingUrl = "/QuickCheck?transitionType=Default&contextData=%28sc.Default%29";          
        protected string TestDocsPath = Environment.CurrentDirectory;

        /// <summary>
        /// Gets the current user.
        /// </summary>
        protected new IOnePassUserInfo CurrentUser { get; set; }

        /// <summary>
        /// On manage credential
        /// </summary>
        protected override void OnManageCredential()
        {
             var userCredential = new UserDbCredential(
                    this.TestContext,
                    PasswordVertical.WlnGrowth,
                    this.GetUserPool()) { ClientId = "Document Analysis Test" };
            
            CredentialPool.RegisterUser(userCredential);

            this.DefaultSignOnContext = new WlnSignOnContext<IUserInfo>(
                                            this.TestExecutionContext,
                                            userCredential.ToWlnUserInfo());

            this.CurrentUser = userCredential;
        }

        /// <summary>
        /// The get user info.
        /// </summary>
        /// <returns>
        /// The <see cref="WlnUserInfo"/>.
        /// </returns>
        protected new WlnUserInfo GetUserInfo() =>
                     this.DefaultSignOnContext.UserInfo as WlnUserInfo;

        /// <summary>
        /// The perform ui postcondition routines.
        /// </summary>
        protected override void PerformUiPostconditionRoutines()
        {
            if (Directory.Exists(this.FolderToSave))
            {
                Directory.Delete(this.FolderToSave, true);
            }

            base.PerformUiPostconditionRoutines();
        }

        /// <summary>
        /// Initialize routing page settings
        /// </summary>
        protected override void InitializeRoutingPageSettings()
        {
            base.InitializeRoutingPageSettings();
            this.Settings.AppendValues(
                EnvironmentConstants.FeatureAccessControlsOn,
                SettingUpdateOption.Append,
                FeatureAccessControl.IndigoPremiumF1);

            this.Settings.AppendValues(
                EnvironmentConstants.FeatureAccessControlsOn,
                SettingUpdateOption.Append,
                FeatureAccessControl.DocAnalysis,
                FeatureAccessControl.AIQuickCheckCounterArguments,
                FeatureAccessControl.AIQuickCheckBSParaphrase);
        }

        /// <summary>
        /// Retrieves Google Chrome browser options.
        /// </summary>
        /// <param name="pathToBrowserExecutable">The path to the browser executable file.</param>
        /// <returns>Google Chrome browser options.</returns>
        protected override ChromeOptions GetChromeOptions(string pathToBrowserExecutable)
        {
            ChromeOptions browserOptions = base.GetChromeOptions(pathToBrowserExecutable);

            browserOptions.AddUserProfilePreference("download.default_directory", this.FolderToSave);
            browserOptions.AddUserProfilePreference("disable-popup-blocking", "true");
            browserOptions.AddArgument("--disable-backgrounding-occluded-windows");
            browserOptions.AddArgument("--remote-allow-origins=*");

            var jenkinsUrl = Environment.GetEnvironmentVariable("JENKINS_URL");

            if (!string.IsNullOrEmpty(jenkinsUrl))
            {
                browserOptions.AddArguments(
                    "--headless=new",
                    "--window-size=1916,1000");
            }

            return browserOptions;
        }
        
        /// <summary>
        /// The perform ui precondition routines.
        /// </summary>
        protected override void PerformUiPreconditionRoutines()
        {
            string welcomeVideoPreferenceValue =
                this.TestContext.Properties["ShowQuickCheckIntroVideo"]?.ToString() ?? "false";

           WebsiteManager.SetPreferences(
                this.CurrentUser,
                this.DefaultCobaltProduct,
                this.TestExecutionContext.TestEnvironment,
                BrowserPool.CurrentBrowser.Driver.GetCookies().GetCookieContainerFromCookies(),
                VerticalName.Website,
                PreferenceName.ShowQuickCheckIntroVideo,
                welcomeVideoPreferenceValue);

            WebsiteManager.SetUserSettings(
                this.CurrentUser,
                this.DefaultCobaltProduct,
                this.TestExecutionContext.TestEnvironment,
                BrowserPool.CurrentBrowser.Driver.GetCookies().GetCookieContainerFromCookies(),
                PreferenceName.ShowHomePageOverview,
                "false");

            WebsiteManager.SetUserSettings(
                this.CurrentUser,
                this.DefaultCobaltProduct,
                this.TestExecutionContext.TestEnvironment,
                BrowserPool.CurrentBrowser.Driver.GetCookies().GetCookieContainerFromCookies(),
                PreferenceName.ShowDocAnalyzerOppWorkOverview,
                "false");

            WebsiteManager.SetUserSettings(
                this.CurrentUser,
                this.DefaultCobaltProduct,
                this.TestExecutionContext.TestEnvironment,
                BrowserPool.CurrentBrowser.Driver.GetCookies().GetCookieContainerFromCookies(),
                PreferenceName.ShowDocAnalyzerOverview,
                "false");
        }
        
        private string GetUserPool()
        {
            return this.TestContext.Properties["Pool"] != null
                       ? this.TestContext.Properties["Pool"].ToString()
                       : this.GetPasswordPool();
        }
    }
}
