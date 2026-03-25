namespace Framework.Common.UI.Products.WestLawNext.Pages.InvestextReport
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Page to search a company in InvestextReport Page
    /// </summary>
    public class InvestextSearchPage : CommonAdvancedSearchPage
    {
        private static readonly By ReportNumberTextBoxLocator = By.Id("co_search_advancedSearch_NUM");
        private static readonly By SearchButtonLocator = By.Id("searchButton");
        private static readonly By TickerSymbolTextBoxLocator = By.Id("co_search_advancedSearch_TS");

        /// <summary>
        /// The search by report number.
        /// </summary>
        /// <param name="reportNumber">
        /// The text for search.
        /// </param>
        /// <typeparam name="T">Page Object</typeparam>
        /// <returns>
        /// The type
        /// </returns>
        public T SearchByReportNumber<T>(string reportNumber) where T : ICreatablePageObject
        {
            DriverExtensions.SetTextField(reportNumber, ReportNumberTextBoxLocator);
            DriverExtensions.WaitForTextInElement(
                string.Format("advanced: NA({0}) & RT((CR IR TR))", reportNumber), SearchButtonLocator); 
            DriverExtensions.WaitForElement(SearchButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// The search by Ticker Symbol.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
        public T SearchByTickerSymbol<T>(string text) where T : ICreatablePageObject
        {
            DriverExtensions.SetTextField(text, TickerSymbolTextBoxLocator);
            DriverExtensions.WaitForElement(SearchButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
