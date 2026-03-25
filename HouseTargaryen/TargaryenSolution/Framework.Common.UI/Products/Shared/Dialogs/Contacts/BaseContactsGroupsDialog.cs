namespace Framework.Common.UI.Products.Shared.Dialogs.Contacts
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Contacts;

    using OpenQA.Selenium;

    /// <summary>
    /// Common class for 'contacts groups' dialogs
    /// </summary>
    public abstract class BaseContactsGroupsDialog : BaseModuleRegressionDialog
    {
        /// <summary>
        /// AddedContacts
        /// </summary>
        public NewGroupComponent NewGroup { get; } = new NewGroupComponent();

        /// <summary>
        /// People
        /// </summary>
        public PeopleComponent People { get; } = new PeopleComponent();

        /// <summary>
        /// Cancel Button
        /// </summary>
        protected By CancelButton { get; set; }

        /// <summary>
        /// Cancel Button
        /// </summary>
        protected By CloseDialogButton { get; set; }

        /// <summary>
        /// Insert Button
        /// </summary>
        protected By SaveGroupButton { get; set; }

        /// <summary>
        /// Click Cancel button
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>New instance of the page</returns>
        public T ClickCancelButton<T>() where T : ICreatablePageObject
        {
            return this.ClickElement<T>(this.CancelButton);
        }

        /// <summary>
        /// Click Close button
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>New instance of the page</returns>
        public T ClickCloseDialogButton<T>() where T : ICreatablePageObject
        {
            return this.ClickElement<T>(this.CloseDialogButton);
        }

        /// <summary>
        /// Click Insert/ Save group button
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>New instance of the page</returns>
        public T ClickSaveGroupButton<T>() where T : ICreatablePageObject
        {
            return this.ClickElement<T>(this.SaveGroupButton);
        }
    }
}