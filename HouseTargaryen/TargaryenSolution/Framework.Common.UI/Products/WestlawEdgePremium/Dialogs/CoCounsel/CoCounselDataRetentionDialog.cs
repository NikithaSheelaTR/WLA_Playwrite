namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.CoCounsel
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    /// <summary>
    /// CoCounsel Data Retention dialog
    /// </summary>
    public class CoCounselDataRetentionDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//saf-dialog[@data-testid='security-modal']");
        private static readonly By ContentLabelLocator = By.XPath(".//*[contains(@class, 'dialogContent')]");
        private static readonly By CloseButonLocator = By.XPath(".//saf-button[@data-testid='security-modal-close']");

        /// <summary>
        /// Content label
        /// </summary>
        public ILabel ContentLabel => new Label(ContainerLocator, ContentLabelLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(ContainerLocator, CloseButonLocator);
    }
}
