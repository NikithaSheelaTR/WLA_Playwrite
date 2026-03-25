namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using OpenQA.Selenium;

    /// <summary>
    /// State Tab component
    /// </summary>
    public class StateTabComponent : BaseClaimsExplorerAnswerTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id='panel_state' or @data-testid='state-content-panel']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "State";
    }
}
