namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsHomePageComponents.Entities
{
    using OpenQA.Selenium;

    /// <summary>
    /// Case Type Tab Search Component
    /// </summary>
    public class LitigationAnalyticsCaseTypeEntityTabComponent : LitigationAnalyticsBaseEntityTabComponent
    {
        private static readonly By ContainerLocator = By.Id("tab-CaseTypes");

        /// <summary>
        /// Tab Name
        /// </summary>
        protected override string TabName => "Case Types";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}