namespace Framework.Common.UI.Products.WestLawNext.Pages.CompanyInvestigator
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    ///  ExperianAstPage
    /// </summary>
    public class ExperianAstPage : BaseModuleRegressionPage
    {

        private static readonly By CompanyNameLocator = By.Id("co_search_advancedSearch_CN");

        private static readonly By ErrorMessageLocator = By.Id("co_search_advancedSearch_errorList");

        private static readonly By PageHeaderLocator = By.Id("co_browsePageLabel");

        private static readonly By SearchButtonLocator = By.Id("co_search_advancedSearchButton_top");

        private static readonly By StateLocator = By.Id("co_search_advancedSearch_ST");
        
        /// <summary>
        /// clicks the Search button on the Experian Advance Search Template 
        /// </summary>
        /// <typeparam name="T">
        /// ICreatablePageObject
        /// </typeparam>
        /// <returns>
        /// the type
        /// </returns>
        public T ClickSearch<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(SearchButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// checks if Exprerian Business Credit Reports string is available
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetTextExperianAstHeading() =>
            DriverExtensions.WaitForElement(PageHeaderLocator).Text; 

        /// <summary>
        /// Gets the error text
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetErrorMessage() => DriverExtensions.WaitForElement(ErrorMessageLocator).Text;

        /// <summary>
        /// Addes Company to textbox, selects the provided report type and click search  
        /// </summary>
        /// <param name="companyName">
        /// The company Name.
        /// </param>
        /// <typeparam name="T">ICreatablePageObject</typeparam>
        /// <returns> Page Object</returns>
        public T SearchByCompanyName<T>(string companyName) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(CompanyNameLocator).SendKeys(companyName);
            return this.ClickSearch<T>();
        }

        /// <summary>
        /// Selects provided State
        /// </summary>
        /// <param name="state">
        /// The state.
        /// </param>
        public void SelectState(string state) => 
            new SelectElement(DriverExtensions.WaitForElement(StateLocator)).SelectByText(state);
    }
}