namespace Framework.Common.UI.Products.WestlawEdge.Pages.CaseEvaluator
{
    using Framework.Common.UI.Products.WestLawNext.Pages.CaseEvaluator;
    using Framework.Common.UI.Products.WestlawEdge.Components;

    /// <summary>
    /// EdgeCaseEvaluatorReportPage
    /// </summary>
    public class EdgeCaseEvaluatorReportPage : CaseEvaluatorReportPage
    {
        /// <summary>
        /// Edge Header component
        /// </summary>
        public new EdgeHeaderComponent Header { get; protected set; } = new EdgeHeaderComponent();
    }
}