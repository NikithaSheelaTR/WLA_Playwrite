namespace Framework.Common.UI.Products.WestLawNext.Pages.SearchResult
{
    using Framework.Common.UI.Products.Shared.Components.BreadCrumb;
    using Framework.Common.UI.Products.Shared.Components.CategoryPage;
    using Framework.Common.UI.Products.Shared.Components.Facets.RightFacets;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <inheritdoc />
    /// <summary>
    /// BrowseResultsPage
    /// </summary>
    public sealed class TenMostRecentResultsPage : BaseSearchResultPageWithResultList<ResultListItem>
    {
        private static readonly By TenMostRecentLocator = By.ClassName("co_browseSearch");

        /// <summary>
        /// Favorites Component (Add To and Edit Favorites Links)
        /// </summary>
        public FavoritesComponent Favorites => new FavoritesComponent();

        /// <summary>
        /// Start Page Component (Add and Remove Page as start after user login)
        /// </summary>
        public StartPageComponent StartPage => new StartPageComponent();

        /// <summary>
        /// Tools And Resources Widget
        /// Might be present on the right hand side for some category pages
        /// </summary>
        public ToolsAndResourcesFacetComponent ToolsAndResourcesComponent => new ToolsAndResourcesFacetComponent();

        /// <summary>
        /// The Breadcrumb Component.
        /// </summary>
        public BreadCrumbComponent Breadcrumb => new BreadCrumbComponent();

        /// <summary>
        /// Gets the search results header
        /// ResultHeader can not exist if page has less that 10 results
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetTenMostRecentHeaderText()
            => DriverExtensions.IsDisplayed(TenMostRecentLocator)
                ? DriverExtensions.GetText(TenMostRecentLocator)
                : string.Empty;

        /// <summary>
        /// Get page's heading (page heading is displayed under global search bar, e.g. Find Results)
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>        
        public override string GetPageHeading() => this.GetBrowsePageTitle();
    }
}