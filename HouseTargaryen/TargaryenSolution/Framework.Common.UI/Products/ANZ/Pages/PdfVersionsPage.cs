namespace Framework.Common.UI.Products.ANZ.Pages
{
    using Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Pdf Versions Page
    /// </summary>
    public class PdfVersionsPage : EdgeTabPage
    {
        private const string PdfIconLctMask = "//input[@title='{0}']/following-sibling::a";

        private static readonly By CurrentVersionPdfListLocator = By.XPath("//*[text()='Thomson Reuters Current Version PDF']/following-sibling::ul");
        private static readonly By GovernmentPdfsListLocator = By.XPath("//*[text()='New Zealand Government PDFs']/following-sibling::ul");
        
        /// <summary>
        /// Get count of checkboxes for TR Current Version Pdf
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetCountOfCurrentVersionItems()
            => this.GetChildCheckboxes(DriverExtensions.WaitForElement(CurrentVersionPdfListLocator)).Count;

        /// <summary>
        /// Get count of checkboxes for NZ Government Pdfs
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetCountOfGovernmentItems()
            => this.GetChildCheckboxes(DriverExtensions.WaitForElement(GovernmentPdfsListLocator)).Count;

        /// <summary>
        /// Click pdf icon by Document Name
        /// </summary>
        /// <param name="docName">
        /// Document Name.
        /// </param>
        public void ClickPdfIconByDocumentName(string docName) => DriverExtensions.WaitForElement(By.XPath(string.Format(PdfIconLctMask, docName))).Click();
    }
}
