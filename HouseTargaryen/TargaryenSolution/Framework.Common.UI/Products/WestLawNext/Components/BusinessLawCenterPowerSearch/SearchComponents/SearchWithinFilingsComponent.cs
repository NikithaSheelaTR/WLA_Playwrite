namespace Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenterPowerSearch.SearchComponents
{
    using Framework.Common.UI.Products.WestLawNext.Pages.BusinessLawCenterPowerSearch;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The search within filings.
    /// </summary>
    public class SearchWithinFilingsComponent : BaseSearchButtonComponent<CompanyFilingsSearchWithinResultsPage>
    {
        private static readonly By FilingDateDropdownLocator =
            By.XPath("//li[contains(., 'Filing Date')]/div/select[@id='dateRangeSelect']");

        private static readonly By SearchCriteriaTextboxLocator = By.Id("query");

        private static readonly By SpecificDateDropdownLocator =
            By.XPath("//li[./label[contains(.,'Filing Date')]]//input[../label[contains(.,'Specific date')]]");

        /// <summary>
        /// The click search button.
        /// </summary>
        /// <returns>The <see cref="CompanyFilingsSearchWithinResultsPage"/>.</returns>
        public override CompanyFilingsSearchWithinResultsPage ClickSearchButton()
        {
            this.BaseClickSearchButton();
            return new CompanyFilingsSearchWithinResultsPage();
        }

        /// <summary>
        /// The set specific date.
        /// </summary>
        /// <param name="specificDate">The specific date.</param>
        public void SetSpecificDate(string specificDate)
        {
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.SetDropdown("Specific date", FilingDateDropdownLocator);
            DriverExtensions.WaitForElement(SpecificDateDropdownLocator).SendKeys(specificDate);
        }

        /// <summary>
        /// The type in search criteria textbox.
        /// </summary>
        /// <param name="content">The content.</param>
        public void TypeInSearchCriteriaTextbox(string content)
        {
            DriverExtensions.WaitForElement(SearchCriteriaTextboxLocator);
            DriverExtensions.SetTextField(content, SearchCriteriaTextboxLocator);
        }
    }
}