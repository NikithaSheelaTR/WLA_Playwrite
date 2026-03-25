namespace Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenterPowerSearch.ContentComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.WestLawNext.Pages.BusinessLawCenterPowerSearch;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The company search results section.
    /// </summary>
    public class CompanySearchResultsComponent : BaseResultsComponent
    {
        private static readonly By ActiveFilerStatusLocator = By.XPath("//li[@class='exchangeActive']");

        private static readonly By AllCompanyNameLinksLocator =
            By.XPath("//ul//a[contains(@class,'companyNameLink ng-binding')]");

        private static readonly By CompanyLinkInRowContainerLocator = By.XPath(".//a[@class='companyNameLink ng-binding']");

        private static readonly By CompanySearchResultRowLocator =
            By.XPath("//*[@id='rightPane']//div[@class='listItems ng-scope']");

        private static readonly By InactiveFilerStatusLocator = By.XPath("//li[@class='exchangeInactive']");

        private static readonly By ErrorMessageSorryNoResultsLocator =
            By.XPath("//div[contains(@class,'searchResults')]/div[@class='errorMessageContainer']/span[text()='Sorry your search did not return any results']");

        private static readonly By RowWithActiveItemLocator = By.XPath("//li[@class='exchangeActive']/..");

        private static readonly By SearchResultColumnHeadersLocator = By.XPath("//ul[@class='resultHeader']/li");

        private static readonly By SearchResultContainerLocator = By.XPath("//div[@class='searchResults fourCol']");

        private static readonly By SearchResultHeaderLocator = By.XPath("//h2[contains(.,'Company Search Results')]");

        private static readonly By TickerInRowContainerLocator = By.XPath(".//span[@class='exchange ng-binding']");

        private static readonly By ContainerLocator = By.ClassName("listItems");

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanySearchResultsComponent"/> class.
        /// </summary>
        public CompanySearchResultsComponent()
        {
            DriverExtensions.WaitForElementDisplayed(SearchResultHeaderLocator);
            DriverExtensions.WaitForElementDisplayed(SearchResultContainerLocator);
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// To click on the company name which is having all CIK,TIEX and ACTIVE.
        /// </summary>
        /// <param name="companyNameToSelect"> The company Name To Select. </param>
        /// <returns> The <see cref="CompanyFilingsPage"/>. </returns>
        public CompanyFilingsPage ClickOnCompanyNameLink(string companyNameToSelect)
        {
            var companyList = new List<IWebElement>();

            while (!companyList.Any())
            {
                companyList = this.GetListOfCompaniesWithTickerOnPage();
                List<IWebElement> desiredCompany = companyList.Where(el => string.Equals(
                        DriverExtensions.GetElement(el, CompanyLinkInRowContainerLocator).Text,
                        companyNameToSelect,
                        StringComparison.InvariantCultureIgnoreCase)).ToList();

                if (!desiredCompany.Any() && DriverExtensions.IsDisplayed(NextArrowLocator, 5))
                {
                    DriverExtensions.WaitForElement(NextArrowLocator).Click();
                }
                else
                {
                    DriverExtensions.GetElement(desiredCompany.First(), CompanyLinkInRowContainerLocator).Click();
                    break;
                }
            }

            return new CompanyFilingsPage();
        }

        /// <summary>
        /// The click on first active company name link.
        /// </summary>
        /// <returns> The <see cref="CompanyFilingsPage"/>. </returns>
        public CompanyFilingsPage ClickOnFirstActiveCompanyNameLink()
        {
            var companyList = new List<IWebElement>();

            while (!companyList.Any())
            {
                companyList = this.GetListOfCompaniesWithTickerOnPage();

                if (!companyList.Any() && DriverExtensions.IsDisplayed(NextArrowLocator, 5))
                {
                    DriverExtensions.WaitForElement(NextArrowLocator).Click();
                }
                else
                {
                    DriverExtensions.GetElement(companyList.First(), CompanyLinkInRowContainerLocator).Click();
                    break;
                }
            }

            return new CompanyFilingsPage();
        }

        /// <summary>
        /// The get all active companies count.
        /// </summary>
        /// <returns> The <see cref="int"/>. </returns>
        public int GetAllActiveCompaniesCount()
        {
            DriverExtensions.WaitForPageLoad();
            return DriverExtensions.GetElements(ActiveFilerStatusLocator).Count;
        }

        /// <summary>
        /// The get all inactive companies count.
        /// </summary>
        /// <returns> The <see cref="int"/>. </returns>
        public int GetAllInactiveCompaniesCount()
        {
            DriverExtensions.WaitForPageLoad();
            return DriverExtensions.GetElements(InactiveFilerStatusLocator).Count;
        }

        /// <summary>
        /// Is company ticker pair in result list.
        /// </summary>
        /// <param name="name"> The name. </param>
        /// <param name="ticker"> The ticker. </param>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsCompanyTickerPairInResultList(string name, string ticker)
        {
            DriverExtensions.WaitForElement(CompanySearchResultRowLocator);
            IList<IWebElement> activeCompanyWithRequiredTickerAndRequiredNameList =
                DriverExtensions.GetElements(RowWithActiveItemLocator)
                                .Where(
                                    el =>
                                        DriverExtensions.GetElement(el, TickerInRowContainerLocator)
                                                        .Text.Contains(
                                                            ticker,
                                                            StringComparison.InvariantCultureIgnoreCase))
                                .Where(
                                    el =>
                                        DriverExtensions.GetElement(el, CompanyLinkInRowContainerLocator)
                                                        .Text.Contains(
                                                            name,
                                                            StringComparison.InvariantCultureIgnoreCase))
                                .ToList();
            return activeCompanyWithRequiredTickerAndRequiredNameList.Any();
        }

        /// <summary>
        /// Is error message sorry no results displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsErrorMessageSorryNoResultsDisplayed()
            => DriverExtensions.WaitForElementDisplayed(ErrorMessageSorryNoResultsLocator).Displayed;

        /// <summary>
        /// The verify company name search returns valid results.
        /// </summary>
        /// <param name="searchedCompanyName"> The searched company name. </param>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool VerifyCompanyNameSearchReturnsValidResults(string searchedCompanyName)
        {
            DriverExtensions.WaitForElement(CompanySearchResultRowLocator);

            return DriverExtensions.GetElements(AllCompanyNameLinksLocator)
                .Any(el => el.Text.Contains(searchedCompanyName, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// The verify company search results header columns display.
        /// </summary>
        /// <param name="expectedColumnsNames">The expected Columns Names.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool VerifyCompanySearchResultsHeaderColumnsDisplay(string[] expectedColumnsNames)
        {
            DriverExtensions.WaitForElementDisplayed(SearchResultColumnHeadersLocator);

            ICollection<string> actualColumnsNames =
                DriverExtensions.GetElements(SearchResultColumnHeadersLocator).Select(el => el.Text).ToList();

            return actualColumnsNames.CollectionEquals(expectedColumnsNames);
        }

        /// <summary>
        /// Get list of companies with ticket on page
        /// </summary>
        /// <returns>List of IWebElements</returns>
        private List<IWebElement> GetListOfCompaniesWithTickerOnPage()
        {
            DriverExtensions.WaitForElement(CompanySearchResultRowLocator);
            return DriverExtensions.GetElements(RowWithActiveItemLocator)
                .Where(el => !string.IsNullOrEmpty(DriverExtensions.GetElement(el, TickerInRowContainerLocator).Text))
                .ToList();
        }
    }
}