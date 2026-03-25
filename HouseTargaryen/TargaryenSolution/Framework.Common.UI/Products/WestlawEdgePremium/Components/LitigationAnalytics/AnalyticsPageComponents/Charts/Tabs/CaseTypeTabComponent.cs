namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts.Tabs
{
    using OpenQA.Selenium;

    /// <summary>
    /// Case Type Tab Component
    /// </summary>
    public class CaseTypeTabComponent : LitigationAnalyticsBaseContentChartComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class= 'la-Layout-chartContainer']");

        /// <summary>
        /// The Case Type tab name.
        /// </summary>
        protected override string TabName => "CaseTypeSubTab";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}