namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Dialogs;

    using OpenQA.Selenium;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage;

    /// <summary>
    /// Precision Get Started dialog
    /// </summary>
    public class PrecisionGetStartedDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id='coid_getStarted_selectionsModal']");
        private static readonly By LimitedMessageLocator = By.XPath(".//div[@class='co_infoBox_message']");
        private static readonly By SaveButtonLocator = By.XPath(".//*[@id='coid_getStarted_modalFooter_save']");
        private static readonly By CancelButtonLocator = By.XPath(".//*[@id='coid_getStarted_modalFooter_close']");
        private static readonly By CloseButtonLocator = By.XPath(".//*[@class='co_overlayBox_closeButton co_iconBtn']");

        /// <summary>
        /// Tab panel
        /// </summary>
        public PrecisionGetStartedBrowseTabComponent TabPanel { get; } = new PrecisionGetStartedBrowseTabComponent();

        /// <summary>
        /// Selections panel
        /// </summary>
        public PrecisionGetStartedSelectionsComponent SelectionsPanel { get; } = new PrecisionGetStartedSelectionsComponent();

        /// <summary>
        /// Get Started dialog Save button
        /// </summary>
        public IButton SaveButton => new Button(ContainerLocator, SaveButtonLocator);

        /// <summary>
        /// Get Started dialog Cancel button
        /// </summary>
        public IButton CancelButton => new Button(ContainerLocator, CancelButtonLocator);

        /// <summary>
        /// Get Started dialog limit message label
        /// </summary>
        public ILabel LimitedMessageLabel => new Label(ContainerLocator, LimitedMessageLocator);

        /// <summary>
        /// Get Started dialog Close button
        /// </summary>
        public IButton CloseButton => new Button(ContainerLocator, CloseButtonLocator);
    }
}
