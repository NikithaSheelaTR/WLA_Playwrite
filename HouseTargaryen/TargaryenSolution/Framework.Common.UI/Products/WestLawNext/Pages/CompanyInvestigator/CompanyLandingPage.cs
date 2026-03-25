namespace Framework.Common.UI.Products.WestLawNext.Pages.CompanyInvestigator
{
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Pages.Alerts;
    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Page that displays company's information
    /// </summary>
    public class CompanyLandingPage : CommonDocumentPage
    {
        private static readonly By AlertIconLocator = By.Id("co_docBusinessInvestigatorAnchor");

        private static readonly By CompanyNameLocator = By.Id("co_bi_header_title");

        /// <summary>
        /// Report dropdown
        /// </summary>
        public CompanyInvestigatorReportDropdown ReportDropdown { get; } = new CompanyInvestigatorReportDropdown();

        /// <summary>
        /// The click on alert icon.
        /// </summary>
        /// <returns> The <see cref="CreateAlertPage"/>. </returns>
        public CreateAlertPage ClickOnAlertIcon()
        {
            DriverExtensions.WaitForElement(AlertIconLocator).Click();
            return new CreateAlertPage();
        }

        /// <summary>
        /// This method verifies Company Name is displayed on the Company Investigator page
        /// </summary>
        /// <returns>True if the Company Name is present</returns>
        public bool IsCompanyNameDisplayed() => DriverExtensions.IsDisplayed(CompanyNameLocator);
    }
}