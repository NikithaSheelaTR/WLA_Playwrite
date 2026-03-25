namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Tables
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using OpenQA.Selenium;

    /// <summary>
    /// Table chart container
    /// </summary>
    public class TableViewContainer : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class= 'la-Layout-chartContainer']");

        /// <summary>
        /// Chart Container Component
        /// </summary>
        public TableContentComponent TableContent { get; } = new TableContentComponent();

        /// <summary>
        /// Chart Container Component
        /// </summary>
        public HeaderCategoryTabPanel HeaderComponent { get; } = new HeaderCategoryTabPanel();

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Current history";
        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

    }
}