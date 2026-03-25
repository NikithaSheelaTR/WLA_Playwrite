namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.NewSearchTemplate
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Precision Search all tab top info column component
    /// </summary>
    public class PrecisionSearchAllTabTopInfoColumnComponent : PrecisionBaseSearchAllTabInfoColumnComponent
    {
        private static readonly By LegalIssueLinkLocator = By.XPath(".//*[@class='co_linkBlue' and contains(text(), 'Legal issue')]");
        private static readonly By AreaOfLawLinkLocator = By.XPath(".//*[@class='co_linkBlue' and contains(text(), 'Area of law')]");
        private static readonly By ContentItemLinkLocator = By.XPath(".//*[@class='co_linkBlue']");

        private readonly By componentLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecisionBaseSearchAllTabInfoColumnComponent"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public PrecisionSearchAllTabTopInfoColumnComponent(By componentLocator) : base(componentLocator)
        {
            this.componentLocator = componentLocator;
        }

        /// <summary>
        /// Legal Issue link
        /// </summary>
        public ILink LegalIssueLink => new Link(this.ComponentLocator, LegalIssueLinkLocator);

        /// <summary>
        /// Area Of Law link
        /// </summary>
        public ILink AreaOfLawLink => new Link(this.ComponentLocator, AreaOfLawLinkLocator);

        /// <summary>
        /// List of Learn more info content items links
        /// </summary>
        public IReadOnlyCollection<ILink> ContentItemListLinks => new ElementsCollection<Link>(this.ComponentLocator, ContentItemLinkLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => this.componentLocator;
    }
}
