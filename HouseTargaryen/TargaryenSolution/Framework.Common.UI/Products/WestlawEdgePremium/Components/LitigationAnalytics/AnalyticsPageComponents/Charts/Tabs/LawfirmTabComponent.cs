namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts.Tabs
{
    using OpenQA.Selenium;

    /// <summary>
    /// Law firm Tab Component
    /// </summary>
    public class LawfirmTabComponent : LitigationAnalyticsBaseContentChartComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class= 'la-Layout-chartContainer']");

        /// <summary>
        /// The Law firm  name.
        /// </summary>
        protected override string TabName => "Lawfirm";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}