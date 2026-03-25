namespace Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator.TableComponents
{
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.WestLawNext.Enums.Report;

    /// <summary>
    /// Awards by largest table in report page
    /// </summary>
    public class AwardsLargestTableComponent : AwardsAndExpertsBaseReportTableComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AwardsLargestTableComponent"/> class.
        /// </summary>
        public AwardsLargestTableComponent()
            : base(ReportPageTables.AwardsByLargest)
        {
        }

        /// <summary>
        /// Gets the SortMenuAwardsByLargestDropdown
        /// </summary>
        public SortMenuAwardsByLargestDropdown Dropdown { get; } = new SortMenuAwardsByLargestDropdown();
    }
}