namespace Framework.Common.UI.Products.WestlawEdge.Pages.CompanyInvestigator
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Indigo company investigator page
    /// </summary>
    public class EdgeCompanyInvestigatorPage : EdgeCommonBrowsePage
    {
        private static readonly By BusinessNameFieldLocator = By.Id("co_search_advancedSearch_NA");

        private static readonly By SearchTopButtonLocator = By.Id("co_search_advancedSearchButton_top");

        private static readonly By ResultTitleLocator 
            = By.XPath("//h3[./a[contains(@id,'cobalt_result_businessinvestigator_title1')]]");
        
        /// <summary>
        /// Performs search filling business name field 
        /// </summary>
        /// <param name="businessName">business name.</param>
        /// <returns>ICreatablePageObject instance</returns>
        public T SearchByBusinessName<T>(string businessName) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(BusinessNameFieldLocator).SetTextField(businessName);
            return this.ClickSearchButton<T>();
        }

        /// <summary>
        /// Clicks search button
        /// </summary>
        /// <returns>ICreatablePageObject instance</returns>
        public T ClickSearchButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(SearchTopButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Checks if results are found
        /// </summary>
        /// <returns>true if results are found</returns>
        public bool IsAnySavedClientResultDisplayed() => DriverExtensions.IsDisplayed(ResultTitleLocator);
    }
}
