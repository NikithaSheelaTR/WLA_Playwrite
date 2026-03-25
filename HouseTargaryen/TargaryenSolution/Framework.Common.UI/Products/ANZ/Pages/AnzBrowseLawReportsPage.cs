namespace Framework.Common.UI.Products.ANZ.Pages
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Products.Shared.Components.Facets.RightFacets;
    using Framework.Common.UI.Products.Shared.Components.CategoryPage;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Core.Utils.Extensions;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.ANZ.Components;

    /// <summary>
    /// Anz Browse Law Report page
    /// </summary>
    public class AnzBrowseLawReportsPage : EdgeCommonSearchResultPage
    {
        private static readonly By PageTitleLocator = By.XPath("//div[@class='co_browseHeaderContent']/h1");

        private static readonly By SortByDropdownLocator = By.XPath("//div[@class='co_dropdownTab']/select");

        private static readonly By SearchTitleLocator = By.ClassName("co_search_titleCount");

        private static readonly By LoadingSpinnerLocator = By.CssSelector(".co_loading, .co_search_ajaxLoading");

        private static readonly By BreadcrumbLocator = By.XPath("//div[@id='coid_website_breadCrumbTrail']");

        /// <summary>
        /// Initializes a new instance of the <see cref="AnzBrowseLawReportsPage"/> class.
        /// </summary>
        public AnzBrowseLawReportsPage()
        {
            // wait while spinner disappear
            DriverExtensions.WaitForElementNotDisplayed(LoadingSpinnerLocator);
        }

        /// <summary>
        /// Header component
        /// </summary>
        public new EdgeHeaderComponent Header { get; } = new EdgeHeaderComponent();

        /// <summary>
        /// Tools And Resources Widget
        /// Might be present on the right hand side for some category pages
        /// </summary>
        public ToolsAndResourcesFacetComponent ToolsAndResourcesComponent { get; private set; } = new ToolsAndResourcesFacetComponent();

        /// <summary>
        /// Favorites Component (Add To and Edit Favorites Links)
        /// </summary>
        public FavoritesComponent Favorites => new FavoritesComponent();

        /// <summary>
        /// Page title text
        /// </summary>
        public ILabel PageTitle => new Label(PageTitleLocator);

        /// <summary>
        /// SearchTitleLabel
        /// </summary>
        public ILabel SearchTitleCountLabel => new Label(SearchTitleLocator);

        /// <summary>
        /// Filter component
        /// </summary>
        public BrowseLawReportsFilterComponent Filter => new BrowseLawReportsFilterComponent();

        /// <summary>
        /// SortBy
        /// todo: merge with or override SortByDropdown of EdgeToolbarComponent
        /// </summary>
        public IDropdown<string> SortBy => new Dropdown(SortByDropdownLocator);

        /// <summary>
        /// Is Breadcrumb Displayed.
        /// </summary>
        /// <returns> True if element exists, false otherwise </returns>
        public bool IsBreadcrumbDisplayed() => DriverExtensions.IsDisplayed(BreadcrumbLocator);

        /// <summary>
        /// The get total document header count.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetTotalDocumentHeaderCount() =>
            DriverExtensions.WaitForElement(SearchTitleLocator).Text
                            .RetrieveCountFromBrackets();
    }
}
