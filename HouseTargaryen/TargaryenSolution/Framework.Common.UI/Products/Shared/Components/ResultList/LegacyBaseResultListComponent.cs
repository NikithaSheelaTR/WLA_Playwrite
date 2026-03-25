namespace Framework.Common.UI.Products.Shared.Components.ResultList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Base Result List Component
    /// </summary>
    public abstract class LegacyBaseResultListComponent : BaseModuleRegressionComponent
    {
        /// <summary>
        /// Locator for SearchResultsElement
        /// </summary>
        protected static readonly By SearchResultsElementLocator = By.XPath("//li[starts-with(@id, 'cobalt_search_')]");

        private const string DocGuidLctMask = "a[docguid='{0}']";

        private const string CheckboxLctMask = "//li[.//label[contains(text(),'{0}')]]/input";

        private static readonly By DocLinkLocator = By.XPath("//*[contains(@class, 'co_searchResultsList')]//h3//a | //*[contains(@class, 'co_searchResultsList')]//h2//a");

        private static readonly By DocumentTitleLocator =
            By.XPath("//div[@id='co_search_results_inner']//*//a[contains(@id, 'cobalt_result_') and contains(@id, '_title')]");

        private static readonly By MoreInfoLinkLocator = By.Id("co_moreInfoLink");

        private static readonly By InfoMessageLocator = By.XPath("//div[contains(@class,'co_searchMoreInfoTooltip')]");

        private static readonly By SearchResultCountLocator = By.ClassName("co_searchCount");

        private static readonly By NoDocumentsFoundMessageLocator = By.Id("cobalt_search_no_results");

        /// <summary>
        /// Gets the search result items count.
        /// </summary>
        public int SearchResultItemsCount
            => DriverExtensions.IsDisplayed(SearchResultsElementLocator, 5)
                    ? DriverExtensions.GetElements(SearchResultsElementLocator).Count
                    : 0;

        /// <summary>
        /// Is 'No Documents Found' message is displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsNoFoundMessageDisplayed() => DriverExtensions.IsDisplayed(NoDocumentsFoundMessageLocator);

        /// <summary>
        /// Check to see if the search results are present on the search results page
        /// </summary>
        /// <returns> True if the search results displayed </returns>
        public bool AreSearchResultsDisplayed() => DriverExtensions.IsDisplayed(SearchResultsElementLocator, 5);

        /// <summary>
        /// Determines whether or not the more info icon is displayed
        /// </summary>
        /// <returns>true if the more info icon is present and displayed/</returns>
        public bool IsMoreInfoIconDisplayed() => DriverExtensions.IsDisplayed(MoreInfoLinkLocator, 5);

        /// <summary>
        /// Gets the displayed text when you hover over the more info icon on the header
        /// </summary>
        /// <returns> Text given when you hover over the more info icon </returns>
        public string GetMoreInfoText()
        {
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForElementDisplayed(MoreInfoLinkLocator).SeleniumHover();
            DriverExtensions.WaitForElementDisplayed(InfoMessageLocator);
            return DriverExtensions.WaitForElement(InfoMessageLocator).Text.Trim();
        }

        /// <summary>
        /// Determines if a specific case is selected
        /// </summary>
        /// <param name="caseTitle"> Case to look at </param>
        /// <returns> True if selected, false otherwise </returns>
        public bool IsCheckboxSelected(string caseTitle)
            => DriverExtensions.GetElement(By.XPath(string.Format(CheckboxLctMask, caseTitle))).Selected;

        /// <summary>
        /// Determines if a specific case is selected
        /// </summary>
        /// <param name="caseTitle"> Case to look at </param>
        /// <param name="set"> Check </param>
        public void SetItemCheckbox(string caseTitle, bool set = true)
            => DriverExtensions.SetCheckbox(By.XPath(string.Format(CheckboxLctMask, caseTitle)), set);

        /// <summary>
        /// The are search results numbers displayed.
        /// </summary>
        /// <returns>
        /// True if the results are displayed<see cref="bool"/>.
        /// </returns>
        public bool AreSearchResultsNumbersDisplayed()
        {
            List<IWebElement> elements = DriverExtensions.GetElements(SearchResultCountLocator).ToList();
            return elements.Count > 0 && elements.All(element => element.Displayed);
        }

        /// <summary>
        /// Clicks on a specific document based on the result's doc GUID 
        /// </summary>
        /// <typeparam name="T"> Page Type </typeparam>
        /// <param name="guid"> The doc GUID  </param>
        /// <returns> DocumentPage from the given doc GUID </returns>
        public T ClickOnSearchResultDocumentByGuid<T>(string guid) where T : ICreatablePageObject
        {
            DriverExtensions.Click(this.GetDocumentElementByGuid(guid));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on a specific document based on the documents index
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="docIndex"> Document index to open </param>
        /// <returns> New instance of the page </returns>
        public T ClickOnSearchResultDocumentByIndex<T>(int docIndex) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(DocLinkLocator);
            DriverExtensions.GetElements(DocLinkLocator).ElementAt(docIndex).JavascriptClick();
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Return document name from search results by index
        /// </summary>
        /// <param name="documentIndex"> Document Index </param>
        /// <returns> Document name </returns>
        public string GetDocumentNameByIndex(int documentIndex) => this.GetDocumentTitleElementByIndex(documentIndex).GetText();

        /// <summary>
        /// Gets the list of all documents titles
        /// </summary>
        /// <returns>The List of titles</returns>
        public IEnumerable<string> GetListOfDocumentsTitles() => this.GetDocumentsTitleElements().Select(elem => elem.Text);

        /// <summary>
        /// Get Document Title Element to use in DragAndDropToFolder method
        /// </summary>
        /// <param name="index">Document Index</param>
        /// <returns>The <see cref="IWebElement"/>.</returns>
        internal IWebElement GetDocumentTitleElementByIndex(int index) => this.GetDocumentsTitleElements().ElementAt(index);

        /// <summary>
        /// Click on random document
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="documentsCount"> Count of document on the search result page</param>
        /// <returns> New instance of the page </returns>
        public T ClickOnRandomSearchResultDocument<T>(int documentsCount) where T : ICreatablePageObject
            => this.ClickOnSearchResultDocumentByIndex<T>(new Random().Next(documentsCount));

        /// <summary>
        /// Click on a specific document based on the documents name
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="name"> Document name </param>
        /// <returns> New instance of the page </returns>
        public T ClickOnSearchResultDocumentByName<T>(string name) where T : ICreatablePageObject
        {
            IWebElement docToClick = DriverExtensions.WaitForElement(By.LinkText(name));
            docToClick.ScrollToElement();
            docToClick.Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Gets IWebElement of the document with desired guid
        /// </summary>
        /// <param name="guid">The document guid</param>
        /// <returns>IWebElement</returns>
        protected IWebElement GetDocumentElementByGuid(string guid)
            => DriverExtensions.GetElement(SearchResultsElementLocator, By.CssSelector(string.Format(DocGuidLctMask, guid)));

        private IReadOnlyCollection<IWebElement> GetDocumentsTitleElements() => DriverExtensions.GetElements(DocumentTitleLocator);
    }
}