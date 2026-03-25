namespace Framework.Common.UI.Products.WestLawNextCanada.Dialogs.AssistedResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// "Tips for best results" dialog
    /// </summary>
    public class TipsForBestResultsDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id='AI-tips-modal']");
        private static readonly By TitleLabelLocator = By.XPath(".//h2 | .//h3");
        private static readonly By CloseButonLocator = By.XPath(".//button[@class='co_primaryBtn']");

        /// <summary>
        /// Title label
        /// </summary>
        public ILabel TitleLabel => new Label(ContainerLocator, TitleLabelLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(ContainerLocator, CloseButonLocator);
    }
}