namespace Framework.Common.UI.Products.WestLawNext.Pages.Document
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Document;
    using Framework.Common.UI.Products.Shared.Dialogs.Dockets;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Products.WestLawNext.Components.Dockets;
    using Framework.Common.UI.Products.WestLawNext.Pages.Dockets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The document page for a Docket
    /// </summary>
    public class DocketDocumentPage : CommonDocumentPage
    {
        private static readonly By RequestMultiplePdfButtonLocator = By.XPath("//*[contains(text(),'Request Multiple PDFs')]"); 

        private static readonly By ViewCalendarInformationLinkLocator = By.Id("co_docketCalendarLink");

        private static readonly By ViewCreditorInformationLinkLocator = By.Id("co_docketCreditorLink");

        private static readonly By CurrentDateAndSourceLocator = By.XPath("//div[text()[contains(.,'Source:')]]");

        private static readonly By HeaderDocketNumberLocator = By.Id("codeSetName");

        private static readonly By DocketCurrentThroughLabelLocator = By.XPath("//div[contains(@class,'co_docketsScrapeDateBold')]");

        private static readonly By DocketNumberLocator = By.Id("docketNumber");

        private static readonly By DocumentDocketsSubSectionLocator =
            By.XPath("//div[contains(@id, 'co_document_0')]//div[contains(@class, 'co_docketsSubSection')]");

        private static readonly By UpdateButtonLocator = By.Id("co_docketsUpdate");

        private static readonly By ClearAllLinkLocator = By.ClassName("co_linkBlue");

        /// <summary> Disclaimer Block for Docket</summary>
        public DisclaimerComponent DisclaimerBlock { get; private set; } = new DisclaimerComponent();

        /// <summary>
        /// Filing Information Block for Docket
        /// </summary>
        public FilingInformationComponent FilingInformationBlock { get; private set; } = new FilingInformationComponent();

        /// <summary>
        /// Docket Proceedings Table
        /// </summary>
        public DocketProceedingsTableComponent ProceedingsTable { get; private set; } = new DocketProceedingsTableComponent();

        /// <summary>
        /// Is 'Request Multiple PDFs' button displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsRequestMultiplePdfButtonDisplayed() => DriverExtensions.IsDisplayed(RequestMultiplePdfButtonLocator);

        /// <summary>
        /// Is 'Update' button displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsUpdateButtonDisplayed() => DriverExtensions.IsDisplayed(UpdateButtonLocator);

        /// <summary>
        /// Is 'Request Multiple PDFs' button enabled
        /// </summary>
        /// <returns>True if enabled, false otherwise</returns>
        public bool IsRequestMultiplePdfButtonEnabled() 
            => !DriverExtensions.GetAttribute("class", RequestMultiplePdfButtonLocator).Contains("disabled");

        /// <summary>
        /// Clicks the 'Request Multiple PDFs' button on the docket page
        /// </summary>
        /// <returns> The <see cref="PrepareMultiplePdfRequestPage"/>. </returns>
        public PrepareMultiplePdfRequestPage ClickRequestMultiplePdfButton()
        {
            DriverExtensions.WaitForElement(RequestMultiplePdfButtonLocator).Click();
            return new PrepareMultiplePdfRequestPage();
        }

        /// <summary>
        /// Get 'Request Multiple PDFs' button text
        /// </summary>
        /// <returns>'Request Multiple PDFs' button text</returns>
        public string GetRequestMultiplePdfButtonText() => DriverExtensions.GetElement(RequestMultiplePdfButtonLocator).Text;

        /// <summary>
        /// Get Request Multiple Pdf Button Title
        /// </summary>
        /// <returns>Green Carat title</returns>
        public string GetRequestMultiplePdfButtonTitle() =>
            DriverExtensions.GetAttribute("title", DriverExtensions.GetElement(RequestMultiplePdfButtonLocator));

        /// <summary>
        /// Clicks the 'Update' button on the docket page
        /// </summary>
        /// <returns> the string </returns>
        public SingleDocketUpdateRequestsDialog ClickUpdateButton()
        {
            DriverExtensions.Click(UpdateButtonLocator);
            return new SingleDocketUpdateRequestsDialog();
        }

        /// <summary>
        /// Docket Current Through Label
        /// </summary>
        public ILabel DocketCurrentTroughLabel => new Label(DocketCurrentThroughLabelLocator);

        /// <summary>
        /// Gets the text of the Current Date and Source element
        /// </summary>
        /// <returns> the string </returns>
        public string GetCurrentDateAndSourceText() => DriverExtensions.GetText(CurrentDateAndSourceLocator);

        /// <summary>
        /// Gets the docket number from the header of the document
        /// </summary>
        /// <returns> the string </returns>
        public string GetDocketNumberFromHeader() => DriverExtensions.GetText(HeaderDocketNumberLocator);

        /// <summary>
        /// Returns the docket number of the document
        /// </summary>
        /// <returns>The docket number</returns>
        public string GetDocketNumber() => DriverExtensions.WaitForElement(DocketNumberLocator).Text;

        /// <summary>
        /// Checks if the View Calendar Information link is displayed or not
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsViewCalendarInformationLinkDisplayed() => DriverExtensions.IsDisplayed(ViewCalendarInformationLinkLocator);

        /// <summary>
        /// Checks if the View Creditor Information link is displayed or not
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsViewCreditorInformationLinkDisplayed() => DriverExtensions.IsDisplayed(ViewCreditorInformationLinkLocator);

        /// <summary>
        /// Tests if the first Participant Information section is collapsed or not
        /// </summary>
        /// <returns>Boolean if section is collapsed or not</returns>
        public bool IsParticipantInformationCollapsed()
            => DriverExtensions.WaitForElement(DocumentDocketsSubSectionLocator).GetAttribute("class").Contains("co_sectionCollapsed");

        /// <summary>
        /// Click Clear All link
        /// </summary>
        /// <returns>Docket Document Page</returns>
        public DocketDocumentPage ClickClearAllLink()
        {
            DriverExtensions.GetElement(ClearAllLinkLocator).Click();
            return this;
        }

        /// <summary>
        /// is Clear All link displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsClearAllLinkDisplayed() => DriverExtensions.IsDisplayed(ClearAllLinkLocator);

        /// <summary>
        /// Clicks the 'View Calendar Information' link on the docket page
        /// </summary>
        /// <returns> Docket Calendar Page</returns>
        public CommonDocketsPage ClickViewCalendarInformationLink()
        {
            DriverExtensions.WaitForElement(ViewCalendarInformationLinkLocator).Click();
            return new CommonDocketsPage();
        }

        /// <summary>
        /// Clicks the 'View Creditor Information' link on the docket page
        /// </summary>
        /// <returns>Docket Calendar Page</returns>
        public CommonDocketsPage ClickCreditorInformationLink()
        {
            DriverExtensions.WaitForElement(ViewCreditorInformationLinkLocator).Click();
            return new CommonDocketsPage();
        }
    }
}