namespace Framework.Common.UI.Products.WestlawEdge.Components.History
{
    using Framework.Common.UI.Products.Shared.Components.FolderHistory;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Raw.WestlawEdge.Items.History;

    using OpenQA.Selenium;

    /// <summary>
    /// The history graphical view on Edge.
    /// </summary>
    public class GraphicalHistoryGridComponent : HistoryGridComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id='co_contentColumn']//div[@class='GH-Timeline-Container']");
        private static readonly By ItemsLocator = By.XPath(".//div[contains(@class,'GH-Timeline-ItemContainer')]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        ///  Get list of edge folder grid items
        /// </summary>
        /// <returns>list of edge folder grid items</returns>
        public ItemsCollection<GraphicalHistoryGridItem> GraphicalHistoryGridItems => new ItemsCollection<GraphicalHistoryGridItem>(this.ComponentLocator, ItemsLocator);
    }
}
