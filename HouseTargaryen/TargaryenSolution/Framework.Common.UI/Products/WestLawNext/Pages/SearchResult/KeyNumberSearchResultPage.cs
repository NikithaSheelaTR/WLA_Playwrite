namespace Framework.Common.UI.Products.WestLawNext.Pages.SearchResult
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Key Number search result page
    /// </summary>
    public class KeyNumberSearchResultPage : CommonAuthenticatedWestlawNextPage
    {
        private static readonly By KeyNumberResultItemLocator = By.ClassName("co_keyNumberResultItemLink");

        private static readonly By CopyLinkLocator = By.Id("co_linkBuilder");

        private static readonly By PageHeadingLocator = By.ClassName("co_customDigestLandingPageLabel");

        private static readonly By SelectViewHeadnotesTextLocator
            = By.XPath("//*[@class='co_navItemsSelected' or @id='coid_selectContentLabel' and contains(text(),'Select content to View ')]");

        /// <summary>
        /// Gets the search results page title/heading
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetPageHeading() => DriverExtensions.GetText(PageHeadingLocator);

        /// <summary>
        /// Get Key Number Results Count
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetKeyNumberResultsCount() => this.GetKeyNumberResults().Count;

        /// <summary>
        /// Clicks key number search result links 
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="linkText">The link Text.</param>
        /// <returns>new page instance</returns>
        public T ClickKeyNumberResultItem<T>(string linkText) where T : ICreatablePageObject
        {
            this.GetKeyNumberResults().First(elem => elem.Text.Contains(linkText)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

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

        /// <summary>
        /// Get Select Content to View text
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetSelectContentToViewText() => DriverExtensions.GetText(SelectViewHeadnotesTextLocator);

        private IReadOnlyCollection<IWebElement> GetKeyNumberResults() => DriverExtensions.GetElements(KeyNumberResultItemLocator);
    }
}