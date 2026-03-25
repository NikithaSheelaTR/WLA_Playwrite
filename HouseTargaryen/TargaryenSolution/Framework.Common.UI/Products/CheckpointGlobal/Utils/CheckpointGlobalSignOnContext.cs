namespace Framework.Common.UI.Products.CheckpointGlobal.Utils
{
    using Framework.Common.UI.DataModel.Configuration;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Core.DataModel.Security.Proxies;

    /// <summary>
    /// The Checkpoint Global sign on context.
    /// </summary>
    /// <typeparam name="TUserInfo"></typeparam>
    public class CheckpointGlobalSignOnContext<TUserInfo> : BaseSignOnContext<TUserInfo> where TUserInfo : IUserInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckpointGlobalSignOnContext{TUserInfo}"/> class.
        /// </summary>
        /// <param name="testExecutionContext"> The test execution context.  </param>
        /// <param name="userInfo"> The user info. </param>
        public CheckpointGlobalSignOnContext(UiTestExecutionContext testExecutionContext, TUserInfo userInfo)
            : base(testExecutionContext, userInfo)
        {
            this.RoutingSettingsInfo = testExecutionContext.RoutingPageSettings;
        }
    }
}
