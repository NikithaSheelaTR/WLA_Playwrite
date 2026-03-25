namespace Framework.Common.UI.Products.WestLawNext.Utils
{
    using Framework.Common.UI.DataModel.Configuration;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Core.DataModel.Security.Proxies;

    /// <summary>
    /// The wln sign on context.
    /// </summary>
    /// <typeparam name="TUserInfo">
    /// </typeparam>
    public class WlnSignOnContext<TUserInfo> : BaseSignOnContext<TUserInfo>
        where TUserInfo : IUserInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WlnSignOnContext{TUserInfo}"/> class.
        /// </summary>
        /// <param name="testExecutionContext">
        /// The test execution context.
        /// </param>
        /// <param name="userInfo">
        /// The user info.
        /// </param>
        public WlnSignOnContext(UiTestExecutionContext testExecutionContext, TUserInfo userInfo)
            : base(testExecutionContext, userInfo)
        {
            this.RoutingSettingsInfo = testExecutionContext.RoutingPageSettings;
        }
    }
}