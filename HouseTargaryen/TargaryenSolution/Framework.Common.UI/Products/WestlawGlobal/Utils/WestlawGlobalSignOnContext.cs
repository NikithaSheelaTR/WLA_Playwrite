using Framework.Common.UI.DataModel.Configuration;
using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
using Framework.Core.DataModel.Security.Proxies;

namespace Framework.Common.UI.Products.WestlawGlobal.Utils
{
    /// <summary>
    /// The Westlaw Global sign on context.
    /// </summary>
    /// <typeparam name="TUserInfo"></typeparam>
    public class WestlawGlobalSignOnContext<TUserInfo> : BaseSignOnContext<TUserInfo> where TUserInfo : IUserInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WestlawGlobalSignOnContext{TUserInfo}"/> class.
        /// </summary>
        /// <param name="testExecutionContext"> The test execution context.  </param>
        /// <param name="userInfo"> The user info. </param>
        public WestlawGlobalSignOnContext(UiTestExecutionContext testExecutionContext, TUserInfo userInfo)
            : base(testExecutionContext, userInfo)
        {
            this.RoutingSettingsInfo = testExecutionContext.RoutingPageSettings;
        }
    }
}
