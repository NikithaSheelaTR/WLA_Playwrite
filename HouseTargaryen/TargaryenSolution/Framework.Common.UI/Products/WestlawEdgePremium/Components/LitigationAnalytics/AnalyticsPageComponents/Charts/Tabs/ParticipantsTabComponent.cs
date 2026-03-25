namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts.SubCharts.Participants;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts.Tabs;
    using OpenQA.Selenium;

    /// <summary>
    /// Participants charts tab component
    /// </summary>
    public class ParticipantsTabComponent : LitigationAnalyticsBaseContentChartComponent
    {
        /// <summary>
        /// Participants charts sub tab panel
        /// </summary>
        public ParticipantsSubChartTabPanel ParticipantsSubChartTabPanel { get; } = new ParticipantsSubChartTabPanel();

        /// <summary>
        /// Container Locator
        /// </summary>

        private static readonly By ContainerLocator = By.XPath("//div[@class= 'la-Layout-chartContainer']");

        /// <summary>
        /// The Participants tab name.
        /// </summary>
        protected override string TabName => "Participants";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}