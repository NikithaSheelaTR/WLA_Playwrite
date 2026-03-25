namespace Framework.Common.UI.Products.WestlawEdge.Components.History
{
    using Framework.Common.UI.Products.WestLawNext.Components;
    using OpenQA.Selenium;

    /// <summary>
    /// Current history tab component
    /// </summary>
    public class CurrentHistoryTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id='historySubTabsMainContent']//a/span[text() = 'Current history']");

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
