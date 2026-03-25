namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs
{
    using OpenQA.Selenium;

    /// <summary>
    /// The ProfileTab 
    /// </summary>
    public class OverviewProfileTabComponent : BaseAnalyticsProfileTabPage
    {
        private static readonly By ContainerLocator = By.Id("co_la_profileReport_tab");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Profile";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}