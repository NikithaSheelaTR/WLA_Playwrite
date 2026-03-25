namespace Framework.Common.UI.Products.GovernmentWeblinks.Pages.Search
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.GovernmentWeblinks.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The page for search by field
    /// </summary>
    public class FieldSearchPage : BaseWeblinksSearchPage, IWeblinksSearchPage
    {
        private static readonly By CourtSelectorLocator = By.Id("Select1");

        private static readonly By DocketNumberTextboxLocator = By.XPath("//div[@class='co_column oneColumn']/input[@id='docket']");

        private static readonly By TitleOrCaseNameTextboxLocator = By.XPath("//div[@class='co_column oneColumn']/input[@id='Text1']");

        private static readonly By SearchTermsTextboxLocator = By.XPath("//div[@class='co_column oneColumn']/input[@id='querytext']");

        /// <summary>
        /// Gets list of queries
        /// </summary>
        /// <returns>The list of string. Query</returns>
        public List<string> GetQuery() => new List<string>
                                              {
            this.GetTextFromTextarea(CourtSelectorLocator),
            this.GetTextFromTextarea(DocketNumberTextboxLocator),
            this.GetTextFromTextarea(TitleOrCaseNameTextboxLocator),
            this.GetTextFromTextarea(SearchTermsTextboxLocator)
        };

        /// <summary>
        /// Search any results
        /// </summary>
        /// <typeparam name="T">The type of a page</typeparam>
        /// <param name="query">The query for search</param>
        /// <returns>The instance of a page</returns>
        public T Search<T>(List<string> query) where T : ICreatablePageObject
        {
            DriverExtensions.SelectElementInListByText(CourtSelectorLocator, query[0]);
            DriverExtensions.SetTextField(query[1], DocketNumberTextboxLocator);
            DriverExtensions.SetTextField(query[2], TitleOrCaseNameTextboxLocator);
            DriverExtensions.SetTextField(query[3], SearchTermsTextboxLocator);
            return this.ClickSearchButton<T>();
        }
    }
}
