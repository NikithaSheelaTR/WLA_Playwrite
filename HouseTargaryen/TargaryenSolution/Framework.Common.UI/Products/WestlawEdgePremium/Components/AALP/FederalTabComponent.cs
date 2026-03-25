namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using OpenQA.Selenium;

    /// <summary>
    /// Federal Tab component
    /// </summary>
    public class FederalTabComponent : BaseClaimsExplorerAnswerTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id='panel_federal' or @data-testid='federal-content-panel']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Federal";
    }
}
