namespace Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo
{
    using Framework.Common.UI.Products.WestlawEdge.Components.StatutesHistory;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Indigo Bill Drafts Page
    /// </summary>
    public class EdgeBillDraftsPage : EdgeTabPage
    {
        private static readonly By MainContentWrapperLocator = By.Id("co_relatedInfo_mainContentWrapper");

        /// <summary>
        ///  Gets or sets The toolbar across the top
        /// </summary>
        public new StatuteHistoryToolbarComponent Toolbar { get; set; } = new StatuteHistoryToolbarComponent();

        /// <summary>
        /// Statute History Content Types Navigation Component
        /// </summary>
        public StatuteHistoryContentTypesNavigationComponent ContentType { get; set; } = new StatuteHistoryContentTypesNavigationComponent();

        /// <summary>
        /// Verify main content wrapper is on the page
        /// </summary>
        /// <returns>
        /// True - if main content wrapper is displayed, false - otherwise
        /// </returns>
        public bool IsMainContentWrapperDisplayed() => DriverExtensions.IsDisplayed(MainContentWrapperLocator, 5);
    }
}