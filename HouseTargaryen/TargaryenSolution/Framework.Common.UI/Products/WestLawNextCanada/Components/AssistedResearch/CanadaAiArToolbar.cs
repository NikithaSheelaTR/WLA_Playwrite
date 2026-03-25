namespace Framework.Common.UI.Products.WestLawNextCanada.Components.AssistedResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;
    using OpenQA.Selenium;

    /// <summary>
    /// Toolbar component
    /// </summary>
    public class CanadaAiArToolbar : EdgeToolbarComponent
    {
        private static readonly By ToolbarContainerLocator = By.XPath("//div[@class='CS-main-content-heading-container']");
        private static readonly By JurisdictionLabelLocator = By.ClassName("AALP-settings-jurisdiction-label");
        private static readonly By NewResearchButtonLocator = By.XPath(".//button[contains(@class, 'CS-main-start-new-button')]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ToolbarContainerLocator;

        /// <summary>
        /// Selected Jurisdictions Label
        /// </summary>
        public ILabel JurisdictionLabel => new Label(JurisdictionLabelLocator);

        /// <summary>
        /// New research button
        /// </summary>
        public IButton NewResearchButton => new Button(this.ComponentLocator, NewResearchButtonLocator);
    }
}