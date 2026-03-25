namespace Framework.Common.UI.Raw.WestlawEdge.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs.Document.Notes;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Enums.Document;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Products.WestlawEdge.Components;
    using Framework.Common.UI.Products.WestlawEdge.Components.Document;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Components.RightPanel;
    using Framework.Common.UI.Products.WestlawEdge.Components.ToC;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Document;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Document.Notes;
    using Framework.Common.UI.Products.WestlawEdge.Elements;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Document;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.FocusHighlighting;
    using Framework.Common.UI.Raw.WestlawEdge.Models.FocusHighlighting;
    using Framework.Common.UI.Utils.Enum;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    using TRGR.Quality.QedArsenal.QualityLibrary.Core.Enums.WebDriver;

    /// <summary>
    /// EdgeCommonDocumentPage
    /// </summary>
    public class EdgeCommonDocumentPage : CommonDocumentPage
    {
        private const string DocTextLctMask = "//*[contains(text(),'{0}')]";
        private const string DocumentTitleInReadingModeLctMask = "//*[@class='co_title']/div[text()=\"{0}\"] | //*[@class='co_title co_headtext' and text()=\"{0}\"]";
        private const string HighlightLctMask = "//span[contains(@class, 'co_selection_') and contains(.,'{0}')]";
        private const string HighlightedTermLctMask = "//span[@id='co_term_{0}']";
        private const string SnippetTermPinpointLocator = "//a[contains(@id,'co_snip')]/following-sibling::span[@class='co_pinpointIcon']/parent::div";
        private const string FocusHighlightedTermAfterApplySearchWithinLctMask = ".//span[contains(@class, 'co_termNav_focusHighlight')]/span[text() = '{0}']";
        private const string InlineKeyCiteLctMask = "//span[contains(@data-cite-text, '{0}')]";
        private const string BinaryLinkLctMask = "//a[contains(@class,'co_downloadBinary') and contains(text(),'{0}')]";
        private const string SectionLctMask = "//div[@id='co_anchor_{0}']";
        private const string PurpleHighlightLctMask = "//span[contains(@class, 'mswSearchTerm') and contains(text(), '{0}')]";
        private const string TooltipTextLctMask = ".//div[contains(@class, 'a11yTooltip-content')]//div[text()='{0}']";
        private const string CaseNavigationPaneLinkLocator = ".//span[contains(text(),'{0}')]/..";

        private static readonly string SelectedItemClassName = "co_currentSearchTerm";

        private static readonly By FocusHighlightedTermAfterApplySearchWithinLocator = By.XPath(".//span[contains(@class, 'co_termNav_focusHighlight')]");
        private static readonly By ParagraphToHighlightLocator = By.XPath(".//div[@class='co_paragraphText']");
        private static readonly By BriefItButtonLocator = By.Id("co_briefItButton");
        private static readonly By MaximizeNoteIconLocator = By.XPath("//button[contains(@title, 'Maximize Note')] | //*[@class='DocumentPanel-featureSelect-text' and contains(text(), 'Notes')] | .//*[contains(@class,'co_noteMaximize co_noteIcon')]");
        private static readonly By MaximizeNoteIconWithSharedIconLocator = By.XPath("//*[contains(@title, 'Maximize') and contains(@class,'co_noteIconShared')] | //*[contains(@class,'co_noteMaximize')]");
        private static readonly By CloseNoteIconLocator = By.ClassName("co_noteCloseButton");
        private static readonly By DocLevelNotesContainerLocator = By.XPath("//div[@id='co_documentNotes']");
        private static readonly By DocMessageLocator = By.XPath("//div[@class= 'co_messageBox']/div");
        private static readonly By DocToDocLinkLocator = By.ClassName("co_link");
        private static readonly By DocumentTextContainerLocator = By.Id("co_document");
        private static readonly By CaseNavigationContainerLocator = By.Id("co_caseNavigationPanel");
        private static readonly By DocumentTitleLocator = By.XPath("//div[@id='co_docHeaderTitle']//descendant::span[@id='title' or @id='titleInfo']//a");
        private static readonly By NotesPaneLocator = By.XPath("//button//span[text()='Notes']");
        private static readonly By EndOfDocumentLocator = By.Id("co_endOfDocument");
        private static readonly By ExpandCollapseChevronButtonLocator = By.XPath("//div[@id='co_collapseButtonRight']");
        private static readonly By ExpandCollapseChevronButtonStatusLocator = By.XPath(".//a");
        private static readonly By FocusHighlightedTermLocator = By.XPath("//span[contains(@class, 'co_termNav_focusHighlight')]");
        private static readonly By GrayQuoteTextLocator = By.XPath("//span[@class='co_quoteExactMatch']");
        private static readonly By InlineKeyCiteFlagLocator = By.XPath("//a[contains(@class,'co_inlineKeyCiteFlagLink')]");
        private static readonly By InlineNoteOnDocContainerLocator = By.XPath("//div[@class='co_noteContainer']");
        private static readonly By NoteIconCountLocator = By.ClassName("co_noteIcon_count");
        private static readonly By NoteLocator = By.XPath(".//*[@class='co_noteContainer']");
        private static readonly By OverrulingRiskArrowLocator = By.XPath("//*[contains(@class, 'co_pinpointIcon OverrulingPinpointIcon')]");
        private static readonly By OverrulingRiskNegativeTreatmentLinkLocator = By.XPath("//div[@id='co_readingModeNegativeTreatment']//a[@class='co_cautionSm']");
        private static readonly By ProceduralPostureTextLocator = By.XPath("//div[contains(@class, 'co_proceduralPostureBlock')]");
        private static readonly By QuotationIconLocator = By.ClassName("co_quotationIcon");
        private static readonly By ExitFullScreenButtonLocator = By.Id("co_exitFullscreenModeLink");
        private static readonly By CloseShadingBoxesButtonLocator = By.XPath("./parent::div//button");
        private static readonly By HeadingShadingBoxesLocator = By.XPath("./parent::div//h3");
        private static readonly By CurrentSearchTermLocator = By.XPath("//span[contains(@class,'co_termNav_focusHighlight co_currentSearchTerm')] | //span[contains(@class,'co_currentSearchTerm')]");
        private static readonly By HighlightedTermLocator = By.XPath("//span[contains(@id,'co_term_')]");
        private static readonly By PurpleHighlightTermLocator = By.XPath("//span[contains(@class, 'co_searchTerm co_locateTerm')]");
        private static readonly By PreviousCarouselButtonLocator = By.XPath("//*[@id='co_docPrimaryTabNavigationContainer']//button[contains(@class, 'DocumentTabScrollButton--previous')]");
        private static readonly By NextCarouselButtonLocator = By.XPath("//*[@id='co_docPrimaryTabNavigationContainer']//button[contains(@class, 'DocumentTabScrollButton--next')]");
        private static readonly By TitleAndCitationToggleButtonLocator = By.XPath(".//button[contains(@class,'Athens-Document-Caption-Toggle')]");
        private static readonly By HighlightedTextLineLocator = By.XPath("//div[@class = 'highlightedTextQuickCheck']");
        private static readonly By HighlightedTextGrayBoxLocator = By.XPath("//span[@class = 'icon25 icon_boxWithCheck-gray']");
        private static readonly By HighlightedTextLocator = By.XPath("//span[contains(@class, 'co_quickCheckText co_selection_quickCheck')]");
        private static readonly By PendoCloseButtonLocator = By.XPath("//*[contains(@class, '_pendo-button') or contains(@class, '_pendo-close')]");
        private static readonly By DocumentCitationLocator = By.XPath("//div[@class='co_cites']");
        private static readonly By CaseVersionContainerLocator = By.Id("coid_caseVersionContainer");

        private static readonly By ClickHereLinkLocator = By.XPath("//div[@class='co_center co_versionUpdate']/a");

        private EnumPropertyMapper<KeyCiteFlag, WebElementInfo> documentKeyCiteFlagsMap;
        private EnumPropertyMapper<TermColors, FocusHighlightingTermInfo> termColorMap;

        /// <summary> 
        /// Credits component 
        /// </summary>
        public CreditsComponent Credits { get; } = new CreditsComponent();

        /// <summary> 
        /// Edge Document Fixed Header component 
        /// </summary>
        public new EdgeDocumentFixedHeaderComponent FixedHeader { get; } = new EdgeDocumentFixedHeaderComponent();

        /// <summary>
        /// Gets the inline notes component.
        /// </summary>
        public EdgeInlineNotesComponent InlineNotes { get; } = new EdgeInlineNotesComponent();

        /// <summary> 
        /// KeyCite Flag Widget Component 
        /// </summary>
        public new EdgeKeyCiteFlagComponent KeyCiteFlagWidgetComponent { get; private set; } = new EdgeKeyCiteFlagComponent();

        /// <summary> 
        /// Edge footer component 
        /// </summary>
        public new EdgeFooterComponent Footer { get; } = new EdgeFooterComponent();

        /// <summary> 
        /// Header component 
        /// </summary>
        public new EdgeHeaderComponent Header { get; } = new EdgeHeaderComponent();

        /// <summary> 
        /// Edge Right Panel component 
        /// </summary>
        public EdgeRightPanelComponent RightPanel => new EdgeRightPanelComponent();

        /// <summary> 
        /// Table of Contents component 
        /// </summary>
        public EdgeTocComponent Toc { get; } = new EdgeTocComponent();

        /// <summary> Toolbar component  </summary>
        public new EdgeToolbarComponent Toolbar { get; protected set; } = new EdgeToolbarComponent();

        /// <summary>
        /// The QuickCheck report tray.
        /// </summary>
        public QuickCheckReportTrayComponent ReportTray => new QuickCheckReportTrayComponent();

        /// <summary> 
        /// Related Documents Tab Panel 
        /// </summary>
        public RelatedDocumentsTabPanel RelatedDocumentsPanel { get; } = new RelatedDocumentsTabPanel();

        /// <summary>
        /// Overruling Risk Negative Treatment image based link displayed at top of document body
        /// </summary>
        public ILink OverrulingRiskNegativeTreatmentLink => new Link(OverrulingRiskNegativeTreatmentLinkLocator);

        /// <summary>
        /// Close Concurrence box button
        /// </summary>
        public IButton CloseConcurrenceBoxButton =>
            new Button(
                By.XPath(this.EdgeDocSectionsMap[EdgeDocumentSection.ConcurringOpinion].LocatorString),
                CloseShadingBoxesButtonLocator);

        /// <summary>
        /// Close Dissent box button
        /// </summary>
        public IButton CloseDissentBoxButton =>
            new Button(
                By.XPath(this.EdgeDocSectionsMap[EdgeDocumentSection.DissentingOpinion].LocatorString),
                CloseShadingBoxesButtonLocator);

        /// <summary>
        /// Previous carousel button
        /// </summary>
        public IButton PreviousCarouselButton => new Button(PreviousCarouselButtonLocator);

        /// <summary>
        /// Next carousel button
        /// </summary>
        public IButton NextCarouselButton => new Button(NextCarouselButtonLocator);

        /// <summary>
        /// Gets the Concurrence box heading label.
        /// </summary>
        public ILabel ConcurrenceBoxHeadingLabel =>
            new Label(
                By.XPath(this.EdgeDocSectionsMap[EdgeDocumentSection.ConcurringOpinion].LocatorString),
                HeadingShadingBoxesLocator);

        /// <summary>
        /// Gets the Dissent box heading label.
        /// </summary>
        public ILabel DissentBoxHeadingLabel =>
            new Label(
                By.XPath(this.EdgeDocSectionsMap[EdgeDocumentSection.DissentingOpinion].LocatorString),
                HeadingShadingBoxesLocator);

        /// <summary>
        /// Gets the Concurrence in part box heading label.
        /// </summary>
        public ILabel ConcurrenceInPartBoxHeadingLabel =>
            new Label(
                By.XPath(this.EdgeDocSectionsMap[EdgeDocumentSection.ConcurringInPartOpinion].LocatorString),
                HeadingShadingBoxesLocator);

        /// <summary>
        /// Gets the Dissent in part box heading label.
        /// </summary>
        public ILabel DissentInPartBoxHeadingLabel =>
            new Label(
                By.XPath(this.EdgeDocSectionsMap[EdgeDocumentSection.DissentingInPartOpinion].LocatorString),
                HeadingShadingBoxesLocator);

        /// <summary>
        /// Get Click Here Link element
        /// </summary>
        public ILink ClickHereLink => new Link(ClickHereLinkLocator);

        /// <summary>
        /// Key Cite Label in the document's body
        /// </summary>
        public IReadOnlyCollection<ILabel> KeyCiteFlagLabelsList =>
            new ElementsCollection<Label>(InlineKeyCiteFlagLocator);

        /// <summary>
        /// Gets the key cite flag.
        /// </summary>
        /// <param name="flags">The flags </param>
        /// <returns> Count of flags with specific color</returns>
        public virtual bool KeyCiteFlagsDisplayedOnDocumentPage(KeyCiteFlag flags) =>
            this.GetFlagsList(flags).All(flag => flag.IsDisplayed());

        /// <summary>
        /// Highlighted terms labels
        /// </summary>
        public IReadOnlyCollection<ILabel> HighlightedTerms => new ElementsCollection<Label>(HighlightedTermLocator);

        /// <summary>
        /// Purple highlighted terms labels
        /// </summary>
        public IReadOnlyCollection<ILabel> PurpleHighlightedTerms => new ElementsCollection<Label>(PurpleHighlightTermLocator);

        /// <summary>
        /// Full caption toggle button
        /// </summary>
        public IToggle TitleAndCitationToggle => new ToggleWithText(DriverExtensions.GetElement(DocumentTextContainerLocator), TitleAndCitationToggleButtonLocator, "Show title and citation");

        /// <summary>
        /// Gets the TermColors enumeration to FocusHighlightingTermInfo map.
        /// </summary>
        protected EnumPropertyMapper<TermColors, FocusHighlightingTermInfo> TermColorMap =>
            this.termColorMap = this.termColorMap
                                ?? EnumPropertyModelCache.GetMap<TermColors, FocusHighlightingTermInfo>(
                                    string.Empty,
                                    @"Resources/EnumPropertyMaps/WestlawEdge/FocusHighlighting");

        /// <summary>
        /// Gets the Flag enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<KeyCiteFlag, WebElementInfo> DocumentKeyCiteFlagsMap =>
            this.documentKeyCiteFlagsMap = this.documentKeyCiteFlagsMap
                                           ?? EnumPropertyModelCache.GetMap<KeyCiteFlag, WebElementInfo>(
                                               string.Empty,
                                               @"Resources/EnumPropertyMaps/WestlawEdge/Document");

        /// <summary>
        /// Gets the EdgeDocumentSection enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<EdgeDocumentSection, WebElementInfo> EdgeDocSectionsMap =>
            EnumPropertyModelCache.GetMap<EdgeDocumentSection, WebElementInfo>(
                string.Empty,
                @"Resources/EnumPropertyMaps/WestlawEdge/Document");

        /// <summary>
        /// Gets the EdgeMultiColorTerms enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<EdgeFocusHighlightingSectionNumber, WebElementInfo> EdgeFocusHighlightingSectionNumberMap =>
            EnumPropertyModelCache.GetMap<EdgeFocusHighlightingSectionNumber, WebElementInfo>(
                string.Empty,
                @"Resources/EnumPropertyMaps/WestlawEdge/Document");

        /// <summary>
        /// Click Overruling Risk Arrow
        /// </summary>
        /// <returns> Overruling Risk Dialog </returns>
        public OverrulingRiskDialog ClickOverrulingArrow()
        {
            DriverExtensions.Click(OverrulingRiskArrowLocator);
            return new OverrulingRiskDialog();
        }

        /// <summary>
        /// Click Brief It button locator
        /// </summary>
        public T ClickBriefItButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(BriefItButtonLocator).Click();

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get snippet text by term id
        /// </summary>
        /// <returns>string</returns>
        public string GetSnippetText() => DriverExtensions.WaitForElement(By.XPath(SnippetTermPinpointLocator)).GetParentElement().Text;

        /// <summary>
        /// Navigate to a DocumentPage by clicking on a link in the current DocumentPage
        /// </summary>
        /// <typeparam name="T"> T </typeparam>
        /// <param name="linkText"> Link Text </param>
        /// <returns> Document Page from the link that was clicked on </returns>
        public T ClickOnLinkInDocumentPage<T>(string linkText) where T : ICreatablePageObject
        {
            TextSearchOption[] options = { TextSearchOption.PartialMatch };

            IWebElement container = DriverExtensions.WaitForElement(DocumentTextContainerLocator);
            IWebElement link = DriverExtensions.GetElementByText(linkText, options, container, DocToDocLinkLocator);
            link.WaitForElementDisplayed();
            link.ClickUsingJavaScriptAsync();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Navigate to a DocumentPage by clicking on a link in the Case navigation pane
        /// </summary>
        /// <typeparam name="T"> T </typeparam>
        /// <param name="linkText"> Link Text </param>
        /// <returns> Document Page from the link that was clicked on </returns>
        public T ClickLinkOnCaseVersionPane<T>(string linkText) where T : ICreatablePageObject
        {
            //IWebElement container = DriverExtensions.WaitForElement(CaseNavigationContainerLocator);
            IWebElement link = DriverExtensions.GetElement(
                    new ByChained(
                        CaseNavigationContainerLocator,
                        By.XPath(string.Format(CaseNavigationPaneLinkLocator, linkText)))
                    );
            link.WaitForElementDisplayed();
            link.ClickUsingJavaScriptAsync();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get text from doc message
        /// </summary>
        /// <returns> The <see cref="string"/>. </returns>
        public string GetDocMessageText() => DriverExtensions.WaitForElement(DocMessageLocator).Text;

        /// <summary>
        /// Get Citations from doc 
        /// </summary>
        /// <returns> The <see cref="string"/>. </returns>
        public string GetCitationFromDocHeader() => DriverExtensions.WaitForElement(DocumentCitationLocator).Text;

        /// <summary>
        /// Get All Citations text from case version container
        /// </summary>
        /// <returns> The <see cref="string"/>. </returns>

        public IList<string> GetCaseVersions() => DriverExtensions.GetElements(CaseVersionContainerLocator, By.XPath("./a/span[2]"))
            .Select(a => a.Text).ToList();

        /// <summary>
        /// Get brief it button text
        /// </summary>
        /// <returns> The <see cref="string"/>. </returns>
        public string GetBriefItButtonText() => DriverExtensions.WaitForElement(BriefItButtonLocator).Text;

        /// <summary>
        /// Get a list of text of Focus Highlighted terms 
        ///  </summary>
        /// <returns> The list of Focus Highlighted terms . </returns>
        public List<string> GetListOfFocusHighlightedTermsText() => DriverExtensions.GetElements(FocusHighlightedTermLocator).Select(el => el.Text).ToList();

        /// <summary>
        /// Get document title
        /// </summary>
        /// <returns> The <see cref="string"/>. </returns>
        public string GetDocumentTitle() => DriverExtensions.GetText(DocumentTitleLocator);

        /// <summary>
        /// Get gray quote text
        /// </summary>
        /// <returns>List of gray quotes text</returns>
        public List<string> GetGrayQuoteText() =>
            DriverExtensions.GetElements(GrayQuoteTextLocator).Select(grayQuote => grayQuote.Text).ToList();

        /// <summary>
        /// Get index (indexes start from 1) of item that is search within term and is selected
        /// </summary>
        /// <returns>number of item</returns>
        public int GetIndexOfSearchHighlightedSelectedItem()
        {
            List<string> classList = this.GetSearchTermsList().Select(item => item.GetAttribute("class")).ToList();
            string selectedItemClassNames = classList.FirstOrDefault(item => item.Contains(SelectedItemClassName));
            return this.GetSearchTermsList().Select(item => item.GetAttribute("class")).ToList()
                       .IndexOf(selectedItemClassNames) + 1;
        }

        /// <summary>
        /// Get highlighted term color by term id
        /// </summary>
        /// <param name="termId"> The term ID </param>
        /// <returns> Term color </returns>
        public TermColors GetTermColor(int termId) =>
            this.GetColorTypeByCode(
                DriverExtensions.GetElement(By.XPath(string.Format(HighlightedTermLctMask, termId)))
                                .GetCssValue("background-color"));

        /// <summary>
        /// Get highlighted term color
        /// </summary>
        /// <param name="selectionNumber"> sequence number of the term selection</param>
        /// <returns> Term color </returns>
        public TermColors GetTermColor(EdgeFocusHighlightingSectionNumber selectionNumber) =>
            this.GetColorTypeByCode(
                DriverExtensions.GetElements(
                                    DocumentTextContainerLocator,
                                    By.XPath(this.EdgeFocusHighlightingSectionNumberMap[selectionNumber].LocatorString)).First()
                                .GetCssValue("background-color"));

        /// <summary>
        /// Get Focus highlighted term color after apply searchWithin
        /// </summary>
        /// <param name="termName"> The term name </param>
        /// <returns> FH term color after apply search within</returns>
        public TermColors GetTermColorAfterApplySearchWithin(string termName) =>
            this.GetColorTypeByCode(
                DriverExtensions.GetElements(DocumentTextContainerLocator, By.XPath(string.Format(FocusHighlightedTermAfterApplySearchWithinLctMask, termName))).First()
                                .GetCssValue("background-color"));

        /// <returns> FH term color after apply search within</returns>
        public TermColors GetTermColorAfterApplySearchWithin() =>
            this.GetColorTypeByCode(
                DriverExtensions.GetElements(DocumentTextContainerLocator, FocusHighlightedTermAfterApplySearchWithinLocator).First()
                                .GetCssValue("background-color"));

        /// <summary>
        /// If all highlighted search terms are in view
        /// </summary>
        /// <returns>The list of ids</returns>
        public List<int> GetAllHighlightedTermIds() =>
            DriverExtensions
                .GetElements(HighlightedTermLocator)
                .Select(el => el.GetAttribute("id").Replace("co_term_", "").ConvertCountToInt())
                .ToList();

        /// <summary>
        /// Is FH term displayed
        /// </summary>
        /// <param name="selectionNumber"> sequence number of the term selection </param>
        /// <returns>  True if term is displayed. </returns>
        public bool IsFocusHighlightingTermDisplayed(EdgeFocusHighlightingSectionNumber selectionNumber) =>
            DriverExtensions.IsDisplayed(DocumentTextContainerLocator, By.XPath(this.EdgeFocusHighlightingSectionNumberMap[selectionNumber].LocatorString));

        /// <summary>
        /// Is FH term in view
        /// </summary>
        /// <param name="selectionNumber">sequence number of the term selection</param>
        /// <param name="section">Edge document section</param>
        /// <returns>True if term is in view</returns>
        public bool IsFocusHighlightingTermInView(
            EdgeFocusHighlightingSectionNumber selectionNumber,
            EdgeDocumentSection section) =>
            DriverExtensions.GetElement(
                By.XPath(this.EdgeDocSectionsMap[section].LocatorString),
                By.XPath(this.EdgeFocusHighlightingSectionNumberMap[selectionNumber].LocatorString)).IsElementInView();

        /// <summary>
        /// Is FH term in view
        /// </summary>
        /// <param name="selectionNumber"> sequence number of the term selection</param>
        /// <param name="section">document section</param>
        /// <returns>True if term is in view</returns>
        public bool IsFocusHighlightingTermInView(
            EdgeFocusHighlightingSectionNumber selectionNumber,
            DocumentSection section) =>
            DriverExtensions.GetElement(
                By.CssSelector(this.DocSectionsMap[section].LocatorString),
                By.XPath(this.EdgeFocusHighlightingSectionNumberMap[selectionNumber].LocatorString)).IsElementInView();

        /// <summary>
        /// Is FH term after apply searchWithin in view
        /// </summary>
        /// <param name="section"> document section </param>
        /// <param name="termName"> term name </param>
        /// <returns> True if term is in view </returns>
        public bool IsFocusHighlightingTermAfterApplySearchWithinInView(DocumentSection section, string termName) =>
            DriverExtensions.GetElement(
                                By.CssSelector(this.DocSectionsMap[section].LocatorString),
                                By.XPath(string.Format(FocusHighlightedTermAfterApplySearchWithinLctMask, termName)))
                            .IsElementInView();

        /// <param name="section"> document section </param>     
        /// <returns> True if term is in view </returns>
        public bool IsFocusHighlightingTermAfterApplySearchWithinInView(DocumentSection section) =>
            DriverExtensions.GetElement(
                                By.CssSelector(this.DocSectionsMap[section].LocatorString), FocusHighlightedTermAfterApplySearchWithinLocator)
                            .IsElementInView();

        /// <summary>
        /// Verify if highlighted text line is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsHighlightedTextLineDisplayed() => DriverExtensions.IsDisplayed(HighlightedTextLineLocator, 5) &&
            DriverExtensions.IsDisplayed(HighlightedTextGrayBoxLocator, 5) &&
            DriverExtensions.IsDisplayed(HighlightedTextLocator, 5);

        /// <summary>
        /// Method retrieves from the page all Web Element items that have the locators for Multiple Search Within Terms
        /// with a purple highlight.
        /// Count the terms, finding ones that match the term sent by the test.
        /// </summary>
        /// <returns>True if term count is at least 1, else false</returns>
        public bool IsSearchWithinPurpleHighlightPresentForTerm(string term) =>
            (from IWebElement webElement in DriverExtensions.GetElements(PurpleHighlightTermLocator)
             where webElement.Text.Equals(term, StringComparison.InvariantCultureIgnoreCase)
             select webElement).Any();

        /// <summary>
        /// Checks if the provided Search Within term with a purple highlight is in view
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public bool IsPurpleHighlightTermInView(string term) =>
            DriverExtensions.GetElement(By.XPath(string.Format(PurpleHighlightLctMask, term))).IsElementInView();

        /// <summary>
        /// Is current search term in view (current search term is term that has a frame when using term navigating)
        /// </summary>
        /// <returns>True if current search term is in view</returns>
        public bool IsCurrentSearchTermInView() => DriverExtensions.GetElement(CurrentSearchTermLocator).IsElementInView();

        /// <summary>
        /// Get current search term text(current search term is term that has a frame when using term navigating)
        /// </summary>
        /// <returns>current search term text</returns>
        public string GetCurrentSearchTerm() => DriverExtensions.GetElement(CurrentSearchTermLocator).Text;

        /// <summary>
        /// Get Document Paragraph Text (paragraphIndex starts from 0)
        /// </summary>
        /// <param name="docSection"> Doc section </param>
        /// <param name="paragraphIndex"> Paragraph Index</param>
        /// <returns> Document Paragraph Text </returns>
        public string GetDocumentParagraphText(DocumentSection docSection, int paragraphIndex)
            => DriverExtensions.GetElements(By.CssSelector(this.DocSectionsMap[docSection].LocatorString), ParagraphToHighlightLocator).ElementAt(paragraphIndex).Text;

        /// <summary>
        /// Get the text in Procedural Posture section 
        /// </summary>
        /// <returns> Procedural Posture Text </returns>
        public string GetProceduralPostureText() => DriverExtensions.WaitForElement(ProceduralPostureTextLocator).Text;

        /// <summary>
        /// Get the number of flags
        /// </summary>
        /// <param name="flags">The flags </param>
        /// <returns> Count of flags with specific color </returns>
        public int GetInlineKeyCiteFlagsCount(KeyCiteFlag flags) =>
            this.GetFlagsList(flags).ToList().Count;

        /// <summary>
        /// Verifies that flags are displayed.s
        /// </summary>
        /// <param name="flags"> The flags. </param>
        /// <returns> The <see cref="bool"/>. True if flags are displayed.  </returns>
        public bool IsFlagsDisplayed(KeyCiteFlag flags) =>
            this.GetFlagsList(flags).ToList().TrueForAll(elem => elem.Displayed);

        /// <summary>
        /// Is brief it mode
        /// </summary>
        /// <returns>True if brief it mode is turned on, false otherwise.</returns>
        public bool IsBriefItMode() => DriverExtensions.GetElement(BriefItButtonLocator).GetAttribute("class").Contains("co_briefItMode");

        /// <summary>
        /// Is screen mode
        /// </summary>
        /// <returns>True screen mode is turned on, false otherwise.</returns>
        public bool IsScreenMode() => DriverExtensions.IsDisplayed(ExitFullScreenButtonLocator);

        /// <summary>
        /// Is inline key cite flag enabled near link
        /// </summary>
        /// <param name="linkName"> The link name </param>
        /// <returns> True if enabled </returns>
        public bool IsInlineKeyCiteFlagEnabledForLink(string linkName) =>
            !DriverExtensions.GetElement(By.XPath(string.Format(InlineKeyCiteLctMask, linkName))).GetAttribute("class")
                             .Contains("co_hideState");

        /// <summary>
        /// Is section in view
        /// </summary>
        /// <param name="sectionId"> Section ID </param>
        /// <returns> True if section ID is in view, false otherwise </returns>
        public bool IsSectionInView(string sectionId) =>
            DriverExtensions.GetElement(By.XPath(string.Format(SectionLctMask, sectionId))).IsElementInView();

        /// <summary>
        /// Verify if overruling risk orange arrow is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsOverrulingArrowDisplayed() => DriverExtensions.IsDisplayed(OverrulingRiskArrowLocator, 5);

        /// <summary>
        /// IsOverrulingArrowInView
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsOverrulingArrowInView() => DriverExtensions.WaitForElement(OverrulingRiskArrowLocator).IsElementInView();

        /// <summary>
        /// Checks to see if the Procedural Posture text is present for a document.
        /// IsPresent is used for negative tests to verify if PP text is not present even in hide state of HTML.
        /// </summary>
        /// <returns> True - if Procedural Posture text is present </returns>
        public bool IsProceduralPostureTextPresent() => DriverExtensions.IsElementPresent(ProceduralPostureTextLocator);

        /// <summary>
        /// Is quotation grey icon displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsQuotationIconDisplayed() => DriverExtensions.IsDisplayed(QuotationIconLocator);

        /// <summary>
        /// Verify that doc section is displayed
        /// </summary>
        /// <param name="option">document section</param>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsDocumentSectionDisplayed(EdgeDocumentSection option) =>
            DriverExtensions.IsDisplayed(By.XPath(this.EdgeDocSectionsMap[option].LocatorString), 5);

        /// <summary>
        /// Scroll to the end of document
        /// 300 is document header height
        /// </summary>
        public void ScrollToEndOfDocument() => DriverExtensions.ScrollIntoView(EndOfDocumentLocator, 300);

        /// <summary>
        /// Scroll to document title
        /// </summary>
        /// <param name="docTitle"> The doc title </param>
        public void ScrollToDocumentTitle(string docTitle)
        {
            DriverExtensions.WaitForElementDisplayed(
                By.XPath(string.Format(DocumentTitleInReadingModeLctMask, docTitle)));
            this.ScrollPageToBottom();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Scroll to section
        /// </summary>
        /// <param name="sectionId"> Section ID </param>
        public void ScrollToSection(string sectionId) =>
            DriverExtensions.ScrollIntoView(By.XPath(string.Format(SectionLctMask, sectionId)), 300);

        /// <summary>
        /// Get highlighted term code by term id
        /// </summary>
        /// <param name="termId"> The term ID </param>
        /// <returns> Term color code </returns>
        public string GetTermColorCode(int termId) =>
                DriverExtensions.GetElement(By.XPath(string.Format(HighlightedTermLctMask, termId)))
                                .GetCssValue("background-color");


        /// <summary>
        /// Check and close pendo dialog
        /// </summary>
        public void CheckAndClosePendoDialog()
        {
            var closePendoButton = DriverExtensions.SafeGetElement(PendoCloseButtonLocator);
            if (closePendoButton != null) closePendoButton.Click();
        }

        /// <summary>
        /// Hovers on the flag specified
        /// </summary>
        /// <param name="flag"></param>
        public T HoverOnKeyCiteFlag<T>(KeyCiteFlag flag) where T : ICreatablePageObject
        {
            //var flagElement = GetFlagsList(flag).First();
            //flagElement.ScrollToElementCenter();
            //flagElement.SeleniumHover();
            //return DriverExtensions.CreatePageInstance<T>();

            var flagElements = GetFlagsList(flag);
            if (flagElements.Any())
            {
                var flagElement = flagElements.First();
                flagElement.ScrollToElementCenter();
                flagElement.SeleniumHover();
            }
            return DriverExtensions.CreatePageInstance<T>();
        }

        #region Methods for notes and highlights
        /// <summary>
        /// Select a specific document section
        /// </summary>
        /// <param name="docSection"> Doc section</param>
        /// <returns> The <see cref="HighlightMenuDialog"/>. </returns>
        public HighlightMenuDialog SelectDocumentSection(DocumentSection docSection) =>
            this.HighlightParagraph(By.CssSelector(this.DocSectionsMap[docSection].LocatorString));

        /// <summary>
        /// Select a entire document section
        /// </summary>
        /// <param name="docSection"> Doc section</param>
        /// <returns> The <see cref="HighlightMenuDialog"/>. </returns>
        public HighlightMenuDialog SelectEntireDocumentSection(DocumentSection docSection)
        {
            var paragraphElements = DriverExtensions.GetElements(By.CssSelector(this.DocSectionsMap[docSection].LocatorString));
            var firstParagraphElement = paragraphElements.ElementAt(0);
            var lastParagraphElement = paragraphElements.ElementAt(paragraphElements.Count - 1);

            firstParagraphElement.ScrollToElementCenter();
            firstParagraphElement.TriggerMouseEventByPoint(Pointer.PointerDown);
            DriverExtensions.HighlightMultipleNodes(firstParagraphElement, lastParagraphElement);
            firstParagraphElement.TriggerMouseEventByPoint(Pointer.PointerUp);

            return new HighlightMenuDialog();
        }

        /// <summary>
        /// Select Paragraph (paragraphIndex starts from 0)
        /// </summary>
        /// <param name="docSection"> Doc section </param>
        /// <param name="paragraphIndex"> Paragraph Index</param>
        /// <returns> EdgeHighlightMenuDialog </returns>
        public new HighlightMenuDialog SelectParagraph(DocumentSection docSection, int paragraphIndex)
        {
            IWebElement elem = DriverExtensions.GetElements(
                new ByChained(
                    By.CssSelector(this.DocSectionsMap[docSection].LocatorString),
                    ParagraphToHighlightLocator)).ElementAt(paragraphIndex);
            return this.HighlightParagraph(elem);
        }

        /// <summary>
        /// Selects specific node that contains given text (textNodeIndex starts from 1)
        /// </summary>
        /// <param name="textToSelect"> Text to be selected </param>
        /// <returns> <see cref="HighlightMenuDialog"/> </returns>
        public HighlightMenuDialog SelectText(string textToSelect) =>
            this.HighlightParagraph(By.XPath(string.Format(DocTextLctMask, textToSelect)));

        /// <summary>
        /// Highlight a term
        /// </summary>
        /// <param name="termId">Term ID </param>
        /// <returns> HighlightMenuDialog </returns>
        public HighlightMenuDialog HighlightTerm(int termId) =>
            this.HighlightParagraph(By.XPath(string.Format(HighlightedTermLctMask, termId)));

        /// <summary>
        /// Click highlight by text
        /// </summary>
        /// <param name="text"> Text </param>
        /// <returns> The <see cref="ManageHighlightDialog"/>. </returns>
        public ManageHighlightDialog ClickHighlightByText(string text)
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(HighlightLctMask, text))).CustomClick();
            return new ManageHighlightDialog();
        }

        /// <summary>
        /// Verifies that the maximize note icon is displayed
        /// </summary>
        /// <returns> True if the maximize note icon is displayed </returns>
        public bool IsMaximizeNoteIconDisplayed() => DriverExtensions.IsDisplayed(MaximizeNoteIconLocator);

        /// <summary>
        /// Verifies that the maximize note icon with shared icon displayed
        /// </summary>
        /// <returns> True if the maximize note icon is displayed </returns>
        public bool IsMaximizeNoteIconWithSharedIconDisplayed() =>
            DriverExtensions.IsDisplayed(MaximizeNoteIconWithSharedIconLocator);

        /// <summary>
        /// Click maximize note icon
        /// </summary>
        public void ClickMaximizeNoteIcon() => DriverExtensions.WaitForElement(MaximizeNoteIconLocator).JavascriptClick();

        /// <summary>
        /// Verifies that icon is in expected color
        /// </summary>
        public HighlightColor GetMaximizeNoteIconColor()
        {
            string elementClass = DriverExtensions.WaitForElement(MaximizeNoteIconLocator).GetAttribute("class");
            return elementClass.Contains("NoteIcon")
                       ? this.HighlightingColor.First(e => elementClass.Contains(e.Value.ClassName)).Key
                       : HighlightColor.None;
        }

        /// <summary>
        /// ClickOnBinaryLink
        /// </summary>
        /// <param name="text">link text</param>
        /// <param name="index">link index</param>
        public void ClickOnBinaryLink(string text, int index = 0)
            => DriverExtensions.GetElements(By.XPath(string.Format(BinaryLinkLctMask, text))).ElementAt(index).Click();

        /// <summary>
        /// Get the number of document notes
        /// </summary>
        /// <returns> Number of notes </returns>
        public int GetCountOfDocLevelNotes() =>
            DriverExtensions.WaitForElement(NoteIconCountLocator).Text.ConvertCountToInt();

        /// <summary>
        /// Close inline note
        /// </summary>
        public void CloseInlineNotes()
        {
            if (DriverExtensions.IsDisplayed(CloseNoteIconLocator))
            {
                DriverExtensions.Click(
                    DriverExtensions.GetElements(CloseNoteIconLocator).FirstOrDefault(icon => icon.Displayed));
            }
        }

        /// <summary>
        /// Gets list of document level notes
        /// </summary>
        /// <returns> List of <see cref="EdgeViewDocLevelNoteDialog"/> </returns>
        public new List<EdgeViewDocLevelNoteDialog> GetDocLevelNotesList() =>
            DriverExtensions.GetElements(DocLevelNotesContainerLocator, NoteLocator)
                            .Select(elem => new EdgeViewDocLevelNoteDialog(elem)).ToList();

        /// <summary>
        /// Click on highlighted text with inline note on document page
        /// </summary>
        /// <returns> The <see cref="EdgeViewInlineNoteDialog"/>.</returns>
        public EdgeViewInlineNoteDialog ClickHighlightTextWithInlineNote(DocumentSection docSection)
        {
            string text = DriverExtensions.GetElement(By.CssSelector(this.DocSectionsMap[docSection].LocatorString))
                                          .Text;
            DriverExtensions.WaitForElement(By.XPath(string.Format(HighlightLctMask, text))).CustomClick();
            return new EdgeViewInlineNoteDialog(DriverExtensions.GetElement(InlineNoteOnDocContainerLocator));
        }

        /// <summary>
        /// Toggle inline notes chevron
        /// </summary>
        /// <param name="toggle">True to expand chevron, false to collapse.</param>
        public void ToggleInlineNotesChevron(bool toggle)
        {
            if (this.IsChevronButtonDisplayed() && (toggle != this.InlineNotes.NavigationComponent.IsDisplayed()))
            {
                DriverExtensions.WaitForElement(ExpandCollapseChevronButtonLocator).Click();
                DriverExtensions.WaitForJavaScript();
            }
        }

        /// <summary>
        /// Is chevron button displayed
        /// </summary>
        public bool IsChevronButtonDisplayed() => DriverExtensions.IsDisplayed(ExpandCollapseChevronButtonLocator);

        /// <summary>
        /// Check if tooltip text is displayed
        /// </summary>
        /// <param name="tooltipText"></param>
        /// <returns>true if displayed, else false</returns>
        public bool IsTooltipTextDisplayed(string tooltipText) => DriverExtensions.IsEnabled(By.XPath(string.Format(TooltipTextLctMask, tooltipText)));

        /// <summary>
        /// Check if case pane is displayed
        /// </summary>
        /// <returns></returns>
        public bool IsCasePaneDisplayed() => DriverExtensions.IsDisplayed(CaseNavigationContainerLocator);

        /// <summary>
        /// Check if notes pane is displayed
        /// </summary>
        /// <returns></returns>
        public bool IsNotesPaneDisplayed() => DriverExtensions.IsDisplayed(NotesPaneLocator);

        /// <summary>
        /// Check if case pane is displayed
        /// </summary>
        /// <returns></returns>
        public bool IsCaseStickinessPaneExpanded() => DriverExtensions.GetText(new ByChained(ExpandCollapseChevronButtonLocator, ExpandCollapseChevronButtonStatusLocator)) == "Collapse Notes";

        /// <summary>
        /// Check if Notes pane is displayed
        /// </summary>
        /// <returns></returns>
        public bool IsNoteStickinessPaneExpanded() => DriverExtensions.GetText(new ByChained(ExpandCollapseChevronButtonLocator, ExpandCollapseChevronButtonStatusLocator)) != "Collapse Notes";

        /// <summary>
        /// Highlight Paragraph
        /// </summary>
        /// <param name="paragraphElement"> Element To Highlight </param>
        /// <returns> The <see cref="HighlightMenuDialog"/>. </returns>
        private HighlightMenuDialog HighlightParagraph(IWebElement paragraphElement)
        {
            paragraphElement.ScrollToElementCenter();
            paragraphElement.TriggerMouseEventByPoint(Pointer.PointerDown);
            paragraphElement.HighlightText();
            paragraphElement.TriggerMouseEventByPoint(Pointer.PointerUp);
            return new HighlightMenuDialog();
        }

        /// <summary>
        /// Highlight Paragraph
        /// </summary>
        /// <param name="elementToHighlight"> Element To Highlight </param>
        /// <returns> The <see cref="HighlightMenuDialog"/>. </returns>
        private HighlightMenuDialog HighlightParagraph(By elementToHighlight)
        {
            DriverExtensions.WaitForElementDisplayed(elementToHighlight);
            IWebElement paragraphElement = DriverExtensions.GetElement(elementToHighlight);
            return this.HighlightParagraph(paragraphElement);
        }
        #endregion

        /// <summary>
        /// Get color type by a code
        /// </summary>
        /// <param name="termCode"> Term color rgb code </param>
        /// <returns> Term color </returns>
        protected TermColors GetColorTypeByCode(string termCode) =>
            Enum.GetValues(typeof(TermColors)).Cast<TermColors>().First(
                color => this.TermColorMap[color].BackgroundColorCode.Equals(termCode));

        /// <summary>
        /// Get flag list
        /// </summary>
        /// <param name="flags"> The flags </param>
        /// <returns> The list of flags </returns>
        private IReadOnlyCollection<IWebElement> GetFlagsList(KeyCiteFlag flags) =>
            DriverExtensions.GetElements(By.XPath(this.DocumentKeyCiteFlagsMap[flags].LocatorString));
    }
}