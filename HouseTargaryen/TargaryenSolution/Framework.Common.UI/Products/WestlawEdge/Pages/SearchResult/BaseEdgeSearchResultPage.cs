namespace Framework.Common.UI.Products.WestlawEdge.Pages.SearchResult
{
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// EdgeBaseSearchResultPage
    /// </summary>
    public abstract class BaseEdgeSearchResultPage : EdgeCommonAuthenticatedWestlawNextPage
    {
        private static readonly By PageHeadingLocator = By.XPath("//div[@class='co_search_result_heading_content']/h1");

        /// <summary>
        /// Get page's heading (page heading is displayed under global search bar, e.g. Find Results)
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public virtual string GetPageHeadingText()
            => DriverExtensions.WaitForElement(PageHeadingLocator).GetText();
    }
}
