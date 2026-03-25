namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts.Tabs
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs;
    using OpenQA.Selenium;

    /// <summary>
    /// Distribution Tab Component
    /// </summary>
    public class DistributionTabComponent : BaseAnalyticsProfileTabPage
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class= 'la-Layout-chartContainer']");

        /// <summary>
        /// The Case Type tab name.
        /// </summary>
        protected override string TabName => "DistributionSubTab";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}