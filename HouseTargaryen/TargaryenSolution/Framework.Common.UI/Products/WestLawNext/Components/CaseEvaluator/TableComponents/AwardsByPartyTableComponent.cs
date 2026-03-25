namespace Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator.TableComponents
{
    using Framework.Common.UI.Products.WestLawNext.Enums.Report;

    /// <summary>
    /// represents the awards by party table in Report page
    /// </summary>
    public class AwardsByPartyTableComponent : BaseReportTableComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AwardsByPartyTableComponent"/> class.
        /// </summary>
        public AwardsByPartyTableComponent()
            : base(ReportPageTables.AwardsByParty)
        {
            this.DateHeader = new DateHeaderComponent(this.TableId, "CEAwarsByParty");
        }

        /// <summary>
        /// Gets the date header.
        /// </summary>
        public DateHeaderComponent DateHeader { get; private set; }
    }
}