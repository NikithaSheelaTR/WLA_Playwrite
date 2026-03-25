namespace Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator.TableComponents
{
    using Framework.Common.UI.Products.WestLawNext.Enums.Report;

    /// <summary>
    /// represents the Trial Court Memoranda table in Report Page
    /// </summary>
    public class TrialCourtMemorandaTableComponent : SummariesAndCourtBaseReportTableComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrialCourtMemorandaTableComponent"/> class. 
        /// constructor
        /// </summary>
        public TrialCourtMemorandaTableComponent()
            : base(ReportPageTables.TrialCourtMemoranda)
        {
        }
    }
}