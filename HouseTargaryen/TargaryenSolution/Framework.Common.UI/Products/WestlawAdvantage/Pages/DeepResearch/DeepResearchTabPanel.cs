namespace Framework.Common.UI.Products.WestlawAdvantage.Pages.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch;
    using OpenQA.Selenium;

    /// <summary>
    /// Deep Research Tab Panel
    /// </summary>
    public class DeepResearchTabPanel : BaseBrowseTabPanelComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id='panel1']");
        private static readonly By ErrorMessageLocator = By.Id("error-container");

        /// <summary>
        /// Tab Name
        /// </summary>
        protected override string TabName => "Deep AI Research";

        /// <summary>
        /// Welcome Component
        /// </summary>
        public AiDeepResearchWelcomeComponent WelcomeComponent { get; } = new AiDeepResearchWelcomeComponent();

        /// <summary>
        /// Error Subscribtion Message
        /// </summary>
        public ILabel ErrorSubscribtionMessage => new Label(ErrorMessageLocator);
        /// <summary>
        /// Component Locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
