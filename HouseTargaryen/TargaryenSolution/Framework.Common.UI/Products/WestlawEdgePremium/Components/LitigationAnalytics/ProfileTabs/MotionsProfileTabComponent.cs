namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.NarrowPane;
    using OpenQA.Selenium;

    /// <summary>
    /// MotionsTab 
    /// </summary>
    public class MotionsProfileTabComponent : BaseAnalyticsProfileTabPage
    {
        private static readonly By ContainerLocator = By.Id("co_la_motionReport_tab");
        private static readonly By SaveToFolderLocator = By.Id("co_saveToWidget");

        /// <summary>
        /// NarrowPane
        /// </summary>
        public new LitigationAnalyticsNarrowPanel NarrowPane => new LitigationAnalyticsNarrowPanel();

        /// <summary>
        /// SaveToFolderButton
        /// </summary>
        public IButton SaveToFolderButton = new Button(SaveToFolderLocator);

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Motions";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}