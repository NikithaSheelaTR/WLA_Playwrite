namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage.HomePageTabs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// Precision Tools Item as a part of Browse Component - Tools List
    /// </summary>
    public class PrecisionToolsTabItem : BaseItem
    {
        private static readonly By LinkLocator = By.XPath(".//a[@data-link-type='category-page'] | .//button[@id='coid_toolTab_KeepList']");
        private static readonly By SummaryTextLocator = By.XPath(".//p[@class='Athens-browse-tool-content-info']");

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecisionToolsTabItem"/> class. 
        /// </summary>
        /// <param name="containerElement"> The container Element. </param>
        public PrecisionToolsTabItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Item header link
        /// </summary>
        public ILink HeaderLink => new Link(this.Container, LinkLocator);

        /// <summary>
        /// Item summary label
        /// </summary>
        public ILink SummaryLabel => new Link(this.Container, SummaryTextLocator);
    }
}
