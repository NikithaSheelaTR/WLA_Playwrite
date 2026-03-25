namespace Framework.Common.UI.Products.WestlawEdge.Components.LegalAnalytics
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;

    using OpenQA.Selenium;

    /// <summary>
    /// The AppealsHierarchicalDonutChartComponent
    /// </summary>
    public class AppealsHierarchicalDonutChartComponent : BaseModuleRegressionComponent, ICreatablePageObject
    {
        private static readonly By ContainerLocator = By.XPath("//la-toggle-widget");

        /// <summary>
        /// get Donut chart
        /// </summary>
        public DonutChartComponent DonutChartComponent { get; } = new DonutChartComponent();

        /// <summary>
        /// Get Donut Legend
        /// </summary>
        public DonutLegendComponent DonutLegendComponent { get; } = new DonutLegendComponent();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}