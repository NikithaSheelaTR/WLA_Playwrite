namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.QuickCheck
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Potential mischaracterization component
    /// </summary>
    public class PotentialMischaracterizationComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath(".//div[contains(@class, 'bsCard-')]");

        private IWebElement ContainerElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="PotentialMischaracterizationComponent"/> class. 
        /// </summary>
        /// <param name="containerElement"> container </param>
        public PotentialMischaracterizationComponent(IWebElement containerElement)
        {
            this.ContainerElement = containerElement;

        }

        /// <summary>
        /// Is component displayed
        /// </summary>
        /// <returns>True - if it is displayed, false - otherwise</returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ContainerElement, ComponentLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
