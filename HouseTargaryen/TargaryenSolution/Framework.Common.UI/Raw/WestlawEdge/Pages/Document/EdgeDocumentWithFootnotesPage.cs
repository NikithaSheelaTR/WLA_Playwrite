namespace Framework.Common.UI.Raw.WestlawEdge.Pages.Document
{
    using Framework.Common.UI.Products.WestlawEdge.Components.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// 
    /// </summary>
    public class EdgeDocumentWithFootnotesPage : EdgeDocumentPageWithHeadnotes
    {
        private const string TextMarkedByFootnoteLctMask = "//*[contains(@class, 'co_footnoteReference') and text()='{0}']/ancestor::span";
        private const string FootnoteReferenceLctMask = "//*[contains(@class, 'co_footnoteReference') and text()='{0}']";
        private const string FootnoteCloseButton = "//button[@id='co_kcFlagPopup_closeButton']";

        private static readonly By FootnoteHoverDivLocator = By.Id("co_footnoteHoverDiv");

        /// <summary> Footnotes Section component </summary>
        public FootnotesSectionComponent FootnotesSection { get; } = new FootnotesSectionComponent();

        /// <summary>
        /// Is footnote reference displayed
        /// </summary>
        /// <param name="footnoteName"> The footnote name </param>
        /// <returns> True if displayed </returns>
        public bool IsFootnoteReferenceDisplayed(string footnoteName) => DriverExtensions.IsDisplayed(By.XPath(string.Format(FootnoteReferenceLctMask, footnoteName)), 5);

        /// <summary>
        /// Is text marked by footnote within a document correct
        /// </summary>
        /// <param name="footnoteName"> The footnote name </param>
        /// <param name="textBeforeFootnote"> The text before footnote </param>
        /// <returns> True if marked correct</returns>
        public bool IsTextMarkedByFootnoteCorrect(string footnoteName, string textBeforeFootnote) => DriverExtensions
                                                                                                     .GetImmediateText(By.XPath(string.Format(TextMarkedByFootnoteLctMask, footnoteName))).Trim()
                                                                                                     .Equals(textBeforeFootnote);

        /// <summary>
        /// Click footnote reference within the document
        /// </summary>
        /// <param name="footnoteName"> The footnote name </param>
        public void ClickFootnoteReference(string footnoteName) => DriverExtensions.Click(By.XPath(string.Format(FootnoteReferenceLctMask, footnoteName)));

        /// <summary>
        /// Hover over footnote reference within a document
        /// </summary>
        /// <param name="footnoteName"> The footnote name </param>
        /// <param name="offsetX"> The horizontal offset to which to move the mouse </param>
        /// <param name="offsetY"> The vertical offset to which to move the mouse </param>
        public void HoverOverFootnoteReference(string footnoteName, int offsetX = 1, int offsetY = 1)
        {
            var footNote = DriverExtensions.GetElement(By.XPath(string.Format(FootnoteReferenceLctMask, footnoteName)));
            footNote.ScrollToElement();
            DriverExtensions.WaitForJavaScript();
            footNote.SeleniumHover();
            footNote.Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Hover out a footnote by index
        /// Move the mouse on 100, 100 to hover off of the link
        /// </summary>
        /// <param name="footnoteName"> The footnote name </param>
        public void HoverOutFootnoteReference(string footnoteName)
        {
            DriverExtensions.GetElement(By.XPath(string.Format(FootnoteReferenceLctMask, footnoteName))).SeleniumHoverOut(100, 100);
            DriverExtensions.WaitForElement(By.XPath(FootnoteCloseButton)).Click();
            DriverExtensions.WaitForElementNotDisplayed(1500, FootnoteHoverDivLocator);
        }

        /// <summary>
        /// Is Footnote dialog hidden
        /// </summary>
        /// <returns> True if it is hidden, false otherwise </returns>
        public bool IsFootnoteDialogHidden()
            => DriverExtensions.WaitForElement(FootnoteHoverDivLocator).GetAttribute("class").ToLowerInvariant().Contains("hidden");
    }
}