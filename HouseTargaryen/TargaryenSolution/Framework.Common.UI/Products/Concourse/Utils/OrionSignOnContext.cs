namespace Framework.Common.UI.Products.Concourse.Utils
{
    using Framework.Common.UI.DataModel.Configuration;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Core.DataModel.Security.Proxies;

    /// <summary>
    /// Orion SignOn Context
    /// </summary>
    /// <typeparam name="TUserInfo">Type of User Info</typeparam>
    public class OrionSignOnContext<TUserInfo> : BaseSignOnContext<TUserInfo>
        where TUserInfo : IUserInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrionSignOnContext{TUserInfo}"/> class.
        /// </summary>
        /// <param name="testExecutionContext">The test execution context.</param>
        /// <param name="userInfo">The user info.</param>
        public OrionSignOnContext(UiTestExecutionContext testExecutionContext, TUserInfo userInfo)
            : base(testExecutionContext, userInfo)
        {
            this.ForceNavigate = true;
            this.ForceRouting = false;
            this.RoutingSettingsInfo = testExecutionContext.RoutingPageSettings;
        }
    }
}