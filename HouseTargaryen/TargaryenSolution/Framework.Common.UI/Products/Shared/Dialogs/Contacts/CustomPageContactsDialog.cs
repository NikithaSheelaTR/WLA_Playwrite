namespace Framework.Common.UI.Products.Shared.Dialogs.Contacts
{
    using OpenQA.Selenium;

    /// <summary>
    /// Custom Page Contacts dialog
    /// </summary>
    public class CustomPageContactsDialog : BaseContactsManagedDialog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomPageContactsDialog"/> class. 
        /// </summary>
        public CustomPageContactsDialog()
        {
            this.InsertButton = By.Id("co_customPages_InsertContacts");
            this.CancelButton = By.Id("co_CustomPagesContactsCancel");
            this.CloseDialogButton = By.Id("co_CustomPagesContactsLightBox_Close");
        }
    }
}