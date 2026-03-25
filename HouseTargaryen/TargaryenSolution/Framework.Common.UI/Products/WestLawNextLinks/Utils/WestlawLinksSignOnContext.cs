namespace Framework.Common.UI.Products.WestLawNextLinks.Utils
{
    using System.Collections.Generic;

    using Framework.Common.UI.DataModel.Configuration;
    using Framework.Common.UI.Enums;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Common.UI.Products.WestLawNext.Enums;
    using Framework.Core.DataModel.Security.Proxies;

    /// <summary>
    /// Westlaw Links sign-on context
    /// </summary>
    /// <typeparam name="TUserInfo"></typeparam>
    public class WestlawLinksSignOnContext<TUserInfo> : BaseSignOnContext<TUserInfo>
        where TUserInfo : IUserInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testExecutionContext"></param>
        /// <param name="userInfo"></param>
        public WestlawLinksSignOnContext(UiTestExecutionContext testExecutionContext, TUserInfo userInfo)
            : base(testExecutionContext, userInfo)
        {
            this.RoutingSettingsInfo = testExecutionContext.RoutingPageSettings;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool AgreementPage { get; set; } = true;

        /// <summary>
        /// Uri resolution method
        /// </summary>
        public UriResolutionMethod UriResolutionMethod { get; set; }

        /// <summary>
        /// Url parameters
        /// </summary>
        public Dictionary<string, string> UrlParameters { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string UrlPath { get; set; }
    }
}