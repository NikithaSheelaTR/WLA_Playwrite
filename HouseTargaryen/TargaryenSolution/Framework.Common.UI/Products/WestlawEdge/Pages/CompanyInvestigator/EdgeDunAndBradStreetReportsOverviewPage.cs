namespace Framework.Common.UI.Products.WestlawEdge.Pages.CompanyInvestigator
{
    using Framework.Common.UI.Products.Shared.Enums.Reports;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// DunAndBradStreet Reports Overview Page
    /// </summary>
    public class EdgeDunAndBradStreetReportsOverviewPage : BaseModuleRegressionPage
    {
        private EnumPropertyMapper<DAndBReportType, WebElementInfo> reportTypeMap;

        /// <summary>
        /// Gets the Documents Icon Map
        /// </summary>
        private EnumPropertyMapper<DAndBReportType, WebElementInfo> ReportTypeMap =>
            this.reportTypeMap = this.reportTypeMap ?? EnumPropertyModelCache.GetMap<DAndBReportType, WebElementInfo>();

        /// <summary>
        /// The click report link.
        /// </summary>
        /// <param name="reportType">
        /// The report type.
        /// </param>
        /// <returns>
        /// The <see cref="EdgeCommonReportDocumentPage"/>.
        /// </returns>
        public EdgeCommonReportDocumentPage ClickReportLink(DAndBReportType reportType)
        {
            DriverExtensions.WaitForElementDisplayed(By.Id(this.ReportTypeMap[reportType].Id)).CustomClick();
            return new EdgeCommonReportDocumentPage();
        }
    }
}