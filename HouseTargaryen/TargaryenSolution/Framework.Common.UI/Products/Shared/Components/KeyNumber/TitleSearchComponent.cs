namespace Framework.Common.UI.Products.Shared.Components.KeyNumber
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Title Search component on the Key Number page
    /// </summary>
    public class TitleSearchComponent : BaseModuleRegressionComponent
    {
        private static readonly By TitleSearchButtonLocator = By.Id("co_searchWithinWidget_searchButton");

        private static readonly By TitleSearchTextboxLocator = By.Id("coid_website_widget_versionTOCSearch_query");

        private static readonly By TitleSearchContainerLocator = By.Id("coid_website_widget_versionTOCSearch");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TitleSearchContainerLocator;

        /// <summary>
        /// Determines whether or not the Title Search field is present
        /// </summary>
        /// <returns>true if the title search field is present</returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(TitleSearchContainerLocator, 5);

        /// <summary>
        /// Performs a title search and returns the resulting page
        /// </summary>
        /// <typeparam name="T">the expected resulting page type</typeparam>
        /// <param name="titleSearchQuery">the query to perform a title search for</param>
        /// <returns> the resulting page</returns>
        public T PerformSearch<T>(string titleSearchQuery) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(TitleSearchTextboxLocator).SetTextField(titleSearchQuery);
            DriverExtensions.WaitForElement(TitleSearchButtonLocator).CustomClick();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
