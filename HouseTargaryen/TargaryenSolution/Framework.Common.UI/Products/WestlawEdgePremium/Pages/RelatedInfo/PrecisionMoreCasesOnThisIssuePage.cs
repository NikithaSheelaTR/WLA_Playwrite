namespace Framework.Common.UI.Products.WestlawEdgePremium.Pages.RelatedInfo
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Toolbar.FooterToolbar;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components.HomePage;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.ResultList;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Precision More Like This search results page
    /// </summary>
    public class PrecisionMoreCasesOnThisIssuePage: EdgeTabPage
    {
        private static readonly By MoreLikeThisBannerContainerLocator = By.XPath(".//div[@class='PrecisionFiltersBanner']");
        private static readonly By ResultListContainerLocator = By.Id("coid_website_searchResults");
        private static readonly By ResultListZeroMessageLabelLocator = By.ClassName("co_zeroStateHeader");

        /// <summary>
        /// Result list zero message label
        /// </summary>
        public ILabel ResultListZeroMessageLabel => new Label(ResultListZeroMessageLabelLocator);

        /// <summary>
        /// Athens More Like This page filter banner component
        /// </summary>
        public PrecisionFiltersBannerComponent MoreLikeThisBanner { get; } = new PrecisionFiltersBannerComponent(MoreLikeThisBannerContainerLocator);

        /// <summary>
        /// Narrow Tab Panel
        /// </summary>
        public NarrowTabPanel NarrowTabPanel { get; } = new NarrowTabPanel();

        /// <summary>
        /// Tray component
        /// </summary>
        public TrayComponent TrayComponent { get; } = new TrayComponent();

        /// <summary>
        /// The result list section of the page
        /// </summary>
        public new PrecisionResultListComponent ResultList => new PrecisionResultListComponent(DriverExtensions.GetElement(ResultListContainerLocator));

        /// <summary>
        /// The results list footer, with options for next page, previous page, etc.
        /// </summary>
        public FooterToolbarComponent FooterToolbar { get; } = new FooterToolbarComponent();
    }
}
