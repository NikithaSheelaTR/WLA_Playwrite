namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts.Tabs
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Tables;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Base Chart Component
    /// </summary>
    public class LitigationAnalyticsBaseContentChartComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@id = 'co_la_casehistory_outcomeChart']");

        /// <summary>
        /// Chart category tab panel
        /// </summary>
        public ChartViewContainer ChartContainer { get; } = new ChartViewContainer();

        /// <summary>
        /// Chart category tab panel
        /// </summary>
        public TableViewContainer TableContainer { get; } = new TableViewContainer();

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => DriverExtensions.GetElement(this.ComponentLocator).Text;

        /// <summary>
        /// Component locator
        /// </summary>sdafgsdfg
        protected override By ComponentLocator => ContainerLocator;
    }
}