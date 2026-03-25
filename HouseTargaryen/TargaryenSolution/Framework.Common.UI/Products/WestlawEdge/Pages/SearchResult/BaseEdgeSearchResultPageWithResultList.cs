namespace Framework.Common.UI.Products.WestlawEdge.Pages.SearchResult
{
    using Framework.Common.UI.Interfaces.Components.ResultLists;
    using Framework.Common.UI.Products.Shared.Components.ResultList;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The base  edge search result page with result list.
    /// </summary>
    /// <typeparam name="TItem"> the type of the result item on the page</typeparam>
    public abstract class BaseEdgeSearchResultPageWithResultList<TItem> : BaseEdgeSearchResultPage where TItem : ResultListItem
    {
        private static readonly By SearchResultsListLocator = By.Id("coid_website_searchResults");

        /// <summary>
        /// Gets the result list.
        /// </summary>
        public ISearchResultList<TItem> ResultList =>
            new SearchResultList<TItem>(DriverExtensions.WaitForElement(SearchResultsListLocator));
    }
}
