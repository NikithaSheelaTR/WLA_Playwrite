namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;

    /// <summary>
    /// Deep Research Tips for best results component
    /// </summary>
    public class AiDeepResearchTipsComponent : BaseModuleRegressionComponent
    {
        private static readonly By TipsContainerLocator = By.XPath("//div[contains(@class,'Welcome-module__tipsContainer')]");
        private static readonly By HowDeepResearchWorksButtonLocator = By.XPath(".//button[@data-testid='how-ai-works']");
        private static readonly By TipsForBestResultsButtonLocator = By.XPath(".//button[@data-testid='tips-for-best-results']");

        /// <summary>
        /// How Deep Research Works Button
        /// </summary>
        public IButton HowDeepResearchWorksButton => new Button(ComponentLocator, HowDeepResearchWorksButtonLocator);

        /// <summary>
        /// Tips For Best Results Button
        /// </summary>
        public IButton TipsForBestResultsButton => new Button(ComponentLocator, TipsForBestResultsButtonLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TipsContainerLocator;
    }
}



