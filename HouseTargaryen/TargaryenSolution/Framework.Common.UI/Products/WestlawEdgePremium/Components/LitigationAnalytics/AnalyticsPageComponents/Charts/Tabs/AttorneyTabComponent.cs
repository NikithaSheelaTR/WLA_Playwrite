namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts.Tabs
{
    using OpenQA.Selenium;

    /// <summary>
    /// Attorney Tab Component
    /// </summary>
    public class AttorneyTabComponent : LitigationAnalyticsBaseContentChartComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class= 'la-Layout-chartContainer']");

        /// <summary>
        /// The Year sub tab name.
        /// </summary>
        protected override string TabName => "Attorney";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}