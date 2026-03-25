namespace Framework.Common.UI.Products.GovernmentWeblinks.Pages.Search
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.GovernmentWeblinks.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The page for search by citation
    /// </summary>
    public class CitationSearchPage : BaseWeblinksSearchPage, IWeblinksSearchPage
    {
        private static readonly By PageNumberTextboxLocator = By.XPath("//div[@class='co_column threeColumn']/input[@id='T2']");

        private static readonly By PublicationDropdownLocator = By.Id("S1");

        private static readonly By TextboxLocator = By.XPath("//div[@class='co_column threeColumn']/input[@id='T1']");

        /// <summary>
        /// Gets list of queries
        /// </summary>
        /// <returns>The list of string. Query</returns>
        public List<string> GetQuery() => new List<string>
                                              {
            this.GetTextFromTextarea(TextboxLocator),
            DriverExtensions.GetSelectedDropdownOptionText(PublicationDropdownLocator),
            this.GetTextFromTextarea(PageNumberTextboxLocator)
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
            DriverExtensions.SelectElementInListByText(PublicationDropdownLocator, query[1]);
            DriverExtensions.SetTextField(query[2], PageNumberTextboxLocator);
            return this.ClickSearchButton<T>();
        }
    }
}
