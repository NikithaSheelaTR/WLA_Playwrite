namespace Framework.Common.UI.Products.WestlawAdvantage.Dialogs.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// How Deep Research works dialog
    /// </summary>
    public class HowDeepResearchWorksDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//*[@data-testid='how-ai-works-dialog']");
        private static readonly By CloseButonLocator = By.XPath(".//saf-button-v3[@data-testid='how-ai-works-close']");
        private static readonly By TipsContentLabelLocator = By.XPath(".//*[contains(@class,'Tips-module__tipsContent')]");

        /// <summary>
        /// Tips Content label
        /// </summary>
        public ILabel TipsContent => new Label(ContainerLocator, TipsContentLabelLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(ContainerLocator, CloseButonLocator);
    }
}

