namespace Framework.Common.UI.Products.WestlawEdge.Components.LegalAnalytics
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// BaseSearchTabComponent
    /// </summary>
    public abstract class BaseSearchTabComponent : BaseTabComponent
    {
        private static readonly By SearchInputLocator = By.Id("la_searchInputId");

        private static readonly By SearchButtonLocator = By.Id("la_searchButton");     

        /// <summary>
        /// Enter search query and click search button
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="searchQuery">Search query</param>
        /// <returns>New instance of the page</returns>
        public T EnterSearchQueryAndClickSearch<T>(string searchQuery)
            where T : ICreatablePageObject
        {
            DriverExtensions.SetTextField(searchQuery, SearchInputLocator);
            DriverExtensions.Click(SearchButtonLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Enter search query
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="searchQuery">Search query</param>
        /// <returns>New instance of the page</returns>
        public T EnterSearchQuery<T>(string searchQuery)
            where T : ICreatablePageObject
        {
            DriverExtensions.SetTextField(searchQuery, SearchInputLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

    }
}
