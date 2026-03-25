namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;

    /// <summary>
    /// This class models the keep list confirmation remove dialog.
    /// </summary>
    public class PrecisionKeepListConfirmationRemoveDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.Id("KeepList-ConfirmationRemoveLightbox");
        private static readonly By ContentLabelLocator = By.ClassName("co_overlayBox_content");
        private static readonly By HeaderLabelLocator = By.XPath(".//*[@class='co_overlayBox_headline']//h2 | .//*[@class ='co_overlayBox_headline']/div/h3");
        private static readonly By ConfirmButtonLocator = By.XPath(".//button[contains(text(),'Confirm')]");
        private static readonly By CancelButtonLocator = By.XPath(".//button[contains(text(),'Cancel')]");
        private static readonly By CloseButtonLocator = By.XPath(".//button[contains(text(),'Close')]");

        /// <summary>
        /// Confirm button
        /// </summary>
        public IButton ConfirmButton => new Button(this.ComponentLocator, ConfirmButtonLocator);

        /// <summary>
        /// Cancel button
        /// </summary>
        public IButton CancelButton => new Button(this.ComponentLocator, CancelButtonLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(this.ComponentLocator, CloseButtonLocator);

        /// <summary>
        /// Header label 
        /// </summary>
        public ILabel HeaderLabel => new Label(this.ComponentLocator, HeaderLabelLocator);

        /// <summary>
        /// Content label 
        /// </summary>
        public ILabel ContentLabel => new Label(this.ComponentLocator, ContentLabelLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected By ComponentLocator => ContainerLocator;
    }
}