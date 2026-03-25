namespace Framework.Common.UI.Products.WestlawEdge.Pages.SearchResult
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Edge Key Number search result page
    /// </summary>
    public class EdgeKeyNumberSearchResultPage : EdgeCommonAuthenticatedWestlawNextPage
    {
        private static readonly By KeyNumberResultItemLocator = By.ClassName("co_keyNumberResultItemLink");

        /// <summary>
        /// Get Key Number Result elements
        /// </summary>
        /// <returns>Collection of elements</returns>
        public IReadOnlyCollection<IWebElement> GetKeyNumberResults() => DriverExtensions.GetElements(KeyNumberResultItemLocator);

        /// <summary>
        /// Clicks random key number search result
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>new page instance</returns>
        public T ClickKeyNumberRandomResultItem<T>() where T : ICreatablePageObject
        {
            this.GetKeyNumberResults().ElementAt(new Random().Next(0, this.GetKeyNumberResults().Count - 1)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
