namespace Framework.Common.UI.Products.WestLawNext.Pages.SearchResult
{
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;

    /// <summary>
    /// CategorySearchResultPage
    /// </summary>
    public class CategorySearchResultPage : BaseCategorySearchResultPage<ResultListItem>
    {
        private static readonly By PageHeaderLocator = By.XPath("//*[@class='co_search_titleCount']|//*[@class='co_search_result_heading_content']/h1");

        /// <summary>
        /// Get page's heading (page heading is displayed under global search bar, e.g. Find Results)
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string GetPageHeading() => DriverExtensions.WaitForElement(PageHeaderLocator).GetText();
    }
}