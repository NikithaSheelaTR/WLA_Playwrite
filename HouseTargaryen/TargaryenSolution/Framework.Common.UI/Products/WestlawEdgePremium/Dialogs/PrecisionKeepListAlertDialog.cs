namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;

    /// <summary>
    /// This class models the keep list dialog.
    /// </summary>
    public class PrecisionKeepListAlertDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("(//*[contains(@class, 'KeepList-infobox-success') or contains(@class, 'KeepList-infobox-error')])[last()]");
        private static readonly By InfoBoxMessageLocator = By.ClassName("co_infoBox_message");
        private static readonly By ViewKeepListButtonLocator = By.XPath(".//button[text()='View Keep List']");

        /// <summary>
        /// Precision Keep List Message alert
        /// </summary>
        public ILabel MessageLabel => new Label(this.ComponentLocator, InfoBoxMessageLocator);

        /// <summary>
        /// AthePrecisionns View Keep List button
        /// </summary>
        public IButton ViewKeepListButton => new Button(this.ComponentLocator, ViewKeepListButtonLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected By ComponentLocator => ContainerLocator;
    }
}