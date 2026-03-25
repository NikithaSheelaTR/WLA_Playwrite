namespace Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator.TableComponents
{
    using Framework.Common.UI.Products.WestLawNext.Enums.Report;

    /// <summary>
    /// represents table for Expert Distribution
    /// </summary>
    public class ExpertDistributionTableComponent : AwardsAndExpertsBaseReportTableComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpertDistributionTableComponent"/> class.
        /// </summary>
        /// <param name="expandTableCompletely">The expand table completely.</param>
        public ExpertDistributionTableComponent(bool expandTableCompletely = false)
            : base(ReportPageTables.ExpertDistribution)
        {
            if (expandTableCompletely)
            {
                this.ExpandTableCompletely();
            }

            this.DateHeader = new DateHeaderComponent(this.TableId, "CEExpertDistribution");
        }

        /// <summary>
        /// Gets the date header.
        /// </summary>
        public DateHeaderComponent DateHeader { get; private set; }

        /// <summary>
        /// Verify View More Functionality 
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsViewMoreFunctionCorrect() => this.IsViewMoreFunctionCorrect((int)ExpertDistributionHeaders.ExpertiseType);
    }
}