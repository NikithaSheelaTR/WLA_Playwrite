namespace Framework.Common.UI.Products.WestLawNext.Pages.BusinessLawCenterPowerSearch
{
    using Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenterPowerSearch.ContentComponents;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The document view page.
    /// </summary>
    public class DocumentDetailsPage : GlobalHeaderFooterPage
    {
        private static readonly By AllCompanyFilingsLocator = By.XPath("//div[@id='leftPane']//a[.='All Company Filings']");

        private static readonly By FilingDetailsLocator =
            By.XPath("//div[@class='documentFilingOptions']//a[.='Filing Details']");

        /// <summary>
        /// Gets the document view section.
        /// </summary>
        public DocumentViewWithSearchTermsComponent DocumentViewWithSearchTermsSection { get; private set; }
            = new DocumentViewWithSearchTermsComponent();

        /// <summary>
        /// Click on all company filings from doc view. 
        /// </summary>
        public void ClickOnAllCompanyFilingsLink() => DriverExtensions.WaitForElement(AllCompanyFilingsLocator).Click();

        /// <summary>
        /// The click on filing details link.
        /// </summary>
        /// <returns>The <see cref="FilingsDetailsPage"/>.</returns>
        public FilingsDetailsPage ClickOnFilingDetailsLink()
        {
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.Click(DriverExtensions.WaitForElement(FilingDetailsLocator));
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
            return new FilingsDetailsPage();
        }

        /// <summary>
        /// To verify whether All Company Filings link is displayed. 
        /// </summary>
        /// <returns>True if All Company Filings Link is displayed, otherwise - false</returns>
        public bool IsAllCompanyFilingsLinkDisplayed()
        {
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
            IWebElement allCompanyLink = DriverExtensions.WaitForElement(AllCompanyFilingsLocator);
            return allCompanyLink.Displayed;
        }

        /// <summary>
        /// To verify whether Filing Details link is displayed. 
        /// </summary>
        /// <returns>True if Filling Details Link is displayed, otherwise - false</returns>
        public bool IsFilingDetailsLinkDisplayed() => DriverExtensions.WaitForElement(FilingDetailsLocator).Displayed;
    }
}