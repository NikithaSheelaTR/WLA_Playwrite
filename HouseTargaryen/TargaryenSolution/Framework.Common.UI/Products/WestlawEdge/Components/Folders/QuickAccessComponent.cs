namespace Framework.Common.UI.Products.WestlawEdge.Components.Folders
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Raw.WestlawEdge.Items.Folders;
    using OpenQA.Selenium;

    /// <summary>
    /// Quick access component
    /// </summary>
    public class QuickAccessComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id='co_quickAccess']");
        private static readonly By ZeroStateLocator = By.XPath("//div[contains(@class,'QuickAccessZeroState--')]");
        private static readonly By ZeroStateMessageLocator = By.XPath("//div[contains(@class,'QuickAccessZeroStateMessage')]");
        private static readonly By ItemsLocator = By.XPath("//li[contains(@class, 'QuickAccessItem')]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Zero state message label
        /// </summary>
        public ILabel ZeroStateMessageLabel => new Label(this.ComponentLocator, ZeroStateMessageLocator);

        /// <summary>
        /// Zero state label
        /// </summary>
        public ILabel ZeroStateLabel => new Label(this.ComponentLocator, ZeroStateLocator);

        /// <summary>
        ///  Get list of the quick access items
        /// </summary>
        /// <returns>list of the quick access items</returns>
           public ItemsCollection<QuickAccessItem> QuickAccessItems =>
            new ItemsCollection<QuickAccessItem>(ContainerLocator, ItemsLocator);
    }
}
       
