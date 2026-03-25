namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Document
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    using OpenQA.Selenium;

    /// <summary>
    /// Ignore document dialog
    /// </summary>
    public class IgnoreDocumentDialog : BaseModuleRegressionDialog
    {
        private static readonly By OkButtonLocator = By.Id("coid_document_widget_captiolWatchIgnore_lightbox_okButton");

        /// <summary>
        /// Ok button
        /// </summary>
        public IButton OkButton => new Button(OkButtonLocator);
    }
}