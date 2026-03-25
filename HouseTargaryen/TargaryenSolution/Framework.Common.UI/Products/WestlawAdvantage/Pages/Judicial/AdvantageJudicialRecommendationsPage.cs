namespace Framework.Common.UI.Products.WestlawAdvantage.Pages.Judicial
{
    using Framework.Common.UI.Products.WestlawAdvantage.Components.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Pages.Judicial;

    /// <summary>
    /// The Advantage judicial recommendations page.
    /// </summary>
    public class AdvantageJudicialRecommendationsPage : JudicialRecommendationsPage
    {
        /// <summary>
        /// Gets The Advantage report tabs component
        /// </summary>
        public new WestlawAdvantageReportTabPanel ReportTabsPanel { get; } = new WestlawAdvantageReportTabPanel();
    }
}