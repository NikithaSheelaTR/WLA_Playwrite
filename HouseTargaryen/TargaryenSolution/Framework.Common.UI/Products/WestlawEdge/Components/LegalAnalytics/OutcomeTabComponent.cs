namespace Framework.Common.UI.Products.WestlawEdge.Components.LegalAnalytics
{
    using Framework.Common.UI.Products.WestLawNext.Components;
    using OpenQA.Selenium;

    /// <summary>
    /// OutcomeTab 
    /// </summary>
    public class OutcomeTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("co_la_byOutcome");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Outcome";

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
