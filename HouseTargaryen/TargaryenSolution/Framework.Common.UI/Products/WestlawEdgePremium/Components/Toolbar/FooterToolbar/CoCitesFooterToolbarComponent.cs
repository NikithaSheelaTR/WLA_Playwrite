namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.Toolbar.FooterToolbar
{
    using Framework.Common.UI.Products.Shared.Components.Toolbar.FooterToolbar;
    using Framework.Common.UI.Products.WestlawEdgePremium.DropDowns.RiTab;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// Co-cites footer toolbar component
    /// </summary>
    public class CoCitesFooterToolbarComponent : FooterToolbarComponent
    {
        private static readonly By ContainerLocator = By.XPath(".//div[@class = 'ToolbarContainer'][.//ul[@aria-label='Items per page']]");

        /// <summary>
        /// Initializes a new instance of the <see cref="CoCitesFooterToolbarComponent"/> class
        /// </summary>
        /// <param name="coCitesComponentContainer">  co-cites component container</param>
        public CoCitesFooterToolbarComponent(By coCitesComponentContainer) => 
            this.ComponentLocator = new ByChained(coCitesComponentContainer, ContainerLocator);

        /// <summary>
        /// Pagination component
        /// </summary>
        public new CoCitesPaginationFooterComponent PaginationComponent => new CoCitesPaginationFooterComponent(this.ComponentLocator);

        /// <summary>
        /// Per Page DropDown
        /// </summary>
        public new CoCitesPerPageDropdown PerPageDropDown => new CoCitesPerPageDropdown(this.ComponentLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator { get; }
    }
}