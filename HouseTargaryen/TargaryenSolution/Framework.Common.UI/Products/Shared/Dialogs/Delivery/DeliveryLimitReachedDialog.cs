namespace Framework.Common.UI.Products.Shared.Dialogs.Delivery
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Delivery Limit Reached Dialog
    /// </summary>
     public class DeliveryLimitReachedDialog : BaseModuleRegressionDialog
    {
        private const string DocumentCheckBoxLctMask = "//input[@id='co_outofplan_{0}']";

        private static readonly By DeliveryLimitReachedMessageLocator = By.ClassName("co_overlayBox_content");

        private static readonly By DeliveryWaitProgressLocator = By.Id("co_deliveryWaitProgress");

        private static readonly By DownloadButtonLocator = By.Id("coid_deliverySessionLimitReached_selectItemsDelivery_btn");

        private static readonly By SelectAllCheckBoxLocator = By.Id("co_deselectAllItems_checkbox");

        private static readonly By SelectItemsButtonLocator = By.Id("coid_deliverySessionLimitReached_selectItems_btn");

        private static readonly By CancelButtonLocator = By.Id("coid_deliveryOutOfPlan_cancelLink");

        /// <summary>
        /// Click Download button 
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickDownloadButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(DownloadButtonLocator);

        /// <summary>
        /// Click Cancel button 
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickCancelButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(CancelButtonLocator);

        /// <summary>
        /// Click Select items button 
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickSelectItemsButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(SelectItemsButtonLocator);

        /// <summary>
        /// Get the delivery message after selecting delivery option 
        /// </summary>
        /// <returns> Delivery message text </returns>
        public string GetDeliveryLimitReachedMessage()
        {
            DriverExtensions.WaitForElementNotDisplayed(DeliveryWaitProgressLocator);
            return DriverExtensions.GetText(DeliveryLimitReachedMessageLocator);
        }

        /// <summary>
        ///  Download button enabled
        /// </summary>
        /// <returns> true if enabled otherwise false </returns>
        public bool IsDownloadButtonEnabled() => DriverExtensions.IsEnabled(DownloadButtonLocator);

        /// <summary>
        /// Sets Select all checkbox
        /// </summary>
        /// <param name="state"> The state. </param>
        public void SetSelectAllCheckbox(bool state = true) => DriverExtensions.SetCheckbox(state, SelectAllCheckBoxLocator);

        /// <summary>
        /// Sets checkbox by index
        /// </summary>
        /// <param name="index"> The index. </param>
        /// <param name="state"> The state. </param>
        public void SetCheckBoxByIndex(int index, bool state = true) =>
            DriverExtensions.GetElement(By.XPath(string.Format(DocumentCheckBoxLctMask, index))).SetCheckbox(state);
    }
}
