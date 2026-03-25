namespace Framework.Common.UI.Products.ANZ.Utils
{
    using Framework.Common.UI.DataModel.Configuration;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.DataModel.Security.Proxies;


    /// <summary>
    /// The ANZ sign on context.
    /// </summary>
    /// <typeparam name="TUserInfo"> User Info type </typeparam>
    public class AnzSignOnContext<TUserInfo> : BaseSignOnContext<TUserInfo> where TUserInfo : IUserInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnzSignOnContext{TUserInfo}"/> class.
        /// </summary>
        /// <param name="testExecutionContext"> The test execution context.  </param>
        /// <param name="userInfo"> The user info. </param>
        public AnzSignOnContext(UiTestExecutionContext testExecutionContext, TUserInfo userInfo)
            : base(testExecutionContext, userInfo)
        {
            this.RoutingSettingsInfo = testExecutionContext.RoutingPageSettings;
            this.RoutingSettingsInfo.RoutingDropdownSettings.Add(RoutingSettingDropdown.SkipAnonymousAuthentication, RoutingSettingDropdownOption.True);
        }
    }
}
