namespace Framework.Common.UI.Products.Shared.Components.KeyNumber
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Search For KeyNumbers component on the Key Number page 
    /// </summary>
    public class SearchForKeyNumbersComponent : BaseModuleRegressionComponent
    {
        private static readonly By RecentSearchesLocator = By.XPath("//a[@id = 'co_keyNumberSearchLast10Link']");

        private static readonly By RecentSearchesQueryLocator =
            By.XPath(".//li//a[@role='menuitem' and @data-query]");

        private static readonly By KeyNumberSearchButtonLocator =
            By.XPath("//*[@class='co_website_widget_keyNumberSearch_searchButton co_secondaryBtn']");

        private static readonly By KeyNumberSearchTextboxLocator = By.Id("coid_website_widget_keyNumberSearch_query_0");

        private static readonly By ContainerLocator = By.Id("coid_keyNumberSearchWidgetContainer");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click on the Recent Searches button
        /// </summary>
        public void ClickRecentSearchesButton()
        {
            DriverExtensions.GetElement(RecentSearchesLocator).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Get Recent Searches queries
        /// </summary>
        /// <returns> List of recent searches </returns>
        public List<string> GetRecentSearchesQueries() =>
            DriverExtensions.GetElements(RecentSearchesQueryLocator).Select(query => query.Text).ToList();

        /// <summary>
        /// Perform Key Number Search
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="searchQuery">searchQuery</param>
        /// <returns> the resulting page</returns>
        public T PerformKeyNumberSearch<T>(string searchQuery) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementDisplayed(KeyNumberSearchTextboxLocator).SetTextField(searchQuery);
            DriverExtensions.WaitForElement(KeyNumberSearchButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}