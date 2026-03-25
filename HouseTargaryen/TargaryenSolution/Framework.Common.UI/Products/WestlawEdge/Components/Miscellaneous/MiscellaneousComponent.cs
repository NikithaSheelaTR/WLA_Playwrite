namespace Framework.Common.UI.Products.WestlawEdge.Components.Miscellaneous
{
    using Framework.Common.UI.Products.Shared.Components;
    using OpenQA.Selenium;

    /// <summary>
    /// Miscellaneous component on the IndigoWestlawHomePage
    /// </summary>
    public class MiscellaneousComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("coid_myContentWidgetContent");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Miscellaneous Tab Panel
        /// </summary>
        public MiscellaneousTabPanel MiscellaneousTabPanel { get; } = new MiscellaneousTabPanel();
    }
}
