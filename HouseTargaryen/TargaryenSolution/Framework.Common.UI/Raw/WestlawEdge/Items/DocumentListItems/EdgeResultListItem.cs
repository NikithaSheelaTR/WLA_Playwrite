namespace Framework.Common.UI.Raw.WestlawEdge.Items.DocumentListItems
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Framework.Common.UI.Interfaces.Components;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Products.WestlawEdge.Components.ResultList;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.KeyCiteFlagDialog;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.FocusHighlighting;
    using Framework.Common.UI.Raw.WestlawEdge.Models.FocusHighlighting;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo;
    using Framework.Common.UI.Raw.WestlawEdge.Utils.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The result list item class for Edge
    /// </summary>
    public class EdgeResultListItem : ResultListItem, IDraggableWebElement
    {
        private const string GetWordsBeforeFirstHitTermRegex = @"[\.]{3}[\w\W]+?(?=<span)";

        private const string PurpleColor = "rgba(190, 190, 252, 1)";

        private const string FirstMissingTermLctMask = "#cobalt_search_results_case{0} > div.co_searchContent > div.co_missingTerms.co_searchResult_containerList.co_search_detailLevel_2 > del";

        private const string MissingTermsLctMask = "//*[@id='missingTermDetail_{0}']/del[{1}]";

        private const string MustIncludeLctMask = "//*[@id='missingTermDetail_{0}']/a[{1}]";

        private const int IndexOfFirsSquareBracket = 69;

        private static readonly By TitleLocator = By.XPath(".//h3//a | .//h2//a");

        // TODO this locator will pick up implied overruling flag too
        private static readonly By KeyCiteFlagLocator = By.XPath(".//a[contains(@title,'KeyCite')]");

        private static readonly By SummaryLocator = By.XPath(".//div[contains(@class,'co_searchResults_summary')]");

        private static readonly By CheckboxLocator = By.XPath(".//input[@type='checkbox']");

        private static readonly By CitingReferencesLinkLocator = By.ClassName("co_doc_citing_refs_link");

        private static readonly By ImpliedOverrulingLocator = By.XPath(".//a[contains(@title,'KeyCite overruling') or contains(@oldtitle,'KeyCite Overruling Risk')] | .//a[contains(@class, 'co_cautionSm')]");

        private static readonly By ItemDetailsLocator = By.XPath(".//span[@class = 'co_search_detailLevel_1' or contains(@class,'co_result_citation')] | .//div[@class='co_searchResults_citation']/span[2]");

        private static readonly By IndexLocator = By.XPath(".//span[@class='co_searchCount' or contains(@class,'co_docNumber')] | .//h3[@id='co_docTitle']/span[@class='ng-binding']");

        private static readonly By SynopsisLinkLocator = By.XPath(".//*[contains(@class, 'co_searchResults_synopsisToggle')]");

        private static readonly By SynopsisAllTextLocator = By.XPath(".//div[contains(@class,'co_searchResults_synopsisContent')]");

        // GetWordsBeforeFirstHitTermRegex const should be adjusted in case of SnippetLocator change
        private static readonly By SnippetLocator = By.XPath(".//*[contains(@ng-repeat, 'snippet') or contains(@class, 'co_snippet')]//a");

        private static readonly By HighlightingLocator = By.XPath(".//span[contains(@class,'co_searchTerm')]");

        private static readonly By HighlightingInSummaryLocator = By.XPath("./span[@class = 'co_searchTerm co_keyword']");

        private static readonly By FocusTermInSummaryLocator = By.XPath(".//span[contains(@class, 'termNav_focusHighlight')]");

        private static readonly By NewRecommendationIconLocator = By.Id("coid_folderAnalysisRecommendations_newRecommendation");

        private static readonly By BinaryLinkLocator = By.ClassName("co_blobLink");

        private static readonly By MissingTermsLocator = By.XPath(".//div[contains(@class, 'co_missingTerms co_searchResult_containerList')]");

        private static readonly By SnippetNavigationLocator = By.ClassName("snippetNavigationPanelControl");

        private static readonly By CitationsFrequencyNoteLocator = By.XPath(".//div[@class='crsw_caseSearchResults_citeFreqNote']/span");

        /// <summary>
        /// The key cite element.
        /// </summary>
        private IWebElement KeyCiteElement => DriverExtensions.SafeGetElement(this.Container, KeyCiteFlagLocator);

        private readonly List<string> jurisdictionKeyWords = new List<string> { "Court" };

        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeResultListItem"/> class. 
        /// The constructor
        /// </summary>
        /// <param name="container"> The container. </param>
        public EdgeResultListItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// The term color map.
        /// </summary>
        private EnumPropertyMapper<TermColors, FocusHighlightingTermInfo> termColorMap;

        /// <summary>
        /// Gets the TermColors enumeration to FocusHighlightingTermInfo map.
        /// </summary>
        protected EnumPropertyMapper<TermColors, FocusHighlightingTermInfo> TermColorMap =>
            this.termColorMap = this.termColorMap
                                ?? EnumPropertyModelCache.GetMap<TermColors, FocusHighlightingTermInfo>(
                                    string.Empty,
                                    @"Resources/EnumPropertyMaps/WestlawEdge/FocusHighlighting");

        #region public properties

        /// <summary>
        /// Snippet Navigation component
        /// </summary>
        public EdgeSnippetNavigationComponent SnippetNavigation => new EdgeSnippetNavigationComponent(this.Container);

        /// <summary>
        /// No snippet navigation component
        /// </summary>
        public NoSnippetsComponent NoSnippets => new NoSnippetsComponent(this.Container);

        /// <summary>
        /// The get item title text
        /// </summary>
        /// <returns></returns>
        public string TitleText => this.TitleElement.Text;

        /// <summary>
        /// The CitingReferences link present.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsCitingReferencesLinkDisplayed => this.CitingReferencesLink.IsDisplayed();

        /// <summary>
        /// Returns citing references count for document
        /// </summary>
        public int CitingReferencesCount => this.CitingReferencesLink?.Text.ConvertCountToInt() ?? 0;

        /// <summary>
        /// The Implied Overruling present.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsImpliedOverrulingPresent => this.ImpliedOverrulingElement != null;

        /// <summary>
        /// The KeyCite present.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsKeyCitePresent => this.KeyCiteElement != null;


        /// <summary>
        /// Get dictionary with term-color pairs
        /// </summary>
        public Dictionary<string, TermColors> TermsColors => this.GetTermsColors();

        /// <summary>
        /// The get Snippets text.
        /// </summary>
        /// <returns></returns>
        public List<string> SnippetsText => this.SnippetsList.Select(element => element.Text).ToList();

        /// <summary>
        /// Gets index of item.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Index
        {
            get
            {
                string text = DriverExtensions.GetElement(this.Container, IndexLocator).Text;
                return text.ConvertCountToInt();
            }
        }

        /// <summary>
        /// The get court level.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string CourtLevel =>
            DriverExtensions.GetElements(this.Container, ItemDetailsLocator)?.FirstOrDefault(
                element => this.jurisdictionKeyWords.Any(word => element.Text.Contains(word)))?.Text;

        /// <summary>
        /// Check if there are highlighted terms in Synopsis
        /// </summary>
        /// <returns></returns>
        public bool IsHighlightedInSynopsis =>
            this.SynopsisElement != null
            && DriverExtensions.GetElements(this.SynopsisElement, HighlightingLocator).Any();

        /// <summary>
        /// Get all highlighted terms in Synopsis
        /// </summary>
        /// <returns></returns>
        public List<string> HighlightedTextInSynopsis =>
            DriverExtensions.GetElements(this.SynopsisElement, HighlightingLocator).Select(i => i.Text).ToList();

        /// <summary>
        /// Get all highlighted terms in Snippets
        /// </summary>
        /// <returns></returns>
        public List<string> HighlightedTextInSnippets =>
            DriverExtensions.GetElements(this.Container, SnippetLocator, HighlightingLocator).Select(i => i.Text).ToList();

        /// <summary>
        /// Highlighted terms in Summary
        /// </summary>
        /// <returns></returns>
        public List<string> HighlightedTextInSummary => DriverExtensions.GetElements(this.SummaryElement, HighlightingLocator).Select(item => item.Text).ToList();

        /// <summary>
        /// Check if there are highlighted terms in Summary
        /// </summary>
        /// <returns></returns>
        public bool IsHighlightedInSummary =>
            this.SummaryElement != null && DriverExtensions.GetElements(this.SummaryElement, HighlightingLocator).Any();

        /// <summary>
        /// The present snippet.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSnippetDisplayed => !DriverExtensions.GetElements(this.Container, SnippetLocator).First().GetAttribute("class").Contains("Athens-hide");

        /// <summary>
        /// The present synopsis link.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSynopsisDisplayed =>
            DriverExtensions.SafeGetElement(this.Container, SynopsisLinkLocator)?.Displayed ?? false;

        /// <summary>
        /// Check if there are highlighted terms in Summary
        /// </summary>
        /// <returns></returns>
        public bool IsHighlightedInSnippets =>
            this.SnippetsList.Any()
            && this.SnippetsList.Any(x => DriverExtensions.GetElements(x, HighlightingLocator).Any());

        /// <summary>
        /// The disclosed synopsis link.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSynopsisExpanded =>
            DriverExtensions.SafeGetElement(this.Container, SynopsisAllTextLocator)?.Displayed ?? false;

        /// <summary>
        /// The get synopsis text.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string SynopsisText =>
            DriverExtensions.SafeGetElement(this.Container, SynopsisAllTextLocator)?.Text ?? string.Empty;

        /// <summary>
        /// Returns the Text in the Missing Terms block 
        /// </summary>
        public ILabel MissingTerms => new Label(this.Container, MissingTermsLocator);

        /// <summary>
        /// Verified if result list items has SnippetNavigation
        /// </summary>
        public bool IsSnippetNavigationDisplayed =>
            DriverExtensions.SafeGetElement(this.Container, SnippetNavigationLocator)?.Displayed ?? false;

        #endregion

        #region Elements
        /// <summary>
        /// Tab title
        /// </summary>
        private IWebElement TitleElement => DriverExtensions.GetElement(this.Container, TitleLocator);

        /// <summary>
        /// The checkbox
        /// </summary>
        private IWebElement CheckboxElement => DriverExtensions.GetElement(this.Container, CheckboxLocator);

        /// <summary>
        /// The summary element.
        /// </summary>
        private IWebElement SummaryElement => DriverExtensions.SafeGetElement(this.Container, SummaryLocator);

        /// <summary>
        /// The citing references link element.
        /// </summary>
        private IWebElement CitingReferencesLink => DriverExtensions.SafeGetElement(this.Container, CitingReferencesLinkLocator);

        /// <summary>
        /// The key cite element.
        /// </summary>
        private IWebElement ImpliedOverrulingElement => DriverExtensions.SafeGetElement(this.Container, ImpliedOverrulingLocator);

        /// <summary>
        /// The Synopsis element
        /// </summary>
        private IWebElement SynopsisElement => DriverExtensions.SafeGetElement(this.Container, SynopsisAllTextLocator);

        /// <summary>
        /// The Binary Link element
        /// </summary>
        private IWebElement BinaryLink => DriverExtensions.SafeGetElement(this.Container, BinaryLinkLocator);

        /// <summary>
        /// Search snippets
        /// </summary>
        private List<IWebElement> SnippetsList =>
            DriverExtensions.GetElements(this.Container, SnippetLocator).Where(x => x.Displayed).ToList();
        #endregion

        #region Public methods
        /// <summary>
        /// The click synopsis link.
        /// </summary>
        public void ClickSynopsisLink()
        {
            DriverExtensions.Click(this.Container, SynopsisLinkLocator);
            DriverExtensions.WaitForJavaScript();
        }


        /// <summary>
        /// Citations Frequency note label
        /// </summary>
        public ILabel CitationsFrequencyNotes =>
            new Label(this.Container, CitationsFrequencyNoteLocator);

        /// <summary>
        /// Click on desired snippet
        /// </summary>
        /// <typeparam name="T">Page type</typeparam>
        /// <param name="itemNumber"> the number of item</param>
        /// <returns>new object of type T</returns>
        public override T ClickSnippet<T>(int itemNumber = 0)
        {
            this.SnippetsList[itemNumber].Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get words before the first hit term for each snippet
        /// </summary>
        /// <returns>List of words before the first hit term for each snippet</returns>
        public List<string> GetListOfSnippetsPhrasesBeforeFirstHitTerm() => this.SnippetsList.Select(x => Regex.Match(x.InnerHtml(), GetWordsBeforeFirstHitTermRegex).Value).ToList();

        /// <summary>
        /// The is item new.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsItemNew() => DriverExtensions.IsDisplayed(this.Container, NewRecommendationIconLocator);

        /// <summary>
        /// Check if there are highlighted Search Within terms in Summary
        /// </summary>
        /// <param name="searchTerm">
        /// The search Term.
        /// </param>
        /// <returns>
        /// true if there are highlighted Search Within terms in Summary
        /// </returns>
        public bool IsSearchWithinHighlightedInSummary(string searchTerm) =>
            this.IsSearchWithinTermHighlightedInElement(searchTerm, this.SummaryElement, HighlightingInSummaryLocator);

        /// <summary>
        /// Checks whether a focus term is displayed in summary section
        /// </summary>
        /// <param name="focusTerm">
        /// The search Term.
        /// </param>
        /// <returns>
        /// True if a term is displayed, false otherwise
        /// </returns>
        public bool IsFocusTermDisplayedInSummary(string focusTerm)
        => DriverExtensions.IsDisplayed(DriverExtensions.GetElements(this.SummaryElement, FocusTermInSummaryLocator)
                                                        .FirstOrDefault(term => term.Text.Contains(focusTerm, StringComparison.InvariantCultureIgnoreCase)));

        /// <summary>
        /// Method retrieves the list of Missing Terms for a given document
        /// </summary>
        /// <param name="docGuid">Guid of document for which Missing Terms are to be retrieved</param>
        /// <param name="docIndex">Index of document in result set to pass to selector</param>
        /// <param name="numberOfTerms">Number of Missing Terms expected</param>
        /// <returns>List of Missing Terms</returns>
        public List<string> GetMissingTermsList(string docGuid, int docIndex, int numberOfTerms)
        {
            var missingTermsList = new List<string>();
            missingTermsList.Add(DriverExtensions.GetElement(By.CssSelector(string.Format(FirstMissingTermLctMask, docIndex))).Text);

            for (int i = 1; i < numberOfTerms; i++)
            {
                missingTermsList.Add(DriverExtensions.GetElement(By.XPath(string.Format(MissingTermsLctMask, docGuid, i))).Text);
            }
            return missingTermsList;
        }

        /// <summary>
        /// Method retrieves the Must Include terms for a given document and returns them in a list
        /// </summary>
        /// <param name="docGuid">Guid of document for which Must Include are to be retrieved</param>
        /// <param name="numberOfTerms">Number of Must Include expected</param>
        /// <returns>List of Must Include terms found for document</returns>
        public List<string> GetMustIncludeTermsList(string docGuid, int numberOfTerms)
        {
            var mustIncludeTermsList = new List<string>();

            for (int i = 1; i <= numberOfTerms; i++)
            {
                mustIncludeTermsList.Add(
                    DriverExtensions.GetElement(By.XPath(string.Format(MustIncludeLctMask, docGuid, i))).Text);
            }

            return mustIncludeTermsList;
        }

        /// <summary>
        /// This method clicks an item in the Must Include list
        /// </summary>
        /// <param name="docGuid">Click Must Include term for this docGuid</param>
        /// <param name="position">Number/position of the Must Include term in the list</param>
        /// <returns>Search Result page that is displayed when the query is re-run </returns>
        public EdgeCommonSearchResultPage ClickMustIncludeTerm(string docGuid, int position)
        {
            string selector = string.Format(MustIncludeLctMask, docGuid, position);

            if (position == 0)
                selector = selector.Remove(IndexOfFirsSquareBracket); //When there is a single term in the Must Include list, the selector does not include a position for the button, hence removing the [x] section of the string.

            DriverExtensions.GetElement(By.XPath(selector)).Click();
            return new EdgeCommonSearchResultPage();
        }

        #endregion

        /// <summary>
        /// Get draggable element
        /// </summary>
        /// <returns>Get draggable IWebElement</returns>
        IWebElement IDraggableWebElement.GetDraggableElement() => DriverExtensions.GetElement(this.Container, TitleLocator);

        /// <summary>
        /// The click citing references link.
        /// </summary>
        /// <returns>
        /// The <see cref="EdgeCitingReferencesPage"/>.
        /// </returns>
        internal EdgeCitingReferencesPage ClickCitingReferencesLink()
        {
            this.CitingReferencesLink?.Click();
            return new EdgeCitingReferencesPage();
        }

        /// <summary>
        /// The click Implied Overruling flag
        /// </summary>
        /// <returns>
        /// The new item of T class
        /// </returns>
        internal KeyCiteFlagDialog ClickImpliedOverrulingFlag()
        {
            this.ImpliedOverrulingElement?.Click();
            return new KeyCiteFlagDialog();
        }

        /// <summary>
        /// The click binary link.
        /// </summary>
        internal void ClickBinaryLink()
        {
            this.BinaryLink?.Click();
        }

        /// <summary>
        /// The set checkbox.
        /// </summary>
        /// <param name="selected">
        /// The selected.
        /// </param>
        internal void SetCheckbox(bool selected) => this.CheckboxElement.SetCheckbox(selected);

        /// <summary>
        /// Get terms colors
        /// </summary>
        /// <returns>Dictionary with term-color values</returns>
        private Dictionary<string, TermColors> GetTermsColors()
        {
            var termsColors = new Dictionary<string, TermColors>();

            DriverExtensions.GetElements(this.Container, HighlightingLocator).ToList()
                            .ForEach(term => termsColors[term.Text] = this.GetColorTypeByCode(term.GetCssValue("background-color")));

            return termsColors;
        }

        /// <summary>
        /// The Search Within term highlighting is present for element.
        /// </summary>
        /// <param name="searchTerm">
        /// The search Term.
        /// </param>
        /// <param name="element">
        /// The element.
        /// </param>
        ///  /// <param name="locator">
        /// The locator.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool IsSearchWithinTermHighlightedInElement(string searchTerm, IWebElement element, By locator) =>
            DriverExtensions.GetElements(element, locator).
                             Any(highlighted => highlighted.Text.ToLower().Contains(searchTerm.ToLower()) && highlighted.GetCssValue("background-color").Contains(PurpleColor));

        /// <summary>
        /// Get color type by a code
        /// </summary>
        /// <param name="termCode">Term color rgb code</param>
        /// <returns>Term color</returns>
        private TermColors GetColorTypeByCode(string termCode) => Enum.GetValues(typeof(TermColors)).Cast<TermColors>()
            .First(color => this.TermColorMap[color].BackgroundColorCode.Equals(termCode));
    
        /// <summary>
        /// Check if summary is displayed
        /// </summary>
        /// <returns>True if summary element is displayed, false otherwise</returns>
        public bool IsSummaryDisplayed => this.SummaryElement?.Displayed ?? false;
    }
}