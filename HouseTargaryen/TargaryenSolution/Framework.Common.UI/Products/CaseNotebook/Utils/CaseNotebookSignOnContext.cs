namespace Framework.Common.UI.Products.CaseNotebook.Utils
{
    using Framework.Common.UI.DataModel.Configuration;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Core.DataModel.Security.Proxies;

    /// <summary>
    /// Case Notebook sign on context
    /// </summary>
    /// <typeparam name="TUserInfo"></typeparam>
    public class CaseNotebookSignOnContext<TUserInfo> : BaseSignOnContext<TUserInfo>
        where TUserInfo : IUserInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CaseNotebookSignOnContext{TUserInfo}"/> class. 
        /// </summary>
        /// <param name="testExecutionContext">
        /// </param>
        /// <param name="userInfo">
        /// </param>
        public CaseNotebookSignOnContext(UiTestExecutionContext testExecutionContext, TUserInfo userInfo)
            : base(testExecutionContext, userInfo)
        {
            this.RoutingSettingsInfo = testExecutionContext.RoutingPageSettings;
        }

        /// <summary>
        /// Gets or sets a value indicating whether starting from westlaw options pages
        /// </summary>
        public bool StartFromWestlawPage { get; set; } = false;

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string UrlPath { get; set; } = string.Empty;
    }
}
