namespace Framework.Common.UI.Products.WestLawNext.Pages.SearchResult
{
    using Framework.Common.UI.Interfaces.Components.ResultLists;
    using Framework.Common.UI.Products.Shared.Components.ResultList;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <inheritdoc />
    /// <summary>
    /// The base search result page with result list.
    /// </summary>
    /// <typeparam name="TItem">
    /// The type of result items on the page
    /// </typeparam>
    public abstract class BaseSearchResultPageWithResultList<TItem> : BaseSearchResultPage where TItem : ResultListItem
    {
        /// <summary>
        /// Gets the result list.
        /// </summary>
        public ISearchResultList<TItem> ResultList =>
            new SearchResultList<TItem>(DriverExtensions.WaitForElement(this.SearchResultsListLocator));
        
        /// <summary>
        /// Gets the search results list locator.
        /// </summary>
        protected virtual By SearchResultsListLocator => By.Id("coid_website_searchResults");
    }
}