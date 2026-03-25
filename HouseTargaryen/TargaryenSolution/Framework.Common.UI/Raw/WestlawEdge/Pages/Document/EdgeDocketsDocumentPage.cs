namespace Framework.Common.UI.Raw.WestlawEdge.Pages.Document
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;
    
    /// <summary>
    /// Indigo Docket Document Page
    /// </summary>
    public class EdgeDocketsDocumentPage : EdgeCommonDocumentPage
    {
        private static readonly By DocketNumberLocator = By.Id("docketNumber");
        private static readonly By DateFiledLocator = By.XPath("//th[text() = 'Date Filed:']/..");
        private static readonly By CaseNumberLocator = By.XPath("//th[text() = 'Case Number:']/../td[@class = 'co_docketsRowText']");
        private static readonly By DocketProceedingsLinkLocator = By.XPath("//table[contains(@class, 'co_docketsTable')]//a[contains(@class, 'co_blobLink')]");
        private static readonly By DocumentDocketsSubSectionLocator =
            By.XPath("//div[contains(@id, 'co_document_0')]//div[contains(@class, 'co_docketsSubSection')]");
        private static readonly By DocketUpdateButtonLocator = By.Id("co_docketsUpdate");
        
        /// <summary>
        /// Returns the docket number of the document
        /// </summary>
        /// <returns>The docket number</returns>
        public string GetDocketNumber() => DriverExtensions.WaitForElement(DocketNumberLocator).Text;

        /// <summary>
        /// Returns the Case number text
        /// </summary>
        /// <returns>v</returns>
        public string GetCaseNumberText() => DriverExtensions.WaitForElement(CaseNumberLocator).Text;

        /// <summary>
        /// Returns the Date filed info
        /// </summary>
        /// <returns>The Date filed info</returns>
        public string GetDateFiled() => DriverExtensions.WaitForElement(DateFiledLocator).Text;

        /// <summary> 
        /// Docket Summary Component  
        /// </summary>
        public DocketSummaryComponent DocketSummary { get; protected set; } = new DocketSummaryComponent();

        /// <summary>
        /// Tests if the first Participant Information section is collapsed or not
        /// </summary>
        /// <returns>Boolean if section is collapsed or not</returns>
        public bool IsParticipantInformationCollapsed()
            => DriverExtensions.WaitForElement(DocumentDocketsSubSectionLocator).GetAttribute("class").Contains("co_sectionCollapsed");
        
        /// <summary>
        /// Verify court number of first docket proceedings link
        /// </summary>
        /// <param name="courtNumber">Court number</param>
        /// <returns>True - if contains correct court number, false - otherwise</returns>
        public bool IsFirstDocketProceedingsLinkContainsCourtNumber(int courtNumber)
            => DriverExtensions.WaitForElement(DocketProceedingsLinkLocator).GetAttribute("data-pdf-link").Contains($"courtnumber={courtNumber}");

        /// <summary> 
        /// Docket update button
        /// </summary>
        public IButton DocketUpdateButton = new Button(DocketUpdateButtonLocator);
    }
}
