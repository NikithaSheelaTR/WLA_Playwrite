namespace Framework.Common.UI.Products.WestLawNext.Pages
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Products.WestLawNext.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using OpenQA.Selenium;

    /// <summary>
    /// ResearchReportPage
    /// </summary>
    public class ResearchReportPage : BaseModuleRegressionPage
    {
        private const string DocAtIndexLctMask = "id('co_docContentBody')/div[contains(@class,'kh_division')][{0}]";

        private const string ItiForDocumentAtIndexLctMask = "id('co_docContentBody')//div[contains(@class,'co_documentSummary')][{0}]";

        private const string DocLevelNotesContainerLctMsk = "//div[contains(@class,'co_documentLevelNote')]//em[contains(text(),'{0}')]";

        private const string InlineNotesContainerLctMsk = "//div[contains(@class,'co_documentInlineNote')]//em[contains(text(),'{0}')]";

        private const string HighlightedTextLctMsk = "//div[contains(@class,'co_documentInlineNote')]//span[contains(text(),'{0}')]";

        private static readonly By DocumentHighlightedTitleLocator = By.XPath(".//h3[@class='co_documentInlineNoteTitle']");

        private static readonly By DocumentHighlightedBodylocator = By.XPath(".//div[contains(@class, 'co_documentInlineNote')]");

        private static readonly By OrganizeReportLinkLocator = By.Id("co_OrganizeReport");

        private static readonly By TitleForDocumentAtIndexLocator = By.XPath(".//h3[@class='co_documentLevelNoteTitle']");

        private static readonly By BodyForDocumentAtIndexLocator = By.XPath(".//div[contains(@class, 'co_documentLevelNote')]");

        private static readonly By CloseTableOfContentLocator = By.CssSelector("a.kh_icon.icon-cross");

        private static readonly By DocLincsLocator = By.CssSelector(".co_document_previouslyviewed");

        private static readonly By TableOfContentLocator = By.CssSelector("div.tableOfContentTocReport.menu-toggle.is-active button");

        private static readonly By ResearchReportListItemLocator = By.XPath("//div[contains(@ng-repeat,'documentAnnotations')]");

        private static readonly By IncludeAnnotationsCheckboxLocator = By.XPath("//label/input[@ng-checked ='shouldDisplayAnnotations']");
        private static readonly By IncludeAnnotationsLabelText = By.XPath("//div[@id='coid_hideAllAnnotationsCheckbox']/label");

        private static readonly By AnnotationsBlockLocator = By.XPath("//div[contains(@class,'annotationsContent')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="ResearchReportPage"/> class. 
        /// </summary>
        public ResearchReportPage()
        {
            DriverExtensions.WaitForElement(ResearchReportListItemLocator);
        }

        /// <summary>
        /// Gets the delivery section.
        /// </summary>
        public DeliveryVertical DeliverySection { get; private set; } = new DeliveryVertical();

        /// <summary>
        /// The is organize report button displayed.
        /// </summary>
        public bool IsOrganizeReportButtonDisplayed => DriverExtensions.IsDisplayed(OrganizeReportLinkLocator);

        /// <summary>
        /// The are annotations displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool AreAnnotationsDisplayed() => !DriverExtensions.WaitForElement(AnnotationsBlockLocator).GetAttribute("class").Contains("ng-hide");

        /// <summary>
        /// Clicks the document link 
        /// </summary>
        /// <param name="index">Int Index</param>
        /// <typeparam name="T">PageObject</typeparam>
        /// <returns>New instance of T page</returns>
        public T ClickDocumentByIndex<T>(int index) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForElement(DocLincsLocator);
            IWebElement docLincIwe = DriverExtensions.GetElements(DocLincsLocator).ElementAt(index);
            docLincIwe.ScrollToElement();
            DriverExtensions.Click(docLincIwe);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on Organize Report link
        /// </summary>
        /// <returns></returns>
        public OrganizeReportDialog ClickOrganizeReport()
        {
            DriverExtensions.GetElement(OrganizeReportLinkLocator).WaitForElementEnabled();
            DriverExtensions.Click(OrganizeReportLinkLocator);
            DriverExtensions.WaitForJavaScript();

            return new OrganizeReportDialog();
        }

        /// <summary>
        /// takes user back to the research summary page
        /// </summary>
        public void CloseToc()
        {
            DriverExtensions.WaitForElement(TableOfContentLocator).Click();
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Verifies doc level note displayed.
        /// </summary>
        /// <param name="authorName">The author Name.</param>
        /// <returns>
        /// true if note is displayed, false otherwise
        /// </returns>
        public bool IsDocLevelNoteDisplayed(string authorName) =>
            DriverExtensions.IsDisplayed(By.XPath(string.Format(DocLevelNotesContainerLctMsk, authorName)));

        /// <summary>
        /// Verifies inline note displayed.
        /// </summary>
        /// <param name="authorName">The author Name.</param>
        /// <returns>
        /// true if note is displayed, false otherwise
        /// </returns>
        public bool IsInlineNoteDisplayed(string authorName) =>
            DriverExtensions.IsDisplayed(By.XPath(string.Format(InlineNotesContainerLctMsk, authorName)));

        /// <summary>
        /// Verifies highlighted text displayed.
        /// </summary>
        /// <param name="text">The highlighted text.</param>
        /// <returns>
        /// true if note is displayed, false otherwise
        /// </returns>
        public bool IsHighlightedTextDisplayed(string text) =>
            DriverExtensions.IsDisplayed(By.XPath(string.Format(HighlightedTextLctMsk, text)));

        /// <summary>
        /// Is the highlighted text in the document displayed
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsHighlightedTextDisplayedForDocumentAtIndex(int index = 0)
        {
            IWebElement docIwe = this.GetDocumentSectionAtIndex(index);
            return DriverExtensions.GetElement(docIwe, DocumentHighlightedTitleLocator).Displayed
                   && DriverExtensions.GetElement(docIwe, DocumentHighlightedBodylocator).Displayed;
        }

        /// <summary>
        /// Is ITI displayed for the document at given index
        /// </summary>
        /// <param name="index">Int index of the document</param>
        /// <returns>True if document ITI is displayed, otherwise - false</returns>
        public bool IsItiDisplayedForDocumentAtIndex(int index = 0)
            => DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(ItiForDocumentAtIndexLctMask, index + 1)).Displayed;

        /// <summary>
        /// Is the note in the document displayed
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsNoteTextDisplayedForDocumentAtIndex(int index = 0)
        {
            IWebElement docIwe = this.GetDocumentSectionAtIndex(index);
            return DriverExtensions.GetElement(docIwe, TitleForDocumentAtIndexLocator).Displayed
                   && DriverExtensions.GetElement(docIwe, BodyForDocumentAtIndexLocator).Displayed;
        }

        /// <summary>
        /// The get research report list items.
        /// </summary>
        /// <returns>The <see cref="ResearchReportListItem"/>.</returns>
        public List<ResearchReportListItem> GetResearchReportListItems()
        {
            DriverExtensions.WaitForElementDisplayed(ResearchReportListItemLocator);
            return DriverExtensions.GetElements(ResearchReportListItemLocator)
                                   .Select(item => new ResearchReportListItem(item)).ToList();
        }

        /// <summary>
        /// The get research report list item by citation.
        /// </summary>
        /// <param name="citation">The citation.</param>
        /// <returns>The <see cref="ResearchReportListItem"/>.</returns>
        public ResearchReportListItem GetResearchReportListItemByCitation(string citation) =>
            this.GetResearchReportListItems().Find(item => item.Citation.Equals(citation));

        /// <summary>
        /// Verifies if include annotations checkbox is displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsIncludeAnnotationsCheckboxDisplayed() =>
            DriverExtensions.IsDisplayed(IncludeAnnotationsCheckboxLocator);

        /// <summary>
        /// Get Include Annotations Checkbox Text
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetIncludeAnnotationsCheckboxText() =>
            DriverExtensions.WaitForElement(IncludeAnnotationsLabelText).Text;

        /// <summary>
        /// The set include annotations checkbox.
        /// </summary>
        /// <param name="setTo">
        /// The state.
        /// </param>
        /// <returns>
        /// The <see cref="ResearchReportPage"/>.
        /// </returns>
        public ResearchReportPage SetIncludeAnnotationsCheckbox(bool setTo = true)
        {
            DriverExtensions.SetCheckbox(IncludeAnnotationsCheckboxLocator, setTo);
            DriverExtensions.WaitForJavaScript();
            return this;
        }

        /// <summary>
        /// Get document section at index
        /// </summary>
        /// <param name="index">Int index</param>
        /// <returns>IWebElement</returns>
        private IWebElement GetDocumentSectionAtIndex(int index) =>
            DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(DocAtIndexLctMask, index + 1));
    }
}