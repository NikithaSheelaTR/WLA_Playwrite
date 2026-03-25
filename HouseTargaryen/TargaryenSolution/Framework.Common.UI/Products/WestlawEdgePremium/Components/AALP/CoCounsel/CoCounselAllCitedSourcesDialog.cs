namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.CoCounsel
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items.AALP.CoCounsel;

    /// <summary>
    /// CoCounselAllCitedSourcesComponent
    /// </summary>
    public class CoCounselAllCitedSourcesComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class='ai-result-panel']");
        private static readonly By DialogHeaderLabelLocator = By.XPath(".//div[contains(@class, 'panel-header') or contains(@data-testid, 'panel-header')]//h3");
        private static readonly By ClosePanelButtonLocator = By.XPath(".//*[contains(@class, 'delphi-close-panel-btn')]");
        private static readonly By CoCounselHeadingContainerLocator = By.XPath(".//saf-accordion-item[@class='delphi-skill--ai-assisted-legal-research__accordion__item']");
        private static readonly By CoCounselRelevantSearchesContainerLocator = By.XPath(".//*[@class='delphi-skill--ai-assisted-legal-research__extra-scroll-container']/li");

        /// <summary>
        /// Dialog header label
        /// </summary>
        public ILabel DialogHeaderLabel => new Label(this.ComponentLocator, DialogHeaderLabelLocator);

        /// <summary>
        /// Close panel button
        /// </summary>
        public IButton ClosePanelButton => new Button(this.ComponentLocator, ClosePanelButtonLocator);

        /// <summary>
        /// Supporting materials items
        /// </summary>
        /// <returns>List of answers</returns>
        public ItemsCollection<CoCounselRelevantSearchesSupportingMaterialsItem> SupportingMaterialsItems => 
            new ItemsCollection<CoCounselRelevantSearchesSupportingMaterialsItem>(this.ComponentLocator, CoCounselRelevantSearchesContainerLocator);

        /// <summary>
        /// Headings (list of accordion items)
        /// </summary>
        /// <returns>List of supporting materials items</returns>
        public ItemsCollection<CoCounselHeadingItem> Headings => new ItemsCollection<CoCounselHeadingItem>(this.ComponentLocator, CoCounselHeadingContainerLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
