namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.QuickCheck
{
    using Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.QuickCheck;

    /// <summary>
    /// Canada Recommendations page for Quick Check feature in Westlaw Edge Canada.
    /// </summary>
    public class CanadaRecommendationsPage : QuickCheckRecommendationsPage
    {
        /// <summary>
        /// Gets The report tabs component
        /// </summary>
        public new CanadaReportTabPanel ReportTabsPanel { get; } = new CanadaReportTabPanel();
    }
}