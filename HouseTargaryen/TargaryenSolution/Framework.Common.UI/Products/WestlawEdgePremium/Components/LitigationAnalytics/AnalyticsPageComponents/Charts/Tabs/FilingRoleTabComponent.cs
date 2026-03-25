namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts.Tabs
{
    using OpenQA.Selenium;

    /// <summary>
    /// Filing Role Tab Component
    /// </summary>
    public class FilingRoleTabComponent : LitigationAnalyticsBaseContentChartComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class= 'la-Layout-chartContainer']");

        /// <summary>
        /// The Case Type tab name.
        /// </summary>
        protected override string TabName => "FilingRoleSubTab";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}