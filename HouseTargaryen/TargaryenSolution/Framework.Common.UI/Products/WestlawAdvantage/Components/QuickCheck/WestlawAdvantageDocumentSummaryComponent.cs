namespace Framework.Common.UI.Products.WestlawAdvantage.Components.QuickCheck
{
    using Framework.Common.UI.Products.Shared.Components;
    using OpenQA.Selenium;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;

    /// <summary>
    /// Argument Counterargument Document Summary Component
    /// </summary>
    public class WestlawAdvantageDocumentSummaryComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//saf-disclosure/parent::div[contains(@class, 'summaryBox')]");
        private static readonly By DocumentSummaryLabelLocator = By.XPath(".//*[contains(@class,'summaryDisclosureHeading')]");
        private static readonly By DocumentSummaryDisclaimerLabelLocator = By.XPath(".//*[contains(@class, 'summaryDisclaimer')]");
        private static readonly By DocumentSummaryContentLabelLocator = By.XPath(".//saf-card[contains(@class,'summaryContentCard')]");

        /// <summary>
        /// Document summary label
        /// </summary>
        public ILabel DocumentSummaryLabel => new Label(this.ComponentLocator, DocumentSummaryLabelLocator);

        /// <summary>
        /// Document summary content label
        /// </summary>
        public ILabel DocumentSummaryContentLabel => new Label(this.ComponentLocator, DocumentSummaryContentLabelLocator);

        /// <summary>
        /// Document summary disclaimer label
        /// </summary>
        public ILabel DocumentSummaryDisclaimerLabel => new Label(this.ComponentLocator, DocumentSummaryDisclaimerLabelLocator);

        /// <summary>
        /// Document Summary Expand
        /// </summary>
        public ILink DocumentSummaryExpandLink => new Link(this.ComponentLocator);


        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
