namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs
{
    using OpenQA.Selenium;

    /// <summary>
    /// Damages Profile Tab Component
    /// </summary>
    public class DamagesProfileTabComponent : BaseAnalyticsProfileTabPage
    {
        private static readonly By ContainerLocator = By.Id("co_la_damages_tab");

        /// <summary>
        /// Content title.
        /// </summary>
        protected string ContentTitle => "Damages analytics";

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Damages";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}