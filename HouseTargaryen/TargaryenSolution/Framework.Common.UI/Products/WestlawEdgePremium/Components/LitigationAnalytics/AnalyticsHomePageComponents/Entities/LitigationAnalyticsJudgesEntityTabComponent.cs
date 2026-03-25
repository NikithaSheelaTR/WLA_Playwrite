namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsHomePageComponents.Entities
{
    using OpenQA.Selenium;

    /// <summary>
    /// Judge Tab Component
    /// </summary>
    public class LitigationAnalyticsJudgesEntityTabComponent : LitigationAnalyticsBaseEntityTabComponent
    {
        private static readonly By ContainerLocator = By.Id("tab-Judges");

        /// <summary>
        /// Tab Name
        /// </summary>
        protected override string TabName => "Judges";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
