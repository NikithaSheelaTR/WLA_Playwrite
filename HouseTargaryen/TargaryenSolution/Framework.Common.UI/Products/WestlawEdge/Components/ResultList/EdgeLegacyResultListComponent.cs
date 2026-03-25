namespace Framework.Common.UI.Products.WestlawEdge.Components.ResultList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.ResultList;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.KeyCiteFlagDialog;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.PreviousInteractions;
    using Framework.Common.UI.Products.WestLawNext.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Items.DocumentListItems;
    using Framework.Common.UI.Raw.WestlawEdge.Models;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;
    
    /// <inheritdoc />
    /// <summary>
    /// Indigo Search Result List
    /// </summary>
    public class EdgeLegacyResultListComponent : LegacyResultList
    {
        private const string PurpleColor = "rgba(190, 190, 252, 1)";
        private const string SearchResultItemLctMask = "//a[@rank='{0}']";
        private const string TitleItemLctMask = ".//a[contains(text(),'{0}')]/ancestor::li";

        private const string SearchResultsLctMask = "//ol[@class='co_searchResult_list'] /li[@id='cobalt_search_results_{0}{1}']";
        private const string GuidItemLctMask = ".//*//a[contains(@docguid, '{0}') or contains(@href, '{0}')]/ancestor::div[contains(@class,'co_searchContent')]";
        private const string DocGuidLctMask = "//li[starts-with(@id, 'cobalt_search_')]//a[@docguid='{0}']";

        private static readonly By ResultItemLocator = By.XPath(".//li[starts-with(@id, 'cobalt_search_')]");
        private static readonly By DocLinkLocator = By.XPath("//div[@id='co_fermiSearchResult_data']//li//h3//a | //div[@id='co_fermiSearchResult_data']//li//h2//a");
        private static readonly By ResultListItemLocator = By.XPath(".//*[contains(@class, 'co_searchContent')]/ancestor::li");
        private static readonly By SelectAllCheckboxLocator = By.XPath(".//*[contains(@id, '_selectAll')]");
        private static readonly By PreviouslyViewedIconLocator = By.XPath("//li[@class = 'co_document_icon_previouslyviewed']");
        private static readonly By PreviouslyFolderedIconLocator = By.XPath("//li[@class = 'co_document_icon_foldered']");
        private static readonly By PreviouslyAnnotatedIconLocator = By.XPath("//li[@class = 'co_document_icon_notes']");
        private static readonly By TotalDocumentsHeaderLocator = By.XPath("//div[@class = 'co_search_result_heading_container']//h1");
        private static readonly By TotalDocumentsCountLocator = new ByChained(TotalDocumentsHeaderLocator, By.XPath("./span | //*[@class='co_search_titleCount']"));
        private static readonly By ClearAllLinkLocator = By.XPath("//li[contains(@class,'co_navItemsSelected')]//a | //button[contains(text(),'Clear selected')]");
        private static readonly By HighlightedTextLocator = By.XPath("//*[@class='co_searchTerm co_keyword']");
        private static readonly By HeaderCountLocator = By.XPath("//div[@id='coid_website_searchAvailableFacets']//span[@class='co_search_titleCount'] | //*[@class='co_search_titleCount']");
        private static readonly By SearchWithinTermLocator = By.XPath("//*[starts-with(@class,'co_searchTerm co_keyword')]");
        private static readonly By CitingReferencesLocator = By.XPath("//a[@class='co_doc_citing_refs_link']");
        private static readonly By RelatedDocumentsButtonLocator = By.XPath("//div[@id = 'relatedContentJumpLink']");
        private static readonly By JurisCitedFrequencyLocator = By.ClassName("crsw_caseSearchResults_bold");
        
        /// <summary>
        /// The content type map.
        /// </summary>
        private EnumPropertyMapper<ContentType, ContentTypeInfo> contentTypeMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeLegacyResultListComponent"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public EdgeLegacyResultListComponent(IWebElement container)
        {
            this.Container = container;
        }

        /// <summary>
        /// Number of search results from the header
        /// </summary>
        public new int HeaderCount =>
            DriverExtensions.WaitForElement(HeaderCountLocator).Text.RetrieveCountFromBrackets();

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected new EnumPropertyMapper<ContentType, ContentTypeInfo> ContentTypeMap =>
            this.contentTypeMap = this.contentTypeMap ?? EnumPropertyModelCache.GetMap<ContentType, ContentTypeInfo>();

        /// <summary>
        /// Gets the container.
        /// </summary>
        private IWebElement Container { get; }

        /// <summary>
        /// Select all checkbox
        /// </summary>
        private IWebElement SelectAllCheckbox =>
            DriverExtensions.WaitForElement(this.Container, SelectAllCheckboxLocator);

        #region Public Methods

        /// <summary>
        /// Get Citing References Count
        /// </summary>
        /// <returns>list of CR counts</returns>
        public List<int> GetCitingReferencesCountList() =>
            DriverExtensions.GetElements(CitingReferencesLocator).Select(elem => elem.Text.ConvertCountToInt()).ToList();

        /// <summary>
        /// Check to see if the search results are sorted  on the search results page by desc
        /// </summary>
        /// <returns> True if the search results sorted by desc </returns>
        public bool AreSearchResultsSortedByName()
        {
            List<string> titleOptionsForResults = this.GetAllSearchResultItems<ResultListItem>()
                                                      .Where(x => !string.IsNullOrWhiteSpace(x.LinkText)).Select(
                                                          x => x.LinkText.NormalizeDocumentName()
                                                                .RemoveArticlesFromDocumentName()).ToList();
            return titleOptionsForResults.SequenceEqual(titleOptionsForResults.OrderBy(dateItem => dateItem));
        }

        /// <summary>
        /// The get result list item model by index.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <typeparam name="TModel">
        /// The Type
        /// </typeparam>
        /// <returns>
        /// The new TModel.
        /// </returns>
        public TModel GetResultListItemModelByIndex<TModel>(int index) =>
            this.GetEdgeResultListItemByIndex(index).ToModel<TModel>();

        /// <summary>
        /// Gets result list item by guid.
        /// </summary>
        /// <typeparam name="TModel"> model </typeparam>
        /// <param name="guid">  document guid </param>
        /// <returns> T Model instance </returns>
        public TModel GetResultListItemModelByGuid<TModel>(string guid) =>
            this.GetEdgeResultListItemByGuid<EdgeResultListItem>(guid).ToModel<TModel>();

        /// <summary>
        /// The get models as list
        /// </summary>
        /// <typeparam name="TModel"> model </typeparam>
        /// <returns> T Model </returns>
        public IEnumerable<TModel> GetResultListItemModels<TModel>() =>
            this.GetItems().Select(x => x.ToModel<TModel>()).ToList();

        /// <summary>
        /// The click key cite flag by index.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <typeparam name="T">
        /// The desired class
        /// </typeparam>
        /// <returns>
        /// The T
        /// </returns>
        public T ClickKeyCiteFlagByIndex<T>(int index)
            where T : ICreatablePageObject =>
            this.GetEdgeResultListItemByIndex(index).ClickKeyCiteFlag<T>();

        /// <summary>
        /// Click Implied Overruling risk flag by search result list index
        /// </summary>
        /// <param name="index">Index of search result in result list</param>
        /// <returns>Edge KeyCiteFlagDialog that contains Overruling risk as well</returns>
        public KeyCiteFlagDialog ClickImpliedOverrulingFlagByIndex(int index) =>
            this.GetEdgeResultListItemByIndex(index).ClickImpliedOverrulingFlag();

        /// <summary>
        /// Click on the first document with Overruling risk flag
        /// </summary>
        /// <returns>Edge KeyCiteFlagDialog that contains Overruling risk as well</returns>
        public KeyCiteFlagDialog ClickFirstOverrulingRiskDocument() =>
            this.GetItems().First(item => item.IsImpliedOverrulingPresent).ClickImpliedOverrulingFlag();

        /// <summary>
        /// Click on the binary link by guid
        /// </summary>
        public void ClickBinaryLinkByGuid(string docGuid) =>
            this.GetEdgeResultListItemByGuid<EdgeResultListItem>(docGuid).ClickBinaryLink();

        /// <summary>
        /// The set all checkbox.
        /// </summary>
        /// <param name="selected">
        /// The selected.
        /// </param>
        public void SetSelectAllCheckbox(bool selected)
        {
            this.SelectAllCheckbox.WaitForElementDisplayed();
            this.SelectAllCheckbox.SetCheckbox(selected);
        }

        /// <summary>
        /// The set checkbox by index.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <param name="selected">
        /// The selected.
        /// </param>
        public void SetCheckboxByIndex(int index, bool selected) =>
            this.GetEdgeResultListItemByIndex(index).SetCheckbox(selected);

        /// <summary>
        /// The count.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Count() => this.GetItems().Count();

        /// <summary>
        /// The get total document header.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetTotalDocumentHeaderText() =>
            DriverExtensions.WaitForElementDisplayed(TotalDocumentsHeaderLocator).Text.RetainText();

        /// <summary>
        /// The get total document header count.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetTotalDocumentHeaderCount() =>
            DriverExtensions.WaitForElement(this.Container, TotalDocumentsCountLocator).Text
                            .RetrieveCountFromBrackets();

        /// <summary>
        /// Verify that all terms are highligted in purple
        /// </summary>
        /// <returns>True if all terms are highlighted in purple, false otherwise</returns>
        public bool AreTermsHighlighted() => DriverExtensions.GetElements(HighlightedTextLocator).All(term => term.GetCssValue("background-color").Contains(PurpleColor));

        /// <summary>
        /// Verify that select all checkbox is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public new bool IsSelectAllCheckboxDisplayed() => this.SelectAllCheckbox.IsDisplayed();

        /// <summary>
        /// The get result documents titles list
        /// </summary>
        /// <returns> List of strings </returns>
        public List<string> GetResultDocumentsNames() => this.GetItems().Select(item => item.TitleText).ToList();

        /// <summary>
        /// Gets the cited frequency notes
        /// </summary>
        /// <returns>List of cited notes</returns>
        public List<string> GetCitedFrequencyNotes() => DriverExtensions.GetElements(JurisCitedFrequencyLocator).Select(note => note.Text).ToList();
        
        /// <summary>
        /// Gets related documents button text.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>. Related documents button text. </returns>
        public string GetRelatedDocumentsButtonText()
        {
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.GetText(RelatedDocumentsButtonLocator);
        }

        /// <summary>
        /// Returns a list of the Search Within terms on the results page for a specified result number
        /// </summary>
        /// <param name="resultNumber"> The result number or rank </param>
        /// <param name="contentType"> The content type of the results </param>
        /// <returns> List of the search within highlighted terms for the result </returns>
        public new List<string> GetSearchWithinTermsForContentTypeByNumber(int resultNumber, ContentType contentType) =>
            this.GetTermsListForSearchResult(resultNumber, contentType, SearchWithinTermLocator);

        /// <summary>
        /// The click title link.
        /// </summary>
        /// <param name="index"> The index. </param>
        /// <returns> T Page </returns>
        public EdgeCitingReferencesPage ClickCitingReferencesLinkByIndex(int index) =>
            this.GetEdgeResultListItemByIndex(index).ClickCitingReferencesLink();

        /// <summary>
        /// The click official cites link.
        /// </summary>
        /// <param name="name">
        /// The index.
        /// </param>
        /// <typeparam name="T">
        /// the desired type
        /// </typeparam>
        /// <returns>
        /// The T
        /// </returns>
        public T ClickOfficialCitesLinkByDocumentName<T>(string name)
            where T : ICreatablePageObject => this.GetEdgeResultListItemByName(name).ClickOfficialCitesLink<T>();

        /// <summary>
        /// The click title link by index.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <typeparam name="T">
        /// the desired type
        /// </typeparam>
        /// <returns>
        /// The T
        /// </returns>
        public T ClickTitleLinkByIndex<T>(int index)
            where T : ICommonDocumentPage => this.GetEdgeResultListItemByIndex(index).ClickTitle<T>();

        /// <summary>
        /// Is Search Within term is highlighted in Summary by document guid
        /// </summary>
        /// <param name="searchWithinTerm">The Search Within term</param>
        /// <param name="docGuid">Document guid</param>
        /// <returns>True if term is highlighted in summary</returns>
        public bool IsSearchWithinTermHighlightedInSummary(string searchWithinTerm, string docGuid) => this
            .GetEdgeResultListItemByGuid<EdgeResultListItem>(docGuid).IsSearchWithinHighlightedInSummary(searchWithinTerm);

        /// <summary>
        /// Is cited frequency note displayed
        /// </summary>
        /// <param name="index">Document index to check</param>
        /// <returns>true if displayed</returns>
        public bool IsCitedFrequencyNoteDisplayed(int index) =>
          this.GetEdgeResultListItemByIndex(index).CitationsFrequencyNotes.Displayed;

        /// <summary>
        /// Gets the cited frequency note 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetCitedFrequencyNote(int index) =>
            this.GetEdgeResultListItemByIndex(index).CitationsFrequencyNotes.Text;

        /// <summary>
        /// The click synopsis link.
        /// </summary>
        /// <param name="model"> The model </param>
        public void ClickItemSynopsisLink(BaseEdgeResultListItemModel model) =>
            this.GetEdgeResultListItemByName(model.TitleText).ClickSynopsisLink();

        /// <summary>
        /// Note! Requires refactoring for AutoMapper
        /// Returns the document GUID for a specified search result.  For example, calling this method
        /// with a value of 1 will return the doc GUID of the 1st search result.
        /// </summary>
        /// <param name="resultRank"> The search result rank/position in the result list </param>
        /// <returns> The search result's GUID </returns>
        public new string GetSearchResultByDocGuid(int resultRank) =>
            DriverExtensions.GetElement(By.XPath(string.Format(SearchResultItemLctMask, resultRank)))
                            .GetAttribute("docguid");

        /// <summary>
        /// Get all search result items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public new IEnumerable<T> GetAllSearchResultItems<T>() where T : ResultListItem
        {
            DriverExtensions.WaitForElement(this.Container, ResultItemLocator);
            return DriverExtensions.GetElements(this.Container, ResultItemLocator).Select(el => (T)Activator.CreateInstance(typeof(T), el));
        }

        /// <summary>
        /// Click on desired snippet
        /// </summary>
        /// <typeparam name="T">The subtype of ICreatablePageObject</typeparam>
        /// <param name="model">The instance of IndigoResultListItemModel model</param>
        /// <param name="itemNumber">The number of the item. Starts from 0</param> <returns>The instance of PageObject</returns>
        public T ClickSnippet<T>(BaseEdgeResultListItemModel model, int itemNumber)
            where T : ICommonDocumentPage =>
            this.GetEdgeResultListItemByName(model.TitleText).ClickSnippet<T>(itemNumber);

        /// <summary>
        /// Click on a specific document based on the documents index
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="docIndex"> Document index to open </param>
        /// <returns> New instance of the page </returns>
        public new T ClickOnSearchResultDocumentByIndex<T>(int docIndex)
            where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(DocLinkLocator);
            DriverExtensions.GetElements(DocLinkLocator).ElementAt(docIndex).JavascriptClick();
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        ///  List of document title links
        /// </summary>
        public IReadOnlyCollection<ILink> ListOfDocumentTitleLinks => new ElementsCollection<Link>(this.Container, DocLinkLocator);

        /// <summary>
        /// Clicks on a specific document based on the result's doc GUID 
        /// </summary>
        /// <typeparam name="T"> Page Type </typeparam>
        /// <param name="guid"> The doc GUID  </param>
        /// <returns> DocumentPage from the given doc GUID </returns>
        public new T ClickOnSearchResultDocumentByGuid<T>(string guid)
            where T : ICreatablePageObject
        {
            DriverExtensions.ClickUsingJavaScript(By.XPath(string.Format(DocGuidLctMask, guid)));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on the the clear selected link
        /// </summary>
        /// <returns>
        /// The <see cref="EdgeLegacyResultListComponent"/>.
        /// </returns>
        public new EdgeLegacyResultListComponent ClickClearSelection()
        {
            DriverExtensions.WaitForElement(ClearAllLinkLocator);
            DriverExtensions.ScrollTo(ClearAllLinkLocator);
            DriverExtensions.Click(ClearAllLinkLocator);
            return this;
        }

        /// <summary>
        /// Click on previously viewed icon by index
        /// </summary>
        /// <param name="iconIndex"> Previously viewed icon index to open </param>
        /// <returns> New instance of the 'Previously Viewed' Dialog </returns>
        public PreviouslyViewedDialog ClickOnPreviouslyViewedIconByIndex(int iconIndex)
        {
            DriverExtensions.GetElements(PreviouslyViewedIconLocator).ElementAt(iconIndex).Click();
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();

            return new PreviouslyViewedDialog();
        }

        /// <summary>
        /// Click on 'previously foldered' icon by index
        /// </summary>
        /// <param name="iconIndex"> Previously foldered icon index to open </param>
        /// <returns> New instance of the Previously Foldered Dialog </returns>
        public PreviouslyFolderedDialog ClickOnPreviouslyFolderedIconByIndex(int iconIndex)
        {
            DriverExtensions.GetElements(PreviouslyFolderedIconLocator).ElementAt(iconIndex).Click();
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();

            return new PreviouslyFolderedDialog();
        }

        /// <summary>
        /// Click on 'previously annotated' icon by index
        /// </summary>
        /// <param name="iconIndex"> Previously Annotated icon index to open </param>
        /// <returns> New instance of the Previously Annotated Dialog </returns>
        public PreviouslyAnnotatedDialog ClickOnPreviouslyAnnotatedIconByIndex(int iconIndex)
        {
            DriverExtensions.GetElements(PreviouslyAnnotatedIconLocator).ElementAt(iconIndex).Click();
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();

            return new PreviouslyAnnotatedDialog();
        }

        /// <summary>
        /// The get result list items.
        /// Note! Needs refactoring for AutoMapper
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        public IEnumerable<T> GetItems<T>()
        {
            DriverExtensions.WaitForElement(this.Container, ResultListItemLocator);
            return DriverExtensions.GetElements(this.Container, ResultListItemLocator)
                                   .Select(item => (T)Activator.CreateInstance(typeof(T), item));
        }

        /// <summary>
        /// Gets item from result list by document guid
        /// </summary>
        /// <param name="guid">document guid</param>
        /// <returns>TItem instance</returns>
        public T GetEdgeResultListItemByGuid<T>(string guid)
        {
            var item = DriverExtensions.WaitForElement(this.Container, By.XPath(string.Format(GuidItemLctMask, guid)));
            return (T)Activator.CreateInstance(typeof(T), item);
        }


        #endregion

        #region Internal methods

        /// <summary>
        /// The get indigo result list item by index.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="EdgeResultListItem"/>.
        /// </returns>
        internal EdgeResultListItem GetEdgeResultListItemByIndex(int index)
        {
            DriverExtensions.WaitForJavaScript();
            Thread.Sleep(70000);
            DriverExtensions.WaitForPageLoad();
            var item = DriverExtensions.GetElements(this.Container, ResultListItemLocator).ElementAt(index);
            return new EdgeResultListItem(item);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The get result list items.
        /// Note! Needs refactoring for AutoMapper
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{EdgeResultListItem}"/>.
        /// </returns>
        private IEnumerable<EdgeResultListItem> GetItems()
        {
            DriverExtensions.WaitForElement(this.Container, ResultListItemLocator);
            return DriverExtensions.GetElements(this.Container, ResultListItemLocator)
                                   .Select(item => new EdgeResultListItem(item));
        }

        /// <summary>
        /// The get indigo result list item by name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="EdgeResultListItem"/>.
        /// </returns>
        private EdgeResultListItem GetEdgeResultListItemByName(string name) =>
            new EdgeResultListItem(
                DriverExtensions.GetElement(this.Container, By.XPath(string.Format(TitleItemLctMask, name))));

        /// <summary>
        /// The get terms list for search result.
        /// </summary>
        /// <param name="resultNumber">
        /// The result number.
        /// </param>
        /// <param name="contentType">
        /// The content type.
        /// </param>
        /// <param name="searchTermLocator">
        /// The search term locator.
        /// </param>
        /// <returns>
        /// The list of strings
        /// </returns>
        private List<string> GetTermsListForSearchResult(
            int resultNumber,
            ContentType contentType,
            By searchTermLocator)
        {
            IWebElement resultItem = this.GetSearchResultItem(resultNumber, contentType);
            IList<IWebElement> terms = DriverExtensions.GetElements(resultItem, searchTermLocator);
            return terms.Select(e => e.Text).ToList();
        }

        /// <summary>
        /// Get Search Result Item
        /// </summary>
        /// <param name="resultNum">
        /// The result num.
        /// </param>
        /// <param name="contentType">
        /// The content type.
        /// </param>
        /// <returns>
        /// The <see cref="IWebElement"/>.
        /// </returns>
        private IWebElement GetSearchResultItem(int resultNum, ContentType contentType)
        {
            string locator = string.Format(
                SearchResultsLctMask,
                this.ContentTypeMap[contentType].SearchResultsLocatorString,
                resultNum);
            return DriverExtensions.WaitForElement(By.XPath(locator));
        }
        #endregion

        /// <summary>
        /// Get result list item by matching texts.
        /// </summary>
        /// <param name="listOfTexts">
        /// Text to check in items.
        /// </param>
        /// <returns>
        /// The <see cref="EdgeResultListItem"/>.
        /// </returns>
        public IEnumerable<EdgeResultListItem> GetResultListItemBasedOnMatchingTexts<TModel>(List<string> listOfTexts) =>
            this.GetItems<EdgeResultListItem>()
            .Where(item => listOfTexts.All(text => item.SnippetsText
                 .Any(snippet => snippet.Contains(text)))).ToList();
    }
}