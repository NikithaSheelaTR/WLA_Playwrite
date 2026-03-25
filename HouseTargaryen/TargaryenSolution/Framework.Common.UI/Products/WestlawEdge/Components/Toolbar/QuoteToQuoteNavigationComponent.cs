namespace Framework.Common.UI.Products.WestlawEdge.Components.Toolbar
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;
    using Framework.Core.Utils.Extensions;

    /// <summary>
    /// Contains all methods pertaining to the NavigationComponent
    /// </summary>
    public class QuoteToQuoteNavigationComponent : BaseModuleRegressionComponent
    {
        private static readonly By NextResultButtonLocator = By.XPath("//*[contains(@class, 'co_tbButton co_next')]");

        private static readonly By PreviousResultButtonLocator = By.XPath("//*[contains(@class, 'co_tbButton co_prev')]");

        private static readonly By CurrentNavigationNumberTextLocator = By.XPath(".//strong[1]");

        private static readonly By TheLastNavigationNumberTextLocator = By.XPath(".//strong[2]");

        private static readonly By ContainerLocator = By.XPath("//div[@class='DA-QuotationsNavigationWidgetContainer']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Next Button in quot to quot navigation
        /// </summary>
        public IButton NextQuotationButton => new CustomEdgeButton(DriverExtensions.GetElement(ComponentLocator), NextResultButtonLocator);

        /// <summary>
        /// Previous Button in quot to quot navigation
        /// </summary>
        public IButton PreviousQuotationButton => new CustomEdgeButton(DriverExtensions.GetElement(ComponentLocator), PreviousResultButtonLocator);

        /// <summary>
        /// Gets the current quotation navigation number
        /// </summary>
        /// <returns>The navigation number of the current quoatation</returns>
        public int GetCurrentQuotationNavigationNumber() => DriverExtensions.GetElement(this.ComponentLocator, CurrentNavigationNumberTextLocator).Text.ConvertCountToInt();

        /// <summary>
        /// Gets the last quotation navigation number
        /// </summary>
        /// <returns>The navigation number of the last quoatation</returns>
        public int GetTheLastQuotationNavigationNumber() => DriverExtensions.GetElement(this.ComponentLocator, TheLastNavigationNumberTextLocator).Text.ConvertCountToInt();

        /// <summary>
        /// Checks if the NavigationComponent is Displayed
        /// </summary>
        /// <returns>Whether or not the NavigationComponent is displayed</returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, 5);
    }
}