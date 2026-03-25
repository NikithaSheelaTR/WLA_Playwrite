namespace Framework.Common.UI.Products.Shared.Pages.Document
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Facets.RightFacets;
    using Framework.Common.UI.Products.Shared.Enums.Document;
    using Framework.Common.UI.Products.WestLawNext.Components.Document;
    using Framework.Common.UI.Products.WestLawNext.Pages.SecondarySources;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// PublicationDocumentPage
    /// </summary>
    public class PublicationDocumentPage : DocumentWithFootnotesPage
    {
        private const string ColorForSearchTermString = "rgba(255, 255, 102, 1)";

        private const string DocumentLinkLctMask = "//*[@id='co_document']//a[text()={0}]";

        private const string DocumentTitleInReadingModeLctMask = "//*[@class='co_title']/div[text()=\"{0}\"] | //*[@class='co_title co_headtext' and text()=\"{0}\"]";

        private const string DocumentLctMask = "documentContainer_{0}";

        private const string ConfirmViewDocButtonLctMask = "confirmViewDoc_{0}";

        private const string HeaderLinkLctMask = "//div[@class='co_headtext' and contains(.,'{0}')]";

        private const string PinpointLocationLctMask = "//div[@class='co_paragraphText' and .//a[contains(@id,'{0}')]]";

        private const string InfiniteScrollPageUrlPart = "/Document/{0}/InfiniteScroll";

        private static readonly By BaselineCopyrightLocator = By.CssSelector(".co_copyright");

        private static readonly By CoCommentaryLocator = By.XPath("//*[contains(@class,'co_commentary')]/..");

        private static readonly By CloseDocumentLoadingDialogButtonLocator = By.XPath("//div[@id='coid_lightboxOverlay']//input[@value='Cancel']");

        private static readonly By DocTextLocator = By.Id("co_document_0");

        private static readonly By DynamicScrollingHeaderTitleLocator = By.XPath("//div[@id='co_docContentHeader']//div[@class='co_headtext']");

        private static readonly By DocumentContentPanelLocator = By.Id("co_contentColumn");

        private static readonly By DocTitleLineLocator = By.XPath("//*[@id='co_document_0']/div[1]");

        private static readonly By EndOfDocumentLocator = By.Id("co_endOfDocument");

        private static readonly By PdfDocumentLocator = By.XPath("//div[@class='co_imageBlock']");

        private static readonly By PdfsElementContainerLocator = By.XPath("//div[@class='co_form']");

        private static readonly By ReadingModePublicationTitleLocator = By.XPath("//h1[@id='co_docHeaderTitleLine']");

        private static readonly By ViewTableIconLocator =
            By.XPath("//a[contains(@class,'co_fullscreenButton')]/span[contains(@class,'icon_table_fullscreen')]");

        private static readonly By ViewTableLinkLocator = By.XPath("//a[contains(@class,'co_fullscreenButton')]");

        private static readonly By DocumentLogoLocator = By.XPath("//div[@class='co_treatisesHeaderImage']/img");

        /// <summary>
        /// Gets the toc doc view component.
        /// </summary>
        public TocDocViewComponent TocDocViewComponent { get; } = new TocDocViewComponent();

        /// <summary>
        /// Gets the inline toc component.
        /// </summary>
        public DocumentInlineTocComponent InlineTocComponent { get; } = new DocumentInlineTocComponent();

        /// <summary>
        /// Form Families Facet Component
        /// </summary>
        public FormFamiliesFacetComponent FormFamiliesFacetComponent { get; } = new FormFamiliesFacetComponent();

        /// <summary>
        /// The are pdf links on separate lines.
        /// selenium can not take text nodes, it is needed to take a text of form element and replace document text with empty string
        /// replace string pattern and check that no more symbols are left in a string
        /// this is done to exclude additional spaces or special symbols are left
        /// </summary>
        /// <param name="pdfImageCount">
        /// The link text.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool ArePdfLinksOnSeparateLines(int pdfImageCount = 5)
        {
            string text = new Regex(@"((\r\n)Notes.*)+([\r\n]+.*)+").Replace(DriverExtensions.GetElement(PdfsElementContainerLocator).Text, string.Empty);
            IEnumerable<IWebElement> pdfElements = DriverExtensions.GetElements(PdfDocumentLocator);
            return new Regex("Image \\d+ within document in PDF format.\\r\\n")
                       .Replace(text, string.Empty).Replace(pdfElements.Last().GetText(), string.Empty).Equals(string.Empty)
                   && pdfElements.Count() == pdfImageCount;
        }

        /// <summary>
        /// The are search terms highlighted.
        /// </summary>
        /// <param name="searchTermText"> The search text. </param>
        /// <returns> True if highlighted, false otherwise  </returns>
        public bool AreSearchTermsHighlighted(string searchTermText)
            => this.GetSearchTermsList().Any(elem => elem.Text.Equals(searchTermText) && elem.Displayed 
            && elem.GetCssValue("background-color").Equals(ColorForSearchTermString));

        /// <summary>
        /// The click link in document.
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="linkText"> The link text. </param>
        /// <returns> New Page instance </returns>
        public T ClickDocumentLink<T>(string linkText) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(DocumentLinkLctMask, linkText)).CustomClick();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// The close document loading popup.
        /// </summary>
        public void CloseDocumentLoadingDialog()
        {
            if (DriverExtensions.IsDisplayed(CloseDocumentLoadingDialogButtonLocator))
            {
                DriverExtensions.Click(CloseDocumentLoadingDialogButtonLocator);
            }
        }

        /// <summary>
        /// The click view table link and switch to new tab.
        /// </summary>
        /// <param name="newTabTitle"> The new tab title. </param>
        /// <returns> The <see cref="SecondarySourcesTablePage"/>. </returns>
        public SecondarySourcesTablePage ClickViewTableLinkAndSwitchToNewTab(string newTabTitle)
            => this.ClickAndOpenNewBrowserTab<SecondarySourcesTablePage>(ViewTableLinkLocator, newTabTitle);

        /// <summary>
        /// The click view table link and switch to new tab.
        /// </summary>
        /// <param name="newTabTitle"> The new tab title. </param>
        public void ClickViewTableIconAndSwitchToNewTab(string newTabTitle)
            => this.ClickAndOpenNewBrowserTab<PublicationDocumentPage>(ViewTableIconLocator, newTabTitle);

        /// <summary>
        /// The click remoteness confirm view button.
        /// </summary>
        /// <param name="documentId"> The document Id. </param>
        public void ClickRemotenessConfirmViewButton(string documentId) 
            => DriverExtensions.GetElement(By.Id(string.Format(ConfirmViewDocButtonLctMask, documentId))).Click();

        /// <summary>
        /// Return the citation title
        /// </summary>
        /// <returns> Citation title </returns>
        public string GetCitationTitle() => DriverExtensions.GetText(DocTitleLineLocator);

        /// <summary>
        /// The get dynamic scrolling header title.
        /// </summary>
        /// <returns> Header Title </returns>
        public string GetDynamicScrollingHeaderTitle() => DriverExtensions.GetText(DynamicScrollingHeaderTitleLocator);

        /// <summary>
        /// The is document displayed in content panel.
        /// </summary>
        /// <returns> True if displayed. false otherwise </returns>
        public bool IsDocumentDisplayedInContentPanel() =>
            DriverExtensions.IsDisplayed(DriverExtensions.GetElement(DocumentContentPanelLocator), DocTextLocator);

        /// <summary>
        /// Verify that the baseline copyright is diplayed
        /// </summary>
        /// <returns>Boolean</returns>
        public bool IsBaselineCopyrightDisplayed() => DriverExtensions.IsDisplayed(BaselineCopyrightLocator, 5);

        /// <summary>
        /// The is co commentary used.
        /// </summary>
        /// <returns> True if commentary displayed, false otherwise </returns>
        public bool IsCoCommentaryUsed() => DriverExtensions.IsDisplayed(DocTextLocator, 5) && DriverExtensions.IsDisplayed(CoCommentaryLocator, 5);

        /// <summary>
        /// Is document link displayed.
        /// </summary>
        /// <param name="linkText"> Link text </param>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsDocumentLinkDisplayed(string linkText) => DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(DocumentLinkLctMask, linkText));

        /// <summary>
        /// The is doc content contains links.
        /// </summary>
        /// <param name="link"> The link. </param>
        /// <returns> True if contains, false otherwise </returns>
        public bool IsDocContentContainsLink(string link)
        {
            IWebElement element = DriverExtensions.WaitForElement(By.CssSelector(this.DocSectionsMap[DocumentSection.Paragraph].LocatorString));
            return DriverExtensions.GetElements(element, By.XPath("./descendant::a")).Count > 0 && element.GetText().Contains(link);
        }

        /// <summary>
        /// The is document title is displayed in reading mode.
        /// </summary>
        /// <param name="docTitle"> The doc title. </param>
        /// <returns> True if title is displayed, false otherwise </returns>
        public bool IsDocumentTitleDisplayedInReadingMode(string docTitle) => DriverExtensions.IsDisplayed(
            By.XPath(string.Format(DocumentTitleInReadingModeLctMask, docTitle)), 5);

        /// <summary>
        /// The is infinit scroll page is opened.
        /// </summary>
        /// <param name="documentGuid"> The document guid. </param>
        /// <returns> True if Infinit page is opened </returns>
        public bool IsInfinitScrollPageIsOpened(string documentGuid)
            => BrowserPool.CurrentBrowser.Url.Contains(string.Format(InfiniteScrollPageUrlPart, documentGuid));
       
        /// <summary>
        /// Verify that pinpoint location displayed.
        /// </summary>
        /// <param name="paragraph">The <see cref="string"/></param>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsPinpointLocationDisplayed(string paragraph) =>
            DriverExtensions.IsDisplayed(By.XPath(string.Format(PinpointLocationLctMask, paragraph)));

        /// <summary>
        /// The is remoteness doc displayed.
        /// </summary>
        /// <param name="documentId"> The document Id. </param>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsRemotenessDocDisplayed(string documentId) => DriverExtensions.IsDisplayed(By.Id(string.Format(DocumentLctMask, documentId)), 5);

        /// <summary>
        /// Verify that text has header style.
        /// </summary>
        /// <param name="headerText"> The link. </param>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsTableOfContentsHeaderDisplayed(string headerText)
            => DriverExtensions.IsDisplayed(By.XPath(string.Format(HeaderLinkLctMask, headerText)));

        /// <summary>
        /// The is reading mode publication title displayed.
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsReadingModePublicationTitleDisplayed() => DriverExtensions.IsDisplayed(ReadingModePublicationTitleLocator);

        /// <summary>
        /// The is view table icon displayed.
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsViewTableIconDisplayed() => DriverExtensions.IsDisplayed(ViewTableIconLocator, 5);

        /// <summary>
        /// The is view table link displayed.
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsViewTableLinkDisplayed() => DriverExtensions.IsDisplayed(ViewTableLinkLocator, 5);

        /// <summary>
        /// The scroll to document title.
        /// </summary>
        /// <param name="docTitle"> The doc title. </param>
        public void ScrollToDocumentTitle(string docTitle) => DriverExtensions.ScrollIntoView(By.XPath(string.Format(DocumentTitleInReadingModeLctMask, docTitle)), 300);

        /// <summary>
        /// The scroll to end of document.
        /// </summary>
        public void ScrollToEndOfDocument() => DriverExtensions.ScrollIntoView(EndOfDocumentLocator, 300);

        /// <summary>
        /// Get list of logos in the document
        /// </summary>
        /// <returns> Document logos </returns>
        public List<string> GetDocumentLogosText() 
            => DriverExtensions.GetElements(DocumentLogoLocator).Select(element => element.GetAttribute("alt")).ToList();
    }
}
