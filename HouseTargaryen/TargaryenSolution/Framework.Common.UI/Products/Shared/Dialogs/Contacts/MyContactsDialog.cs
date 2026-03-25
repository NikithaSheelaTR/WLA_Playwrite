namespace Framework.Common.UI.Products.Shared.Dialogs.Contacts
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Contacts;

    using OpenQA.Selenium;

    /// <summary>
    /// Describe 'My Contacts' dialog
    /// </summary>
    public class MyContactsDialog : BaseModuleRegressionDialog
    {
        /// <summary>
        /// Close Dialog Button
        /// </summary>
        protected By CloseDialogButton;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyContactsDialog"/> class. 
        /// </summary>
        public MyContactsDialog()
        {
            this.Group = new GroupComponent();
            this.CloseDialogButton = By.Id("coid_contacts_closeImage");
        }

        /// <summary>
        /// Group
        /// </summary>
        public GroupComponent Group { get; }

        /// <summary>
        /// Click 'Close' button
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>New instance of the page</returns>
        public T ClickCloseDialogButton<T>() where T : ICreatablePageObject
        {
            return this.ClickElement<T>(this.CloseDialogButton);
        }
    }
}