namespace Framework.Common.UI.Products.Shared.Dialogs.Contacts
{
    using OpenQA.Selenium;

    /// <summary>
    /// Contacts Groups dialog
    /// </summary>
    public class ContactsGroupsDialog : BaseContactsGroupsDialog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactsGroupsDialog"/> class. 
        /// </summary>
        public ContactsGroupsDialog()
        {
            this.SaveGroupButton = By.Id("coid_contacts_newGroup_saveButton");
            this.CancelButton = By.Id("coid_contacts_newGroup_cancelLink");
            this.CloseDialogButton = By.XPath("//a[@class='co_overlayBox_closeButton' and text()='Close']");
        }
    }
}