namespace Framework.Common.UI.Products.Shared.Components.ResultList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Enums.Document;
    using Framework.Common.UI.Products.Shared.Enums.Search;
    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.DataModel;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Base component object for module regression suites
    /// todo: merge with IndigoResultList
    /// </summary>
    public class LegacyResultList : LegacyBaseResultListComponent
    {
        // Yellow color
        private const string ColorForSearchTermString = "rgba(255, 255, 102, 1)";
        private const string ColorForSearchTermStringShade = "rgba(255, 248, 96, 1)";

        // Purple color
        private const string ColorForSearchWithinTermString = "rgba(190, 190, 252, 1)";

        private const string DetailLevelLctMask = "co_search_detailLevel_{0}";

        private const string SearchResultCitationLctMask = "co_searchResults_citation_{0}";

        private const string SearchResultItemLctMask = "//a[@rank='{0}' and contains(@id, 'result')]";

        private const string SearchResultsByContentTypeLctMask = "//li[starts-with(@id, 'cobalt_search_results_{0}')]";

        private const string SearchResultsContainer = "cobalt_search_{0}_results";

        private const string SearchResultsLctMask = "cobalt_search_results_{0}{1}";

        private const string SearchResultSnippetLctMask = "cobalt_result_{0}_snippet_{1}_{2}";

        private const string SearchResultSummaryLctMask = "co_searchResults_summary_{0}";

        private const string SearchResultTitleLctMask = "//label[contains(.,'{0}')]";

        private const string SnippetLctMask = "co_snippet_{0}_{1}";

        private const string SpecificContentTypeResultListItemLocator = "//a[contains(@id, 'cobalt_result_{0}_title')]";

        private const string ViewAllLinkLctMask = "//div[@id='cobalt_search_{0}_results']/h2/a";

        private const string ItemsForCitationLctMask
            = "//h2[@id='cobalt_citation_search_{0}_results_header']/..//a[@docguid]";

        private static readonly By AdditionalWestSearchLinkLocator = By.XPath("//div[@id='co_flipToFermiLinkContent']//a");

        private static readonly By ClearSelectedLinkLocator = By.LinkText("clear selected");

        private static readonly By DocContainerLocator = By.XPath("//ol[@class='co_searchResult_list']/li");

        private static readonly By SectionCitationLocator = By.XPath(".//strong");

        private static readonly By DocLinkLocator = By.XPath("//*[contains(@class, 'co_searchResultsList')]//h3//a | //*[contains(@class, 'co_searchResultsList')]//h2//a");

        private static readonly By ExpertLinkLocator = By.XPath("//a[contains(@id, 'cobalt_result_profilerExpertWitness_title')]");

        private static readonly By HeaderCountLocator = By.XPath("//div[@id='coid_website_searchAvailableFacets']//span");

        private static readonly By KeyCiteFlagLocator = By.XPath(".//div[@class='co_search_keyciteFlag']/a");

        private static readonly By NumberSelectedItemsLocator = By.XPath("//span[@id='co_searchHeader_dockItemsSelected']");

        private static readonly By ResultCheckboxeslocator = By.CssSelector("input[type='checkbox'][id*='checkbox_']");

        private static readonly By ResultHeaderLocator = By.XPath("//div[@class='co_search_result_heading_content']//h1");

        private static readonly By SearchResultCitationLocator = By.ClassName("co_searchResults_citation");

        private static readonly By SearchResultHeaderLocator = By.XPath("//h2[@class='co_search_header']");

        private static readonly By SearchResultsListLocator = By.Id("coid_website_searchResults");

        private static readonly By SearchTermLocator = By.ClassName("co_searchTerm");

        private static readonly By SearchWithinTermLocator = By.XPath(".//span[@class='co_searchTerm co_keyword']");

        private static readonly By SectionHeaderLocator = By.XPath("//div[@id='co_search_results_inner']//h2");

        private static readonly By SelectAllItemsCheckboxLocator = By.XPath("//input[@id='co_searchHeader_selectAll']");

        private static readonly By SearchResultOutOfPlanLinksLocator = By.XPath("//div[@class='co_outOfPlanLabel']/following-sibling::div[contains(@class,'co_searchContent')]//a[contains(@id,'_title')]");

        private static readonly By SmartAnswerLocator = By.Id("co_smartAnswer");

        private static readonly By SmartAnswerTitleLocator = By.ClassName("draggable_document_link");

        private static readonly By SnippetsLinkLocator = By.ClassName("co_snippet_link");

        private static readonly By SourceLocator = By.XPath(".//div[contains(@class,'co_searchContent')]//span/h3");

        private static readonly By DocumentLinkLocator = By.XPath(".//div[contains(@class,'co_searchContent')]/h3/a[@docguid]");

        private static readonly By SpellCheckLink = By.Id("co_search_spellCheck");

        private static readonly By SummariesListLocator = By.XPath(".//div[contains(@id, 'co_searchResults_summary_')]");

        private static readonly By PreviouslyViewedIconLocator = By.XPath("//li[@class = 'co_document_icon_previouslyviewed']//img");

        private static readonly By ResultItemLocator = By.XPath("//div[contains(@class,'co_searchContent')]");

        private static readonly By PreviewContentLocator = By.XPath(".//div[contains(@class, 'co_searchResults_previewContent')]");

        private static readonly By PreviewToggleLocator = By.XPath(".//a[contains(@class, 'co_searchResults_previewToggle')]");

        private static readonly By PublicationsLocator = By.XPath(".//a[contains(text(),'Publications')]");

        private static readonly By OutOfPlanCheckBoxLocator = By.XPath("//*[@class = 'co_outOfPlanLabel']/preceding-sibling::input[contains(@id,'cobalt_search_case_checkbox' )]");

        private static readonly By TestimonialHistoryLocator = By.XPath(".//a[contains(text(),'Testimonial History')]");

        private static readonly By DocTitlesLocator = By.XPath("//h3[@id='co_docTitle']/a");

        private static readonly By SearchContentFormTitleLocator = By.XPath("./ancestor::div[contains(@class,'co_searchContent')]");

        private static readonly By ResultsSection = By.XPath("//div[contains(@id,'cobalt_citation_search_results_')]");

        private static readonly By DocumentTrackButtonLocator = By.XPath("//a[@title='Track' and @class='co_tbButton co_search_trackable_document']");

        private static readonly By ContainerLocator = By.ClassName("co_searchResultsList");

        private EnumPropertyMapper<ContentType, ContentTypeInfo> contentTypeMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Additional West Search link
        /// </summary>
        public ILink AdditionalWestSearchLink => new Link(AdditionalWestSearchLinkLocator);

        /// <summary>
        /// The click random track button.
        /// </summary>
        /// <typeparam name="T"> Type of object to return.  </typeparam>
        /// <returns> New page instance./>. </returns>
        public T ClickTrackButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(DocumentTrackButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Number of search results from the header
        /// </summary>
        public int HeaderCount => DriverExtensions.WaitForElement(HeaderCountLocator).Text.RetrieveCountFromBrackets();

        /// <summary>
        /// The spell check text if displayed, otherwise empty string
        /// </summary>
        public string SpellCheckText
            => DriverExtensions.IsDisplayed(SpellCheckLink, 5)
                ? DriverExtensions.WaitForElementDisplayed(SpellCheckLink).Text
                : string.Empty;

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<ContentType, ContentTypeInfo> ContentTypeMap
            => this.contentTypeMap = this.contentTypeMap ?? EnumPropertyModelCache.GetMap<ContentType, ContentTypeInfo>();

        /// <summary>
        /// Are duplicates displayed
        /// </summary>
        /// <param name="searchResultItems"> Search result list items. </param>
        /// <returns> True if displayed, false otherwise </returns>
        public bool AreDuplicatesDisplayed(IEnumerable<NewsResultListItem> searchResultItems)
            => searchResultItems.Any(el => el.GetListDuplicateItems().Count > 0);

        /// <summary>
        /// Check to see if the search results are sorted  on the search results page by desc
        /// </summary>
        /// <returns> True if the search results sorted by desc </returns>
        public bool AreSearchResultsSortedByDate()
        {
            List<DateTime> dateOptionsForResults = this.GetAllSearchResultItems<ResultListItem>().Where(x => !string.IsNullOrWhiteSpace(x.Date)).Select(x => DateTime.Parse(x.Date)).ToList();
            return dateOptionsForResults.SequenceEqual(dateOptionsForResults.OrderByDescending(dateItem => dateItem));
        }

        /// <summary>
        /// Click on the the clear selected link
        /// </summary>
        public void ClickClearSelection()
        {
            DriverExtensions.WaitForElement(ClearSelectedLinkLocator).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Click on random document link 
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name= "onlyOutOfPlan"> set if only out of plan document or not have to be clicked</param>
        /// <returns> New instance of the page </returns>
        public T ClickRandomSearchResult<T>(bool onlyOutOfPlan = true) where T : ICreatablePageObject
        {
            var results = onlyOutOfPlan
                              ? DriverExtensions.GetElements(SearchResultOutOfPlanLinksLocator)
                              : DriverExtensions.GetElements(DocLinkLocator);
            results.ElementAt(new Random().Next(0, results.Count - 1)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks on a specific document based on the result's doc GUID
        /// </summary>
        /// <param name="resultIndex"> The document GUID </param>
        /// <returns> Expert Witness Profile from the given index </returns>
        public OutOfPlanDialog ClickExpertWitnessLink(int resultIndex)
        {
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.GetElements(ExpertLinkLocator).ElementAt(resultIndex).Click();
            return new OutOfPlanDialog();
        }

        /// <summary>
        /// Get the search result items count by content type
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="contentType"> Content type </param>
        /// <param name="index"> Document index to open </param>
        /// <returns> List of search terms </returns>
        public T ClickOnSearchResultItemsByContentType<T>(ContentType contentType, int index)
            where T : ICreatablePageObject
        {
            DriverExtensions.GetElements(By.XPath(string.Format(SpecificContentTypeResultListItemLocator, this.ContentTypeMap[contentType].SearchResultsLocatorString)))
                            .ElementAt(index)
                            .Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on the snippet by Content Type
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="contentType"> Content Type </param>
        /// <param name="documentNumber"> Document number </param>
        /// <param name="snippetNumber"> Snippet number </param>
        /// <returns> New instance of the page </returns>
        public T ClickOnSnippet<T>(ContentType contentType, int documentNumber, int snippetNumber) where T : ICreatablePageObject
        {
            string contentTypeString = this.ContentTypeMap[contentType].SearchResultsLocatorString;
            DriverExtensions.WaitForElement(
                By.Id(
                    string.Format(
                        SearchResultSnippetLctMask,
                        contentTypeString,
                        documentNumber,
                        snippetNumber))).Click();
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary> 
        /// Clicks the spell check link 
        /// </summary> 
        /// <typeparam name="T"> Page Type </typeparam> 
        /// <returns> New instance of the page </returns> 
        public T ClickSpellCheckLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForElementDisplayed(SpellCheckLink).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click Publications link
        /// </summary>
        /// <param name="documentName">
        /// The Document name
        /// </param>
        /// <typeparam name="T"> Page Type </typeparam> 
        /// <returns> New instance of the page </returns> 
        public T ClickPublicationsLink<T>(string documentName) where T : ICreatablePageObject
        {
            DriverExtensions.Click(DriverExtensions.GetElements(ResultItemLocator).First(e => e.Text.Contains(documentName)), PublicationsLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click Testimonial link
        /// </summary>
        /// <param name="documentName">
        /// The Document name
        /// </param>
        /// <typeparam name="T"> Page Type </typeparam> 
        /// <returns> New instance of the page </returns> 
        public T ClickTestimonialHistoryLink<T>(string documentName)
            where T : ICreatablePageObject
        {
            DriverExtensions.Click(DriverExtensions.GetElements(ResultItemLocator).First(e => e.Text.Contains(documentName)), TestimonialHistoryLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on view All cases
        /// </summary>
        /// <param name="type">The type of the content</param>
        public void ClickViewAll(ContentType type) =>
            DriverExtensions.WaitForElementDisplayed(By.XPath(string.Format(ViewAllLinkLctMask, this.ContentTypeMap[type].SearchResultsLocatorString))).Click();

        /// <summary>
        /// Get Search Result Items
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> Search result list </returns>
        public IEnumerable<T> GetAllSearchResultItems<T>() where T : ResultListItem
        {
            DriverExtensions.WaitForElement(SearchResultsElementLocator);
            return DriverExtensions.GetElements(SearchResultsElementLocator).Select(el => (T)Activator.CreateInstance(typeof(T), el));
        }

        /// <summary>
        /// Get the search result items count by content type
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="contentType"> Content type </param>
        /// <returns> List of search terms </returns>
        public IEnumerable<T> GetSearchResultItemsByContentType<T>(ContentType contentType) where T : ResultListItem
        {
            DriverExtensions.WaitForElement(SearchResultsElementLocator);
            return DriverExtensions.GetElements(By.XPath(string.Format(SearchResultsByContentTypeLctMask, this.ContentTypeMap[contentType].SearchResultsLocatorString))).Select(el => (T)Activator.CreateInstance(typeof(T), el));
        }

        /// <summary>
        /// Gets the list of search terms on the page for a specific content type
        /// </summary>
        /// <param name="contentType"> Content type to look at the snippets for </param>
        /// <returns> A list of search terms for that content type </returns>
        public List<string> GetAllSearchTermsForContentType(ContentType contentType)
        {
            var resultList = new List<string>();
            for (int number = 1; number <= this.GetSearchResultItemsByContentType<ResultListItem>(contentType).ToList().Count; number++)
            {
                resultList.AddRange(this.GetSearchTermsForContentTypeByNumber(number, contentType));
            }

            return resultList;
        }

        /// <summary>
        /// Gets Citation text for a specified search result
        /// </summary>
        /// <param name="searchResultItemIndex"> The index/rank of the search result. Starts from 0</param>
        /// <returns> Text of the citation</returns>
        public string GetCitationForResult(int searchResultItemIndex)
            => DriverExtensions.GetText(By.Id(string.Format(SearchResultCitationLctMask, searchResultItemIndex + 1)));

        /// <summary>
        /// Gets Details text for a specified search result
        /// </summary>
        /// <param name="index"> Index. Starts from 0</param>
        /// <returns> Text of the details </returns>
        public string GetDetailLevelText(int index) =>
            DriverExtensions.WaitForElement(
                DriverExtensions.GetElements(ResultItemLocator).ElementAt(index), By.ClassName(string.Format(DetailLevelLctMask, index + 2))).Text;

        /// <summary>
        /// Gets all of the document icons on the search result
        /// </summary>
        /// <param name="docGuid"> The GUID of the document in the search result </param>
        /// <returns> The IList of the document icons </returns>
        public IList<DocumentIcon> GetDocIconsForSearchResult(string docGuid)
        {
            IWebElement docLink = this.GetDocumentElementByGuid(docGuid);

            IWebElement searchResultContainer = docLink.GetParentElement(".co_searchContent");

            IEnumerable<string> pathsToSrc =
                DriverExtensions.GetElements(searchResultContainer, By.XPath(".//img"))
                                .Select(elem => elem.GetAttribute("src"));
            EnumPropertyMapper<DocumentIcon, WebElementInfo> docIconsMap =
                EnumPropertyModelCache.GetMap<DocumentIcon, WebElementInfo>();

            return
                docIconsMap.Where(pair => pathsToSrc.Any(path => path.Contains(pair.Value.SourceFile)))
                           .Select(pair => pair.Key)
                           .ToList();
        }

        /// <summary>
        /// Gets a list of any expert witness results.  Not all search results will have these
        /// </summary>
        /// <returns> List of expert witness results </returns>
        public List<string> GetExpertWitnessResults()
        {
            string expertWitness = "expertWitness";
            return
                DriverExtensions.GetElements(
                                    By.XPath(string.Format(SpecificContentTypeResultListItemLocator, expertWitness)))
                                .Select(a => a.Text)
                                .ToList();
        }

        /// <summary>
        /// Gets the collection of the citation headers of search results. Available only after search for several citations.    
        /// </summary>
        /// <returns>
        /// The collection of the citation headers of search results. 
        /// </returns>
        public List<string> GetHeaderCitationsOfSearchResults()
            => DriverExtensions.GetElements(ResultsSection, SectionCitationLocator).Select(citation => citation.Text).ToList();

        /// <summary>
        /// Returns the number of results sections, used to verify that all displayed citations actually have results
        /// </summary>
        /// <returns>a count of the number of results section elements on the page</returns>
        public int GetNumberOfResultsSections() => DriverExtensions.GetElements(ResultsSection).Count;

        /// <summary>
        /// GetNumberOfResultsForCitation
        /// </summary>
        /// <param name="cite">The cite.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetNumberOfResultsForCitation(string cite) => this.GetDocElementsForCitation(cite).Count;

        /// <summary>
        /// Gets a list of all result doc guids for a specified citation
        /// </summary>
        /// <param name="cite">the citation to get result guids for</param>
        /// <returns>a list of all result doc guids for the citation </returns>
        public List<string> GetDocGuidsForCitationResults(string cite)
            => this.GetDocElementsForCitation(cite).Select(el => el.GetAttribute("docguid")).ToList();

        /// <summary>
        /// Get item title by guid
        /// </summary>
        /// <param name="guid">Item guid</param>
        /// <returns>Item title</returns>
        public IList<string> GetItemCitationsByGuid(string guid) => this
            .GetAllSearchResultItems<ResultListItem>().Where(i => i.Guid.Equals(guid)).Select(i => i.Citations).FirstOrDefault();

        /// <summary>
        /// Get item title by guid
        /// </summary>
        /// <param name="guid">Item guid</param>
        /// <returns>Item title</returns>
        public string GetItemTitleByGuid(string guid) => this
            .GetAllSearchResultItems<ResultListItem>().Where(i => i.Guid.Equals(guid)).Select(i => i.LinkText)
            .FirstOrDefault();

        /// <summary>
        /// Gets the jurisdiction for a given document
        /// </summary>
        /// <param name="document"> Document to get the jurisdiction for </param>
        /// <returns> The jurisdiction </returns>
        public string GetJurisdiction(string document)
            => DriverExtensions.WaitForElement(By.LinkText(document)).GetAttribute("jur");

        /// <summary>
        /// Get the KeyCite Flag (if any) for the specified search result item
        /// </summary>
        /// <param name="resultIndex"> the index of the search result item </param>
        /// <param name="contentType"> the content type of the search result </param>
        /// <returns> The <see cref="KeyCiteFlag"/>. </returns>
        public KeyCiteFlag GetKeyCiteFlagColorForResult(int resultIndex, ContentType contentType)
        {
            IWebElement searchResultItem = this.GetSearchResultItem(resultIndex, contentType);

            if (DriverExtensions.IsDisplayed(searchResultItem, KeyCiteFlagLocator))
            {
                IWebElement keyCiteFlagWrapper = DriverExtensions.GetElement(
                    searchResultItem,
                    KeyCiteFlagLocator);

                string flagClass = keyCiteFlagWrapper.GetAttribute("class");
                return flagClass.GetEnumValueByPropertyModel<KeyCiteFlag, WebElementInfo>(mod => mod.ClassName);
            }

            return KeyCiteFlag.NoFlag;
        }

        /// <summary>
        /// Click on the flag of the document with desired title
        /// </summary>
        /// <returns>New instance of T page</returns>
        public T ClickKeyCiteFlag<T>(string title) where T : ICreatablePageObject
        {
            var doc = DriverExtensions.GetElements(SearchResultsElementLocator).First(i => i.Text.Contains(title));
            DriverExtensions.GetElement(doc, KeyCiteFlagLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get GUID document by origination context
        /// </summary>
        /// <param name="originationContext"> Origination content </param>
        /// <returns> GUID document </returns>
        public string GetDocGuidByOriginationContext(string originationContext)
        {
            ResultListItem documentItem =
                this.GetAllSearchResultItems<ResultListItem>()
                    .ToList()
                    .FirstOrDefault(
                        elem => this.GetOriginationContextParameter(elem.Link).Equals(originationContext));
            return documentItem == null ? string.Empty : documentItem.Guid;
        }

        /// <summary>
        /// Parameter 'origination context' from the URL
        /// </summary>
        /// <param name="link"> URL </param>
        /// <returns> Origination Context </returns>
        private string GetOriginationContextParameter(string link)
        {
            int startIndex = link.IndexOf("originationContext", StringComparison.Ordinal);
            int endIndex = link.IndexOf("transitionType=SearchItem", StringComparison.Ordinal);
            string originalContext = link.Substring(startIndex, endIndex - startIndex - 1);
            originalContext = originalContext.Replace("%20", string.Empty).Replace("originationContext=", string.Empty);
            return originalContext;
        }

        /// <summary>
        /// Get the number of KeyCite Flags currently displayed on the search results page
        /// </summary>
        /// <returns> The number of KeyCite Flags </returns>
        public int GetNumberOfKeyCiteFlags() => DriverExtensions.GetElements(KeyCiteFlagLocator).Count;

        /// <summary>
        /// Get a list of snippets for a specific content type on a search results overview page
        /// </summary>
        /// <param name="contentType"> The content type to get snippets for </param>
        /// <returns> List of the snippets text </returns>
        public List<string> GetOverviewSnippets(ContentType contentType)
            => this.GetSnippetElements(contentType).Select(element => element.Text).ToList();

        /// <summary>
        /// Get the first highlighted search term in a snippet on a search results overview page
        /// </summary>
        /// <param name="contentType"> The content type to get snippets for </param>
        /// <returns> List of each first highlighted term in all snippets for the content type </returns>
        public List<string> GetOverviewSnippetsFirstHighlightedTerm(ContentType contentType)
            =>
                this.GetSnippetElements(contentType)
                    .Select(element => DriverExtensions.GetElement(element, SearchTermLocator).Text)
                    .ToList();

        /// <summary>
        /// Gets the header for the results
        /// </summary>
        /// <returns>Header for the results</returns>
        public string GetResultsHeader() => DriverExtensions.GetElement(ResultHeaderLocator).Text;

        /// <summary>
        /// Returns a list of all the different content type results headers
        /// </summary>
        /// <returns> list of all the different content type results headers </returns>
        public List<string> GetResultsHeaders()
        {
            var excludeViewAllRegex = new Regex(@"(?<resultsHeader>\w+\D+)View all\s[\d,]+$");
            return
                DriverExtensions.GetElements(SearchResultHeaderLocator)
                                .Select(e => excludeViewAllRegex.Match(e.Text).Groups["resultsHeader"].Value.Trim())
                                .ToList();
        }

        /// <summary>
        /// Gets all of the case titles
        /// </summary>
        /// <returns> A list of all case titles </returns>
        public List<string> GetResultTitles()
            => DriverExtensions.GetElements(DocTitlesLocator).Select(element => element.Text).ToList();

        /// <summary>
        /// Gets all of the case titles
        /// </summary>
        /// <param name="contentType"> The content Type. </param>
        /// <returns> A list of all case titles </returns>
        public List<string> GetResultTitlesByContentType(ContentType contentType)
            => this.GetResultListElementsByContentType(contentType).Select(element => element.Text).ToList();

        /// <summary>
        /// Returns the document GUID for a specified search result.  For example, calling this method
        /// with a value of 1 will return the doc GUID of the 1st search result.
        /// </summary>
        /// <param name="resultRank"> The search result rank/position in the result list </param>
        /// <returns> The search result's GUID </returns>
        public string GetSearchResultByDocGuid(int resultRank)
            =>
                DriverExtensions.GetElement(By.XPath(string.Format(SearchResultItemLctMask, resultRank)))
                                .GetAttribute("docguid");

        /// <summary>
        /// Returns a list of the case GUIDS for each search result displayed
        /// </summary>
        /// <param name="contentType">the content type of the search results</param>
        /// <returns>a list of case GUIDS for the search results</returns>
        public List<string> GetSearchResultCaseDocGuids(ContentType contentType) => this.GetDocGuids(contentType, true);

        /// <summary>
        /// Gets the text of the "citation" element for the search results (not the citation of the document)
        /// </summary>
        /// <param name="contentType"> The content type of the result on the overview page </param>
        /// <returns> The text of the citation element </returns>
        public virtual IList<string> GetSearchResultCitations(ContentType contentType)
        {
            DriverExtensions.WaitForJavaScript();
            string contentTypeString = this.ContentTypeMap[contentType].SearchResultsLocatorString;
            By container = By.Id(string.Format(SearchResultsContainer, contentTypeString));
            IList<IWebElement> citationElementsList = DriverExtensions.GetElements(
                container,
                SearchResultCitationLocator);

            return
                citationElementsList.Select(
                    citation =>
                        DriverExtensions.GetElements(citation, By.XPath("./span"))
                                        .Select(elem => elem.Text)
                                        .ToList()
                                        .Aggregate((i, j) => i + " " + j)
                                        .Trim()).ToList();
        }

        /// <summary>
        /// Returns a list of the GUIDS for each search result displayed
        /// </summary>
        /// <returns> List of GUIDS for the search results</returns>
        public List<string> GetSearchResultDocGuids() =>
            DriverExtensions.GetElements(DocTitlesLocator, SearchContentFormTitleLocator).Select(result => result.GetAttribute("id")).ToList();

        /// <summary>
        /// Returns a list of the GUIDS for search result page, regardless content type
        /// </summary>
        /// <returns> List of GUIDS for the search results</returns>
        public List<string> GetSearchResultDocGuidsForOverview() =>
            DriverExtensions.GetElements(SearchResultsListLocator, DocumentLinkLocator).Select(result => result.GetAttribute("docguid")).ToList();

        /// <summary>
        /// Returns a list of the GUIDS for each search result displayed
        /// </summary>
        /// <param name="contentType"> The content type of the search results</param>
        /// <returns> List of GUIDS for the search results</returns>
        public List<string> GetSearchResultDocGuidsByContentType(ContentType contentType) => this.GetDocGuids(contentType, false);

        /// <summary>
        /// Gets the source of the search result, through the string that appears in the result item
        /// header when LoggingQuantity is set to Verbose on the routing page
        /// </summary>
        /// <param name="searchResultIndex"> The index/rank of the search result to get the source of </param>
        /// <param name="contentType"> The content type of the result </param>
        /// <returns> List of the search result sources </returns>
        public List<SearchResultSource> GetSearchResultSources(int searchResultIndex, ContentType contentType)
        {
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
            IWebElement resultItem = this.GetSearchResultItem(searchResultIndex, contentType);
            IWebElement resultSourceElement = DriverExtensions.GetElement(resultItem, SourceLocator);

            string[] resultSourceStrings = resultSourceElement.Text.Split(',');

            Dictionary<string, SearchResultSource> sourceTypesList = this.GetDictionaryForSearchResultSource();

            return
                resultSourceStrings.Where(elem => sourceTypesList.ContainsKey(elem))
                                   .Select(elem => sourceTypesList[elem])
                                   .ToList();
        }

        /// <summary>
        /// Gets the list of search terms
        /// </summary>
        /// <returns> A list of search terms </returns>
        public List<string> GetSearchTerms()
            => DriverExtensions.GetElements(SearchTermLocator).Select(e => e.Text).ToList();

        /// <summary>
        /// Returns a list of the Search Within terms on the results page for a specified result number
        /// </summary>
        /// <param name="resultNumber"> The result number or rank </param>
        /// <param name="contentType"> The content type of the results </param>
        /// <returns> List of the search within highlighted terms for the result </returns>
        public List<string> GetSearchTermsForContentTypeByNumber(int resultNumber, ContentType contentType)
            => this.GetTermsListForSearchResult(resultNumber, contentType, SearchTermLocator);

        /// <summary>
        /// Returns a list of the Search Within terms on the results page for a specified result number
        /// </summary>
        /// <param name="resultNumber"> The result number or rank </param>
        /// <param name="contentType"> The content type of the results </param>
        /// <returns> List of the search within highlighted terms for the result </returns>
        public List<string> GetSearchWithinTermsForContentTypeByNumber(int resultNumber, ContentType contentType)
            => this.GetTermsListForSearchResult(resultNumber, contentType, SearchWithinTermLocator);

        /// <summary>
        /// Returns the number selected text
        /// </summary>
        /// <returns>Number selected text</returns>
        public string GetSelectedNumberText() => DriverExtensions.GetElement(NumberSelectedItemsLocator).Text.Trim();

        /// <summary>
        /// Gets a set amount of highlighted key words from a given snippet
        /// </summary>
        /// <param name="contentType"> The content type </param>
        /// <param name="documentNumber"> Number of document to find terms  </param>
        /// <param name="snippetNumber"> Number of snippet </param>
        /// <param name="termsCount"> Count of search terms </param>
        /// <returns> String of KeyWords </returns>
        public string GetSnippetKeyWords(ContentType contentType, int documentNumber, int snippetNumber, int termsCount)
        {
            string contentTypeString = this.ContentTypeMap[contentType].SearchResultsLocatorString;
            string keyWords;

            By keyWordLocator =
                By.XPath($"//a[@id='cobalt_result_{contentTypeString}_snippet_{documentNumber}_{snippetNumber}']/span");
            List<IWebElement> keyWordelements = DriverExtensions.GetElements(keyWordLocator).ToList();
            if (keyWordelements.Count >= termsCount)
            {
                keyWords =
                    Enumerable.Range(0, termsCount)
                              .Select(i => keyWordelements.ElementAt(i).Text)
                              .ToList()
                              .Aggregate((s1, s2) => s1 + s2);
            }
            else
            {
                keyWords = keyWordelements.Select(elem => elem.Text).ToList().Aggregate((s1, s2) => s1 + s2);
            }

            return keyWords;
        }

        /// <summary>
        /// Gets the list of snippets on the page 
        /// </summary>
        /// <returns>A list of snippets </returns>
        public List<string> GetSnippets()
        {
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForElementDisplayed(SnippetsLinkLocator);
            return
                DriverExtensions.GetElements(SearchResultsListLocator, SnippetsLinkLocator).Select(e => e.Text).ToList();
        }

        /// <summary>
        /// Gets the list of snippets for document by index
        /// </summary>
        /// <returns>A list of snippets </returns>
        public List<string> GetSnippetsByIndex(int index)
        {
            IWebElement item = DriverExtensions.GetElements(ResultItemLocator).ElementAt(index);

            DriverExtensions.WaitForJavaScript();
            return
                DriverExtensions.GetElements(item, SnippetsLinkLocator).Select(e => e.Text).ToList();
        }

        /// <summary>
        /// Are Snippets Present
        /// </summary>
        /// <returns>true if snippets are present</returns>
        public bool AreSnippetsPresent() => DriverExtensions.IsDisplayed(SnippetsLinkLocator);

        /// <summary>
        /// The list of summaries on the page
        /// </summary>
        /// <returns>return the list of summaries on the page</returns>
        public List<string> GetSummariesList()
        {
            var summaries = new List<string>();
            foreach (IWebElement doc in DriverExtensions.GetElements(DocContainerLocator))
            {
                IWebElement summary;
                summaries.Add(
                    DriverExtensions.TryGetElement(doc, SummariesListLocator, out summary) ? summary.Text : string.Empty);
            }

            return summaries;
        }

        /// <summary>
        /// Returns a list of the titles of all displayed results for a specified content type
        /// </summary>
        /// <param name="contentType"> The current content type </param>
        /// <returns> List of all result titles </returns>
        public List<string> GetTitlesForContentType(ContentType contentType)
        {
            string contentTypeString = this.ContentTypeMap[contentType].Text;

            IReadOnlyCollection<IWebElement> summaries =
                DriverExtensions.GetElements(By.XPath(string.Format(SearchResultTitleLctMask, contentTypeString)));

            return summaries.Select(e => e.Text).ToList();
        }

        /// <summary>
        /// Expand Preview of the result item
        /// </summary>
        /// <param name="index"> Index of the item. Starts from 0 </param>
        public void ExpandPreview(int index)
        {
            IWebElement item = DriverExtensions.GetElements(ResultItemLocator).ElementAt(index);

            if (DriverExtensions.IsDisplayed(item, PreviewToggleLocator) && !this.IsPreviewExpanded(index))
            {
                DriverExtensions.WaitForElement(item, PreviewToggleLocator).Click();
                DriverExtensions.WaitForJavaScript();
            }
        }

        /// <summary>
        /// Expand Preview of the result item
        /// </summary>
        /// <param name="index"> Index of the item. Starts from 0 </param>
        /// <returns>True if section is expanded</returns>
        public bool IsPreviewExpanded(int index) =>
            DriverExtensions.IsDisplayed(DriverExtensions.GetElements(ResultItemLocator).ElementAt(index), PreviewContentLocator);

        /// <summary>
        /// Checks whether or not a Citation element is displayed for a specified search result
        /// </summary>
        /// <param name="searchResultItemIndex"> The index/rank of the search result </param>
        /// <returns> True if the citation is displayed, false otherwise </returns>
        public bool IsCitationDisplayedForResult(int searchResultItemIndex)
            =>
                DriverExtensions.IsDisplayed(
                    By.Id(string.Format(SearchResultCitationLctMask, searchResultItemIndex)));

        /// <summary>
        /// Checks whether or not expected detail elements are visible for the specified detail level
        /// </summary>
        /// <param name="searchDetailLevel"> The detail level to check </param>
        /// <returns> True if all elements of the specified detail level are displayed, false otherwise </returns>
        public bool IsDetailLevelComponentDisplayed(DetailLevel searchDetailLevel)
        {
            int numberOfDetailLevel = this.GetNumberOfDetailLevel(searchDetailLevel);
            IReadOnlyCollection<IWebElement> elements =
                DriverExtensions.GetElements(By.ClassName(string.Format(DetailLevelLctMask, numberOfDetailLevel)));
            return elements.All(elem => elem.IsDisplayed());
        }

        /// <summary>
        /// Verify that the link contains the listPageSource parameter in the HREF
        /// </summary>
        /// <param name="resultIndex"> index </param>
        /// <param name="contentType"> Content type </param>
        /// <returns> True if contains, false otherwise </returns>
        public bool IsListPageSourcePresent(int resultIndex, ContentType contentType)
        {
            List<IWebElement> resultLinks = this.GetResultListElementsByContentType(contentType);
            return resultLinks[resultIndex - 1].GetAttribute("href").Contains("listPageSource=");
        }

        /// <summary>
        /// checks if a Publications Link item is displayed for an item
        /// </summary>
        /// <param name="documentName">
        /// The Document name
        /// </param>
        /// <returns>
        /// true if the result has the link
        /// </returns>
        public bool IsPublicationsLinkDisplayed(string documentName) =>
            DriverExtensions.IsDisplayed(DriverExtensions.GetElements(ResultItemLocator).First(e => e.Text.Contains(documentName)), PublicationsLocator);

        /// <summary>
        /// checks if a Testimonial Link item is displayed for an item
        /// </summary>
        /// <param name="documentName">
        /// The Document name
        /// </param>
        /// <returns>
        /// true if the result has the link
        /// </returns>
        public bool IsTestimonialHistoryLinkDisplayed(string documentName) =>
            DriverExtensions.IsDisplayed(DriverExtensions.GetElements(ResultItemLocator).First(e => e.Text.Contains(documentName)), TestimonialHistoryLocator);

        /// <summary>
        /// Verify that previously viewed icon is displayed
        /// </summary>
        /// <param name="resultRank"> result rank </param>
        /// <param name="contentType"> The content type </param>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsPreviouslyViewedIconDisplayed(int resultRank, ContentType contentType)
        {
            DriverExtensions.WaitForJavaScript();
            IWebElement resultItem = this.GetSearchResultItem(resultRank, contentType);
            return DriverExtensions.IsDisplayed(resultItem, PreviouslyViewedIconLocator);
        }

        /// <summary>
        /// Verify that search term is highlighted
        /// </summary>
        /// <param name="searchTerm"> Term to search </param>
        /// <returns> True if term highlighted, false otherwise </returns>
        public bool IsSearchTermHighlighted(string searchTerm)
        => DriverExtensions.GetElements(SearchTermLocator).Where(elem => elem.Text.Contains(searchTerm)).Any(
                elem => elem.GetCssValue("background-color").Equals(ColorForSearchTermString)
                        || elem.GetCssValue("background-color").Equals(ColorForSearchTermStringShade));

        /// <summary>
        /// Verify that search within term is highlighted
        /// </summary>
        /// <param name="searchTerm"> Term to search </param>
        /// <returns> True if term highlighted, false otherwise </returns>
        public bool IsSearchWithinTermHighlighted(string searchTerm)
            => DriverExtensions.GetElements(SearchWithinTermLocator).Where(elem => elem.Text.Contains(searchTerm))
                .Any(elem => elem.GetCssValue("background-color").Equals(ColorForSearchWithinTermString));

        /// <summary>
        /// Method verifies whether the Content Type section is displayed on the Overview page
        /// </summary>
        /// <param name="type">The type of the content</param>
        /// <returns>true if section exists</returns>
        public bool IsSectionDisplayed(ContentType type) => DriverExtensions.GetElements(SectionHeaderLocator).Any(e => e.Text == this.ContentTypeMap[type].Text);

        /// <summary>
        /// Determines if the select all checkbox is disabled
        /// </summary>
        /// <returns>True if disabled, false otherwise</returns>
        public bool IsSelectAllCheckboxDisabled()
            =>
                DriverExtensions.WaitForElement(SelectAllItemsCheckboxLocator)
                                .GetAttribute("class")
                                .Contains("co_disabled");

        /// <summary>
        /// Verify that select all checkbox is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsSelectAllCheckboxDisplayed() => DriverExtensions.IsDisplayed(SelectAllItemsCheckboxLocator);

        /// <summary>
        /// Returns true if the blue Smart Answer box is displayed on the page.  This box is used for showing
        /// famous cases/etc. that return for the search but are not in the set jurisdiction
        /// </summary>
        /// <returns> True if the Smart Answer box is displayed </returns>
        public bool IsSmartAnswerDisplayed() => DriverExtensions.IsDisplayed(SmartAnswerLocator, 5);

        /// <summary>
        /// Is Smart Answer Result Present
        /// </summary>
        /// <param name="resultRank"> The result Rank. </param>
        /// <param name="contentType"> The content Type. </param>
        /// <returns> True if present, false otherwise </returns>
        public bool IsSmartAnswerResultPresent(int resultRank, ContentType contentType)
        {
            IWebElement searchResult = this.GetSearchResultItem(resultRank, contentType);
            DriverExtensions.WaitForJavaScript();
            IWebElement searchResultTitle = DriverExtensions.GetElement(searchResult, SmartAnswerTitleLocator);
            return searchResultTitle.GetAttribute("href").Contains("originationContext=Smart%20Answer");
        }

        /// <summary>
        /// Checks whether or not the specified snippet is present and displayed for a specified search result
        /// </summary>
        /// <param name="resultIndex"> The index/rank of the search result </param>
        /// <param name="snippetIndex"> The index/rank of the snippet </param>
        /// <returns> True if the snippet is displayed, false otherwise </returns>
        public bool IsSnippetDisplayedForResult(int resultIndex, int snippetIndex)
        {
            bool result = false;
            DriverExtensions.WaitForJavaScript();
            By locator = By.Id(string.Format(SnippetLctMask, resultIndex, snippetIndex));
            if (DriverExtensions.IsDisplayed(locator))
            {
                result = DriverExtensions.IsDisplayed(locator);
            }

            return result;
        }

        /// <summary>
        /// Checks whether or not a Summary is displayed for a specified search result
        /// </summary>
        /// <param name="searchResultItemIndex"> The index/rank of the search result </param>
        /// <returns> True if the summary is displayed, false otherwise </returns>
        public bool IsSummaryDisplayedForSearchResult(int searchResultItemIndex)
            =>
                DriverExtensions.IsDisplayed(
                    By.Id(string.Format(SearchResultSummaryLctMask, searchResultItemIndex)));

        /// <summary>
        /// Returns a list of all the different content type results headers
        /// </summary>
        /// <param name="type">type of the Content</param>
        /// <returns> list of all the different content type results headers </returns>
        public bool IsViewAllLinkDisplayed(ContentType type) => DriverExtensions.GetElements(SearchResultHeaderLocator)
            .Where(e => e.Text.Contains(this.ContentTypeMap[type].Text))
            .Select(e => e.Text.Contains("View all")).First();

        /// <summary>
        /// Click on the the select all items checkbox
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T SelectAllItemsCheckbox<T>() where T : BaseModuleRegressionPage
        {
            DriverExtensions.WaitForElement(SelectAllItemsCheckboxLocator).SetCheckbox(true);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Sets the checkbox corresponding to the specified title to the value of the checked variable
        /// </summary>
        /// <param name="resultTitle"> The title of the desired result list item </param>
        /// <param name="state"> The state of the checkbox </param>
        public void SetResultListCheckbox(string resultTitle, bool state = true) => DriverExtensions.SetCheckbox(state, resultTitle);

        /// <summary>
        /// Sets the checkbox corresponding to the specified index to the value of the checked variable
        /// </summary>
        /// <param name="index"> The index(starts at 0) of the result list item amongst the other result list items </param>
        public void SetResultListCheckbox(int index)
        {
            DriverExtensions.WaitForElement(ResultCheckboxeslocator);
            IWebElement checkbox = DriverExtensions.GetElements(ResultCheckboxeslocator).ElementAt(index);
            checkbox.ScrollToElement();
            if (!checkbox.Selected)
            {
                checkbox.Click();
            }
        }

        /// <summary>
        /// Sets random outOfPlanCheckBox checkbox 
        /// </summary>
        /// <param name="state"> checkBox check state </param>
        public void SetRandomOutOfPlanCheckBox(bool state = true)
            => DriverExtensions.GetElements(OutOfPlanCheckBoxLocator)
            .ElementAt(new Random().Next(0, DriverExtensions.GetElements(OutOfPlanCheckBoxLocator).Count - 1))
            .SetCheckbox(state);

        /// <summary>
        /// Sets the checkbox corresponding to the specified index to the value of the checked variable
        /// </summary>
        /// <param name="indexes"> The indexes (starts at 0) of the result list items amongst the other result list items </param>
        /// ToDo : remove SetResultListCheckbox(int index) method and change all links to current method.
        public void SetResultListCheckboxes(params int[] indexes)
            => indexes.ToList().ForEach(this.SetResultListCheckbox);

        private List<IWebElement> GetDocElementsForCitation(string cite)
            => DriverExtensions.GetElements(By.XPath(string.Format(ItemsForCitationLctMask, cite)))
                .Where(el => el.Displayed).ToList();

        private Dictionary<string, SearchResultSource> GetDictionaryForSearchResultSource()
        {
            var resultList = new Dictionary<string, SearchResultSource>();

            foreach (
                KeyValuePair<SearchResultSource, BaseTextModel> pair in
                EnumPropertyModelCache.GetMap<SearchResultSource, BaseTextModel>())
            {
                resultList.Add(pair.Value.Text, pair.Key);
            }

            return resultList;
        }

        private List<string> GetDocGuids(ContentType contentType, bool isCaseDocGuid)
        {
            string typeDocGuid = isCaseDocGuid ? "casedocguid" : "docguid";

            string contentTypeString = this.ContentTypeMap[contentType].SearchResultsLocatorString;
            By resultListItemLink = By.XPath(string.Format(SpecificContentTypeResultListItemLocator, contentTypeString));
            DriverExtensions.WaitForElementDisplayed(resultListItemLink);
            DriverExtensions.WaitForJavaScript();
            IEnumerable<IWebElement> searchResults = DriverExtensions.GetElements(resultListItemLink);

            return searchResults.Select(result => result.GetAttribute(typeDocGuid)).ToList();
        }

        private int GetNumberOfDetailLevel(DetailLevel searchDetailLevel)
        {
            int detailLevelNum = 0;
            switch (searchDetailLevel)
            {
                case DetailLevel.LessDetail:
                    detailLevelNum = 1;
                    break;
                case DetailLevel.MoreDetail:
                    detailLevelNum = 2;
                    break;
                case DetailLevel.MostDetail:
                    detailLevelNum = 3;
                    break;
            }

            return detailLevelNum;
        }

        private List<IWebElement> GetResultListElementsByContentType(ContentType contentType)
        {
            string contentTypeString = this.ContentTypeMap[contentType].SearchResultsLocatorString;
            return
                DriverExtensions.GetElements(
                                    By.XPath(
                                        string.Format(SpecificContentTypeResultListItemLocator, contentTypeString)))
                                .ToList();
        }

        private IWebElement GetSearchResultItem(int resultNum, ContentType contentType)
        {
            string locator = string.Format(
                SearchResultsLctMask,
                this.ContentTypeMap[contentType].SearchResultsLocatorString,
                resultNum);
            return DriverExtensions.WaitForElement(By.Id(locator));
        }

        private IList<IWebElement> GetSnippetElements(ContentType contentType)
        {
            string contentTypeString = this.ContentTypeMap[contentType].SearchResultsLocatorString;
            return DriverExtensions.GetElements(
                By.Id(string.Format(SearchResultsContainer, contentTypeString)),
                SnippetsLinkLocator);
        }

        private List<string> GetTermsListForSearchResult(
            int resultNumber,
            ContentType contentType,
            By searchTermLocator)
        {
            IWebElement resultItem = this.GetSearchResultItem(resultNumber, contentType);
            IList<IWebElement> terms = DriverExtensions.GetElements(resultItem, searchTermLocator);
            return terms.Select(e => e.Text).ToList();
        }
    }
}