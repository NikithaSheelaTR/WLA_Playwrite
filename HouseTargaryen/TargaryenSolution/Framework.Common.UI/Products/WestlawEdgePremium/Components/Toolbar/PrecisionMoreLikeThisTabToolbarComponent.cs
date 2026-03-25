namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.Toolbar
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// MLT tab toolbar
    /// </summary>
    public class PrecisionMoreLikeThisTabToolbarComponent : BaseModuleRegressionComponent
    {
        private static readonly By ToolbarContainerLocator = By.XPath("//div[@id='co_docToolbar']");
        private static readonly By JurisdictionButtonLocator = By.XPath(".//*[@id='jurisdictionId_athens']");
        private static readonly By TabDescriptionLocator = By.XPath(".//*[@class='docToolbar-flex']/span[not(contains(@class, 'jurisdiction'))]");

        /// <summary>
        /// Jurisdiction button
        /// </summary>
        public IButton JurisdictionButton => new Button(ToolbarContainerLocator, JurisdictionButtonLocator);

        /// <summary>
        /// Tab description label
        /// </summary>
        public ILabel TabDescriptionLabel => new Label(ToolbarContainerLocator, TabDescriptionLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ToolbarContainerLocator;
    }
}
