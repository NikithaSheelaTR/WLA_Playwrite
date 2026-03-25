namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs
{
    using OpenQA.Selenium;

    /// <summary>
    /// THe HistoryTab class
    /// </summary>
    public class ExperienceProfileTabComponent : BaseAnalyticsProfileTabPage
    {
        private static readonly By ContainerLocator = By.Id("co_la_caseHistoryReport_tab");

        /// <summary>
        /// Content title.
        /// </summary>
        protected string ContentTitle => "Docket analytics";

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Experience";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}