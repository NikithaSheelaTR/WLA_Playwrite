namespace Framework.Common.UI.Products.WestLawNext.Pages.SearchResult
{
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Components.Toolbar;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Components.SearchResults;

    using OpenQA.Selenium;

    /// <summary>
    /// Content finder result list page
    /// </summary>
    public class ContentFinderResultListPage : BaseModuleRegressionPage
    {
        private static readonly By ItemLocator = By.ClassName("co_searchContent");

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentFinderResultListPage"/> class. 
        /// </summary>
        /// <param name="container">
        /// </param>
        public ContentFinderResultListPage(IWebElement container)
        {
            this.Container = container;
        }

        /// <summary>
        /// Gets the toolbar.
        /// </summary>
        public Toolbar Toolbar { get; } = new Toolbar();

        /// <summary>
        /// Search within facet component
        /// </summary>
        public SearchWithinFacetComponent SearchWithinFacetComponent { get; } = new SearchWithinFacetComponent();

        /// <summary>
        /// ResultItems
        /// </summary>
        public ItemsCollection<ContentFinderResultItem> ResultItems => new ItemsCollection<ContentFinderResultItem>(this.Container, ItemLocator);

        /// <summary>
        /// Container
        /// </summary>
        protected IWebElement Container { get; }
    }
}
