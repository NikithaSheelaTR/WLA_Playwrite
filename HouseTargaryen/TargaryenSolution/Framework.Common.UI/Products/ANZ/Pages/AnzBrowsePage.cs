namespace Framework.Common.UI.Products.ANZ.Pages
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Products.WestlawEdge.Components.ResultList;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Anz Browse page
    /// </summary>
    public class AnzBrowsePage : EdgeCommonBrowsePage
    {
        private static readonly By LoadingSpinnerLocator = By.CssSelector(".co_loading, .co_search_ajaxLoading");
        private static readonly By ResultListContainerLocator = By.Id("coid_website_searchResults");
        private static readonly By SearchTitleLocator = By.ClassName("co_search_titleCount");

        /// <summary>
        /// Initializes a new instance of the <see cref="AnzBrowsePage"/> class.
        /// </summary>
        public AnzBrowsePage()
        {
            // wait while spinner disappear
            DriverExtensions.WaitForElementNotDisplayed(LoadingSpinnerLocator);
        }

        /// <summary>
        /// The result list section of the page
        /// </summary>
        public EdgeLegacyResultListComponent ResultList => new EdgeLegacyResultListComponent(DriverExtensions.GetElement(ResultListContainerLocator));

        /// <summary>
        /// New Narrow Pane Component (left side of search results page) 
        /// </summary>
        public EdgeNarrowPaneComponent NarrowPane { get; } = new EdgeNarrowPaneComponent();

        /// <summary>
        /// SearchTitleLabel
        /// </summary>
        public ILabel SearchTitleCountLabel => new Label(SearchTitleLocator);
    }
}
