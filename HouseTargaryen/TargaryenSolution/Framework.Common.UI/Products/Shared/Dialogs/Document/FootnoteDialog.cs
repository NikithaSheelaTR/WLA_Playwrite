namespace Framework.Common.UI.Products.Shared.Dialogs.Document
{
    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// FootnoteDialog - appears after hovering Footnote link
    /// </summary>
    public class FootnoteDialog : BaseModuleRegressionDialog
    {
        private const string FootnoteTextLctMask = "//div[@id='co_footnoteHoverDiv']//div[@class='co_paragraphText' and contains(text(),'{0}')]";

        private const string FootnoteLinkLctMask = "//div[@class='co_footnoteHoverBody']//a[contains(text(),'{0}')]";

        private static readonly By ContentLocator = By.XPath("//div[@class='co_footnoteHoverContent']");

        private static readonly By TitleLocator = By.XPath("//div[@class='co_footnoteHoverTitle']/span");

        /// <summary>
        /// Verify if scroll bar displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsScrollbarDisplayed()
        {
            IWebElement footnoteHoverContent = DriverExtensions.SafeGetElement(ContentLocator);
            return footnoteHoverContent != null
                   && footnoteHoverContent.GetElementScrollHight() > footnoteHoverContent.Size.Height;
        }

        /// <summary>
        /// Get Content text
        /// </summary>
        /// <returns> Content text </returns>
        public string GetContentText() => DriverExtensions.GetText(ContentLocator);

        /// <summary>
        /// Get Footnote Dialog title
        /// </summary>
        /// <returns> Title </returns>
        public string GetTitleText() => DriverExtensions.GetText(TitleLocator);

        /// <summary>
        /// Verify that title is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsTitleDisplayed() => DriverExtensions.IsDisplayed(TitleLocator);

        /// <summary>
        /// Verify that text is displayed in footnote dialog
        /// </summary>
        /// <param name="text"> The hover text. </param>
        /// <returns> True if text displayed in Footnote dialog, false otherwise </returns>
        public bool IsFootnoteTextDisplayed(string text)
            => DriverExtensions.IsDisplayed(By.XPath(string.Format(FootnoteTextLctMask, text)), 5);

        /// <summary>
        /// The click footnote hover link.
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="linkText"> The link text. </param>
        /// <returns> New instance of the page </returns>
        public T ClickFootnoteLink<T>(string linkText) where T : DocumentWithFootnotesPage
        {
            DriverExtensions.WaitForElement(TitleLocator);
            DriverExtensions.JavascriptClick(By.XPath(string.Format(FootnoteLinkLctMask, linkText))); 
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
