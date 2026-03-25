namespace Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator.TableComponents
{
    using Framework.Common.UI.Products.WestLawNext.Enums.Report;

    /// <summary>
    /// the appellate court documents table in report page
    /// </summary>
    public class AppellateCourtDocumentsTableComponent : SummariesAndCourtBaseReportTableComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppellateCourtDocumentsTableComponent"/> class.
        /// </summary>
        public AppellateCourtDocumentsTableComponent()
            : base(ReportPageTables.AppellateCourtDocuments)
        {
        }
    }
}