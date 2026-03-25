namespace Framework.Common.UI.Products.WestlawEdge.Components.LegalAnalytics
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;

    using OpenQA.Selenium;

    /// <summary>
    /// The StateChartComponent
    /// </summary>
    public class StateChartComponent : BaseModuleRegressionComponent, ICreatablePageObject
    {
        private static readonly By ContainerLocator = By.XPath("//*[@class= 'la-Chart-state']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}