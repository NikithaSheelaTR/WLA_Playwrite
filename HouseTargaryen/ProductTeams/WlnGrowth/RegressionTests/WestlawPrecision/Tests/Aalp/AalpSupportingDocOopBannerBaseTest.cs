namespace WestlawPrecision.Tests.Aalp
{
    using Framework.Common.UI.DataModel;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.CommonTypes.Enums;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The AALP Supporting document OOP banner base test.
    /// </summary>
    [DeploymentItem(@"Resources\RP.config.json")]
    [TestClass]
    public class AalpSupportingDocOopBannerBaseTest : AalpBaseTest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AalpSupportingDocOopBannerBaseTest"/> class.
        /// </summary>
        public AalpSupportingDocOopBannerBaseTest()
        {
            this.Settings.Append(
                           EnvironmentConstants.PasswordPoolName,
                           "IndigoPremiumOOP",
                           SettingUpdateOption.Overwrite);

            this.UiExecutionSettings = UiExecutionSettings.SetFlags(
                UiExecutionFlags.AllowUiPreconditionRoutines);
        }

        protected override void InitializeRoutingPageSettings()
        {
            this.Settings.Append(
                EnvironmentConstants.BlockCiam,
                RoutingSettingDropdownOption.False.ToString(),
                SettingUpdateOption.Overwrite);
        }
    }
}
