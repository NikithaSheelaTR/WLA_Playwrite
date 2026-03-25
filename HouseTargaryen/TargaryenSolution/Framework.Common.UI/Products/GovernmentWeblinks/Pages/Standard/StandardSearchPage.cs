namespace Framework.Common.UI.Products.GovernmentWeblinks.Pages.Standard
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.GovernmentWeblinks.Components;
    using Framework.Common.UI.Products.GovernmentWeblinks.Interfaces;
    using Framework.Common.UI.Products.GovernmentWeblinks.Pages.Search;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The standard page for search
    /// </summary>
    public class StandardSearchPage : BaseWeblinksSearchPage, IWeblinksSearchPage
    {
        private static readonly By TextboxLocator = By.XPath("//textarea[@name='t_querytext']");

        private static readonly By SearchForWordsLocator = By.LinkText("Search for Words");

        /// <summary>
        /// Header component
        /// </summary>
        public new WeblinksStandardHeaderComponent Header { get; } = new WeblinksStandardHeaderComponent();

        /// <summary>
        /// Gets list of queries
        /// </summary>
        /// <returns>The list of string. Query</returns>
        public List<string> GetQuery() => new List<string>
                                              {
            this.GetTextFromTextarea(TextboxLocator)
        };

        /// <summary>
        /// Verifies that Searchforwords link is displayed
        /// </summary>
        /// <returns> Returns true if search for words link is currently displayed on screen </returns>
        public bool IsSearchForWordsLinkDisplayed() => DriverExtensions.IsDisplayed(SearchForWordsLocator);

        ///<summary>
        /// Clicks if Search for words link is displayed
        ///</summary>
        ///<returns> The instance of a page</returns>
        public T ClickOnSearchForWordsLinkIfDisplayed<T>() where T : ICreatablePageObject
        {
            if (IsSearchForWordsLinkDisplayed())
            {
                DriverExtensions.Click(SearchForWordsLocator);
            }

            return DriverExtensions.CreatePageInstance<T>();
        }

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
    }
}
