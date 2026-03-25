namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Trend Chart By Practice Area
    /// </summary>
    public class TrendChartByPracticeArea : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.CssSelector("div.la-Chart-compare-side:nth-child(2)");
        private static readonly By BackButtonLocator = By.XPath("//div[@id ='la-Chart-compare-control']/button[contains(@class, 'saf-button_secondary')]");
        private static readonly By ForwardButtonLocator = By.XPath("//div[@id ='la-Chart-compare-control']/button[contains(@class, 'saf-button_primary')]");
        private static readonly By LineChartHeaderLocator = By.XPath("//div[contains(@id, 'la-LineChart_header_charts')]");

        /// <summary>
        /// Trend Chart By Practice Area
        /// </summary>
        public TrendChartByPracticeArea()
        {
        }

        /// <summary>
        /// BackButton
        /// </summary>
        public IButton BackButton => new Button(DriverExtensions.GetElement(BackButtonLocator));

        /// <summary>
        /// ForwardButton
        /// </summary>
        public IButton ForwardButton => new Button(DriverExtensions.GetElement(ForwardButtonLocator));

        /// <summary>
        /// LineChartHeader
        /// </summary>
        public ILabel LineChartHeader => new Label(DriverExtensions.GetElement(LineChartHeaderLocator));

        /// <summary>
        /// ComponentLocator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}