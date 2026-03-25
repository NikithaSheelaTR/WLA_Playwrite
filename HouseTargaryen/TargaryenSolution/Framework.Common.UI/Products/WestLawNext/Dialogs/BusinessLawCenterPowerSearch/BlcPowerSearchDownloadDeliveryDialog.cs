namespace Framework.Common.UI.Products.WestLawNext.Dialogs.BusinessLawCenterPowerSearch
{
    using Framework.Common.UI.Products.WestLawNext.Utils.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The download delivery popup.
    /// </summary>
    public class BlcPowerSearchDownloadDeliveryDialog : BlcPowerSearchDeliveryDialog
    {
        private static readonly By BasicsDeliveryOptionAsSingleMergedFileLocator =
            By.XPath("//div[contains(@class,'co_overlayBox_container')]//div[@class='co_deliveryContentRight']//option[text()='A Single Merged File']");

        private static readonly By DownloadButtonLocator =
            By.XPath("//div[@ng-hide='isProcessing || showConfirmationMessage']//input[@value='Download' and @role='button']");

        private static readonly By SelectorFormatPdfLocator =
            By.XPath("//div[contains(@class,'co_overlayBox_container')]//div[@class='co_deliveryContentRight']//option[text()='PDF']");

        /// <summary>
        /// Initializes a new instance of the <see cref="BlcPowerSearchDownloadDeliveryDialog"/> class.
        /// </summary>
        public BlcPowerSearchDownloadDeliveryDialog()
        {
            DriverExtensions.WaitForElement(DownloadButtonLocator);
        }

        /// <summary>
        /// ClickOnDownload - Select the download button in pop up.
        /// </summary>
        /// <returns>The <see cref="BlcPowerSearchReadyForDownloadDialog"/>.</returns>
        public BlcPowerSearchReadyForDownloadDialog ClickOnDownload()
        {
            IWebElement downloadBtn = DriverExtensions.WaitForElement(DownloadButtonLocator);
            ActionExtensions.DoUntilConditionWillBecomeTrue(
                () => DriverExtensions.Click(downloadBtn),
                () => !DriverExtensions.IsDisplayed(DownloadButtonLocator), 5);
            DriverExtensions.WaitForElementNotDisplayed(DownloadSpinnerLocator);
            return new BlcPowerSearchReadyForDownloadDialog();
        }

        /// <summary>
        /// SetDocumentDownloadTypeToPDF  - Set download document type to word.
        /// </summary>
        public void SetDocumentDownloadFormatPdf() => DriverExtensions.WaitForElement(SelectorFormatPdfLocator).Click();

        /// <summary>
        /// The set download type to multiple.
        /// </summary>
        public void SetDownloadTypeToSingleMerged() => DriverExtensions.WaitForElement(BasicsDeliveryOptionAsSingleMergedFileLocator).Click();
    }
}