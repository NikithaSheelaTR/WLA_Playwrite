namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsHomePageComponents.Entities
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Base Search TabComponent
    /// </summary>
    public abstract class LitigationAnalyticsBaseEntityTabComponent : BaseTabComponent
    {
        private static readonly By SearchInputLocator = By.XPath("//div[@class = 'co_textarea']/input");
        private static readonly By SearchButtonLocator = By.Id("la_searchButton");
        private static readonly By TypeaheadLocator = By.Id("contentTypeDetailsContainer");

        /// <summary>
        /// Search Button
        /// </summary>
        public IButton SearchButton => new Button(SearchButtonLocator);

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
            DriverExtensions.WaitForElementDisplayed(TypeaheadLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}