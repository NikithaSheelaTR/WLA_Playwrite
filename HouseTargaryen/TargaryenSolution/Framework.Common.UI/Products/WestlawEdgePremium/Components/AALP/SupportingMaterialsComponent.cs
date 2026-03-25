namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using System.Collections.Generic;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items.AALP;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// Supporting Materials component
    /// </summary>
    public class SupportingMaterialsComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath(".//*[@class='CS-accordion-panel-content']");
        private static readonly By SupportingMaterialItemLocator = By.XPath(".//*[@class='CS-additional-content-title' and text()='Cited sources']/following-sibling::div/ol[@class='CS-supporting-materials-list-box']//li");
        private static readonly By ContentLabelsLocator = By.XPath(".//h4[@class='CS-additional-content-title']");
        private static readonly By CasesItemLocator = By.XPath(".//li[not(contains(@class, 'show-supporting')) and parent::ol[@id='sect-0']]");
        private static readonly By SecondarySourcesItemLocator = By.XPath(".//li[not(contains(@class, 'show-supporting')) and parent::ol[@id='sect-0-secondary_sources']]");
        private static readonly By PracticalLawItemLocator = By.XPath(".//li[not(contains(@class, 'show-supporting')) and parent::ol[@id='sect-0-practical_law']]");
        private static readonly By AdminDecisionsItemLocator = By.XPath(".//li[not(contains(@class, 'show-supporting')) and parent::ol[@id='sect-0-admin_decision']]");
        private static readonly By AuSupportingMaterialItemLocator = By.XPath(".//*[@class='CS-additional-content-title' and text()='Cases, Acts, Legislative Instruments and Government & Regulatory Materials']/following-sibling::div/ol[@class='CS-supporting-materials-list-box']//li");
        private static readonly By SecondarySourcesSummariesHeadingLabelLocator = By.XPath(".//*[@class='AAR-additionalSearchSummaries-heading']");
        private static readonly By SecondarySourcesSummariesItemLocator = By.XPath(".//ul[@class='AAR-additionalSearchSummaries-list']/li");
        private static readonly By NumberedHeadingsLocator = By.XPath("//div/span[@class = 'CS-supporting-material-item-rank']/following-sibling::a");
        private static readonly By SourceNumberLocator = By.XPath("//div/span[@class = 'CS-supporting-material-item-rank']");
        private static readonly By CitationLocator = By.XPath("//div[@class='co_searchResults_citation']");

        private IWebElement ContainerElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupportingMaterialsComponent"/> class. 
        /// </summary>
        /// <param name="containerElement"> container </param>
        public SupportingMaterialsComponent(IWebElement containerElement)
        {
            this.ContainerElement = containerElement;
        }

        /// <summary>
        /// Secondary Sources Summaries Heading Label
        /// </summary>
        public ILabel SecondarySourcesSummariesHeadingLabel => new Label(this.ComponentLocator, SecondarySourcesSummariesHeadingLabelLocator);

        /// <summary>
        /// Secondary Sources Summaries items
        /// </summary>
        /// <returns>List of Secondary Sources Summaries</returns>
        public ItemsCollection<SecondarySourcesSummariesItem> SecondarySourcesSummariesItems =>
            new ItemsCollection<SecondarySourcesSummariesItem>(this.ContainerElement, new ByChained(this.ComponentLocator, SecondarySourcesSummariesItemLocator));

        /// <summary>
        /// Supporting materials items
        /// </summary>
        /// <returns>List of answers</returns>
        public ItemsCollection<SupportingMaterialsItem> SupportingMaterialsItems =>
            new ItemsCollection<SupportingMaterialsItem>(this.ContainerElement, new ByChained(this.ComponentLocator, SupportingMaterialItemLocator));

        /// <summary>
        /// Cases items
        /// </summary>
        /// <returns>List of Cases answers</returns>
        public ItemsCollection<SupportingMaterialsItem> CasesItems =>
            new ItemsCollection<SupportingMaterialsItem>(this.ContainerElement, new ByChained(this.ComponentLocator, CasesItemLocator));

        /// <summary>
        /// Secondary Sources items
        /// </summary>
        /// <returns>List of Secondary Sources answers</returns>
        public ItemsCollection<SupportingMaterialsItem> SecondarySourcesItems =>
            new ItemsCollection<SupportingMaterialsItem>(this.ContainerElement, new ByChained(this.ComponentLocator, SecondarySourcesItemLocator));

        /// <summary>
        /// Practical Law items
        /// </summary>
        /// <returns>List of Practical Law answers</returns>
        public ItemsCollection<SupportingMaterialsItem> PracticalLawItems =>
            new ItemsCollection<SupportingMaterialsItem>(this.ContainerElement, new ByChained(this.ComponentLocator, PracticalLawItemLocator));

        /// <summary>
        /// Admin Decisions items
        /// </summary>
        /// <returns>List of Admin Decisions answers</returns>
        public ItemsCollection<SupportingMaterialsItem> AdminDecisionsItems =>
            new ItemsCollection<SupportingMaterialsItem>(this.ContainerElement, new ByChained(this.ComponentLocator, AdminDecisionsItemLocator));

        /// <summary>
        /// Content labels
        /// </summary>
        public IReadOnlyCollection<ILabel> ContentLabels => new ElementsCollection<Label>(this.ComponentLocator, ContentLabelsLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator { get; } = ContainerLocator;

        /// <summary>
        /// ANZ AAR Supporting materials items
        /// </summary>
        /// <returns>List of answers</returns>
        public ItemsCollection<SupportingMaterialsItem> AuSupportingMaterialsItems =>
            new ItemsCollection<SupportingMaterialsItem>(this.ContainerElement, new ByChained(this.ComponentLocator, AuSupportingMaterialItemLocator));

        /// <summary>
        /// Source Numbered Items
        /// </summary>
        public IReadOnlyCollection<ILabel> NumberedItemLabel => new ElementsCollection<Label>(this.ComponentLocator, SourceNumberLocator);

        /// <summary>
        /// Source Heading Items
        /// </summary>
        public IReadOnlyCollection<ILabel> NumberedHeadingLabels => new ElementsCollection<Label>(this.ComponentLocator, NumberedHeadingsLocator);

        /// <summary>
        /// Source Citation Items
        /// </summary>
        public IReadOnlyCollection<ILabel> CitationLabel => new ElementsCollection<Label>(this.ComponentLocator, CitationLocator);
    }
}
