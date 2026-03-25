namespace Framework.Common.UI.Products.WestlawEdgePremium.Items.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    /// <summary>
    /// AI Claims Exporer supporting material item (accordion item)
    /// </summary>
    public class AiClamsExplorerHeadingItem : BaseItem
    {
        private static readonly By SubHeadingContainerLocator = By.XPath(".//div[@class='CE-accordion-item'] | .//following::*[contains(@class, 'citationWrapper')]/parent::div");
        private static readonly By DocumentLinkLocator = By.XPath(".//p[@class='CE-accordion-title']//a[@href and not(contains(@class, 'Flag'))] | .//saf-anchor[@data-testid='document-link']");
        private static readonly By KeyCiteFlagLinkLocator = By.XPath(".//span[@class='AALP-supporting-materials-keycite-items']//a[contains(@href, 'RelatedInformation')] | .//div[@data-testid='key-cite-flags']");
        private static readonly By FindDefensesLinkLocator = By.XPath(".//a[@href='#']|.//following::saf-anchor[@href='#']");
        private static readonly By OutOfPlanLabelLocator = By.XPath("./preceding-sibling::*//*[contains(@class, 'saf-badge') and contains(text(), 'Out of plan')] | .//saf-badge[@appearance='warning-light']");
        private static readonly By HeadingAccordionButtonLocator = By.XPath(".//button[contains(@id, 'saf-accordion__control')] | .//saf-button[contains(@class,'cocoAccordionItemButton')]");
        private static readonly By FindDefensesConcurrentSearchesLimitInfoboxLocator = By.XPath(".//*[@class='saf-alert__content']");
        private static readonly By LastAmendedLabelLocator = By.XPath(".//saf-metadata-item[contains(text(), 'Last amended')]");
        private static readonly By ActionableDataLinkLocator = By.XPath(".//saf-metadata-item[contains(text(), 'Actionable under')]//a|.//saf-metadata-item[contains(text(), 'Actionable under')]//saf-anchor");
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AiClamsExplorerHeadingItem"/> class.
        /// </summary>
        /// <param name="containerElement">
        /// The container locator.
        /// </param>
        public AiClamsExplorerHeadingItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// AI Claims Explorer sub heading items (inside accordion item)
        /// </summary>
        public ItemsCollection<AiClaimsSubHeadingItem> SubHeadings => new ItemsCollection<AiClaimsSubHeadingItem>(this.Container, SubHeadingContainerLocator);

        /// <summary>
        /// Hwading accordion button
        /// </summary>
        public IButton HeadingAccordionButton => new Button(this.Container, HeadingAccordionButtonLocator);

        /// <summary>
        /// Out of plan label
        /// </summary>
        public ILabel OutOfPlanLabel => new Label(this.Container, OutOfPlanLabelLocator);

        /// <summary>
        /// Last Amended label
        /// </summary>
        public ILabel LastAmendedLabel => new Label(this.Container, LastAmendedLabelLocator);

        /// <summary>
        /// Actionable Data link
        /// </summary>
        public ILink ActionableDataLink => new Link(this.Container, ActionableDataLinkLocator);

        /// <summary>
        /// Document link
        /// </summary>
        public ILink DocumentLink => new Link(this.Container, DocumentLinkLocator);

        /// <summary>
        /// 'Find Defenses' link
        /// </summary>
        public ILink FindDefensesLink => new Link(this.Container, FindDefensesLinkLocator);

        /// <summary>
        /// Key Cite flag link
        /// </summary>
        public ILink KeyCiteFlagLink => new Link(this.Container, KeyCiteFlagLinkLocator);

        /// <summary>
        /// 'Find Defenses' concurrent searches limit infobox
        /// </summary>
        public IInfoBox FindDefensesConcurrentSearchesLimitInfobox => new InfoBox(DriverExtensions.GetElement(this.Container, FindDefensesConcurrentSearchesLimitInfoboxLocator));
    }
}
