namespace Framework.Common.UI.Products.TaxnetPro.Pages
{
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Products.TaxnetPro.Components.ResultList;
    using Framework.Common.UI.Products.TaxnetPro.Components.Toolbar;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Products.WestLawNext.Pages.SearchResult.ContentTypeSearchResultPages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Taxnet Pro Content type Search Result Page
    /// </summary>
    public class TaxnetProContentTypeResultPage : BaseContentTypeSearchResultPage<ResultListItem>
    {
        /// <summary>
        /// Gets the search results list locator.
        /// </summary>
        protected override By SearchResultsListLocator => By.XPath("//ol[@class='co_searchResult_list']");

        /// <summary>
        /// Taxnet Pro Tool bar component
        /// </summary>
        public TaxnetProToolbarComponent TaxnetProToolbar { get; protected set; } = new TaxnetProToolbarComponent();

        /// <summary>
        /// Narrow Pane Component (left side of search results page) 
        /// </summary>
        public new EdgeNarrowPaneComponent NarrowPane { get; } = new EdgeNarrowPaneComponent();

        /// <summary>
        /// Gets the result list.
        /// </summary>
        public new TaxnetProResultList<ResultListItem> ResultList =>
            new TaxnetProResultList<ResultListItem>(DriverExtensions.WaitForElement(this.SearchResultsListLocator));

		/// <summary>
		/// Snippet Info Onboadring Component
		/// </summary>
		public SnippetDisplayInfoBoxComponent SnippetInfo { get; set; } = new SnippetDisplayInfoBoxComponent();
	}
}