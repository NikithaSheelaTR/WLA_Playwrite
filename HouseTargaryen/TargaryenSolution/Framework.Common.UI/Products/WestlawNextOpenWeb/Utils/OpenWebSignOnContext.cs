namespace Framework.Common.UI.Products.WestlawNextOpenWeb.Utils
{
    using Framework.Common.UI.DataModel.Configuration;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Core.DataModel.Security.Proxies;

    /// <summary>
    /// OpenWebSignOnContext
    /// </summary>
    /// <typeparam name="TUserInfo"> userInfo </typeparam>
    public class OpenWebSignOnContext<TUserInfo> : BaseSignOnContext<TUserInfo> where TUserInfo : IUserInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenWebSignOnContext{TUserInfo}"/> class. 
        /// </summary>
        /// <param name="testExecutionContext"> test Execution Context </param>
        /// <param name="userInfo"> userInfo </param>
        public OpenWebSignOnContext(UiTestExecutionContext testExecutionContext, TUserInfo userInfo)
            : base(testExecutionContext, userInfo)
        {
            this.RoutingSettingsInfo = testExecutionContext.RoutingPageSettings;
        }
    }
}
