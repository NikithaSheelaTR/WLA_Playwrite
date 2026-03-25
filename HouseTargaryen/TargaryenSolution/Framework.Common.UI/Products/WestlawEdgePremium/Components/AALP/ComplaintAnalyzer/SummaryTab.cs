namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.ComplaintAnalyzer
{
    using System.Collections.Generic;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// Summary Tab
    /// </summary>
    public class SummaryTab : BaseBrowseTabPanelComponent
    {
        private static readonly By TabContainerLocator = By.XPath("//saf-tab-panel-v3[@data-testid='results-summary-content-panel'] | //saf-tab-panel[@data-testid='results-summary-content-panel']");
        private static readonly By FillingInfoCardCaseNumberLabelLocator = By.XPath(".//li[@data-testid='case-number-container']");
        private static readonly By FillingInfoCardCaptionLabelLocator = By.XPath(".//h4[text()='Caption']//parent::div//following-sibling::p | .//h5[text()='Caption']//parent::div//following-sibling::p");
        private static readonly By PartiesCardPlaintifLabelLocator = By.XPath(".//saf-card-v3[@data-testid='keyParties-section-container']//div[@data-testid='keyParties-plaintiffs']//li | .//saf-card[@data-testid='keyParties-section-container']//div[@data-testid='keyParties-plaintiffs']//li | .//div[@data-testid='keyParties-section-container']//div[@data-testid='keyParties-plaintiffs']//li");
        private static readonly By PartiesCardDefendantsLabelLocator = By.XPath(".//saf-card-v3[@data-testid='keyParties-section-container']//div[@data-testid='keyParties-defendants']//li | .//saf-card[@data-testid='keyParties-section-container']//div[@data-testid='keyParties-defendants']//li | .//div[@data-testid='keyParties-section-container']//div[@data-testid='keyParties-defendants']//li");
        private static readonly By KeyClaimsLocator = By.XPath(".//saf-card-v3[@data-testid='pleadingOverview-section-container']//div[@data-testid='general-allegations']//li[text()] | .//saf-card[@data-testid='pleadingOverview-section-container']//div[@data-testid='general-allegations']//li[text()] | .//div[@data-testid='pleadingOverview-section-container']//div[@data-testid='general-allegations']//li[text()]");
        private static readonly By ReliefSoughtLocator = By.XPath(".//h4[text()='Relief sought']//parent::div//following-sibling::ul/li | .//h5[text()='Relief sought']//parent::div//following-sibling::ul/li");
        private static readonly By ChatResultExpandButtonLocator = By.XPath(".//saf-accordion-v3[@data-testid='summary-section-accordion']//*[contains(@id, 'accordion')]");

        /// <summary>
        /// Filling card case number label
        /// </summary>
        public ILabel FillingInfoCardCaseNumberLabel => new Label(this.ComponentLocator, FillingInfoCardCaseNumberLabelLocator);

        /// <summary>
        /// Filling card caption label
        /// </summary>
        public ILabel FillingInfoCardCaptionLabel => new Label(this.ComponentLocator, FillingInfoCardCaptionLabelLocator);

        /// <summary>
        /// Parties plantiff label
        /// </summary>
        public ILabel PartiesCardPlaintifLabel => new Label(this.ComponentLocator, PartiesCardPlaintifLabelLocator);

        /// <summary>
        /// Pleading Overview key claims labels
        /// </summary>
        public IReadOnlyCollection<ILabel> KeyClaimsLabels => new ElementsCollection<Label>(this.ComponentLocator, KeyClaimsLocator);

        /// <summary>
        /// Overview summary parties label
        /// </summary>
        public IReadOnlyCollection<ILabel> PartiesCardDefendantsLabels => new ElementsCollection<Label>(this.ComponentLocator, PartiesCardDefendantsLabelLocator);

        /// <summary>
        /// Pleading Overview Relief Sought labels
        /// </summary>
        public IReadOnlyCollection<ILabel> ReliefSoughtLabels => new ElementsCollection<Label>(this.ComponentLocator, ReliefSoughtLocator);

        /// <summary>
        /// Chat Result Expand Buttons list
        /// </summary>
        public IReadOnlyCollection<IButton> ChatResultExpandButtons => new ElementsCollection<Button>(this.ComponentLocator, ChatResultExpandButtonLocator);

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Summary";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TabContainerLocator;
    }
}
