namespace Framework.Common.UI.Products.WestlawEdgePremium.Items.AALP.CoCounsel
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    /// <summary>
    /// CoCounsel Relevant Searches supporting material item
    /// </summary>
    public class CoCounselRelevantSearchesSupportingMaterialsItem : BaseItem
    {
        private static readonly By DocumentTitleLinkLocator = By.XPath(".//saf-anchor[contains(@href, 'Document') and not(@class)]");
        private static readonly By DocumentPassageLinkLocator = By.XPath("");
        private static readonly By MetadataLabelLocator = By.XPath("./following-sibling::span");
        private static readonly By KeyCiteFlagLinkLocator = By.XPath("");
        private static readonly By OutOfPlanBannerLabelLocator = By.XPath("");

        /// <summary>
        /// Initializes a new instance of the <see cref="CoCounselRelevantSearchesSupportingMaterialsItem"/> class.
        /// </summary>
        /// <param name="containerElement">
        /// The container locator.
        /// </param>
        public CoCounselRelevantSearchesSupportingMaterialsItem(IWebElement containerElement) : base(containerElement)
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
        public ILabel MetadataLabel => new Label(this.Container, DocumentTitleLinkLocator, MetadataLabelLocator);

        /// <summary>
        /// Out of plan banner label
        /// </summary>
        public ILabel OutOfPlanBannerLabel => new Label(this.Container, OutOfPlanBannerLabelLocator);
    }
}
