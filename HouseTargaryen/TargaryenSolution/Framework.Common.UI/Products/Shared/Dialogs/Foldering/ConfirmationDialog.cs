namespace Framework.Common.UI.Products.Shared.Dialogs.Foldering
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Describes Folder Delete Confirmation Dialog
    /// </summary>
    public class ConfirmationDialog : BaseModuleRegressionDialog
    {
        private static readonly By CancelButtonLocator =
            By.XPath("//div[@class='co_overlayBox_optionsBottom']//a[text() = 'Cancel']");

        private static readonly By OkButtonLocator =
            By.XPath("//div[@class='co_overlayBox_optionsBottom']//input[@value='OK' or @value='Ok' or @value = 'Confirm']");

        private static readonly By DialogMessageLocator = By.ClassName("co_overlayBox_content");
        
        /// <summary>
        /// Click Ok Button
        /// </summary>
        /// <typeparam name="T">Page instance</typeparam>
        /// <returns> Research Organizer Page </returns>
        public T ClickOkButton<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(OkButtonLocator);

        /// <summary>
        /// Click Ok Button
        /// </summary>
        /// <typeparam name="T">Page instance</typeparam>
        /// <returns> Research Organizer Page </returns>
        public T ClickCancelButton<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(CancelButtonLocator);

        /// <summary>
        /// Get Dialog Message
        /// </summary>
        /// <returns> Dialog message </returns>
        public string GetDialogMessage()
            =>
                this.IsDialogMessageDisplayed()
                    ? DriverExtensions.GetElement(DialogMessageLocator).Text
                    : string.Empty;

        /// <summary>
        /// Is Dialog Message Displayed
        /// </summary>
        /// <returns> true if displayed </returns>
        public bool IsDialogMessageDisplayed() => DriverExtensions.IsDisplayed(DialogMessageLocator);
    }
}