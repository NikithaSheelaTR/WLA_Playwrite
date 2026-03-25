namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts.Tabs
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts.SubCharts.TransactionStatus;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs;
    using OpenQA.Selenium;

    /// <summary>
    /// Consideration Offered Tab Component
    /// </summary>
    public class ConsiderationOfferedTabComponent : BaseAnalyticsProfileTabPage
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class= 'la-Layout-chartContainer']");

        /// <summary>
        /// Consideration Offered Tab name.
        /// </summary>
        protected override string TabName => "Consideration Offered";

        /// <summary>
        /// Transaction status charts sub tab panel
        /// </summary>
        public TransactionStatusSubChartPanel TransactionStatusSubChartPanel { get; } = new TransactionStatusSubChartPanel();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}