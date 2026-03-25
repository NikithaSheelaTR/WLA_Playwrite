namespace Framework.Common.UI.Products.WestLawNext.Dialogs.BusinessLawCenterPowerSearch
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The print delivery popup.
    /// </summary>
    public class BlcPowerSearchPrintDeliveryDialog : BlcPowerSearchDeliveryDialog
    {
        private static readonly By ButtonPrintLocator =
            By.XPath("//div[contains(@class,'co_overlayBox_container')]//div[@class='co_overlayBox_optionsBottom']//input[@value='Print' and @ng-click='ok()']");

        private static readonly By ClosePrintDialogButtonLocator =
            By.XPath("//div[contains(@class,'co_overlayBox_container')]//div/a[@class='co_overlayBox_closeButton']");

        private static readonly By MessageItemsAreReadyToPrintLocator =
            By.XPath("//div[contains(@class,'co_overlayBox_container')]/div[contains(@class, 'co_deliveryWaitMessageItemTitle ng-binding') and contains(text(), 'Item(s) are ready to print.')]");
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BlcPowerSearchPrintDeliveryDialog"/> class.
        /// </summary>
        public BlcPowerSearchPrintDeliveryDialog()
        {
            DriverExtensions.WaitForElement(ButtonPrintLocator);
        }

        /// <summary>
        /// ClickOnPrint - Select the print button in pop up.
        /// </summary>
        public void ClickOnPrint()
        {
            DriverExtensions.WaitForElement(ButtonPrintLocator).Click();
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Closing popup.
        /// </summary>
        public void ClosePopup() => DriverExtensions.WaitForElement(ClosePrintDialogButtonLocator).Click();

        /// <summary>
        /// The is message "Items are ready to print." displayed
        /// </summary>
        /// <returns>flag whether the message is displayed.</returns>
        public bool IsMessageDislayedItemsAreReadyToPrint()
            => DriverExtensions.WaitForElementDisplayed(MessageItemsAreReadyToPrintLocator).Displayed;
    }
}