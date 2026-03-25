namespace Framework.Common.UI.Products.Shared.Dialogs.InternationalFind
{
    using System.Linq;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Non Unique Citation Dialog
    /// </summary>
    public class NonUniqueCitationDialog : FindByCitationBaseDialog
    {
        private static readonly By NonUniqueCitationsLocator = By.XPath(
            "//div[contains(@class, 'co_overlayBox_container')]//li[@class='co_formTextInline']");

        private static readonly By CitationCheckboxLocator = By.XPath(".//input[@type='radio']");

        private static readonly By DoNotDeliverCitationCheckboxLocator = By.XPath("//input[@id='co_doNotDeliverallcheckbox']");

        private static readonly By DialogMessageLocator = By.XPath("//div[@id='co_nonunique_citations_div']/p");

        private static readonly By InfoBoxContainerLocator = By.XPath("//div[normalize-space(@class)='co_infoBox warning bottom']");

        private static readonly By InfoBoxMessageLocator = By.XPath(".//div[@class='co_infoBox_message']");

        /// <summary>
        /// Initializes a new instance of the <see cref="NonUniqueCitationDialog"/> class. 
        /// The Dialog Constructor
        /// </summary>
        public NonUniqueCitationDialog()
            : base("Non Unique Citation")
        {
        }

        /// <summary>
        /// InfoBox
        /// </summary>
        public IInfoBox InfoBox => new InfoBox(DriverExtensions.GetElement(InfoBoxContainerLocator, InfoBoxMessageLocator));

        /// <summary>
        /// Clicks a radio button whose caption contains the citationText passed in.
        /// </summary>
        /// <param name="citationText">portion of citation name.</param>
        public void SelectCitation(string citationText) =>
            DriverExtensions.Click(
                DriverExtensions.GetElements(NonUniqueCitationsLocator).First(el => el.Text.Contains(citationText)),
                CitationCheckboxLocator);

        /// <summary>
        /// Select 'Do not deliver the non-unique citation' checkbox
        /// </summary>
        /// <returns>New instance of NonUniqueCitationDialog</returns>
        public NonUniqueCitationDialog SelectDoNotDeliverNonUniqueCitationCheckbox()
        {
            DriverExtensions.WaitForElement(DoNotDeliverCitationCheckboxLocator).Click();
            return new NonUniqueCitationDialog();
        }

        /// <summary>
        /// Get Dialog message
        /// </summary>
        /// <returns> The <see cref="string"/>NonUnique light-box message </returns>
        public string GetDialogMessage() => DriverExtensions.GetText(DialogMessageLocator);
       
    }
}