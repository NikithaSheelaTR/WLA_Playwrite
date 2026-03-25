namespace Framework.Common.UI.Products.WestLawAnalytics.Utils
{
    using Framework.Common.UI.DataModel.Configuration;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Core.DataModel.Security.Proxies;

    /// <summary>
    /// The WL Analytics sign on context.
    /// </summary>
    /// <typeparam name="TUserInfo"> User Info type </typeparam>
    public class AnalyticsSignOnContext<TUserInfo> : BaseSignOnContext<TUserInfo>
        where TUserInfo : IUserInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsSignOnContext{TUserInfo}"/> class.
        /// </summary>
        /// <param name="testExecutionContext"> The test execution context. </param>
        /// <param name="userInfo"> The user info. </param>
        public AnalyticsSignOnContext(UiTestExecutionContext testExecutionContext, TUserInfo userInfo)
            : base(testExecutionContext, userInfo)
        {
            this.RoutingSettingsInfo = testExecutionContext.RoutingPageSettings;
        }
    }
}