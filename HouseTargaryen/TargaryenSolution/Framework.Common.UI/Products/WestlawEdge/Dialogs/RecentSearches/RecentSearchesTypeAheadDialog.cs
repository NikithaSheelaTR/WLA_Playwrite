namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.RecentSearches
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The recent searches list box dialog.
    /// </summary>
    public class RecentSearchesListBoxDialog : BaseModuleRegressionDialog
    {
        private const string RecentSearchStarIconLctMask = ".//*[text()='Saved searches']/preceding::li/input[@value='{0}']/following::label";
        private const string RemoveSearchStarIcon = "label[contains(@for, 'coid_recetQueryLitItem')]";
        private const string SavedSearchStarIcon = "input[contains(@id, 'coid_recetQueryLitItem')]";
        private const string SavedSearchStarIconLctMask = ".//*[text()='Saved searches']/following::li/input[@value='{0}']/following::{1}";
        private const string SavedSearchLctMask = ".//input[contains(@value,'{0}')]";

        private static readonly By SavedSearchLocator = By.XPath(".//*[text()='Saved searches']/following-sibling::ul/li");
        private static readonly By RecentSearchesLocator = By.XPath(".//*[text()='Saved searches']/preceding-sibling::ul/li");
        private static readonly By StarIconLocator = By.XPath("//*[contains(@class, 'co_star_')]");
        private static readonly By SearchButtonLocator = By.Id("searchButton");
        private static readonly By SearchTextLocator = By.XPath("//input[@class='co_recent_search']");
        private static readonly By RecentSearchesLinksLocator = By.XPath(".//li/*[@class='co_recent_search'] | .//li//a[@role='menuitem' and @data-query]");
        private static readonly By ContainerLocator = By.XPath("//*[@id='co_searchLast10List']");
        private static readonly By InfoIconLocator = By.XPath("//span[@class='icon25 icon_help-blueOutline']");
        private static readonly By HoverMessageLocator = By.XPath("//div[@class='a11yTooltip-content a11yTooltip--right']");

        /// <summary>
        /// Container
        /// </summary>
        private IWebElement Container => DriverExtensions.WaitForElementDisplayed(ContainerLocator);

        /// <summary>
        /// Returns true if search query is saved to favorites
        /// </summary>
        /// <param name="query">True if search is saved</param>
        /// <returns>True if search is saved</returns>
        public bool IsSearchSaved(string query)
            =>
                DriverExtensions.IsElementPresent(this.Container, By.XPath(string.Format(SavedSearchStarIconLctMask, query, SavedSearchStarIcon)))
                && DriverExtensions.WaitForElement(this.Container, By.XPath(string.Format(SavedSearchStarIconLctMask, query, SavedSearchStarIcon)))
                                   .GetAttribute("class")
                                   .Equals("co_star_on");

        /// <summary>
        /// Verify is  recently search query enabled
        /// </summary>
        /// <param name="query">Query</param>
        /// <returns>True if search query enabled</returns>
        public bool IsSearchQueryEnabled(string query) =>
            !DriverExtensions.SafeGetElement(this.Container, By.XPath(string.Format(SavedSearchLctMask, query)))
                             ?.GetAttribute("class").Contains("disabled") ?? false;

        /// <summary>
        /// Returns true if star icon is present on the page.
        /// </summary>
        /// <returns>True if favorite searches feature displayed</returns>
        public bool IsFavoriteSearchesFeatureDisplayed() => DriverExtensions.IsDisplayed(StarIconLocator, 5);

        /// <summary>
        /// Check if the info icon (i) displayed
        /// </summary>
        /// <returns>True if the info icon is displayed</returns>
        public bool IsInfoIconDiplayed() => DriverExtensions.IsDisplayed(InfoIconLocator);

        /// <summary>
        /// Check is hover message displayed
        /// </summary>
        /// <returns>
        /// True if the hover message is displayed
        /// </returns>
        public bool IsHoverMessageDisplayed()
        {
            DriverExtensions.Hover(InfoIconLocator);
            return DriverExtensions.WaitForElement(HoverMessageLocator).GetAttribute("aria-hidden").Equals("false");
        }

        /// <summary>
        /// Saves search to favorites
        /// </summary>
        /// <param name="query">Query</param>
        public void SaveSearch(string query)
        {
            if (!this.IsSearchSaved(query))
            {
                this.ClickElement(
                    DriverExtensions.WaitForElement(
                        this.Container,
                        By.XPath(string.Format(RecentSearchStarIconLctMask, query))));
            }
        }

        /// <summary>
        /// Performs search by saved search query
        /// </summary>
        /// <param name="searchQuery">Search query</param>
        /// <typeparam name="T">T</typeparam>
        /// <returns>T</returns>
        public T ClickOnSavedQuery<T>(string searchQuery)
            where T : ICreatablePageObject => this.ClickElement<T>(
            DriverExtensions.WaitForElement(this.Container, By.XPath(string.Format(SavedSearchLctMask, searchQuery))));

        /// <summary>
        /// Click on search button
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>T</returns>
        public T ClickOnSearchButton<T>()
            where T : ICreatablePageObject => this.ClickElement<T>(SearchButtonLocator);

        /// <summary>
        /// Performs search by recent search query
        /// </summary>
        /// <param name="index">The index</param>
        /// <typeparam name="T">T</typeparam>
        /// <returns>T</returns>
        public T ClickOnRecentQuery<T>(int index)
            where T : ICreatablePageObject => this.ClickElement<T>(
            DriverExtensions.GetElements(this.Container, RecentSearchesLinksLocator).ElementAt(index));

        /// <summary>
        /// Deletes search from favorites
        /// </summary>
        /// <param name="query">Query</param>
        public void DeleteSearch(string query)
        {
            if (this.IsSearchSaved(query))
            {
                this.ClickElement(this.Container, By.XPath(string.Format(SavedSearchStarIconLctMask, query, RemoveSearchStarIcon)));
            }
        }

        /// <summary>
        /// Returns a list of recent searches
        /// </summary>
        /// <returns>List of queries</returns>
        public List<string> GetAllRecentSearches() => DriverExtensions
            .GetElements(this.Container, RecentSearchesLocator)
            .Select(element => DriverExtensions.WaitForElement(element, SearchTextLocator).Text).ToList();

        /// <summary>
        /// Returns a list of saved searches
        /// </summary>
        /// <returns>List of queries</returns>
        public List<string> GetAllSavedSearches() => DriverExtensions
            .GetElements(this.Container, SavedSearchLocator)
            .Select(element => DriverExtensions.WaitForElement(element, SearchTextLocator).Text).ToList();

        /// <summary>
        /// Gets a list of the recent searches from the recent searches dropdown in the search bar
        /// </summary>
        /// <returns>List of recent searches</returns>
        public IEnumerable<string> GetRecentSearches() => DriverExtensions
            .GetElements(this.Container, RecentSearchesLinksLocator).Select(e => e.GetText()).ToList();
    }
}