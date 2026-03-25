namespace Framework.Common.UI.Products.GovernmentWeblinks.Pages
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.GovernmentWeblinks.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The home page for sites with search
    /// </summary>
    public class SearchOnlyHomePage : WeblinksHomePage, IWeblinksSearchPage
    {
        private static readonly By HelpLinkLocator = By.XPath("//div[@class='co_subHeader']/a");

        private static readonly By NaturalLanguageLinkLocator = By.XPath("//label[@for='WIN']/a");

        private static readonly By NaturalLanguageRadioButtonLocator = By.XPath("//input[@id='WIN']");

        private static readonly By SearchButtonLocator = By.XPath("//div[@class='co_column oneColumn']/input[@class='co_formBtnGreen']");

        private static readonly By SearchTermsTextboxLocator = By.XPath("//textarea[@id='query']");

        private static readonly By TermsAndConnectorsLinkLocator = By.XPath("//label[@for='TNC']/a");

        /// <summary>
        /// Clicks help link
        /// </summary>
        /// <returns>The instance of the weblinks help page</returns>
        public WeblinksHelpPage ClickHelpLink()
        {
            DriverExtensions.WaitForElement(HelpLinkLocator).Click();
            return new WeblinksHelpPage();
        }

        /// <summary>
        /// Gets list of queries
        /// </summary>
        /// <returns>The list of string. Query</returns>
        public List<string> GetQuery() => new List<string>
                                              {
            DriverExtensions.WaitForElement(SearchTermsTextboxLocator).GetAttribute("value")
        };

        /// <summary>
        /// Verifies is help link displayed
        /// </summary>
        /// <returns>The instance of the page</returns>
        public bool IsHelpLinkDisplayed() =>
            DriverExtensions.IsDisplayed(HelpLinkLocator)
            && DriverExtensions.WaitForElement(HelpLinkLocator).GetAttribute("href").Contains("Help");

        /// <summary>
        /// Verifies is natural language link displayed
        /// </summary>
        /// <returns>The instance of the page</returns>
        public bool IsNaturalLanguageLinkDisplayed() =>
            DriverExtensions.IsDisplayed(NaturalLanguageLinkLocator)
            && DriverExtensions.WaitForElement(NaturalLanguageLinkLocator).GetAttribute("href").Contains("/Help#searchingNL");

        /// <summary>
        /// Verifies is terms and connectors link displayed
        /// </summary>
        /// <returns>The instance of the page</returns>
        public bool IsTermsAndConnectorsLinkDisplayed() =>
            DriverExtensions.IsDisplayed(TermsAndConnectorsLinkLocator)
            && DriverExtensions.WaitForElement(TermsAndConnectorsLinkLocator).GetAttribute("href").Contains("/Help#searchingT&C");

        /// <summary>
        /// Verifies is search button displayed
        /// </summary>
        /// <returns>The instance of the page</returns>
        public bool IsSearchButtonDisplayed() => DriverExtensions.IsDisplayed(SearchButtonLocator);

        /// <summary>
        /// Verifies is textbox displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsTextboxDisplayed() => DriverExtensions.IsDisplayed(SearchTermsTextboxLocator, 5);

        /// <summary>
        /// Search any results
        /// </summary>
        /// <typeparam name="T">The type of a page</typeparam>
        /// <param name="query">The query for search</param>
        /// <returns>The instance of a page</returns>
        public T Search<T>(List<string> query) where T : ICreatablePageObject
        {
            this.EnterSearchTerms(query[0]);
            return this.ClickSearchButton<T>();
        }

        /// <summary>
        /// Select natural language radio button
        /// </summary>
        public void SelectNaturalLanguage() => DriverExtensions.WaitForElement(NaturalLanguageRadioButtonLocator).Click();

        /// <summary>
        /// Clicks on the search button
        /// </summary>
        /// <typeparam name="T">The type of the page</typeparam>
        /// <returns>The instance of the page</returns>
        private T ClickSearchButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(SearchButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        private void EnterSearchTerms(string query) => DriverExtensions.SetTextField(query, SearchTermsTextboxLocator);
    }
}
