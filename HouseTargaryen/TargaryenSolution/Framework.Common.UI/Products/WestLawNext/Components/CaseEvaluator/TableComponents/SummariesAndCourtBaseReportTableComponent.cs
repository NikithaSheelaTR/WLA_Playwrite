namespace Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator.TableComponents
{
    using Framework.Common.UI.Products.WestLawNext.Enums.Report;
    using Framework.Common.UI.Products.WestLawNext.Pages.CaseEvaluator;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The summaries and court base report table component.
    /// </summary>
    public abstract class SummariesAndCourtBaseReportTableComponent : BaseReportTableComponent
    {
        /// <summary>
        /// Full document link locator
        /// </summary>
        private static readonly By FullDocumentLinkLocator = By.XPath(".//a[text()='Full Document List']");

        /// <summary>
        /// Initializes a new instance of the <see cref="SummariesAndCourtBaseReportTableComponent"/> class.
        /// </summary>
        /// <param name="table">The table.</param>
        protected SummariesAndCourtBaseReportTableComponent(ReportPageTables table)
            : base(table)
        {
        }

        /// <summary>
        /// click the full doc list link
        /// </summary>
        /// <returns>doc list page</returns>
        public ReportDocumentListPage ClickFullDocumentListLink()
        {
            DriverExtensions.GetElement(this.ComponentLocator).ScrollToElement();
            DriverExtensions.Click(DriverExtensions.WaitForElement(DriverExtensions.GetElement(this.ComponentLocator), FullDocumentLinkLocator));
            return new ReportDocumentListPage();
        }
    }
}