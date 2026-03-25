namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs.Delivery;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Litigation Analytics Ready For Delivery Dialog
    /// </summary>
    public class LitigationAnalyticsReadyForDeliveryDialog : ReadyForDeliveryDialog
    {
        private static By DownloadButtonLocator = By.Id("coid_deliveryWaitMessage_downloadButton");

        /// <summary>
        /// Download Button
        /// </summary>
        public IButton DownloadButton => new Button(DriverExtensions.WaitForElementDisplayed(DownloadButtonLocator, 50000));
    }
}