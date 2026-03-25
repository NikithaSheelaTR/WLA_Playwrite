namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.ResultList;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.NarrowPane;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using OpenQA.Selenium;

    /// <summary>
    /// Base Litigation Analytics Page. 
    /// </summary>
    public class BaseAnalyticsProfileTabPage : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("co_body");

        private static readonly By LoadingSpinnerLocator = By.XPath("//div[@class ='la-Loading co_clearfix la-Loading-extra-space']");

        /// <summary>
        /// Narrow panel.
        /// </summary>
        public LitigationAnalyticsNarrowPanel NarrowPane => new LitigationAnalyticsNarrowPanel();

        /// <summary>
        /// Content view tab panel.
        /// </summary>
        public LitigationAnalyticsContentViewTabPanel ContentViewTabPanel => new LitigationAnalyticsContentViewTabPanel();

        /// <summary>
        /// Dockets result list component.
        /// </summary>
        public LitigationAnalyticsPageResultListComponent ResultListComponent => new LitigationAnalyticsPageResultListComponent();

        /// <summary>
        /// Loading Spinner
        /// </summary>
        public ILabel LoadingSpinner => new Label(LoadingSpinnerLocator);

        /// <summary>
        /// TabName
        /// </summary>
        protected override string TabName => throw new System.NotImplementedException();

        /// <summary>
        /// ComponentLocator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}