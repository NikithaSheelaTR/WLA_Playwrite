namespace WestlawPrecision.Tests.SeparateAthensFeature.CodesAndRegs
{
    using Framework.Common.UI.DataModel;
    using Framework.Common.UI.Products.WestLawNext.Utils;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Tests;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Security;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.DataModel.Security.Specialized;
    using Framework.Core.Utils.Extensions;

    public class CodesAndRegsBaseTest : BaseWebUiTest
    {
        protected const string CurrentTestCategory = "CodesAndRegs";

        /// <summary>
        /// Initializes a new instance of the <see cref="CodesAndRegsBaseTest"/> class.
        /// </summary>
        public CodesAndRegsBaseTest()
        {
            this.UiExecutionSettings = this.UiExecutionSettings.SetFlags(UiExecutionFlags.AllowAutoSignOff);
            this.DefaultCobaltProduct = TestConfigurationRepository.DefaultInstance.FindProduct(CobaltProductId.WestlawNext);
            this.Settings.Append(EnvironmentConstants.PasswordPoolName, "IndigoBanffGalapagos", SettingUpdateOption.Overwrite);
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
                { ClientId = "CodesAndRegsTest" };
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
            this.GetHomePage<EdgeHomePage>().Header.ClickLogo<EdgeHomePage>();
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
                "IAC-TOC-STATUTES-FUTURE-DATE");

            this.Settings.AppendValues(
                EnvironmentConstants.InfrastructureAccessControlsOff,
                SettingUpdateOption.Append,
                "IAC-QUICKCHECK-SUBMIT-HIGHLIGHTING",
                "IAC-A11Y-DROPDOWN-DELIVERY-SEARCH");

            this.Settings.Append(
                 EnvironmentConstants.BlockCiam,
                 RoutingSettingDropdownOption.False.ToString(),
                 SettingUpdateOption.Overwrite);
        }
    }
}