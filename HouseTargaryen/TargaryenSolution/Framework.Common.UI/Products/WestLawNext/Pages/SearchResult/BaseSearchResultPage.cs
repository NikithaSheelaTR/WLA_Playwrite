namespace Framework.Common.UI.Products.WestLawNext.Pages.SearchResult
{
    using Framework.Common.UI.Products.Shared.Components.SelectAll;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// BaseSearchResultPage
    /// </summary>
    public abstract class BaseSearchResultPage : CommonAuthenticatedWestlawNextPage
    {
        private static readonly By SearchFindErrorLocator = By.Id("co_infoBox_searchFindErrors");

        private static readonly By SearchErrorLocator = By.Id("co_infoBox_searchErrors");

        private static readonly By SelectAllComponentLocator = By.XPath("//ul[@class='co_navOptions']");

        /// <summary>
        /// Gets the select all component.
        /// </summary>
        public SelectAllComponent SelectAllComponent { get; } = new SelectAllComponent(SelectAllComponentLocator);

        /// <summary>
        /// Get page's heading (page heading is displayed under global search bar, e.g. Find Results)
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public abstract string GetPageHeading();

        /// <summary>
        /// Get Search error message
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetSearchFindErrorText() => DriverExtensions.GetText(SearchFindErrorLocator);

        /// <summary>
        /// Get Search error message
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetSearchErrorText() => DriverExtensions.GetText(SearchErrorLocator);

        /// <summary>
        /// IsSearchErrorMessageDisplayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsSearchErrorMessageDisplayed() => DriverExtensions.IsDisplayed(SearchErrorLocator);

        /// <summary>
        /// IsSearchFindErrorMessageDisplayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsSearchFindErrorMessageDisplayed() => DriverExtensions.IsDisplayed(SearchFindErrorLocator);
    }
}