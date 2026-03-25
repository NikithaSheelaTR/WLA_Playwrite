namespace Framework.Common.UI.Products.GovernmentWeblinks
{
    using Framework.Common.UI.DataModel.Configuration;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Core.DataModel.Security.Proxies;

    /// <summary>
    /// The gov sign on context.
    /// </summary>
    public class GovSignOnContext<TUserInfo> : BaseSignOnContext<TUserInfo>
        where TUserInfo : IUserInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GovSignOnContext{TUserInfo}"/> class. 
        /// </summary>
        /// <param name="testExecutionContext">
        /// </param>
        /// <param name="userInfo">
        /// </param>
        public GovSignOnContext(UiTestExecutionContext testExecutionContext, TUserInfo userInfo)
            : base(testExecutionContext, userInfo)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GovSignOnContext{TUserInfo}"/> class. 
        /// </summary>
        /// <param name="testExecutionContext">
        /// </param>
        /// <param name="site">The <see cref="string"/></param>
        public GovSignOnContext(UiTestExecutionContext testExecutionContext, string site)
        {
            this.TestEnvironment = testExecutionContext.TestEnvironment;
            this.SiteName = site;
        }

        /// <summary>
        /// Gets the site.
        /// </summary>
        public string SiteName { get; }
    }
}