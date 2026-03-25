namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// AI Deep Research Welcome component
    /// </summary>
    public class AiDeepResearchWelcomeComponent : BaseModuleRegressionComponent
    {
        private static readonly By WelcomeContainerLocator = By.XPath("//div[contains(@class,'Welcome-module__welcomeContainer')]");
        private static readonly By WelcomeHeaderLabelLocator = By.XPath(".//h2[contains(@class,'WelcomeHeading-module__welcomeHeader')]");
        private static readonly By WelcomeSubHeaderLabelLocator = By.XPath(".//p[contains(@class,'WelcomeHeading-module__welcomeSubHeader')]");
        private static readonly By InputValidationErrorLabelLocator = By.XPath(".//span[contains(@class,'InputValidationError')]");

        /// <summary>
        /// Deep Research Input Component
        /// </summary>
        public AiDeepResearchInputComponent InputComponent { get; } = new AiDeepResearchInputComponent();

        /// <summary>
        /// Deep Research Input Footer Component
        /// </summary>
        public AiDeepResearchInputFooterComponent InputFooterComponent { get; } = new AiDeepResearchInputFooterComponent();

        /// <summary>
        /// Deep Research Tips Component
        /// </summary>
        public AiDeepResearchTipsComponent TipsComponent { get; } = new AiDeepResearchTipsComponent();

        /// <summary>
        /// Welcome header label
        /// </summary>
        public ILabel WelcomeHeaderLabel => new Label(WelcomeHeaderLabelLocator);

        /// <summary>
        /// Welcome Sub Header Label
        /// </summary>
        public ILabel WelcomeSubHeaderLabel => new Label(WelcomeSubHeaderLabelLocator);

        /// <summary>
        /// Input Validation Error Label
        /// </summary>
        public ILabel InputValidationErrorLabel => new Label(InputValidationErrorLabelLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => WelcomeContainerLocator;
    }
}