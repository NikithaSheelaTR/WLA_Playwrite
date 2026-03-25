namespace Framework.Common.UI.Products.Shared.Pages.Document
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Framework.Common.UI.Enums;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Document;
    using Framework.Common.UI.Products.Shared.Components.Document.RI;
    using Framework.Common.UI.Products.Shared.Components.Facets.RightFacets;
    using Framework.Common.UI.Products.Shared.Components.Toolbar;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Dialogs.Document.Notes;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Enums.Document;
    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Products.Shared.Models.Annotations;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Components.EdgeResponsive;
    using Framework.Common.UI.Products.WestLawNext.Dialogs.Header;
    using Framework.Common.UI.Products.WestLawNext.Models.IpTools;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;
    using Framework.Common.UI.Utils;
    using Framework.Common.UI.Utils.Enum;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.Utils.Enums;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements;


    /// <summary>
    /// Common Document Page
    /// </summary>
    public class CommonDocumentPage : CommonAuthenticatedWestlawNextPage, ICommonDocumentPage
    {
        private const string HighlightLctMask = "//span[contains(@class, 'co_hl') and contains(text(),\"{0}\")]";
        private const string HighlightByIndexLctMask = "//span[contains(@id, 'co_selection_{0}')]";
        private const string CoCitesElementLctMask = "//div[@class='co_cites'][contains(.,'{0}')]";
        private const string TermTextLctMask = "//span[contains(@id,'co_term_{0}')]";
        private const string TextLctMask = "//*[contains(text(),'{0}')] | //*[text()[contains(.,'{0}')]]";
        private const string ImgLctmask = "//img[contains(@alt,{0})]";
        private const string NavigatedElementLctMask = "(//h2[@id={0}]/self::*[string-length() > 0] | //h2[@id={0}]/following::*[string-length() > 0])[1]";
        private const string NoteByIndexLctMask = "//div[@id= 'co_note_{0}']//div[contains(@class, 'co_viewNote')]";
        private const string NoteByTextLctMask = "//div[contains(@class, 'co_viewNote') and contains(text(), {0})]";
        private const string NoteIconHideStateLctMask = "//div[text() = '{0}']//ancestor::span[contains(@class, 'noteHolder')]//a[contains(@class, 'Maximize')]";
        private const string CoLinkIdLctMask = "co_link_{0}";
        private const string DocumentLinkItemLctMask = "//div[@class='co_paragraphText']//a[text()='{0}']";
        private const string SnippetTextLctMask = "//div[text()='{0}']";

        private static readonly By DocumentFlagLocator = By.XPath(".//a[contains(@class,'Flag')]");
        private static readonly By HighlightLocator = By.XPath("//span[contains(@id, 'co_selection_')]");
        private static readonly By HighlightedSnippetLocator = By.CssSelector("#co_selection_snippet");
        private static readonly By NoteLocator = By.ClassName("co_noteContainer");
        private static readonly By DocLevelNotesContainerLocator = By.Id("co_documentNotes");
        private static readonly By InlineNotesLocator = By.XPath("//span[contains(@id,'co_noteHolder_') and .//div[@class='co_noteContainer']]");
        private static readonly By EditorsAndRevisorsNotesLocator = By.XPath("//div[@class='co_historyNotes co_disableHighlightFeatures']");
        private static readonly By LostNoteLocator = By.Id("co_lostNotes");
        private static readonly By AttorneyLinkLocator = By.XPath("//div[contains(@class, 'co_attorneyBlock')]//div//a");

        private static readonly By BeginTextToBeSelectedBy = By.XPath("//*[@class='co_document_moreless_text co_ellipsis']");
        private static readonly By EndTextToBeSelectedBy = By.XPath("//*[text()='Target']");

        private static readonly By BestPortionArrowLocator = By.XPath("//*[contains(@class, 'co_pinpointIcon')]");
        private static readonly By ContentBlockPreformattedLocator = By.CssSelector("div.co_contentBlock.co_preformattedTextBlock");
        private static readonly By DocumentTitleLocator = By.XPath("//*[@class='co_title']");
        private static readonly By OutOfPlanDocumentTitleLocator = By.XPath("//div[@id='coid_infiniteScrollWidgetContainer']/div[contains(@id,'documentContainer')][last()]//following-sibling::div[@class='co_title co_headtext']");
        private static readonly By DocumentHeadingLocator = By.XPath("//h2[contains(@class,'co_printHeading')]");
        private static readonly By NetscanLogoLocator = By.ClassName("co_netscan");
        private static readonly By HideAnnotationsLocator = By.XPath("//div[@id='coid_website_documentWidgetDiv' and contains(@class,'co_hideAnnotations')]");
        private static readonly By DottedBoxLocator = By.XPath("//*[@class='co_hl co_selection_snippet co_hlShared firstChunk']");
        private static readonly By EffectiveDateLocator = By.ClassName("co_effectiveDate");
        private static readonly By EndOfDocumentTextLocator = By.XPath("//table[@id='co_endOfDocument']/tbody/tr/td");
        private static readonly By SnippetInfoMessageLocator = By.CssSelector(".co_foldering_popupMessageContainer .co_infoBox_message strong");
        private static readonly By LeaderDotsParagraphLocator = By.ClassName("co_leaderDots");
        private static readonly By MoreLessLinkLocator = By.XPath("//a[contains(@class,'co_floatRight co_moreLessLink')]/div");
        private static readonly By NotesOfDecisionsLink = By.ClassName("co_notesOfDecisionsLink");
        private static readonly By PdfLinkTextLocator = By.XPath("//a[contains(@type, 'application/pdf')]");
        private static readonly By SearchWithinPurpleTermLocator = By.ClassName("co_locateTerm");
        private static readonly By SearchWithinGreenTermLocator = By.ClassName("co_searchWithinTerm");
        private static readonly By SearchWithinGreenTermLinkLocator = By.PartialLinkText("EDCR");
        private static readonly By YellowAndPurpleSearchTermLocator = By.CssSelector(".co_searchTerm, .co_locateTerm");
        private static readonly By HighlightedSearchTermLocator = By.XPath("//*[contains(@class, 'co_searchTerm') and not(contains(@class,'co_locateTerm'))]");
        private static readonly By SharedByMessageLocator = By.CssSelector("#createdBy");
        private static readonly By SharedIconLocator = By.XPath(".//div[@class='icon25 icon_people']");
        private static readonly By TopicalHeaderLocator = By.ClassName("x_topicalHead");
        private static readonly By CopyrightMessageLocator = By.XPath("//*[contains(@class, 'Copyright')]");
        private static readonly By DocLevelNoteToggleLocator = By.XPath("//a[contains(@class, 'co_noteIcon_toggle')]");
        private static readonly By PdfLinkLocator = By.XPath("//*[contains(@class,'co_pdfIcon')]");
        private static readonly By BestPortionArrowForSnippets = By.XPath("//a[contains(@id,'co_snip')]/following-sibling::span[@class='co_pinpointIcon']");
        private static readonly By LeftColumnLocator = By.XPath("//body[contains(@class, 'ShowLeftColumn')]");
        private static readonly By SeeFullProfileLocator = By.Id("linkToProfile");

        private EnumPropertyMapper<DocumentSection, WebElementInfo> docSectionsMap;
        private EnumPropertyMapper<HighlightColor, WebElementInfo> highlightColor;

        /// <summary>
        /// Pdf Link
        /// </summary>
        public ILink PdfLink => new Link(PdfLinkLocator);

        /// <summary> Document Fixed Header Component </summary>
        public DocumentFixedHeaderComponent FixedHeader { get; } = new DocumentFixedHeaderComponent();

        /// <summary> KeyCite Flag Widget Component </summary>
        public KeyCiteFlagComponent KeyCiteFlagWidgetComponent { get; } = new KeyCiteFlagComponent();

        /// <summary> Citation Block </summary>
        public DocumentCitationComponent CitationBlock { get; } = new DocumentCitationComponent();

        /// <summary> Related Topics Pane  </summary>
        public RelatedTopicsComponent RelatedTopics { get; } = new RelatedTopicsComponent();

        /// <summary> Related info tabs </summary>
        public RelatedInfoTabComponent RiTabs { get; } = new RelatedInfoTabComponent();

        /// <summary> Table of Contents Component </summary>
        public TableOfContentsComponent TableOfContentsComponent { get; } = new TableOfContentsComponent();

        /// <summary> Toolbar component  </summary>
        public Toolbar Toolbar { get; } = new Toolbar();

        /// <summary> Tools Pane </summary>
        public ToolsAndResourcesFacetComponent ToolsAndResourcesFacet { get; } = new ToolsAndResourcesFacetComponent();

        /// <summary> 
        /// Tools Right Panel component for responsive
        /// </summary>
        public ToolsRightPanelComponent ResponsiveRightPanel { get; } = new ToolsRightPanelComponent();

        /// <summary> 
        /// Left Panel component for responsive
        /// </summary>
        public ContentTypesLeftPanelComponent ResponsiveLeftPanel { get; } = new ContentTypesLeftPanelComponent();

        /// <summary>
        /// Gets the DocumentSection enumeration to WebElementInfo map.
        /// </summary>
        internal EnumPropertyMapper<DocumentSection, WebElementInfo> DocSectionsMap
            => this.docSectionsMap = this.docSectionsMap ?? EnumPropertyModelCache.GetMap<DocumentSection, WebElementInfo>();

        /// <summary>
        /// Gets the KeyCiteFlag enumeration to WebElementInfo map.
        /// </summary>
        protected virtual EnumPropertyMapper<KeyCiteFlag, WebElementInfo> KeyCiteFlagsMap
            => EnumPropertyModelCache.GetMap<KeyCiteFlag, WebElementInfo>();

        /// <summary>
        /// Gets the Color to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<HighlightColor, WebElementInfo> HighlightingColor =>
            this.highlightColor = this.highlightColor
                                      ?? EnumPropertyModelCache.GetMap<HighlightColor, WebElementInfo>();

        /// <summary>
        /// Gets the document title from document body content.InlineKeyCiteFlagLocator
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetDocumentBodyTitle() => DriverExtensions.WaitForElement(DocumentTitleLocator).GetText();

        /// <summary>
        /// Verify that search within terms are displayed
        /// </summary>
        /// <param name="terms"> The terms. </param>
        /// <returns> True if displayed, false otherwise </returns>
        public bool AreSearchWithinTermsDisplayed(List<string> terms)
        {
            List<string> displayedTerms = this.GetSearchWithinHighlightedTerms();
            return terms.TrueForAll(term => displayedTerms.Contains(term, StringComparer.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Get document section text by document section index
        /// </summary>
        /// <param name="docSection"> The doc Section. </param>
        /// <param name="paragraphIndex"> The paragraph Index. </param>
        /// <returns> Text from document section </returns>
        public virtual string GetDocumentSectionText(DocumentSection docSection, int paragraphIndex = 0)
            => DriverExtensions.GetElements(By.CssSelector(this.DocSectionsMap[docSection].LocatorString)).ElementAt(paragraphIndex).Text;

        /// <summary>
        /// Get text from the first document section
        /// </summary>
        /// <param name="docSection"> Document section </param>
        /// <returns> Text from the first section </returns>
        public string GetFirstSectionText(DocumentSection docSection)
            => DriverExtensions.WaitForElementDisplayed(By.CssSelector(this.DocSectionsMap[docSection].LocatorString)).Text;

        /// <summary>
        /// Click by Notes of Decisions link
        /// </summary>
        /// <returns>Notes of Decisions page</returns>
        public NotesOfDecisionsPage ClickNotesOfDecisionsLink()
        {
            DriverExtensions.WaitForElementDisplayed(NotesOfDecisionsLink).Click();
            return new NotesOfDecisionsPage();
        }

        /// <summary>
        /// Navigate to a DocumentPage by clicking on a link in the current DocumentPage
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="linkId"> Link ID</param>
        /// <returns> Document Page from the link that was clicked on </returns>
        public T ClickLinkById<T>(string linkId) where T : CommonAuthenticatedWestlawNextPage
        {
            DriverExtensions.WaitForElement(By.Id(linkId)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// The click out of plan if displayed.
        /// </summary>
        public void ClickOutOfPlanButtonIfDisplayed()
        {
            if (this.IsDisplayed(Dialogs.OutOfPlan))
            {
                new OutOfPlanDialog().ClickViewDocumentButton<CommonDocumentPage>();
            }
        }

        /// <summary>
        /// Click on paragraph location on the current page
        /// </summary>      
        /// <param name="index">Index of the paragraph</param>
        public void ClickParagraph(int index)
            => DriverExtensions.GetElements(By.CssSelector(this.DocSectionsMap[DocumentSection.Paragraph].LocatorString))
                                .ElementAt(index).Click();

        /// <summary>
        /// Cites Location
        /// </summary>
        /// <param name="citesValue">value of cites</param>
        /// <returns>Location of cites</returns>
        public Point GetCitesLocation(string citesValue)
            => DriverExtensions.WaitForElement(By.XPath(string.Format(CoCitesElementLctMask, citesValue))).Location;

        /// <summary>
        /// Copy selected text to memory and Paste to the Node field
        /// </summary>
        public void CopyAndPasteSelectedTextToNote()
        {
            DriverExtensions.PressKey(Keys.Control + "c");
            var editNoteDialog = this.Toolbar.AnnotationsDropdown.SelectOption<EditNoteDialog>(AnnotationsOption.AddNote);
            DriverExtensions.PressKey(Keys.Control + "v");
            editNoteDialog.TypeNote(Keys.Control + "v");
            editNoteDialog.ClickSave<CommonDocumentPage>();
        }

        /// <summary>
        /// Verifies is Netscan logo displayed.
        /// </summary>
        /// <returns>true if Netscan logo displayed, false otherwise</returns>
        public bool IsNetscanLogoDisplayed() => DriverExtensions.IsDisplayed(NetscanLogoLocator);

        /// <summary>
        /// Is document level note collapsed
        /// </summary>
        /// <returns>True if the note icon toggle(plus) is displayed, false otherwise</returns>
        public bool IsDocumentLevelNoteCollapsed() => !DriverExtensions.GetElement(DocLevelNoteToggleLocator).GetAttribute("class").Contains("close");

        /// <summary>
        /// Click doc level note toggle
        /// </summary>
        /// <param name="expand">True to expand a dialog, false to collapse</param>
        public void ClickDocLevelNoteToggle(bool expand)
        {
            if (expand == this.IsDocumentLevelNoteCollapsed())
            {
                DriverExtensions.GetElement(DocLevelNoteToggleLocator).Click();
            }
        }

        /// <summary>
        /// Expand a Collapsed Text
        /// </summary>
        public void ExpandCollapsedText() => DriverExtensions.GetElement(MoreLessLinkLocator).Click();

        /// <summary>
        /// Gets a list of the attorneys for the document
        /// </summary>
        /// <returns>List of attorneys for the document</returns>
        public List<string> GetAttorneys() => DriverExtensions.GetElements(AttorneyLinkLocator).Select(e => e.Text).ToList();

        /// <summary>
        /// Returns the id of the current displayed chunk
        /// </summary>
        /// <returns>id of the current displayed chunk</returns>
        public string GetDocumentId()
            => DriverExtensions.WaitForElement(By.CssSelector(this.DocSectionsMap[DocumentSection.DocumentContainer].LocatorString + " div")).GetAttribute("id");

        /// <summary>
        /// Gets text from the end of document.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetEndOfDocumentText() => DriverExtensions.GetText(EndOfDocumentTextLocator);

        /// <summary>
        /// Return list of strings for all highlighted text in the document content.
        /// </summary>
        /// <returns>List of strings</returns>
        public List<string> GetDocumentSearchHighlights()
            => this.GetSearchTermsList().Where(e => e.Displayed).Select(e => e.Text.Trim()).ToList();

        /// <summary>
        /// Gets count of search terms.
        /// </summary>
        /// <returns> The <see cref="int"/>. Count of search terms </returns>
        public int GetSearchTermsCount() => this.GetSearchTermsList().Count;

        /// <summary>
        /// Checks if search term is scrolled into view
        /// </summary>
        /// <param name="index">Zero-based index of the search term that will be checked</param>
        /// <returns>True if search term is scrolled into view, false otherwise</returns>
        public bool IsSearchTermScrolledIntoView(int index)
            => DriverExtensions.GetElements(YellowAndPurpleSearchTermLocator).ElementAt(index).IsElementInView();

        /// <summary>
        /// Get Document Title For further Delivery
        /// </summary>
        /// <returns>file name </returns>
        public string GetDocumentNameForDelivery()
        {
            string title =
                $"{this.FixedHeader.GetTitleText().Replace("&", "And").Replace(".", string.Empty).Replace(",", string.Empty).Replace("§", string.Empty).Replace(":", string.Empty).Trim()}.pdf";

            return title.Length < 80 ? title : string.Concat(title.Substring(0, 80), ".pdf");
        }

        /// <summary>
        /// This returns a string of the effective date on the current document
        /// </summary>
        /// <returns>The Effective Date of a given document</returns>
        public string GetEffectiveDate() => DriverExtensions.SafeGetElement(EffectiveDateLocator)?.Text ?? string.Empty;

        /// <summary>
        /// Return Font-Weight value of Effective date text
        /// </summary>
        /// <returns> Font-Weight value of Effective date </returns>
        public string GetEffectiveDateFontWeight()
            => DriverExtensions.GetElement(EffectiveDateLocator).GetComputedStylePropertyValue("font-weight");

        /// <summary>
        /// Returns a series of Highlight terms for the current document.
        /// </summary>
        /// <param name="terms">
        /// The terms.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetHighlightedTerms(List<string> terms) => string.Join(string.Empty, terms.Select(this.GetTermText).ToList());

        /// <summary>
        /// Get list of document headings
        /// </summary>
        /// <returns> Document headings </returns>
        public List<string> GetListOfDocumentHeadings() => DriverExtensions.GetElements(DocumentHeadingLocator).Select(e => e.Text).ToList();

        /// <summary>
        /// The get lost note count.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetLostNoteCount() => DriverExtensions.GetElements(LostNoteLocator).Count;

        /// <summary>
        /// Gets the value of the PDF link on the current document page
        /// </summary>
        /// <returns>
        /// String value of the PDF link, if it is displayed
        /// Or empty string, if it is not displayed
        /// </returns>
        public string GetPdfLinkText() =>
            DriverExtensions.IsDisplayed(PdfLinkTextLocator) ? DriverExtensions.WaitForElement(PdfLinkTextLocator).Text : string.Empty;

        /// <summary>
        /// The get highlighted search within terms with purple color.
        /// </summary>
        /// <returns>List of purple highlighted terms</returns>
        public List<string> GetPurpleHighlightedSearchWithinTerms()
            => DriverExtensions.GetElements(SearchWithinPurpleTermLocator).Select(el => el.Text).ToList();

        /// <summary>
        /// Return list of strings for search within highlighted text in the document content.
        /// </summary>
        /// <returns>List of strings</returns>
        public List<string> GetSearchWithinHighlightedTerms() => DriverExtensions.GetElements(SearchWithinGreenTermLocator).Select(elem => elem.Text).ToList();

        /// <summary>
        /// gets the text of a term from a specified number
        /// </summary>
        /// <param name="termnum">number of the term that is being searched for in string form</param>
        /// <returns>returns text of the string being searched for</returns>
        public string GetTermText(string termnum) => DriverExtensions.WaitForElement(By.XPath(string.Format(TermTextLctMask, termnum))).Text;

        /// <summary>
        /// Verify that search terms are displayed
        /// </summary>
        /// <param name="terms"> The terms. </param>
        /// <returns> True if displayed, false otherwise </returns>
        public bool AreSearchTermsDisplayed(List<string> terms)
            => terms.TrueForAll(term => DriverExtensions.IsDisplayed(By.XPath(string.Format(TermTextLctMask, term))));

        /// <summary>
        /// Checks if a term is in view
        /// </summary>
        /// <param name="idIndex">term id index.</param>
        /// <returns>True if search term is scrolled into view, false otherwise</returns>
        public bool IsHighlightedSearchTermScrolledIntoView(int idIndex)
        {
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.GetElement(By.XPath(string.Format(TermTextLctMask, idIndex))).IsElementInView();
        }

        /// <summary>
        /// Verify if flag near the link in the document is displayed
        /// </summary>
        /// <param name="documentLink"> Document link </param>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsFlagForDocumentLinkItemDisplayed(string documentLink)
        {
            IWebElement linkElement = DriverExtensions.WaitForElementDisplayed(By.XPath(string.Format(DocumentLinkItemLctMask, documentLink)));
            return DriverExtensions.IsDisplayed(linkElement, DocumentFlagLocator);
        }

        /// <summary>
        /// Gets paragraphs with leader dots.
        /// </summary>
        /// <returns>List of paragraphs with leader dots. </returns>
        public List<string> GetParagraphsWithLeaderDots() =>
            DriverExtensions.GetElements(LeaderDotsParagraphLocator).Select(p => p.Text.Trim()).ToList();

        /// <summary>
        /// Get topical header
        /// </summary>
        /// <returns>Topical header</returns>
        public string GetTopicalHeader() => DriverExtensions.WaitForElement(TopicalHeaderLocator).Text;

        /// <summary>
        /// Goes to the end of the current document
        /// </summary>
        public void GoToEndOfDocument() => DriverExtensions.WaitForElement(EndOfDocumentTextLocator).Click();

        /// <summary>
        /// The hover link.
        /// </summary>
        /// <param name="link"> The link. </param>
        public void HoverLink(string link) => DriverExtensions.Hover(By.LinkText(link));

        /// <summary>
        /// Verify if best portion green arrow is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsBestPortionArrowDisplayed() => DriverExtensions.IsDisplayed(BestPortionArrowLocator, 5);

        /// <summary>
        /// Verify if best portion green arrow is in view
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsBestPortionArrowInView() => DriverExtensions.WaitForElement(BestPortionArrowLocator).IsElementInView();

        /// <summary>
        /// Verify if green arrow for snippet is displayed
        /// </summary>
        /// <returns> Boolean value </returns>
        public bool IsBestPortionArrowForSnippetsDisplayed() => DriverExtensions.IsDisplayed(BestPortionArrowForSnippets);

        /// <summary>
        /// Checks if the current document has loaded
        /// </summary>
        /// <returns>Boolean Value</returns>
        public bool IsDocumentLoaded() => DriverExtensions.WaitForElement(By.Id("co_document")).Displayed; // does not guarantee the document loaded

        /// <summary>
        /// Verifies dotted box appears around the snippet text in the snippet document.
        /// </summary>
        /// <returns>True if DottedBox displayed, False otherwise</returns>
        public bool IsDottedBoxPresent() => DriverExtensions.IsDisplayed(DottedBoxLocator, 5);

        /// <summary>
        /// Verifies whether the part from the Editor's and Revisor's Notes is displayed
        /// </summary>
        /// <returns> true if the tab part is present </returns>
        public bool IsEditorsAndRevisorsPartDisplayed() => DriverExtensions.IsDisplayed(EditorsAndRevisorsNotesLocator);

        /// <summary>
        /// Checks if the the end of document displayed
        /// </summary>
        /// <returns>Boolean Value</returns>
        public bool IsEndOfDocumentTextDisplayed() => DriverExtensions.IsDisplayed(EndOfDocumentTextLocator);

        /// <summary>
        /// Get Link by Id
        /// </summary>
        /// <param name="linkId"> ID link </param>
        /// <returns> Link </returns>
        public string GetLinkValueByLinkId(string linkId) => DriverExtensions.WaitForElement(By.Id(linkId)).GetAttribute("href");

        /// <summary>
        /// Get Link by Link Text
        /// </summary>
        /// <param name="linkText"> Link Text </param>
        /// <returns> Link </returns>
        public string GetLinkValueByLinkText(string linkText) => DriverExtensions.WaitForElement(By.LinkText(linkText)).GetAttribute("href");

        /// <summary>
        /// Checks if a link is present or not
        /// </summary>
        /// <param name="linkId"> ID link </param>
        /// <returns> True if links is displayed, false otherwise </returns>
        public bool IsLinkByIdDisplayed(string linkId) => DriverExtensions.IsDisplayed(By.Id(linkId));

        /// <summary>
        /// Checks if a link containing specified partial link text is scrolled into view
        /// </summary>
        /// <param name="partialLinkText">Partial link text of the link that will be checked</param>
        /// <returns>True if link is scrolled into view, false otherwise</returns>
        public bool IsLinkScrolledIntoView(string partialLinkText)
            => DriverExtensions.GetElement(By.Id(string.Format(CoLinkIdLctMask, partialLinkText))).IsElementInView();

        /// <summary>
        /// Verify is text scrolled in to view
        /// </summary>
        /// <param name="text">text</param>
        /// <returns>true if scrolled, false otherwise</returns>
        public bool IsTextScrolledIntoView(string text) =>
            DriverExtensions.GetElement(By.XPath(string.Format(TextLctMask, text))).IsElementInView();

        /// <summary>
        /// Click element by text using XPath
        /// </summary>
        /// <typeparam name="T">Page type</typeparam>
        /// <param name="text">Text to search for</param>
        /// <returns>New instance of the page</returns>
        public T ClickByText<T>(string text) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(TextLctMask, text))).Click();
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Check whether navigated to appropriate document section
        /// </summary>
        /// <param name="option">Option</param>
        /// <returns>true or false</returns>
        public bool IsNavigatedToOption(DocumentSection option)
            => DriverExtensions.GetElement(By.CssSelector(this.DocSectionsMap[option].LocatorString)).IsElementInView();

        /// <summary>
        /// Verify is text scrolled in to quotation
        /// </summary>
        /// <returns>true if scrolled, false otherwise</returns>
        public bool IsFirstHighlightInView()
        {
            DriverExtensions.WaitForPageLoad();
            return DriverExtensions.GetElements(HighlightedSearchTermLocator).First().IsElementInView();
        }

        /// <summary>
        /// Get highlighted search term that is in view
        /// </summary>
        /// <returns>highlighted search term that is in view</returns>
        public IWebElement GetHighlightedSearchTermInView() =>
            DriverExtensions.GetElements(HighlightedSearchTermLocator).First(i => i.IsElementInView());

        /// <summary>
        /// Get Element Text of Navigated Element for Option
        /// </summary>
        /// <param name="section">Option</param>
        /// <returns> Navigated element text </returns>
        public string GetNavigatedElementText(DocumentSection section) => Regex.Split(
            DriverExtensions.GetElement(SafeXpath.BySafeXpath(NavigatedElementLctMask, this.DocSectionsMap[section].Id))
                            .Text,
            "\r\n|\r|\n")[0];

        /// <summary>
        /// determines if ultra wide table is present on the document
        /// </summary>
        /// <returns> true if table is present </returns>
        public bool IsUltraWideTableDisplayed() => DriverExtensions.WaitForElement(ContentBlockPreformattedLocator).Displayed;

        /// <summary>
        /// Checks to see if the Flag is displayed for a document.
        /// </summary>
        /// <param name="flag">The flag.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsFlagDisplayed(KeyCiteFlag flag) => DriverExtensions.IsDisplayed(By.ClassName(this.KeyCiteFlagsMap[flag].ClassName), 500);

        /// <summary>
        /// Verify that the copyright is displayed
        /// </summary>
        /// <returns>Boolean</returns>
        public bool IsCopyrightDisplayed() => DriverExtensions.IsDisplayed(CopyrightMessageLocator);

        /// <summary>
        /// Check if the title is scrolled into view
        /// </summary>
        /// <returns></returns>
        public bool IsTitleInView() => DriverExtensions.GetElement(DocumentTitleLocator).IsElementInView();

        /// <summary>
        /// Get copyright text
        /// </summary>
        /// <returns> The <see cref="string"/>. </returns>
        public string GetCopyrightText() => DriverExtensions.GetText(CopyrightMessageLocator);

        /// <summary>
        /// Select a specific document section
        /// </summary>
        /// <param name="docSection">DocSection</param>
        /// <param name="paragraphIndex">The index of a paragraph</param>
        /// <returns> The <see cref="HighlightMenuDialog"/>. </returns>
        public HighlightMenuDialog SelectDocumentSection(DocumentSection docSection, int paragraphIndex = 0)
        {
            By documentSectionLocator = By.CssSelector(this.DocSectionsMap[docSection].LocatorString);
            DriverExtensions.WaitForElementDisplayed(documentSectionLocator);

            return this.SelectSnippet(DriverExtensions.GetElements(documentSectionLocator).ElementAt(paragraphIndex));
        }

        /// <summary>
        /// The highlight a part of a specific document and open the CopyWithReference option list.
        /// </summary>
        /// <param name="text"> Text for selecting. </param>
        /// <typeparam name="T"> T </typeparam>
        /// <returns> New instance of the page </returns>
        public T SelectSnippetByText<T>(string text) where T : ICreatablePageObject
        {
            this.SelectSnippet(DriverExtensions.GetElement(By.XPath(string.Format(SnippetTextLctMask, text))));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Select expanded text
        /// </summary>
        /// <returns> The <see cref="HighlightMenuDialog"/>. </returns>
        public HighlightMenuDialog SelectExpandedText()
        {
            IWebElement beginTextToBeSelected = DriverExtensions.GetElement(BeginTextToBeSelectedBy);
            IWebElement endTextToBeSelected = DriverExtensions.GetElement(EndTextToBeSelectedBy);

            this.SelectText(beginTextToBeSelected, endTextToBeSelected);
            return new HighlightMenuDialog();
        }

        /// <summary>
        /// Select Paragraph
        /// </summary>
        /// <param name="docSection">DocSection</param>
        /// <param name="paragraphIndex">The index of a paragraph</param>
        /// <returns>The <see cref="HighlightMenuDialog"/></returns>
        public HighlightMenuDialog SelectParagraph(DocumentSection docSection, int paragraphIndex)
        {
            By selector = By.CssSelector(this.DocSectionsMap[docSection].LocatorString + " .co_paragraphText");
            DriverExtensions.WaitForElementDisplayed(selector);

            return this.SelectSnippet(DriverExtensions.GetElements(selector).ElementAt(paragraphIndex));
        }

        /// <summary>
        /// Selects a text in the document and add inline notes.
        /// </summary>
        /// <param name="notes">
        /// Text to select
        /// </param>
        /// <param name="index">
        /// Index of text to select
        /// </param>
        /// <returns> The <see cref="EditNoteDialog"/>. </returns>
        public EditNoteDialog SelectParagraphForInlineNotes(string notes, int index = 0)
        {
            var editNoteDialog = new EditNoteDialog();
            IReadOnlyCollection<IWebElement> paragraphs = DriverExtensions.GetElements(By.CssSelector(this.DocSectionsMap[DocumentSection.ParagraphText].LocatorString));
            bool highlightDone = false;

            while (!highlightDone && paragraphs.Count > index)
            {
                if (paragraphs.ElementAt(index).Text.Contains(notes))
                {
                    this.SelectText(paragraphs.ElementAt(index), paragraphs.ElementAt(index + 1));
                    editNoteDialog = new HighlightMenuDialog().ClickColorNoteOption<EditNoteDialog>(HighlightColor.Blue);
                    highlightDone = true;
                }
                index++;
            }

            return editNoteDialog;
        }

        /// <summary>
        /// Get Information about the document
        /// </summary>
        /// <returns> The <see cref="DocumentInfoModel"/>. </returns>
        public DocumentInfoModel GetDocumentInfo()
            => new DocumentInfoModel
            {
                LongTitle = this.FixedHeader.GetTitleText(),
                Citation = this.CitationBlock.GetPrimaryCitationText(),
                Guid = WindowUtils.GetDocumentGuid()
            };

        /// <summary>
        /// The verify high resolution image.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool VerifyHighResolutionImage(string title)
            => DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(ImgLctmask, title));

        /// <summary>
        /// Verify that Snippet confirmation message is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsSnippetConfirmationMessageDisplayed()
        {
            this.WaitForSnippetConfirmationMessageDisplayed();
            return DriverExtensions.WaitForElement(SnippetInfoMessageLocator).Text.Contains("Selected");
        }

        /// <summary>
        /// Wait for Snippet message displayed
        /// </summary>
        public void WaitForSnippetConfirmationMessageDisplayed() => DriverExtensions.WaitForElementDisplayed(SnippetInfoMessageLocator);

        /// <summary>
        /// Copy or Move an item to a folder in the RecentFoldersDialog using Drag and Drop
        /// </summary>
        /// <param name="targetFolder">The name of Target Folder.</param>
        /// <param name="copyOrMove">The drag And Drop Enum.</param>
        /// /// <param name="wait">for snippet message.</param>
        /// ///  <param name="timeOut">for time out.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public virtual string DragAndDropTitleElementToRecentFolder(string targetFolder, CopyOrMoveEnum copyOrMove = CopyOrMoveEnum.Copy, bool wait = false, int timeOut = 30000)
        {
            String message;
            DriverExtensions.DragAndHold(this.Header.GetFoldersLinkElement(), this.FixedHeader.TitleElement);
            this.DragAndDropToFolder(
                new RecentFoldersDialog().GetFolderElement(targetFolder), this.FixedHeader.TitleElement, copyOrMove);
            if (wait)
            {
                message = this.Header.GetInfoMessage(wait: true, timeOut: timeOut);
            }
            else
            {
                message = this.Header.GetInfoMessage();
            }
            return message;
        }

        #region Methods for working with Notes and Highlights
        /// <summary>
        /// Get Notes List
        /// </summary>
        /// <returns> 
        /// List of notes
        /// If note is not displayed, returns empty list
        /// </returns>
        public List<ViewNoteDialog> GetNotesList()
            => DriverExtensions.IsDisplayed(NoteLocator, 5)
                    ? DriverExtensions.GetElements(NoteLocator).Select(elem => new ViewNoteDialog(elem)).ToList() : new List<ViewNoteDialog>();

        /// <summary>
        /// Get Note by note number
        /// </summary>
        /// <param name="index"> Index </param>
        /// <returns> The <see cref="ViewNoteDialog"/>. </returns>
        public ViewNoteDialog GetNoteByIndex(int index)
            => new ViewNoteDialog(DriverExtensions.WaitForElement(By.XPath(string.Format(NoteByIndexLctMask, index))));

        /// <summary>
        /// Get Note by note number
        /// </summary>
        /// <param name="noteItem"> The note Item. </param>
        /// <returns> The <see cref="ViewNoteDialog"/>. </returns>
        public ViewNoteDialog GetNote(NoteItemModel noteItem)
            => new ViewNoteDialog(DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(NoteByTextLctMask, noteItem.NoteText)));

        /// <summary>
        /// Get list of inline notes
        /// </summary>
        /// <returns> Inline notes list </returns>
        public List<ViewNoteDialog> GetInlineNotesList()
            => DriverExtensions.GetElements(InlineNotesLocator).Select(elem => new ViewNoteDialog(elem)).ToList();

        /// <summary>
        /// Get List of Document Level notes
        /// </summary>
        /// <returns> Document Level notes list </returns>
        public List<ViewNoteDialog> GetDocLevelNotesList()
            => DriverExtensions.GetElements(DocLevelNotesContainerLocator, NoteLocator).Select(elem => new ViewNoteDialog(elem)).ToList();

        /// <summary>
        /// Get highlights list
        /// This method should be used in Annotation Manager, not in tests
        /// </summary>
        /// <returns> Highlights list </returns>
        public List<IWebElement> GetHighlightedElements() => DriverExtensions.GetElements(HighlightLocator).ToList();

        /// <summary>
        /// Get highlight by highlight's text
        /// This method should be used in Annotation Manager, not in tests
        /// </summary>
        /// <param name="text"> Text </param>
        /// <returns> Highlight </returns>
        public IWebElement GetHighlightedElementByText(string text)
            => DriverExtensions.SafeGetElement(By.XPath(string.Format(HighlightLctMask, text)));

        /// <summary>
        /// Get Highlight Color by text
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public HighlightColor GetHighlightColorByIndex(int index)
        {
            IWebElement highlight = this.GetHighlightedElementByIndex(index);
            string elementClass = highlight.GetAttribute("class");
            return this.HighlightingColor.First(e => elementClass.Contains(e.Value.ClassName)).Key;
        }

        /// <summary>
        /// Get collapsed note icon element
        /// </summary>
        /// <param name="noteItem">Note item</param>
        /// <returns>IWebElement instance</returns>
        public IWebElement GetCollapsedNoteIcon(NoteItemModel noteItem) => DriverExtensions.SafeGetElement(By.XPath(string.Format(NoteIconHideStateLctMask, noteItem.NoteText)));

        /// <summary>
        /// Get highlight by highlight's number
        /// This method should be used in Annotation Manager, not in tests
        /// </summary>
        /// <param name="index"> index </param>
        /// <returns> Highlight </returns>
        public IWebElement GetHighlightedElementByIndex(int index)
        {
            By highlightLocator = By.XPath(string.Format(HighlightByIndexLctMask, index));
            return DriverExtensions.IsDisplayed(highlightLocator) ? DriverExtensions.GetElement(highlightLocator) : null;
        }

        /// <summary>
        /// Get highlighted snippets
        /// This method should be used in Annotation Manager, not in tests
        /// </summary>
        /// <returns> Highlighted snippets list </returns>
        public List<IWebElement> GetHighlightedSnippets()
            => DriverExtensions.GetElements(HighlightedSnippetLocator).ToList();

        /// <summary>
        /// Click on highlight
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="highlightNo"> Number of highlight for clicking </param>
        /// <returns> New instance of the page </returns>
        public T ClickHighlightByNumber<T>(int highlightNo) where T : ICreatablePageObject
        {
            IWebElement highlightedElement = this.GetHighlightedElements().ElementAt(highlightNo);
            highlightedElement.ScrollToElement();
            highlightedElement.Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Verify Shared By is displayed for reviewer Highlights.
        /// </summary>
        /// <returns>true if Shared icon is displayed for all owner's notes, false otherwise</returns>
        public bool IsSharedByInfoMessageDisplayed() => DriverExtensions.IsDisplayed(SharedByMessageLocator);

        /// <summary>
        /// Verify Shared icon is displayed for reviewer Highlights.
        /// </summary>
        /// <param name="ownerName">The owner Name.</param>
        /// <returns>
        /// true if Shared icon is displayed for all owner's notes, false otherwise
        /// </returns>
        public bool IsSharedHighlightsOwnerNameDisplayed(string ownerName)
            => this.IsSharedByInfoMessageDisplayed()
                && DriverExtensions.WaitForElement(SharedByMessageLocator).Text.Equals("Shared by " + ownerName, StringComparison.InvariantCultureIgnoreCase);

        /// <summary>
        /// Verify that shared icon is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsIconDisplayedInSharedInfoMessage() => DriverExtensions.IsDisplayed(SharedByMessageLocator, SharedIconLocator);

        /// <summary>
        /// Verify that annotations are hidden
        /// </summary>
        /// <returns> True if annotations are hidden, false otherwise </returns>
        public bool AreAnnotationsHidden() => DriverExtensions.IsDisplayed(HideAnnotationsLocator);

        /// <summary>
        /// Verify that left panel is displayed
        /// </summary>
        /// <returns> True if left panel is displayed, false otherwise </returns>
        public bool IsLeftPanelDisplayed() => DriverExtensions.IsDisplayed(LeftColumnLocator);
        #endregion

        /// <summary>
        /// returns list of highlighted terms in a document from quotations
        /// </summary>
        /// <returns>List of highlighted terms</returns>
        public List<string> GetHighlightedTermsList() => DriverExtensions.GetElements(HighlightedSearchTermLocator)
                                                                                  .Select(item => item.Text).ToList();

        /// <summary>
        /// Click the PDF link displayed on the company page
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>return new page</returns>
        public T ClickPdfLinkAndOpenNewTab<T>()
            where T : ICreatablePageObject
        {
            this.ClickAndOpenNewBrowserTab<T>(PdfLinkLocator, "PDFDoc");
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Selects snippet of text and returns highlight menu dialog
        /// </summary>
        /// <param name="element">IWebElement</param>
        /// <returns>The <see cref="HighlightMenuDialog"/></returns>
        public HighlightMenuDialog SelectSnippet(IWebElement element)
        {
            this.SelectSnippetAndGetSelectedText(element);
            return new HighlightMenuDialog();
        }

        /// <summary>
        /// Get Highlight Color by text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public HighlightColor GetHighlightColorByText(string text)
        {
            IWebElement highlight = this.GetHighlightedElementByText(text);
            string elementClass = highlight.GetAttribute("class");
            return this.HighlightingColor.First(e => elementClass.Contains(e.Value.ClassName)).Key;
        }

        /// <summary>
        /// Selects snippet of text and returns selected text
        /// </summary>
        /// <param name="element">IWebElement</param>
        /// <returns>The <see cref="HighlightMenuDialog"/></returns>
        internal string SelectSnippetAndGetSelectedText(IWebElement element)
        {
            element.ScrollToElementCenter();
            element.Focus();
            element.TriggerMouseEventByPoint(Pointer.PointerDown);
            string highlightedText = element.HighlightText();
            element.TriggerMouseEventByPoint(Pointer.PointerUp);
            return highlightedText;
        }

        /// <summary>
        /// returns list of search terms
        /// </summary>
        /// <returns>List of search terms</returns>
        protected List<IWebElement> GetSearchTermsList() => DriverExtensions.GetElements(HighlightedSearchTermLocator).ToList();

        private void SelectText(IWebElement beginTextToBeSelected, IWebElement endTextToBeSelected)
        {
            beginTextToBeSelected.Focus();
            DriverExtensions.DoubleClick(beginTextToBeSelected);
            DriverExtensions.HighlightMultipleNodes(beginTextToBeSelected, endTextToBeSelected);
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Return list of links for search within highlighted text in the document content.
        /// </summary>
        /// <returns>List of strings</returns>
        public IReadOnlyCollection<ILink> SearchWithinGreenTermLinks => new ElementsCollection<Link>(SearchWithinGreenTermLinkLocator);

        /// <summary>
        /// Click See Full Profile link
        /// </summary>
        public void ClickSeeFullProfile()
        {
            DriverExtensions.WaitForElement(SeeFullProfileLocator).Click();
            DriverExtensions.WaitForJavaScript();
        }

    }
}