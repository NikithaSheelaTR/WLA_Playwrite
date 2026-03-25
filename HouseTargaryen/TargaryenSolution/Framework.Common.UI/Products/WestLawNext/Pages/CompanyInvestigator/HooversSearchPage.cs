namespace Framework.Common.UI.Products.WestLawNext.Pages.CompanyInvestigator
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Page to search a company on Hoover AST search page
    /// </summary>
    public class HooversSearchPage : BaseModuleRegressionPage
    {
        private const string InvalidSearchMessage = "Please enter a query.";

        private static readonly By CompanyNameTextFieldLocator = By.XPath("//*[@id='co_search_advancedSearch_CN']");

        private static readonly By HooversPageLabelLocator = By.XPath("//*[@id='co_browsePageLabel']");

        private static readonly By SearchButtonLocator = By.XPath("//*[@id='searchButton']");

        /// <summary>
        /// Determines if AST navigates to hoovers page.
        /// </summary>
        /// <returns>True if contains hoover`s heading</returns>
        public string GetHooversHeading() => DriverExtensions.WaitForElementDisplayed(HooversPageLabelLocator).Text;

        /// <summary>
        /// Checks if seach is invalid
        /// It is invalid with empty string 
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>True if search is valid</returns>
        public bool IsSearchInvalid(string query) =>
            string.IsNullOrWhiteSpace(query) & DriverExtensions.IsTextOnPage(InvalidSearchMessage);

        /// <summary>
        /// Searches company by name and matches the text from global search text area and AST's company name text area.
        /// If text doesn't match, redo it.
        /// </summary>
        /// <param name="companyName">Name of the company.</param>
        /// <returns>ICreatablePageObject instance</returns>
        public T SearchByCompanyName<T>(string companyName) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(CompanyNameTextFieldLocator).SendKeysSlow(companyName);
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForElementDisplayed(SearchButtonLocator).Click();
            DriverExtensions.WaitForPageLoad();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}