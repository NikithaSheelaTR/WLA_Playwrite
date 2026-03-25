namespace Framework.Common.UI.Products.GovernmentWeblinks.Pages.Search
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.GovernmentWeblinks.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The page for search by party name
    /// </summary>
    public class PartyNameSearchPage : BaseWeblinksSearchPage, IWeblinksSearchPage
    {
        private static readonly By FirstTextboxLocator = By.Id("Text1");

        private static readonly By SecondTextboxLocator = By.Id("Text2");

        /// <summary>
        /// Gets list of queries
        /// </summary>
        /// <returns>The list of string. Query</returns>
        public List<string> GetQuery() => new List<string>
                                              {
            this.GetTextFromTextarea(FirstTextboxLocator),
            this.GetTextFromTextarea(SecondTextboxLocator)
        };

        /// <summary>
        /// Search any results
        /// </summary>
        /// <typeparam name="T">The type of a page</typeparam>
        /// <param name="query">The query for search</param>
        /// <returns>The instance of a page</returns>
        public T Search<T>(List<string> query) where T : ICreatablePageObject
        {
            DriverExtensions.SetTextField(query[0], FirstTextboxLocator);
            DriverExtensions.SetTextField(query[1], SecondTextboxLocator);
            return this.ClickSearchButton<T>();
        }
    }
}
