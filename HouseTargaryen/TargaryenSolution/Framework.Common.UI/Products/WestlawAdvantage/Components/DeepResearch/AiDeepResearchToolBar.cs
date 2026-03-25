namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// Deep Research Toolbar
    /// </summary>
    public class AiDeepResearchToolBar : BaseModuleRegressionComponent
    {
        private static readonly By ToolbarContainerLocator = By.XPath("//div[contains(@class, 'toolbarContainer')]");
        private static readonly By SaveToFolderButtonLocator = By.XPath(".//saf-button-v3[contains(@class, 'FolderingButton')]");
        private static readonly By CopyLinkButtonLocator = By.XPath(".//saf-button-v3[contains(@class, 'CopyLink')]");
        private static readonly By CopiedLinkSuccessLabelLocator = By.XPath(".//div[contains(@class,'StatusMessageDialogBox-module__statusDialogText')]");
        private static readonly By DownloadReportButtonLocator = By.XPath(".//saf-button-v3[@data-testid='delivery-button']");
        private static readonly By SuccessfulStatusMessageLocator = By.XPath(".//saf-icon-v3[contains(@aria-label, 'Success')]");

        /// <summary>
        /// Save to folder button
        /// </summary>
        public IButton SaveToFolderButton => new Button(ComponentLocator, SaveToFolderButtonLocator);

        /// <summary>
        /// Copy link button
        /// </summary>
        public IButton CopyLinkButton => new Button(ComponentLocator, CopyLinkButtonLocator);

        /// <summary>
        /// Copied link success label
        /// </summary>
        public ILabel CopiedLinkSuccessLabel => new Label(ComponentLocator, CopiedLinkSuccessLabelLocator);

        /// <summary>
        /// Download report button
        /// </summary>
        public IButton DownloadReportButton => new Button(ComponentLocator, DownloadReportButtonLocator);

        /// <summary>
        /// Successful Status Message
        /// </summary>
        public ILabel SuccessfulStatusMessage => new Label(ComponentLocator, SuccessfulStatusMessageLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ToolbarContainerLocator;
    }
}
