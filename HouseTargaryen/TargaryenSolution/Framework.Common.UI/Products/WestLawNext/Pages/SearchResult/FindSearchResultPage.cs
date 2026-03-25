namespace Framework.Common.UI.Products.WestLawNext.Pages.SearchResult
{
    using Framework.Common.UI.Interfaces.Components.ResultLists;
    using Framework.Common.UI.Products.Shared.Components.ResultList;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <inheritdoc />
    /// <summary>
    /// The find search result page
    /// </summary>
    public sealed class FindSearchResultPage : BaseFindSearchResultPage
    {
        /// <summary>
        /// Gets the search results list locator.
        /// </summary>
        private static readonly By SearchResultsListLocator = By.Id("coid_website_searchResults");

        /// <summary>
        /// Gets the search result list
        /// </summary>
        public IFindSearchResultList<ResultListItem> ResultList =>
            new FindSearchResultList<ResultListItem>(DriverExtensions.WaitForElement(SearchResultsListLocator));
    }
}
