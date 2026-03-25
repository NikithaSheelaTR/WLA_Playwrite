namespace Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenterPowerSearch.SearchComponents
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Pages.BusinessLawCenterPowerSearch;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The base search section.
    /// </summary>
    /// <typeparam name="TReturnPage"> Type of page returns after search button click
    /// </typeparam>
    public abstract class BaseSearchComponent<TReturnPage> : BaseSearchButtonComponent<TReturnPage>
        where TReturnPage : BaseModuleRegressionPage
    {
        private static readonly By CompanyNameTextBoxLocator = By.Id("companyName");

        private static readonly By CompanySearchTabLocator = By.XPath("//ul[@class='searchTabs']//a[contains(.,'Company Search')]");

        private static readonly By CompanySearchTabActiveLocator = By.XPath("//li[@class='active']/a[contains(.,'Company Search')]");

        private static readonly By DropdownlistForHeadquartersLocator = By.ClassName("ng-binding");

        private static readonly By FullTextSearchTabLocator =
            By.XPath("//ul[@class='searchTabs']//a[contains(.,'Full Text Search')]");

        private static readonly By FullTextSearchTabActiveLocator =
            By.XPath("//li[@class='active']/a[contains(.,'Full Text Search')]");

        private static readonly By LocationOfHqTextBoxLocator =
            By.XPath("//div[@search-service='headquartersSearchService']//input[@id='location']");

        private static readonly By TickerSymbolTextBoxLocator = By.Id("ticker");

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSearchComponent{TReturnPage}"/> class.
        /// </summary>
        protected BaseSearchComponent()
        {
            DriverExtensions.WaitForElementDisplayed(TickerSymbolTextBoxLocator);
            DriverExtensions.WaitForElementDisplayed(CompanyNameTextBoxLocator);
        }

        /// <summary>
        /// Click on the Company Search tab
        /// </summary>
        /// <returns>CompanySearchPage</returns>
        public CompanySearchPage ClickOnCompanySearchTab()
        {
            if (DriverExtensions.IsDisplayed(CompanySearchTabActiveLocator))
            {
                DriverExtensions.WaitForElement(CompanySearchTabLocator).Click();
            }

            DriverExtensions.WaitForPageLoad();
            return new CompanySearchPage();
        }

        /// <summary>
        /// Click on the Full Text Search tab
        /// </summary>
        /// <returns>the page of FullTextSearchPage</returns>
        public FullTextSearchPage ClickOnFullTextSearchTab()
        {
            if (!DriverExtensions.IsDisplayed(FullTextSearchTabActiveLocator))
            {
                DriverExtensions.WaitForElement(FullTextSearchTabLocator).Click();
                DriverExtensions.WaitForPageLoad();
            }

            return new FullTextSearchPage();
        }

        /// <summary>
        /// Types specified text into the Company Name text box
        /// </summary>
        /// <param name="contentToSearch">Content to search</param>
        public void TypeCompanyName(string contentToSearch) => DriverExtensions.SetTextField(contentToSearch, CompanyNameTextBoxLocator);

        /// <summary>
        /// The type location of headquarters.
        /// </summary>
        /// <param name="nameToSearch">The name to search.</param>
        public void TypeLocationOfHeadquarters(string nameToSearch)
        {
            IWebElement locationOfHqTextElement = DriverExtensions.WaitForElement(LocationOfHqTextBoxLocator);
            locationOfHqTextElement.Clear();
            locationOfHqTextElement.Click();

            foreach (char c in nameToSearch)
            {
                locationOfHqTextElement.SendKeys(c.Equals('(') ? Keys.Shift + "9" : c.ToString());
            }

            IReadOnlyCollection<IWebElement> dropdownElements =
                DriverExtensions.GetElements(DropdownlistForHeadquartersLocator);
            dropdownElements.First(el => el.Text.Equals(nameToSearch)).Click();
        }

        /// <summary>
        /// The type ticker symbol.
        /// </summary>
        /// <param name="tickerSymbolToSearch">The ticker symbol to search.</param>
        public void TypeTickerSymbol(string tickerSymbolToSearch)
        {
            DriverExtensions.WaitForElement(TickerSymbolTextBoxLocator);
            DriverExtensions.SetTextField(tickerSymbolToSearch, TickerSymbolTextBoxLocator);
        }
    }
}