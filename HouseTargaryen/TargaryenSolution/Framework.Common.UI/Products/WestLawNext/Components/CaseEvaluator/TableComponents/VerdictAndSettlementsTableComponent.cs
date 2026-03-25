namespace Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator.TableComponents
{
    using Framework.Common.UI.Products.WestLawNext.Enums.Report;

    /// <summary>
    /// represents the verdict and settlements table in report list
    /// </summary>
    public class VerdictAndSettlementsTableComponent : SummariesAndCourtBaseReportTableComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VerdictAndSettlementsTableComponent"/> class. 
        /// constructor
        /// </summary>
        public VerdictAndSettlementsTableComponent()
            : base(ReportPageTables.VerdictAndSettlement)
        {
        }
    }
}