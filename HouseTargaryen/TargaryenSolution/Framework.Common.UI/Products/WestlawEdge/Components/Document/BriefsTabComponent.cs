namespace Framework.Common.UI.Products.WestlawEdge.Components.Document
{
    using Framework.Common.UI.Products.WestLawNext.Components;

    using OpenQA.Selenium;

    /// <summary>
    /// Briefs tab component
    /// </summary>
    public class BriefsTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("tab_briefsTabId");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Briefs";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
