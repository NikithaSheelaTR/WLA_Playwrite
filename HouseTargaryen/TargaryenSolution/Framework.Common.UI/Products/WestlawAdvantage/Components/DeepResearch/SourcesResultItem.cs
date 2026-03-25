namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// Sources result item
    /// </summary>
    public class SourcesResultItem : BaseItem
    {
        private static readonly By DocumentTitleLinkLocator = By.XPath(".//a[contains(@href, 'Document')]");
        private static readonly By MetadataLabelLocator = By.XPath(".//saf-metadata-v3[contains(@class='ResultItemMetadata-module__resultItemMetaData')]");
        private static readonly By DocumentSummaryLabelLocator = By.XPath(".//div[contains(@class='CitationCard-module__citationCardSummary')]");
        private static readonly By SupportingSectionLabelLocator = By.XPath(".//div[contains(@class='CitationCard-module__citationCardSnippet')]");
        private static readonly By KeyCiteFlagLinkLocator = By.XPath(".//saf-anchor-v3[contains(@href, 'RelatedInformation')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="SourcesResultItem"/> class.
        /// </summary>
        /// <param name="containerElement">
        /// The container locator.
        /// </param>
        public SourcesResultItem(IWebElement containerElement) : base(containerElement)
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
        /// Metadata label
        /// </summary>
        public ILabel MetadataLabel => new Label(this.Container, MetadataLabelLocator);

        /// <summary>
        /// Document Summary Label
        /// </summary>
        public ILabel DocumentSummaryLabel => new Label(this.Container, DocumentSummaryLabelLocator);

        /// <summary>
        /// Supporting Section Label
        /// </summary>
        public ILabel SupportingSectionLabel => new Label(this.Container, SupportingSectionLabelLocator);
    }
}

