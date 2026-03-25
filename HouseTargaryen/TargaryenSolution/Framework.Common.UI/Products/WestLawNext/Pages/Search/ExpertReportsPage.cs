namespace Framework.Common.UI.Products.WestLawNext.Pages.Search
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Expert Reports page
    /// </summary>
    public class ExpertReportsPage : CommonAdvancedSearchPage
    {
        private static readonly By SearchTopButtonLocator = By.Id("co_search_advancedSearchButton_top");

        /// <summary>
        /// Clicks search button
        /// </summary>
        /// <returns>ICreatablePageObject instance</returns>
        public T ClickSearchButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(SearchTopButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
