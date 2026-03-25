namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage.HomePageTabs
{
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// Precision Tools Tab panel
    /// </summary>
    public class PrecisionToolsTabPanel : BaseBrowseTabPanelComponent
    {
        private static readonly By ToolsElementLocator = By.XPath(".//li");
        private static readonly By ContainerLocator = By.XPath("//*[@class='Athens-browse-tools']/ancestor::*[contains(@id, 'panel')]");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Tools";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        ///  All Tools Items
        /// </summary>
        public ItemsCollection<PrecisionToolsTabItem> ToolsItems => new ItemsCollection<PrecisionToolsTabItem>(ComponentLocator, ToolsElementLocator);
    }
}
