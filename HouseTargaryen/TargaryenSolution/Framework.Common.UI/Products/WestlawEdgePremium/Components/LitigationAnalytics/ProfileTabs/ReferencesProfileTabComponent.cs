namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs
{
    using Framework.Common.UI.Products.Shared.Components.IpTools;
    using OpenQA.Selenium;

    /// <summary>
    /// ReferencesTabComponent
    /// </summary>
    public class ReferencesProfileTabComponent : BaseAnalyticsProfileTabPage
    {
        private static readonly By ContainerLocator = By.Id("co_la_references_tab");

        /// <summary>
        /// ResultList
        /// </summary>
        public ReferenceCitedGridComponent ResultList => new ReferenceCitedGridComponent();

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "References";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}