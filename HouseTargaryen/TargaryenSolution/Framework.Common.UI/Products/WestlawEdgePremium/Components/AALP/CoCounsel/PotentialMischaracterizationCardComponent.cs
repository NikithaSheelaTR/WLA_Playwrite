namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.CoCounsel
{
    using Framework.Common.UI.Products.Shared.Components;
    using OpenQA.Selenium;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    /// <summary>
    /// Potential Mischaracterization Card Component
    /// </summary>
    public class PotentialMischaracterizationCardComponent : BaseModuleRegressionComponent
    {
        private static readonly By CardContainerLocator = By.XPath(".//saf-card[contains(@class, 'ResultsList-module')]");
        private static readonly By CardTitleLabelLocator = By.XPath(".//*[contains(@class, 'mcHeading')]");

        private IWebElement ContainerElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="PotentialMischaracterizationCardComponent"/> class. 
        /// </summary>
        /// <param name="containerElement"> container </param>
        public PotentialMischaracterizationCardComponent(IWebElement containerElement)
        {
            this.ContainerElement = containerElement;
        }

        /// <summary>
        /// Card title label
        /// </summary>
        public ILabel CardTitleLabel => new Label(this.ContainerElement, CardContainerLocator, CardTitleLabelLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator { get; } = CardContainerLocator;
    }
}
