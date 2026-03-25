namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using OpenQA.Selenium;

    /// <summary>
    /// "How AI-Assisted Research works" dialog
    /// </summary>
    public class HowAiAssistedResearchWorksDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id='AI-works-modal']");
        private static readonly By CloseButonLocator = By.XPath(".//button[@class='co_primaryBtn']");
        private static readonly By DescriptionLabelLocator = By.XPath(".//*[@class='co_overlayBox_content']");
        private static readonly By TitleLabelLocator = By.XPath(".//h2 | .//h3");
        private static readonly By ReviewAiCourtRulesLinkLocator = By.XPath(".//a[contains(@href, 'AICourtRules')]");

        /// <summary>
        /// Title label
        /// </summary>
        public ILabel TitleLabel => new Label(ContainerLocator, TitleLabelLocator);

        /// <summary>
        /// Description label
        /// </summary>
        public ILabel DescriptionLabel => new Label(ContainerLocator, DescriptionLabelLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(ContainerLocator, CloseButonLocator);

        /// <summary>
        /// Review Ai Court rules link
        /// </summary>
        public ILink ReviewAiCourtRulesLink => new Link(ContainerLocator, ReviewAiCourtRulesLinkLocator);
    }
}
