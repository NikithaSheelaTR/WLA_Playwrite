namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs
{
    using OpenQA.Selenium;

    /// <summary>
    /// Expert Challenges Tab Component
    /// </summary>
    public class ExpertChallengesTabComponent : BaseAnalyticsProfileTabPage
    {
        private static readonly By ContainerLocator = By.Id("co_la_expertsReport_tab");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Expert challenges";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

    }
}