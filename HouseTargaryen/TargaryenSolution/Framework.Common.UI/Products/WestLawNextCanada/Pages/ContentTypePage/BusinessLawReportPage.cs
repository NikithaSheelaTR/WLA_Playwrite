namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.ContentTypePage
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Components.ResultLists;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Document.RI;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Products.WestlawEdge.Elements.Judicial;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.ContentType;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.FocusHighlighting;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
   
    /// <summary>
    /// Represents a Businnes Reports for content type of WLCA
    /// </summary>
    public class BusinessLawReportPage : EdgeCommonDocumentPage, IAdvancedSearchPage
    {
        private const string HighlightLctMask = "//span[contains(@class, 'co_searchTerm') and contains(.,'{0}')]";
        private static readonly By SortByOptionsLocator = By.XPath("//select[@class='co_RI_SortBy']");
        private static readonly By SearchResultsListLocator = By.XPath("//ol[@class='co_searchResult_list']");
        private static readonly By ApplyButtonLocator = By.XPath("//button[@id='co_multifacet_selector_1_applyFacetFilter']");

        /// <summary>
        /// Business Component
        /// </summary>
        public BusinessReportComponent BusinessReportComponent { get; } = new BusinessReportComponent();

        /// <summary>
        /// Business Component
        /// </summary>
        public CourtDocumentComponent CourtDocumentComponent { get; } = new CourtDocumentComponent();

        /// <summary>
        /// DocumentTypeFacetComponent
        /// </summary>
        public DocumentTypeFacetOptionComponent DocumentTypeOptionFacet { get; } = new DocumentTypeFacetOptionComponent();

        /// <summary>
        /// The Narrow Pane (left side of search results page)
        /// </summary>
        public NarrowPaneComponent NarrowPaneCompanent { get; } = new NarrowPaneComponent();

        /// <summary>
        /// Sort dropdown
        /// </summary>
        public IDropdown<string> SortDropdown { get; } = new Dropdown(SortByOptionsLocator);

        /// <summary>
        /// Get highlighted term color by term text
        /// </summary>
        /// <param name="term"> The term text </param>
        /// <returns> Term color </returns>
        public TermColors GetTermColorByText(string term) =>
            this.GetColorTypeByCode(
                DriverExtensions.GetElement(By.XPath(string.Format(HighlightLctMask, term)))
                                .GetCssValue("background-color"));
        /// <summary>
        /// Gets the result list.
        /// </summary>
        public  ISearchResultList<ResultListItem> ResultList =>
            new Components.ResultList.CanadaSearchResultList<ResultListItem>(
                DriverExtensions.WaitForElement(SearchResultsListLocator));
        /// <summary>
        /// Apply button
        /// </summary>
        public IButton ApplyButton => new CustomClickButton(ApplyButtonLocator);

        /// <summary>
        /// Click on Apply filter button if displayed
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns>Creates Page instance</returns>
        public T ClickApplyFilterButton<T>()
            where T : ICreatablePageObject
        {
            if (this.ApplyButton.Displayed)
            {
                return this.ApplyButton.Click<T>();
            }

            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
