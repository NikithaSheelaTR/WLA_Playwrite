namespace Framework.Common.UI.Products.WestlawAdvantage.Components.KnowYourJudge
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// Fact based Tab
    /// </summary>
    public class FactBasedTab : BaseBrowseTabPanelComponent
    {
        private static readonly By TabContainerLocator = By.XPath("//div[@data-testid='fact-report-container']");

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Fact-based";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TabContainerLocator;
    }
}


