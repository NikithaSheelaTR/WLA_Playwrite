namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.AALP
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
        private static readonly By CloseButonLocator = By.XPath(".//button[@class='co_primaryBtn']");
        private static readonly By DescriptionLabelLocator = By.XPath(".//*[@class='co_overlayBox_content']");
        private static readonly By TitleLabelLocator = By.XPath(".//h2 | .//h3");

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
    }
}
