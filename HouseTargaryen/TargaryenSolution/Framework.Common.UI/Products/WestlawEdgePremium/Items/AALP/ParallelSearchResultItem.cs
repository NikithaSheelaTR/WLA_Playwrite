namespace Framework.Common.UI.Products.WestlawEdgePremium.Items.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;
    
    /// <summary>
    /// Search result item
    /// </summary>
    public class ParallelSearchResultItem : BaseItem
    {
        private static readonly By DocumentTitleLinkLocator = By.XPath(".//saf-anchor[contains(@class, 'parallelSearchResultsItemLabel')]");
        private static readonly By DocumentPassageLinkLocator = By.XPath(".//saf-anchor[contains(@href, 'Document') and contains(@class, 'parallelSearchCardLinkWrapper')]/span");
        private static readonly By MetadataLabelLocator = By.XPath(".//saf-metadata[contains(@class,'parallelSearchResultsMeta')]");
        private static readonly By KeyCiteFlagLinkLocator = By.XPath(".//saf-anchor[contains(@href, 'RelatedInformation')]");
        private static readonly By OutOfPlanBannerLabelLocator = By.XPath(".//saf-badge[contains(@class, '__parallelSearchResultsItemOOP')]");
        private static readonly By MetadataJurisLocator = By.XPath(".//saf-metadata-item[1]");
        private static readonly By ViewedPUIButtonLocator = By.XPath(".//button[contains(@class, 'PUI-button--viewed')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="ParallelSearchResultItem"/> class.
        /// </summary>
        /// <param name="containerElement">
        /// The container locator.
        /// </param>
        public ParallelSearchResultItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Document title link
        /// </summary>
        public ILink DocumentTitleLink => new Link(this.Container, DocumentTitleLinkLocator);

        /// <summary>
        /// KeyCite flag link
        /// </summary>
        public ILink KeyCiteFlagLink => new Link(this.Container, KeyCiteFlagLinkLocator);

        /// <summary>
        /// Document passage link
        /// </summary>
        public ILink DocumentPassageLink => new Link(this.Container, DocumentPassageLinkLocator);

        /// <summary>
        /// Metadata label
        /// </summary>
        public ILabel MetadataLabel => new Label(this.Container, MetadataLabelLocator);

        /// <summary>
        /// Out of plan banner label
        /// </summary>
        public ILabel OutOfPlanBannerLabel => new Label(this.Container, OutOfPlanBannerLabelLocator);

        /// <summary>
        /// Metadata juridiction label
        /// </summary>
        public ILabel MetadataJurisLabel => new Label(this.Container, MetadataJurisLocator);

        /// <summary>
        /// Viewed PUI Button
        /// </summary>
        public IButton ViewedPUIButton => new Button(this.Container, ViewedPUIButtonLocator);
    }
}
