namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts.Tabs
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts.SubCharts.TransactionStatus;

    /// <summary>
    /// Transaction Status Tab Component
    /// </summary>
    public class TransactionStatusTabComponent : LitigationAnalyticsBaseContentChartComponent
    {
        /// <summary>
        /// Transaction status charts sub tab panel
        /// </summary>
        public TransactionStatusSubChartPanel TransactionStatusSubChartPanel { get; } = new TransactionStatusSubChartPanel();
    }
}