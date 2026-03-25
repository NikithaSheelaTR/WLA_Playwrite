namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawAdvantage.DropDowns.DeepResearch;
    using OpenQA.Selenium;

    /// <summary>
    /// AI Deep Research Input footer component
    /// </summary>
    public class AiDeepResearchInputFooterComponent : BaseModuleRegressionComponent
    {
        private static readonly By InputFooterContainerLocator = By.XPath("//div[contains(@class,'Input-module__inputFooterContainer')]");
        private static readonly By EmailMeButtonLocator = By.XPath(".//saf-switch-v3[contains(@class,'EmailMe-module__emailMeSwitch')]");
        private static readonly By EmailMeCheckedLabelLocator = By.XPath(".//*[contains(@class,'EmailMe-module__emailMeSwitch') and @current-checked='true']");
        private static readonly By HowDeepResearchWorksButtonLocator = By.XPath(".//button[@data-testid='how-ai-works']");
        private static readonly By TipsForBestResultsButtonLocator = By.XPath(".//button[@data-testid='tips-for-best-results']");

        /// <summary>
        /// Recent questions dropdown
        /// </summary>
        public RecentQuestionsDropdown RecentQuestionsDropdown => new RecentQuestionsDropdown();

        /// <summary>
        /// Email Me Button
        /// </summary>
        public IButton EmailMeButton => new Button(ComponentLocator, EmailMeButtonLocator);

        /// <summary>
        /// Email Me Checked Label
        /// </summary>
        public ILabel EmailMeCheckedLabel => new Label(ComponentLocator, EmailMeCheckedLabelLocator);

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
        protected override By ComponentLocator => InputFooterContainerLocator;
    }
}



