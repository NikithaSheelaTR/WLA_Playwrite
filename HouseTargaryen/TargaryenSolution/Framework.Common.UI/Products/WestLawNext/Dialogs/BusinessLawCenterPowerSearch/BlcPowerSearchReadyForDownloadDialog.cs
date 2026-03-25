namespace Framework.Common.UI.Products.WestLawNext.Dialogs.BusinessLawCenterPowerSearch
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The ready for download popup.
    /// </summary>
    public class BlcPowerSearchReadyForDownloadDialog : BaseModuleRegressionDialog
    {
        private static readonly By CloseButtonLocator = By.XPath("//a[@class='co_overlayBox_closeButton']");

        private static readonly By DownloadButtonLocator = By.XPath("//div[@ng-show='downloadReady']//input[@value='Download' and @role='button']");

        private static readonly By MessageItemsReadyToDownloadLocator =
            By.XPath("//div[contains(@class,'co_overlayBox_container')]/div[contains(@class, 'co_deliveryWaitMessageItemTitle ng-binding') and contains(text(), 'Item(s) are ready to download.')]");
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BlcPowerSearchReadyForDownloadDialog"/> class.
        /// </summary>
        public BlcPowerSearchReadyForDownloadDialog()
        {
            DriverExtensions.WaitForElementDisplayed(DownloadButtonLocator, 60000);
        }

        /// <summary>
        /// The close popup.
        /// </summary>
        public void ClosePopup()
        {
            DriverExtensions.WaitForElement(CloseButtonLocator).Click();
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForElementNotDisplayed(CloseButtonLocator);
        }

        /// <summary>
        /// The is download button present.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsMessageDislayedItemsReadyToDownload() => DriverExtensions.IsDisplayed(MessageItemsReadyToDownloadLocator, 5);
    }
}