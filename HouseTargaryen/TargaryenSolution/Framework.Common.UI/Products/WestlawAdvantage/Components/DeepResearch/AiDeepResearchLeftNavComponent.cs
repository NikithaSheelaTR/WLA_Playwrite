namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// AI Deep Research left nav component
    /// </summary>
    public class AiDeepResearchLeftNavComponent : BaseModuleRegressionComponent
    {
        private static readonly By LeftNavContainerLocator = By.XPath("//div[@data-testid='left-nav-container']");
        private static readonly By HistoryButtonLocator = By.XPath(".//button[contains(@class, 'historyButton')]");
        private static readonly By HistoryConversationListLocator = By.XPath("//ul[contains(@class, 'Panel-module__historyList')]/li");
        private static readonly By HistoryConversationTimeLocator = By.XPath(".//saf-metadata-v3/saf-metadata-item-v3/time");
        private static readonly By HistoryProgressBarLocator = By.XPath("//saf-progress-ring-v3[@role='progressbar']");
        private static readonly By FullHistoryLinkLocator = By.XPath("//saf-anchor-v3[contains(@class, 'openHistoryLink')]");

        /// <summary>
        /// History Button
        /// </summary>
        public IButton HistoryButton => new Button(ComponentLocator, HistoryButtonLocator);

        /// <summary>
        /// History first conversation list item 
        /// </summary>
        public IButton HistoryFirstConversation => new Button(ComponentLocator, HistoryConversationListLocator);

        /// <summary>
        /// History first conversation list item time label
        /// </summary>
        public ILabel HistoryFirstConversationTime => new Label(ComponentLocator, HistoryConversationListLocator, HistoryConversationTimeLocator);

        /// <summary>
        /// History progress bar for existing conversation 
        /// </summary>
        public ILabel HistoryProgressBar => new Label(ComponentLocator, HistoryProgressBarLocator);

        /// <summary>
        /// Full history link 
        /// </summary>
        public IButton FullHistoryLink => new Button(ComponentLocator, FullHistoryLinkLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => LeftNavContainerLocator;
    }
}


