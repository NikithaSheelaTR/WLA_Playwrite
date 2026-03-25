namespace Framework.Common.UI.Products.WestlawEdge.Pages.SearchResult
{ 
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Components.ResultLists;
    using Framework.Common.UI.Products.Shared.Components.ResultList;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The base category search result page.
    /// </summary>
    /// <typeparam name="TItem">the type of result list item</typeparam>
    public abstract class BaseEdgeCategorySearchResultPage<TItem> : BaseEdgeFindSearchResultPage where TItem : ResultListItem
    {
        private static readonly By BackToCategoryPageLinkLocator = By.Id("coid_website_backtoCategoryPageLink");

        private static readonly By SearchResultsListLocator = By.Id("coid_website_searchResults");

        /// <summary>
        /// Old narrow panel in the form of a single panel (without tabs)
        /// </summary>
        public EdgeNarrowPaneComponent NarrowPane { get; } = new EdgeNarrowPaneComponent();

        /// <summary>
        /// New narrow pane with 'Content types' and 'Filters' tabs
        /// </summary>
        public NarrowTabPanel NarrowTabPane { get; } = new NarrowTabPanel();

        /// <summary>
        /// Gets the result list.
        /// </summary>
        public ISearchResultList<TItem> ResultList => new SearchResultList<TItem>(DriverExtensions.WaitForElement(SearchResultsListLocator));

        /// <summary>
        /// Clicks the back to browse page link
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>A new page</returns>
        public T ClickBackToBrowsePageLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(BackToCategoryPageLinkLocator);
            DriverExtensions.Click(BackToCategoryPageLinkLocator, By.TagName("a"));
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
