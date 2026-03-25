namespace Framework.Common.UI.Products.Shared.Pages.Document
{
    using Framework.Common.UI.Products.Shared.Components.Document;
    using Framework.Common.UI.Products.Shared.Dialogs.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Document Page with Footnotes component
    /// </summary>
    public class DocumentWithFootnotesPage : CommonDocumentPage
    {
        private const int TimeToWait = 1500;

        private const string FootnoteReferenceLctMask = "//*[contains(@class, 'co_footnoteReference') and text()='{0}']";

        private const string TextRelatedToFootnoteLctMask = "//span[.//*[contains(@class, 'co_footnoteReference') and text()='{0}']]";

        private static readonly By FootnoteInfoMessageLocator = By.Id("co_footnoteHoverDiv");

        private const string FootnoteCloseButton = "//button[@id='co_kcFlagPopup_closeButton']";
        /// <summary>
        /// Footnotes
        /// </summary>
        public FootnotesComponent Footnotes { get; set; } = new FootnotesComponent();

        /// <summary>
        /// Verify that Footnote information message is hidden
        /// </summary>
        /// <returns> True if hidden, false otherwise </returns>
        public bool IsFootnoteDialogHidden()
            => DriverExtensions.WaitForElement(FootnoteInfoMessageLocator).GetAttribute("class").ToLowerInvariant().Contains("hidden");

        /// <summary>
        /// Clicks on a footnote by index
        /// </summary>
        /// <param name="footnoteName"> Footnote name </param>
        public void ClickOnFootnote(string footnoteName)
            => DriverExtensions.WaitForElement(By.XPath(string.Format(FootnoteReferenceLctMask, footnoteName))).Click();

        /// <summary>
        /// Hovers over a footnote by index.
        /// </summary>
        /// <param name="footnoteName"> Footnote name </param>
        /// <returns> The <see cref="FootnoteDialog"/>. </returns>
        public FootnoteDialog HoverOverFootnote(string footnoteName)
        {
            var footNote = DriverExtensions.GetElement(By.XPath(string.Format(FootnoteReferenceLctMask, footnoteName)));
            footNote.ScrollToElement();
            DriverExtensions.WaitForJavaScript();
            //footNote.SeleniumHover();
            footNote.Click();
            DriverExtensions.WaitForJavaScript();
            return new FootnoteDialog();
        }

        /// <summary>
        /// Hover out footnote and hovers LogoLink
        /// </summary>
        public void HoverOutFootnote()
        {
            this.Header.HoverOverLogoLink();
            DriverExtensions.WaitForElement(By.XPath(FootnoteCloseButton)).Click();
            DriverExtensions.WaitForElementNotDisplayed(TimeToWait, FootnoteInfoMessageLocator);
        }

        /// <summary>
        /// The is footnote reference displayed.
        /// </summary>
        /// <param name="footnoteName"> The footnote name. </param>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsFootnoteReferenceDisplayed(string footnoteName) =>
            DriverExtensions.IsDisplayed(By.XPath(string.Format(FootnoteReferenceLctMask, footnoteName)));

        /// <summary>
        /// Get text related to footnote
        /// </summary>
        /// <param name="footNoteName"> Footnote name </param>
        /// <returns> Text near the footnote </returns>
        public string GetTextRelatedToFootnote(string footNoteName)
            => DriverExtensions.GetImmediateText(By.XPath(string.Format(TextRelatedToFootnoteLctMask, footNoteName))).Trim();
    }
}
