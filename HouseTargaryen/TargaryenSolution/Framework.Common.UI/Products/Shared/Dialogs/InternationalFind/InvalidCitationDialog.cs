namespace Framework.Common.UI.Products.Shared.Dialogs.InternationalFind
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// The Invalid Citation Dialog
    /// </summary>
    public class InvalidCitationDialog : FindByCitationBaseDialog
    {
        private static readonly By CitationFieldLocator = By.Id("co_invalid_citations");

        private static readonly By DialogMessageLocator = By.XPath("//div[@class = 'co_overlayBox_content']/div[1]");

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCitationDialog"/> class. 
        /// </summary>
        public InvalidCitationDialog()
            : base("Invalid Citation")
        {
        }

        /// <summary>
        /// Correct Invalid citation
        /// </summary>
        /// <param name="citation">correct citation</param>
        /// <returns>The <see cref="InvalidCitationDialog"/></returns>
        public InvalidCitationDialog CorrectInvalidCitation(string citation)
        {
            DriverExtensions.WaitForElement(CitationFieldLocator).Clear();
            DriverExtensions.GetElement(CitationFieldLocator).SendKeys(citation);
            return this;
        }

        /// <summary>
        /// Get Dialog message
        /// </summary>
        /// /// <returns> The <see cref="string"/>Invalid light-box message </returns>
        public string GetDialogMessage() => DriverExtensions.GetText(DialogMessageLocator);
    }
}