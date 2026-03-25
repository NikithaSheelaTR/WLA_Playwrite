namespace Framework.Common.UI.Products.WestLawNext.Components.IpTools
{
    using Framework.Common.UI.Products.Shared.Components.RightPaneComponents;
    using Framework.Common.UI.Products.Shared.Enums.Widgets;

    using OpenQA.Selenium;

    /// <summary>
    /// ToolsLinkComponent
    /// </summary>
    public sealed class ToolsLinksComponent : LinksComponent
    {
        private const string ComponentLctMask =
            "//div[contains(@class,'co_tabShow')]//div[contains(@class,'co_genericBox')][contains(./h3/span/text(), '{0}')]";

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolsLinksComponent"/> class. 
        /// </summary>
        public ToolsLinksComponent()
            : base(LinksNames.Tools)
        {
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => By.XPath(string.Format(ComponentLctMask, this.ComponentName));
    }
}
