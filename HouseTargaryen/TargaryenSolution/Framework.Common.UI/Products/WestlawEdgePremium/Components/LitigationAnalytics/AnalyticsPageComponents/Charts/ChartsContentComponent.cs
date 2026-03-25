namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items.LitigationAnalytics;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Charts content component
    /// </summary>
    public class ChartsContentComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class= 'la-Chart-svgHolder']");
        private static readonly By ContainerLocatorChartCoverLocator = By.XPath(".//*[@class= 'la-Chart-state']");

        /// <summary>
        /// Gets the chart content list.
        /// </summary>
        /// <value>
        /// The recent research list.
        /// </value>
        public List<ChartContentItem> ChartContentItemsList =>
            DriverExtensions.GetElements(DriverExtensions.GetElement(this.ComponentLocator), ContainerLocatorChartCoverLocator)
            .Select(element => new ChartContentItem(element))
            .ToList();

        /// <summary>
        /// Show more button
        /// </summary>
        public IButton ShowMoreButton => new Button(By.XPath(".//button[contains(@id = 'co_la_chart_viewAllButtont_charts')]"));

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}