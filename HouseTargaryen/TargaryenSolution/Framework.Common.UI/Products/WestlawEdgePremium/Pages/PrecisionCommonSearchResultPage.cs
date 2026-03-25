namespace Framework.Common.UI.Products.WestlawEdgePremium.Pages
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestlawEdge.Components.HomePage;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.ResultList;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Precision common search result page
    /// </summary>
    public class PrecisionCommonSearchResultPage: EdgeCommonSearchResultPage
    {
        private static readonly By PresicionSearchBannerContainerLocator = By.Id("AthensFiltersCollector");
        private static readonly By ResultListContainerLocator = By.Id("coid_website_searchResults");
        private static readonly By AdditionalCasesHeaderLinkLocator = By.XPath("//*[@id='Athens-additional-cases-top- header-link']");

        /// <summary>
        /// Athens Search results page selected precision search attributes banner component
        /// </summary>
        public PrecisionFiltersBannerComponent PrecisionSearchBanner { get; } = new PrecisionFiltersBannerComponent(PresicionSearchBannerContainerLocator);

        /// <summary>
        /// Athens Additional cases component
        /// </summary>
        public PrecisionAdditionalCasesComponent AdditionalCases { get; } = new PrecisionAdditionalCasesComponent();

        /// <summary>
        /// Tray component
        /// </summary>
        public TrayComponent TrayComponent { get; } = new TrayComponent();

        /// <summary>
        /// The result list section of the page
        /// </summary>
        public new PrecisionResultListComponent ResultList => new PrecisionResultListComponent(DriverExtensions.GetElement(ResultListContainerLocator));

        /// <summary>
        /// Get the additional cases scroller link
        /// </summary>
        public ILink AdditionalCasesHeaderLink => new Link(AdditionalCasesHeaderLinkLocator);
    }
}
