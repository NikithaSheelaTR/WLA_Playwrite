namespace Framework.Common.UI.Products.WestLawNext.Pages.Search
{
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Products.WestLawNext.Pages.SearchResult;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Content finder page
    /// </summary>
    public class ContentFinderPage : CommonAuthenticatedWestlawNextPage
    {
        private static readonly By TitleLocator = By.Id("co_search_advancedSearch_TITLE");

        private static readonly By AdvancedSearchButtonLocator = By.Id("co_search_advancedSearchButton_top");

        private static readonly By ResultListLocator = By.ClassName("co_searchResult_list");

        /// <summary>
        /// Enter title
        /// </summary>
        /// <param name="text">title</param>
        /// <returns>current page</returns>
        public ContentFinderPage EnterTitle(string text)
        {
            DriverExtensions.WaitForElementDisplayed(TitleLocator).SendKeys(text);
            return this;
        } 

        /// <summary>
        /// Click search button
        /// </summary>
        /// <returns>current page</returns>
        public ContentFinderResultListPage ClickSearch()
        {
            DriverExtensions.Click(AdvancedSearchButtonLocator);
            return new ContentFinderResultListPage(DriverExtensions.WaitForElement(ResultListLocator));
        }

        /// <summary>
        /// Verify is search button displayed
        /// </summary>
        /// <returns>true if displayed, false otherwise</returns>
        public bool IsSearchButtonDisplayed() => DriverExtensions.IsDisplayed(AdvancedSearchButtonLocator);
    }
}
