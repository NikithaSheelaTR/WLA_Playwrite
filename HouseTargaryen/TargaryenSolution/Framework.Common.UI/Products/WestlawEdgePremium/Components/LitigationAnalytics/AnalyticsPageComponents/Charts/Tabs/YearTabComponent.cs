using OpenQA.Selenium;

namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts.Tabs
{
    /// <summary>
    /// Year Tab Component
    /// </summary>
    public class YearTabComponent : LitigationAnalyticsBaseContentChartComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class= 'la-Layout-chartContainer']");

        /// <summary>
        /// The Year sub tab name.
        /// </summary>
        protected override string TabName => "Year";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}