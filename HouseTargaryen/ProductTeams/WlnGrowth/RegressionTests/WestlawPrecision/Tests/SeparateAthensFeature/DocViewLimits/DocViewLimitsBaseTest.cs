namespace WestlawPrecision.Tests.SeparateAthensFeature.DocViewLimits
{
    using Framework.Common.UI.DataModel;
    using Framework.Common.UI.Products.WestLawNext.Utils;
    using Framework.Common.UI.Tests;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.DataModel.Security;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.DataModel.Security.Specialized;
    using Framework.Core.Utils.Extensions;

    public class DocViewLimitsBaseTest : BaseWebUiTest
    {
        protected const string CurrentTestCategory = "DocViewLimitsPrecision";

        /// <summary>
        /// Initializes a new instance of the <see cref="DocViewLimitsBaseTest"/> class.
        /// </summary>
        public DocViewLimitsBaseTest()
        {
            this.Settings.Append(
                EnvironmentConstants.PasswordPoolName,
                "IndigoPremium",
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
            { ClientId = "DocViewLimitsTest" };
            CredentialPool.RegisterUser(userCredential);

            this.DefaultSignOnContext = new WlnSignOnContext<IUserInfo>(this.TestExecutionContext, userCredential.ToWlnUserInfo());
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
                "IAC-WL-PL-DOC-VIEW-LIMITS");

            this.Settings.AppendValues(
                EnvironmentConstants.DocViewLimitPracticalLawDaily,
                SettingUpdateOption.Append, "2");

            this.Settings.AppendValues(
                EnvironmentConstants.DocViewLimitCatchAllDaily,
                SettingUpdateOption.Append, "2");

            this.Settings.AppendValues(
                EnvironmentConstants.FeatureAccessControlsOn,
                SettingUpdateOption.Append,
                FeatureAccessControl.DocViewDailyLimitPracticalLaw);

            this.Settings.AppendValues(
                EnvironmentConstants.FeatureAccessControlsOn,
                SettingUpdateOption.Append,
                FeatureAccessControl.DocViewDailyLimits);
        }
    }
}
