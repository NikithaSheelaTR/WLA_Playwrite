namespace Framework.Common.UI.Products.WestlawEdge.Components.LegalAnalytics
{
    using Framework.Common.UI.Products.WestLawNext.Components;
    using OpenQA.Selenium;

    /// <summary>
    /// The TimeToRuleTab
    /// </summary>
    public class TimeToRuleTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("co_la_byTimeToRule");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Time to rule";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// THe StateChartComponent
        /// </summary>
        public StateChartComponent StateChartComponent => new StateChartComponent();
    }
}
