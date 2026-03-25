namespace WestlawPrecision.Tests.SeparateAthensFeature.ResponsiveEdge
{
    using System;

    using Framework.Common.UI.DataModel;
    using Framework.Common.UI.Products.WestLawNext.Utils;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Tests;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.DataModel.Security;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.DataModel.Security.Specialized;
    using Framework.Core.Utils.Extensions;
    using OpenQA.Selenium.Chrome;

    public class EdgeResponsiveBaseTest : BaseWebUiTest
    {
        protected const string CurrentTestCategory = "ResponsiveEdge";

        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeResponsiveBaseTest"/> class.
        /// </summary>
        public EdgeResponsiveBaseTest()
        {
            this.Settings.Append(
                EnvironmentConstants.PasswordPoolName,
                "WestlawGrowthEpam",
                SettingUpdateOption.Overwrite);

            this.UiExecutionSettings = this.UiExecutionSettings.SetFlags(UiExecutionFlags.AllowUiPreconditionRoutines);
        }

        /// <summary>
        /// On Manage Credential
        /// </summary>
        protected override void OnManageCredential()
        {
            var userCredential = new UserDbCredential(
                    this.TestContext,
                    PasswordVertical.WlnGrowth,
                    this.GetPasswordPool())
                { ClientId = "ResponsiveEdgeTest" };
            CredentialPool.RegisterUser(userCredential);

            this.DefaultSignOnContext = new WlnSignOnContext<IUserInfo>(this.TestExecutionContext, userCredential.ToWlnUserInfo());
        }

        /// <summary>
        /// Set the browser width and height
        /// </summary>
        /// <param name = "browserWidth" > The browser width to set</param>
        /// <param name = "browserHeight" > The browser height to set</param>
        protected void SetBrowserSize(int browserWidth, int browserHeight)
        {
            BrowserPool.CurrentBrowser.SetWindowSize(browserWidth, browserHeight);
        }

        /// <summary>
        /// Go to Edge home page by clicking home logo
        /// </summary>
        protected void GoToEdgeHomePage()
        {
            var homePage = this.GetHomePage<EdgeHomePage>();
            if (homePage.TakeTheTourComponent.IsDisplayed())
            {
                homePage.TakeTheTourComponent.ClickCloseButton();
            }
            homePage.Header.ClickLogo<EdgeHomePage>();
        }

        /// <summary>
        /// Initialize routing page settings
        /// </summary>
        protected override void InitializeRoutingPageSettings()
        {
            base.InitializeRoutingPageSettings();

            this.Settings.AppendValues(
                EnvironmentConstants.InfrastructureAccessControlsOn,
                SettingUpdateOption.Append,
                "IAC-WESTLAW-RESPONSIVE-DOCUMENT",
                "IAC-WESTLAW-RESPONSIVE-DOC-RI-TABS",
                "IAC-EDGE-RESPONSIVE-DOCUMENTVIEW",
                "IAC-EDGE-RESPONSIVE-FAVORITES",
                "IAC-EDGE-RESPONSIVE-CUSTOMPAGES");

            this.Settings.AppendValues(
                EnvironmentConstants.InfrastructureAccessControlsOff,
                SettingUpdateOption.Append,
                "IAC-QUICKCHECK-SUBMIT-HIGHLIGHTING",
                "IAC-A11Y-DROPDOWN-DELIVERY-SEARCH");
        }

        /// <summary>
        /// Retrieves Google Chrome browser options.
        /// </summary>
        /// <param name="pathToBrowserExecutable">The path to the browser executable file.</param>
        /// <returns>Google Chrome browser options.</returns>
        protected override ChromeOptions GetChromeOptions(string pathToBrowserExecutable)
        {
            ChromeOptions browserOptions = base.GetChromeOptions(pathToBrowserExecutable);

            browserOptions.AddArguments(
                "--start-maximized",
                "--disable-gpu",
                "--disable-backgrounding-occluded-windows",
                "--disable-infobars");

            var jenkinsUrl = Environment.GetEnvironmentVariable("JENKINS_URL");

            if (!string.IsNullOrEmpty(jenkinsUrl))
            {
                browserOptions.AddArguments(
                    "--headless=new",
                    "--window-size=1916,1000");
            }

            return browserOptions;
        }
    }
}