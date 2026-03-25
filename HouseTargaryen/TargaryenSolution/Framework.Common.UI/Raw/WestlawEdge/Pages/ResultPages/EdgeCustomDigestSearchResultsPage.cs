namespace Framework.Common.UI.Raw.WestlawEdge.Pages.ResultPages
{
using Framework.Common.UI.Interfaces;
using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
using Framework.Core.Utils.Extensions;

using OpenQA.Selenium;

    /// <summary>
    /// Indigo Custom Digest Search Results Page
    /// </summary>
    public class EdgeCustomDigestSearchResultsPage : EdgeCommonSearchResultPage
    {
        private static readonly By SearchTitleLocator = By.ClassName("co_search_titleCount");

        private static readonly By ChangeJurisdictionLocator = By.ClassName("co_search_changeJurisdiction");

        /// <summary>
        /// Clicks a document title link
        /// </summary>
        /// <typeparam name="T">Page we'll end up on</typeparam>
        /// <param name="documentName">Name of the document to click</param>
        /// <returns>New instance of a document page</returns>
        public T ClickDocumentLink<T>(string documentName) where T : ICreatablePageObject => this.ClickLinkByText<T>(documentName);

        /// <summary>
        /// Gets the number of search results that were returned
        /// </summary>
        /// <returns>the number of search results</returns>
        public int GetResultsCount() => DriverExtensions.WaitForElement(SearchTitleLocator).Text.RetrieveCountFromBrackets();

        /// <summary>
        /// Clicks on the Change Jurisdiction button
        /// </summary>
        /// <returns>Jurisdiction Options Dialog Component</returns>
        public T ClickChangeJurisdiction<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(ChangeJurisdictionLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
