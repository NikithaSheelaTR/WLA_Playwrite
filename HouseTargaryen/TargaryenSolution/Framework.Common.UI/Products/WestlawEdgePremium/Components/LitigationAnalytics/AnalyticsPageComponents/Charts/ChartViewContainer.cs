namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Tables;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using OpenQA.Selenium;

    /// <summary>
    /// Chart Container Component
    /// </summary>
    public class ChartViewContainer : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[contains(@class,  'la-Layout-subChartContainer')]");
        private static readonly By ShowMoreButtonChartViewLocator = By.XPath(".//button[contains(@class ,'la-Chart-showMore co_secondaryBtn')]");
        private static readonly By ChartsHeaderIndividualyHeaderLocator = By.XPath(".//div[@class = 'la-Chart-header']/h3");

        /// <summary>
        /// ChartViewContainer
        /// </summary>
        public ChartViewContainer()
        {
        }

        /// <summary>
        /// ChartViewContainer
        /// </summary>
        /// <param name="container"></param>
        public ChartViewContainer(IWebElement container)
        {
            this.Container = container;
        }

        /// <summary>
        /// Chart Container Component
        /// </summary>
        public ILabel ChartsHeaderIndividualyHeader => new Label(this.Container, ChartsHeaderIndividualyHeaderLocator);

        /// <summary>
        /// Chart Container Component
        /// </summary>
        public ChartsContentComponent ChartsContent { get; } = new ChartsContentComponent();

        /// <summary>
        /// Table View Container
        /// </summary>
        public TableContentComponent TableContentComponent { get; } = new TableContentComponent();

        /// <summary>
        /// Trend Chart Container Component
        /// </summary>
        public TrendChartByPracticeArea TrendChartByPracticeArea { get; } = new TrendChartByPracticeArea();

        /// <summary>
        /// Table ShowMore Button Component
        /// </summary>
        public IButton ShowMoreButton => new Button(ShowMoreButtonChartViewLocator);

        /// <summary>
        /// Chart Container Component
        /// </summary>
        public LegendComponent Legend { get; } = new LegendComponent();

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Current history";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Container Element
        /// </summary>
        protected IWebElement Container;
    }
}