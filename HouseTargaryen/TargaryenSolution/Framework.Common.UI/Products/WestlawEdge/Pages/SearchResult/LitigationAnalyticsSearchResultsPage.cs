using Framework.Common.UI.Interfaces.Elements;
using Framework.Common.UI.Products.Shared.Elements.Links;
using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel;
using OpenQA.Selenium;

namespace Framework.Common.UI.Products.WestlawEdge.Pages.SearchResult
{
    /// <summary>
    /// LitigationAnalytics Search Result Page no search selection 
    /// </summary>
    public class LitigationAnalyticsSearchResultsPage : BaseEdgeSearchResultPage
    {
        private static readonly By LitigationAnalyticsLogoLocator = By.XPath("//div[@class = 'HeaderBrand-subLogo']/a");

        /// <summary>
        /// The logo link.
        /// </summary>
        public ILink LogoLink => new Link(LitigationAnalyticsLogoLocator);

        /// <summary>
        /// LitigationAnalyticsBaseSearchResultPage
        /// </summary>
        public LitigationAnalyticsNarrowPanel NarrowPanel = new LitigationAnalyticsNarrowPanel();
    }
}
