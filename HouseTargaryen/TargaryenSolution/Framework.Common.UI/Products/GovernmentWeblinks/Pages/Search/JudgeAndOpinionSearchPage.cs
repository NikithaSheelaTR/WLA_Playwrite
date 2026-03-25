namespace Framework.Common.UI.Products.GovernmentWeblinks.Pages.Search
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.GovernmentWeblinks.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The page for search by judge and opin name
    /// </summary>
    public class JudgeAndOpinionSearchPage : BaseWeblinksSearchPage, IWeblinksSearchPage
    {
        private static readonly By JudgeNameTextboxLocator = By.Id("jn");

        private static readonly By OpinionTypeDropdownLocator = By.Id("op");

        private static readonly By SearchTermsTextboxLocator = By.Id("querytext");

        /// <summary>
        /// Gets list of queries
        /// </summary>
        /// <returns>The list of string. Query</returns>
        public List<string> GetQuery() => new List<string>
                                              {
            this.GetTextFromTextarea(JudgeNameTextboxLocator),
            DriverExtensions.GetSelectedDropdownOptionText(OpinionTypeDropdownLocator),
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
            DriverExtensions.SetTextField(query[0], JudgeNameTextboxLocator);
            DriverExtensions.SelectElementInListByText(OpinionTypeDropdownLocator, query[1]);
            DriverExtensions.SetTextField(query[2], SearchTermsTextboxLocator);
            return this.ClickSearchButton<T>();
        }
    }
}
