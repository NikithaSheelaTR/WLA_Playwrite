namespace Framework.Common.UI.Products.WestLawNext.Pages.CompanyInvestigator
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Pages.SearchResult.ContentTypeSearchResultPages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Page to search a company
    /// </summary>
    public class DunAndBradStreetSearchPage : BaseModuleRegressionPage
    {
        private const string SearchNotCompletedMessage = "Your search could not be completed";

        private static readonly By PageLabelLocator = By.Id("co_browsePageLabel");

        private static readonly By SearchButtonLocator = By.Id("co_search_advancedSearchButton_top");

        private static readonly By TextFieldForCompanyNameLocator = By.XPath("//*[@id='co_search_advancedSearch_CN']");

        private static readonly By TextFieldForDunsNumberLocator = By.Id("co_search_advancedSearch_DUNS");

        /// <summary>
        /// The flag that checks whether the DunAndBradStreet Heading is displayed
        /// </summary>
        /// <returns>Heading text</returns>
        public string GetPageLabelText() =>
            DriverExtensions.WaitForElementDisplayed(PageLabelLocator).Text;

        /// <summary>
        /// The flag that check whether the message for the invalid query is present on the page
        /// </summary>
        /// <param name="query">text for search</param>
        /// <returns>True if Text is on Page</returns>
        public bool IsSearchInvalid(string query) =>
            string.IsNullOrWhiteSpace(query) & DriverExtensions.IsTextOnPage(SearchNotCompletedMessage);

        /// <summary>
        /// Search company by name.
        /// </summary>
        /// <param name="companyName">Name of the company.</param>
        /// <returns>Page Instance</returns>
        public T SearchByCompanyName<T>(string companyName) where T : ICreatablePageObject
        {
            DriverExtensions.SetTextField(companyName, TextFieldForCompanyNameLocator);
            DriverExtensions.WaitForElement(SearchButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Search by DUNS number
        /// </summary>
        /// <param name="dunsNumber">DUNS number</param>
        /// <returns>The <see cref="ReportSearchResultPage"/>.</returns>
        public T SearchByDunsNumber<T>(string dunsNumber) where T : ICreatablePageObject
        {
            DriverExtensions.SetTextField(dunsNumber, TextFieldForDunsNumberLocator);
            DriverExtensions.WaitForElement(SearchButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}