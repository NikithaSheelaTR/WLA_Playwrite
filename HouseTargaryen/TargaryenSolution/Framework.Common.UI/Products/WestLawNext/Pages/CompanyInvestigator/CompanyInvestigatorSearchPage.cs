namespace Framework.Common.UI.Products.WestLawNext.Pages.CompanyInvestigator
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums.Reports;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Page to search a company
    /// </summary>
    public class CompanyInvestigatorSearchPage : CommonAdvancedSearchPage
    {
        private static readonly By BusinessNameLocator = By.Id("co_search_advancedSearch_NA");

        private static readonly By DunsNumberLocator = By.Id("co_search_advancedSearch_DUNS");

        private static readonly By TickerOrSymbolLocator = By.Id("co_search_advancedSearch_TS");

        private static readonly By FilingDateLocator = By.Id("co_search_advancedSearch_FD");

        private static readonly By SearchButtonLocator = By.Id("co_search_advancedSearchButton_bottom");

        /// <summary>
        /// Clicks the dun and bradstreet.
        /// </summary>
        /// <param name="report">OtherBusinessReport type</param>
        /// <typeparam name="T">CreatablePageObject</typeparam>
        /// <returns>
        /// The type
        /// </returns>
        public T ClickOtherBusinessReport<T>(OtherBusinessReports report) where T : ICreatablePageObject 
            => this.ClickLinkByText<T>(report.GetEnumTextValue());

        /// <summary>
        /// Search a company by Business Name
        /// </summary>
        /// <param name="businessName">Businees name</param>
        /// <typeparam name="T">Page Object</typeparam>
        /// <returns>
        /// The type
        /// </returns>
        public T SearchByBusinessName<T>(string businessName)
            where T : ICreatablePageObject => this.SearchCompany<T>(businessName, BusinessNameLocator);

        /// <summary>
        /// The search by duns number.
        /// </summary>
        /// <param name="dunsNumber">
        /// The duns number.
        /// </param>
        /// <typeparam name="T">Page Object</typeparam>
        /// <returns>
        /// The type
        /// </returns>
        public T SearchByDunsNumber<T>(string dunsNumber)
            where T : ICreatablePageObject => this.SearchCompany<T>(dunsNumber, DunsNumberLocator);

        /// <summary>
        /// The search by Ticker/Symbol.
        /// </summary>
        /// <param name="tickerOrSymbol">
        /// Ticker or symbol.
        /// </param>
        /// <typeparam name="T">Page Object</typeparam>
        /// <returns>
        /// The type
        /// </returns>
        public T SearchByTickerOrSymbol<T>(string tickerOrSymbol)
            where T : ICreatablePageObject => this.SearchCompany<T>(tickerOrSymbol, TickerOrSymbolLocator);

        /// <summary>
        /// The search by duns number.
        /// </summary>
        /// <param name="searchText">
        /// The text for search.
        /// </param>
        /// <param name="textboxLocator">
        /// The locator of textbox
        /// </param>
        /// <typeparam name="T">Page Object</typeparam>
        /// <returns>
        /// The type
        /// </returns>
        private T SearchCompany<T>(string searchText, By textboxLocator) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementDisplayed(FilingDateLocator);
            DriverExtensions.ScrollTo(textboxLocator);
            DriverExtensions.SetTextField(searchText, textboxLocator);
            DriverExtensions.ScrollTo(SearchButtonLocator);
            DriverExtensions.WaitForElement(SearchButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}