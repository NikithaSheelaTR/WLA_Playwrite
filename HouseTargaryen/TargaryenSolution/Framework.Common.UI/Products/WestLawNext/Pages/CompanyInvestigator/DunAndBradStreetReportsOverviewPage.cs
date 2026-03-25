namespace Framework.Common.UI.Products.WestLawNext.Pages.CompanyInvestigator
{
    using Framework.Common.UI.Products.Shared.Enums.Reports;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// DunAndBradStreet Reports Overview Page
    /// </summary>
    public class DunAndBradStreetReportsOverviewPage : BaseModuleRegressionPage
    {
        private static readonly By PageIdLocator = By.XPath("//h1[text()='Company Information']");

        private EnumPropertyMapper<DAndBReportType, WebElementInfo> reportTypeMap;

        /// <summary>
        /// DunAndBradStreet Reports Overview Page
        /// </summary>
        public DunAndBradStreetReportsOverviewPage()
        {
            DriverExtensions.WaitForElement(PageIdLocator);
        }

        /// <summary>
        /// Gets the Documents Icon Map
        /// </summary>
        private EnumPropertyMapper<DAndBReportType, WebElementInfo> ReportTypeMap
            =>
                this.reportTypeMap =
                    this.reportTypeMap ?? EnumPropertyModelCache.GetMap<DAndBReportType, WebElementInfo>();

        /// <summary>
        /// The click report link.
        /// </summary>
        /// <param name="reportType">
        /// The report type.
        /// </param>
        /// <returns>
        /// The <see cref="DunAndBradStreetReportDocumentPage"/>.
        /// </returns>
        public DunAndBradStreetReportDocumentPage ClickReportLink(DAndBReportType reportType)
        {
            DriverExtensions.WaitForElementDisplayed(By.Id(this.ReportTypeMap[reportType].Id)).Click();
            return new DunAndBradStreetReportDocumentPage();
        }
    }
}