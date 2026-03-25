namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items.AALP;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Base answer tab component
    /// </summary>
    public abstract class BaseClaimsExplorerAnswerTabComponent : BaseTabComponent
    {
        private static readonly By TryAiAssistedResearchLinkLocator = By.XPath(".//a[@href and contains(text(), 'AI-Assisted Research')]");
        private static readonly By AllFilterButtonLocator = By.XPath(".//*[contains(text(), 'All')]/parent::*[contains(@class, 'saf-chip--button')] | .//button[@data-testid='All']");
        private static readonly By SupportedFilterButtonLocator = By.XPath(".//*[contains(text(), 'Supported')]/parent::*[contains(@class, 'saf-chip--button')] | .//button[@data-testid='Supported']");
        private static readonly By AdditionalFactsNeededFilterButtonLocator = By.XPath(".//*[contains(text(), 'Additional facts needed')]/parent::*[contains(@class, 'saf-chip--button')] | .//button[@data-testid='Additional facts needed']");
        private static readonly By SupportedMaterialHeadingContainerLocator = By.XPath(".//*[@class='saf-accordion CE-accordion' or @data-testid='accordion-item']");
        private static readonly By NewClaimsSearchButtonLocator = By.XPath(".//button[@class='CS-results-new-search']");
        private static readonly By FilterButtonLocator = By.XPath(".//span[@class='saf-chip__text']/parent::*[contains(@class, 'saf-chip--button')] | .//button[contains(@class,'Filters-module__ResultsCardFiltersChip')]");

        /// <summary>
        /// Filter buttons
        /// </summary>
        public IReadOnlyCollection<IButton> FilterButtons => new ElementsCollection<Button>(this.ComponentLocator, FilterButtonLocator);

        /// <summary>
        /// 'Try AI-Assisted Research' link
        /// </summary>
        public ILink TryAiAssistedResearchLink => new Link(this.ComponentLocator, TryAiAssistedResearchLinkLocator);

        /// <summary>
        /// 'All' filter button
        /// </summary>
        public IButton AllFilterButton => new Button(this.ComponentLocator, AllFilterButtonLocator);

        /// <summary>
        /// 'Supported' filter button
        /// </summary>
        public IButton SupportedFilterButton => new Button(this.ComponentLocator, SupportedFilterButtonLocator);

        /// <summary>
        /// 'Additional facts needed' filter button
        /// </summary>
        public IButton AdditionalFactsNeededFilterButton => new Button(this.ComponentLocator, AdditionalFactsNeededFilterButtonLocator);

        /// <summary>
        /// New claims search button
        /// </summary>
        public IButton NewClaimsSearchButton => new Button(this.ComponentLocator, NewClaimsSearchButtonLocator);

        /// <summary>
        /// Headings (list of accordion items)
        /// </summary>
        /// <returns>List of supporting materials items</returns>
        public ItemsCollection<AiClamsExplorerHeadingItem> Headings =>
            new ItemsCollection<AiClamsExplorerHeadingItem>(this.ComponentLocator, SupportedMaterialHeadingContainerLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator { get; }
    }
}
