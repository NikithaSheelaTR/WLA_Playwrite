namespace Framework.Common.UI.Products.WestLawNext.Pages.SearchResult.ContentTypeSearchResultPages
{
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// ContentTypeSearchResultsPage
    /// </summary>
    public class ContentTypeSearchResultsPage : BaseContentTypeSearchResultPage<ResultListItem>
    {
        /// <summary>
        /// The Narrow Pane (left side of search results page)
        /// </summary>
        public NarrowPaneComponent NarrowPaneCompanent { get; } = new NarrowPaneComponent();

        /// <summary>
        /// Click link by partial text
        /// That method is needed for categories which have 'nbsp;' symbols in html markup
        /// </summary>
        /// <typeparam name="T"> Page object </typeparam>
        /// <param name="linkText"> The link text. </param>
        /// <returns> New page object </returns>
        public override T ClickLinkByText<T>(string linkText)
        {
            DriverExtensions.WaitForElement(By.PartialLinkText(linkText)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}