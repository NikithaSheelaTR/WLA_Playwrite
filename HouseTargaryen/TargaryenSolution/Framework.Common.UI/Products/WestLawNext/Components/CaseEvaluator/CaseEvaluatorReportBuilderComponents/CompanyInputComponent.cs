namespace Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator.CaseEvaluatorReportBuilderComponents
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// CaseTypeInputComponent
    /// </summary>
    public class CompanyInputComponent : CaseEvReportBuilderBaseComponent
    {
        private const string CompanyCheckboxLctMask = ".//label[contains(text(),'{0}')]";

        private static readonly By ContainerLocator = By.XPath("//div[@section='templateData.company']");

        private static readonly By CompanySearchResultsLocator = By.XPath(".//div[@ng-show='hasResults()']//input");

        private static readonly By SearchButtonLocator = By.XPath(".//input[@type ='submit']");

        private static readonly By SearchTextFieldLocator = By.XPath(".//input[@id='companySearchInput']");

        private static readonly By TickerOnlyCheckboxLocator = By.XPath(".//input[@id='co_isTickerSearch']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// click company name checkbox
        /// </summary>
        /// <param name="companyName">The company Name.</param>
        public void AddCompany(string companyName)
            => DriverExtensions.WaitForElement(DriverExtensions.WaitForElement(this.ComponentLocator), By.XPath(string.Format(CompanyCheckboxLctMask, companyName.ToUpper())))
            .Click();

        /// <summary>
        /// Search for Company
        /// </summary>
        /// <param name="company">The company.</param>
        /// <param name="tickerOnly">The ticker Only.</param>
        public void SearchAndSelectCompany(string company, bool tickerOnly = false)
        {
            company = company.ToUpper();
            this.SearchCompany(company, tickerOnly);
            this.AddCompany(company);
        }

        /// <summary>
        /// Select Company 
        /// </summary>
        /// <param name="company">The company.</param>
        /// <param name="tickerOnly">The ticker Only.</param>
        public void SearchCompany(string company, bool tickerOnly = false)
        {
            if (tickerOnly)
            {
                DriverExtensions.WaitForElement(TickerOnlyCheckboxLocator).Click();
            }

            DriverExtensions.SetTextField(company, SearchTextFieldLocator);
            DriverExtensions.GetElement(this.ComponentLocator, SearchButtonLocator).Click();
            DriverExtensions.WaitForElement(DriverExtensions.WaitForElement(this.ComponentLocator), CompanySearchResultsLocator);
        }
    }
}