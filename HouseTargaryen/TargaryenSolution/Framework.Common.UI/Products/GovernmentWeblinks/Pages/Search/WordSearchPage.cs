namespace Framework.Common.UI.Products.GovernmentWeblinks.Pages.Search
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.GovernmentWeblinks.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The page for search by word
    /// </summary>
    public class WordSearchPage : BaseWeblinksSearchPage, IWeblinksSearchPage
    {
        private static readonly By SearchTermsTitleLocator = By.XPath("//textarea[@id='querytext']/preceding-sibling::label");

        private static readonly By TextboxLocator = By.XPath("//div[@class='co_column oneColumn']/textarea[@id='querytext']");

        /// <summary>
        /// Gets list of queries
        /// </summary>
        /// <returns>The list of string. Query</returns>
        public List<string> GetQuery() => new List<string>
                                              {
            this.GetTextFromTextarea(TextboxLocator)
        };

        /// <summary>
        /// Search any results
        /// </summary>
        /// <typeparam name="T">The type of a page</typeparam>
        /// <param name="query">The query for search</param>
        /// <returns>The instance of a page</returns>
        public T Search<T>(List<string> query) where T : ICreatablePageObject
        {
            DriverExtensions.SetTextField(query[0], TextboxLocator);
            return this.ClickSearchButton<T>();
        }

        /// <summary>
        /// Get Search Terms Title
        /// </summary>
        /// <returns>search terms title</returns>
        public string GetSearchTermsTitle() =>
            DriverExtensions.GetElement(SearchTermsTitleLocator).Text;
    }
}
