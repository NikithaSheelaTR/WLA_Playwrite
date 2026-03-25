namespace Framework.Common.UI.Products.WestlawEdgePremium.Items.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using Framework.Common.UI.Products.Shared.Elements;

    /// <summary>
    /// Supporting material item
    /// </summary>
    public class SupportingMaterialsItem : BaseItem
    {
        private static readonly By DocumentTitleLinkLocator = By.XPath(".//a[contains(@href, 'Document') and not(contains(@class, 'co_snippet_link'))]");
        private static readonly By DocumentPassageLinkLocator = By.XPath(".//a[contains(@href, 'Document') and contains(@class, 'co_snippet_link')]");
        private static readonly By MetadataLabelLocator = By.XPath(".//div[@class='co_searchResults_citation']");
        private static readonly By KeyCiteFlagLinkLocator = By.XPath(".//a[contains(@href, 'RelatedInformation')]");
        private static readonly By OutOfPlanBannerLabelLocator = By.XPath(".//div[contains(@class, 'saf-badge')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="SupportingMaterialsItem"/> class.
        /// </summary>
        /// <param name="containerElement">
        /// The container locator.
        /// </param>
        public SupportingMaterialsItem(IWebElement containerElement) : base(containerElement)
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
        /// Document passages links
        /// </summary>
        public IReadOnlyCollection<ILink> DocumentPassageLinks => new ElementsCollection<Link>(this.Container, DocumentPassageLinkLocator);

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
