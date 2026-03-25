namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;

    /// <summary>
    /// Precision Research Topics Coverage dialog
    /// </summary>
    public class PrecisionResearchTopicsCoverageDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//div[contains(@class, 'container Athens-HomepageScopeModal')]");
        private static readonly By TitleLabelLocator = By.XPath(".//h1[contains(@id, 'coid_lightboxAriaLabel')]");
        private static readonly By CloseButtonLocator = By.XPath(".//button[@class='Button-secondary']");
        private static readonly By StartPrecisionSearchButtonLocator = By.XPath(".//button[@class='Button-primary']");
        private static readonly By ContentHeadingLabelLocator = By.XPath(".//h2[contains(@class, 'contentSubHeading')]");

        /// <summary>
        /// Title label
        /// </summary>
        public ILabel TitleLabel => new Label(this.ComponentLocator, TitleLabelLocator);

        /// <summary>
        /// Content heading (Antitrust, Commercial law and ect.)
        /// </summary>
        public ILabel ContentHeadingLabel => new Label(this.ComponentLocator, ContentHeadingLabelLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(this.ComponentLocator, CloseButtonLocator);

        /// <summary>
        /// Start Precision Search button
        /// </summary>
        public IButton StartPrecisionSearchButton => new Button(this.ComponentLocator, StartPrecisionSearchButtonLocator);

        /// <summary>
        /// Topics section
        /// </summary>
        public PrecisionResearchTopicsComponent Topics { get; } = new PrecisionResearchTopicsComponent();

        /// <summary>
        /// Component locator
        /// </summary>
        protected By ComponentLocator => ContainerLocator;
    }
}

