namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Donuts;
    using OpenQA.Selenium;

    /// <summary>
    /// OutcomeTab 
    /// </summary>
    public class OutcomesProfileTabComponent : BaseAnalyticsProfileTabPage
    {
        private static readonly By ContainerLocator = By.Id("co_la_byOutcome");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Outcomes";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets AppealsHierarchicalDonutChart
        /// </summary>
        public AppealsHierarchicalDonutChartComponent AppealsHierarchicalDonutChartComponent => new
            AppealsHierarchicalDonutChartComponent();
    }
}