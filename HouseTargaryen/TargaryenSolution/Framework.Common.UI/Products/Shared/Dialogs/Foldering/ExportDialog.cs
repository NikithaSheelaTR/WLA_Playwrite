namespace Framework.Common.UI.Products.Shared.Dialogs.Foldering
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// This class has actions for the actual export functionality once the step 1 is complete for selecting items
    /// </summary>
    public class ExportDialog : BaseModuleRegressionDialog
    {
        private static readonly By ExportButtonLocator = By.XPath("//input[@name='Export']");

        private static readonly By ExportMessageLocator =
            By.XPath("//div[@id='co_deliveryWaitMessageTitle' and text() ='The items are ready to download.']");

        private static readonly By ReadyForDownloadCloseButtonLocator =
            By.XPath(
                "//div[@class='co_overlayBox_container co_deliveryWaitMessage']//*[contains(@class, 'co_overlayBox_minimizeButton')]");

        /// <summary>
        /// Clicks on the export button on the export widget
        /// </summary>
        public void ClickExportButton() => this.ClickElement(ExportButtonLocator);

        /// <summary>
        /// Verifies is export successful
        /// </summary>
        /// <returns>
        /// True if Ready For Download message appeared
        /// </returns>
        public bool IsReadyToExport()
        {
            bool isDeliveryReady = DriverExtensions.IsDisplayed(ExportMessageLocator, 60);
            isDeliveryReady &= DriverExtensions.IsDisplayed(ReadyForDownloadCloseButtonLocator, 5);
            return isDeliveryReady;
        }
    }
}