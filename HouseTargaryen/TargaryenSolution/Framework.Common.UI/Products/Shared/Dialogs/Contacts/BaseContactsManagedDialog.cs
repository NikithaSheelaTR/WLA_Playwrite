namespace Framework.Common.UI.Products.Shared.Dialogs.Contacts
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Contacts;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Common class for all contacts dialogs
    /// </summary>
    public abstract class BaseContactsManagedDialog : MyContactsDialog
    {
        /// <summary>
        /// Cancel Button
        /// </summary>
        protected By CancelButton;

        /// <summary>
        /// Insert Button
        /// </summary>
        protected By InsertButton;

        /// <summary>
        /// AddedContacts
        /// </summary>
        public AddedContactsComponent AddedContacts { get; } = new AddedContactsComponent();

        /// <summary>
        /// People
        /// </summary>
        public PeopleComponent People { get; } = new PeopleComponent();

        /// <summary>
        /// Click 'Cancel' button
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>New instance of the page</returns>
        public T ClickCancelButton<T>() where T : ICreatablePageObject
        {
            return this.ClickElement<T>(this.CancelButton);
        }

        /// <summary>
        /// Click 'Insert' button
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>New instance of the page</returns>
        public T ClickInsertButton<T>() where T : ICreatablePageObject
        {
            return this.ClickElement<T>(this.InsertButton);
        }

        /// <summary>
        /// Verify 'Insert' button is displayed
        /// </summary>
        /// <returns>True if insert button is displayed, false otherwise</returns>
        public bool IsInsertButtonDisplayed()
        {
            return DriverExtensions.IsDisplayed(this.InsertButton);
        }
    }
}