namespace Framework.Common.UI.Products.Shared.Dialogs.Foldering
{
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Delete folder widget
    /// </summary>
    public class DeleteFolderDialog : BaseModuleRegressionDialog
    {
        private static readonly By OkButtonLocator = By.XPath("//div[@id='coid_lightboxOverlay']//button[contains(@class,'co_dropdownBox_ok')]");

        /// <summary>
        /// Constructor Delete Folder Widget and waits for OK button on the Delete folder widget
        /// </summary>
        public DeleteFolderDialog()
        {
            DriverExtensions.WaitForElement(OkButtonLocator);
        }

        /// <summary>
        /// Folder Delete ConfirmissionDialog
        /// </summary>
        /// <returns> Folder Delete Confirmation Dialog </returns>
        public ConfirmationDialog ClickOkButton()
            => this.ClickElement<ConfirmationDialog>(OkButtonLocator);

        /// <summary>
        /// Deletes the folder given. Precondition for this folder to delete is selected
        /// </summary>
        /// <returns>The <see cref="ResearchOrganizerPage"/></returns>
        public ResearchOrganizerPage DeleteFolder() => this.ClickOkButton().ClickOkButton<ResearchOrganizerPage>();

        /// <summary>
        /// Deletes the folder given. Precondition for this folder to delete is selected
        /// </summary>
        /// <returns> Message after deleting </returns>
        public string DeleteFolderAndGetMessage()
        {
            ConfirmationDialog confirmationDialog = this.ClickOkButton();
            string message = confirmationDialog.GetDialogMessage();
            confirmationDialog.ClickOkButton<ResearchOrganizerPage>();
            return message;
        }
    }
}