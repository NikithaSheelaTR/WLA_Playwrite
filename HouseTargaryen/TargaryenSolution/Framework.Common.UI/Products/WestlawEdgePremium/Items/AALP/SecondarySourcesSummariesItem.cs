namespace Framework.Common.UI.Products.WestlawEdgePremium.Items.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// Secondary Sources Summaries Item
    /// </summary>
    public class SecondarySourcesSummariesItem : BaseItem
    {
        private static readonly By DocumentTitleLinkLocator = By.XPath(".//a[contains(@href, 'Document') and not(contains(@class, 'co_snippet_link'))]");
        private static readonly By MetadataLabelLocator = By.XPath(".//div[@class='co_searchResults_citation']");
        private static readonly By OutOfPlanBannerLabelLocator = By.XPath(".//div[contains(@class, 'saf-badge')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="SecondarySourcesSummariesItem"/> class.
        /// </summary>
        /// <param name="containerElement">
        /// The container locator.
        /// </param>
        public SecondarySourcesSummariesItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Document title link
        /// </summary>
        public ILink DocumentTitleLink => new Link(this.Container, DocumentTitleLinkLocator);

        /// <summary>
        /// Metadata label
        /// </summary>
        public ILabel MetadataLabel => new Label(this.Container, MetadataLabelLocator);

        /// <summary>
        /// Out of plan banner label
        /// </summary>
        public ILabel OutOfPlanBannerLabel => new Label(this.Container, OutOfPlanBannerLabelLocator);
    }
}

