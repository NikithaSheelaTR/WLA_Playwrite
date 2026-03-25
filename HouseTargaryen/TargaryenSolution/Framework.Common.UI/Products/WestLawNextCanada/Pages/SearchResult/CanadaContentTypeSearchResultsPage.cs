namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.SearchResult
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Components.ResultLists;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel;
    using Framework.Common.UI.Products.WestLawNext.Pages.SearchResult.ContentTypeSearchResultPages;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.ContentType;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.NarrowPane;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The Canada ContenttypeSearchResultPage
    /// </summary>
    public class CanadaContentTypeSearchResultsPage : BaseContentTypeSearchResultPage<ResultListItem>
    {
        private static readonly By SearchHeaderLocator = By.XPath(".//*[@class = 'co_search_header']");
        private static readonly By ViewAllLinkLocator = By.XPath(".//*[@class = 'co_search_header']/a");
        private static readonly By PageHeadingLocator = By.ClassName("co_search_result_heading_content");

        /// <summary>
        /// The container locator mask.
        /// </summary>
        private readonly string containerLocatorMask = "cobalt_search_can_{0}_results";


        /// <summary>
        /// New Narrow Pane Component (left side of search results page)
        /// </summary>
        public EdgeNarrowPaneComponent NarrowPanel { get; } = new EdgeNarrowPaneComponent();

        /// <summary>
        /// Canada Classic left narrow pane component
        /// </summary>
        public LeftNarrowPaneComponent LeftNarrowPane { get; } = new LeftNarrowPaneComponent();

        /// <summary>
        /// New narrow pane with 'Content types' and 'Filters' tabs
        /// </summary>
        public NarrowTabPanel NarrowTabPane { get; } = new NarrowTabPanel();

        /// <summary>
        /// Business Component
        /// </summary>
        public CourtDocumentComponent CourtDocumentComponent { get; } = new CourtDocumentComponent();

        /// <summary>
        /// DocumentTypeFacetComponent
        /// </summary>
        public DocumentTypeFacetOptionComponent DocumentTypeOptionFacet { get; } = new DocumentTypeFacetOptionComponent();

        /// <summary>
        /// Gets the result list.
        /// </summary>
        public new ISearchResultList<ResultListItem> ResultList =>
            new Components.ResultList.CanadaSearchResultList<ResultListItem>(
                DriverExtensions.WaitForElement(this.SearchResultsListLocator));

        /// <summary>
        /// Gets the search results list locator.
        /// </summary>
        protected override By SearchResultsListLocator => By.XPath("//ol[@class='co_searchResult_list']");

        /// <summary>
        /// Result Header Label
        /// </summary>
        public ILabel SearchHeader(ContentType contentType) => new Label(By.Id(string.Format(this.containerLocatorMask, this.ContentTypeMap[contentType].SearchResultsLocatorString)), SearchHeaderLocator);

        /// <summary>
        /// View all link
        /// </summary>
        public ILink ViewAllLink(ContentType contentType) => new Link(By.Id(string.Format(this.containerLocatorMask, this.ContentTypeMap[contentType].SearchResultsLocatorString)), ViewAllLinkLocator);

        /// <summary>
        /// Get list of Search Headers
        /// </summary>
        /// <returns> List of search headers</returns>
        public List<string> GetSearchHeadersList()
        {
            List<IWebElement> headers = DriverExtensions.GetElements(SearchHeaderLocator).ToList();
            return headers.Select(el => el.Text).ToList();
        }

        /// <summary>
        /// Gets the search results page title/heading
        /// </summary>
        /// <returns>the search results page title/heading</returns>
        public new string GetPageHeading()
            => DriverExtensions.WaitForElementDisplayed(PageHeadingLocator) != null ? DriverExtensions.GetText(PageHeadingLocator) : string.Empty;
    }

    }
