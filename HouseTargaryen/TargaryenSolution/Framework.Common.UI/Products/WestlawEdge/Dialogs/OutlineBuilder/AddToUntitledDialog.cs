namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.OutlineBuilder
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;   
    using OpenQA.Selenium;

    /// <summary>
    /// Add To Untitled Dialog
    /// </summary>
    public class AddToUntitledDialog : BaseModuleRegressionDialog
    {
        private static readonly By OutlineBuilderContainerLocator = By.XPath("//div[@id='coid_OutlineBuilderModalLightbox']");
        private static readonly By DialogTitleLocator = By.XPath(".//h2[contains(@id,'coid_lightboxAriaLabel')]");
        private static readonly By SaveOutlineButtonLocator = By.CssSelector("button.co_primaryBtn.OutlineBuilderModalSave");
        private static readonly By OutlineCancelButtonLocator = By.CssSelector("button.OutlineBuilderModalCancel");


        /// <summary>
        /// Browse Component
        /// </summary>
        public AddToOutlineTabPanel AddToOutlinePanelComponent { get; } = new AddToOutlineTabPanel();

        /// <summary>
        /// Save Outline button
        /// </summary>
        public IButton SaveOutlineButton => new Button(this.ComponentLocator, SaveOutlineButtonLocator);

        /// <summary>
        /// Cancel Outline modal button
        /// </summary>
        public IButton OutlineModalCancelButton => new Button(this.ComponentLocator, OutlineCancelButtonLocator);


        /// <summary>
        /// Create New Outline modal title
        /// </summary>
        public ILabel DialogModalTitle => new Label(this.ComponentLocator, DialogTitleLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected By ComponentLocator => OutlineBuilderContainerLocator;
    }
}
