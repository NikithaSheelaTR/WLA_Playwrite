namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsHomePageComponents.Entities
{
    using OpenQA.Selenium;

    /// <summary>
    /// Courts Tab Search Component
    /// </summary>
    public class LitigationAnalyticsCourtsEntityTabComponent : LitigationAnalyticsBaseEntityTabComponent
    {
        private static readonly By ContainerLocator = By.Id("tab-Courts");

        /// <summary>
        /// Tab Name
        /// </summary>
        protected override string TabName => "Courts";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}