namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.AALP.ComplaintAnalyzer
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Complaint Analyzer Delivery Dialog
    /// </summary>
    public class ComplaintAnalyzerDeliveryDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//saf-dialog-v3[@data-testid='delivery-dialog']");
        private static readonly By CancelDownloadLocator = By.XPath("//saf-button-v3[@data-testid='cancel-delivery-dialog']");

        /// <summary>
        /// Cancel download button
        /// </summary>
        public IButton CancelDownloadButton => new Button(ContainerLocator, CancelDownloadLocator);
    }
}
