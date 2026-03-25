namespace Framework.Common.UI.Products.Shared.Dialogs.Delivery
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Ready For Delivery Dialog
    /// </summary>
    public class ReadyForDeliveryDialog : BaseModuleRegressionDialog
    {
        private static readonly By DeliveryWaitMessageTitleLocator = By.Id("co_deliveryWaitMessageTitle");

        private static readonly By DeliveryWaitProgressLocator = By.Id("co_deliveryWaitProgress");

        private static readonly By DownloadButtonLocator = By.XPath("//*[@id='co_deliveryDownloadButton' or @id='coid_deliveryWaitMessage_downloadButton']");

        private static readonly By MinimizeButtonLocator = By.Id("coid_deliveryWaitMessage_minimizeButton");

        /// <summary>
        /// Click on the Download button 
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickDownloadButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(DownloadButtonLocator);

        /// <summary>
        /// Get the delivery message after selecting delivery option 
        /// </summary>
        /// <returns> Delivery message text </returns>
        public string GetDeliveryMessage()
        {
            DriverExtensions.WaitForElementNotDisplayed(DeliveryWaitProgressLocator);
            return DriverExtensions.GetText(DeliveryWaitMessageTitleLocator);
        }

        /// <summary>
        /// Verifies if Download Button is Displayed
        /// </summary>
        /// <returns> True if displayed,false otherwise </returns>
        public bool IsDownloadButtonDisplayed() => DriverExtensions.IsDisplayed(DownloadButtonLocator, 5);

        /// <summary>
        /// Verifies if Ready To Download/Email etc. message displayed inside  Ready To Delivery Dialog
        /// </summary>
        /// <returns>true or false</returns>
        public bool IsReadyToDeliveryMessageDisplayed()
        {
            DriverExtensions.WaitForElementNotDisplayed(DeliveryWaitProgressLocator);
            return DriverExtensions.IsDisplayed(DeliveryWaitMessageTitleLocator);
        }

        /// <summary>
        /// Click minimize button in the 'Ready For Download' dialog
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T MinimizeDeliveryDialog<T>()
            where T : ICreatablePageObject
        {
            this.ClickElement<T>(MinimizeButtonLocator);
            DriverExtensions.WaitForAnimation();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Waits For Email Dialog To Disappear
        /// </summary>
        public void WaitForEmailDialogToDisappear()
        {
            if (DriverExtensions.IsDisplayed(DeliveryWaitMessageTitleLocator))
            {
                DriverExtensions.WaitForElementNotDisplayed(DeliveryWaitMessageTitleLocator);
            }
        }
    }
}