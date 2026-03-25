namespace Framework.Common.UI.Products.Shared.Dialogs.Document.Notes
{
    using System;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs.Contacts;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Models.Annotations;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// This class contains elements and methods pertaining to a Note page
    /// </summary>
    public class EditNoteDialog : BaseModuleRegressionDialog
    {
        private static readonly By AddContactsButtonLocator =
            By.XPath("//span[@id='addBtn']");

        private static readonly By AddedContactsInputLocator =
            By.XPath("//*[@id='coid_contacts_addedContactsInput' and contains(@class,'addedContactsInputActive')]");

        private static readonly By CancelButtonLocator =
            By.XPath("//div[contains(@style,'visible')]//a[@class='co_note_cancelbutton']");

        private static readonly By ContactsLinkLocator = By.CssSelector("#contactsId");

        private static readonly By DeleteButtonLocator =
            By.XPath("//div[contains(@style,'visible')]//*[contains(@class, 'co_noteDelete')]");

        private static readonly By NoteInputLocator =
            By.XPath("//div[contains(@style,'visible')]//div[@class='co_noteBody']/textarea");

        private static readonly By PreviouslySharedLinkLocator =
            By.XPath(
                "//div[contains(@class,'contactsActive')]//*[@id='previousId' and text()='previously shared' ][not(contains(@class,'disable'))]");

        private static readonly By RemoveAllButtonLocator =
            By.XPath("//div[contains(@class,'contactsActive')]//span[@id='deleteBtn']");

        private static readonly By RemoveByContactLinkLocator =
            By.XPath("//ul[@id='coid_contacts_addedContactsInput']//a");

        private static readonly By SaveButtonLocator =
            By.XPath("//div[contains(@style,'visible')]//div[@class='co_noteFooter']//input[@value='Save' or 'Enregistre']");

        private static readonly By SharedWithLinkLocator =
            By.XPath("//div[contains(@class,'contactsActive')]//span[@class='sharedWithText']");

        private static readonly By CloseLinkLocator = By.CssSelector(".co_notes_closeLink, .co_overlayBox_closeButton.co_iconBtn");

        private static readonly By NotesUndoLinkLocator = By.ClassName("co_notes_undoLink");

        private static readonly By NoteColorBoxContainerLocator =
            By.XPath("//div[@class='co_colorOptionsContainer']");

        private EnumPropertyMapper<HighlightColor, WebElementInfo> highlightColorPickerMap;

        /// <summary>
        /// Gets the Flag enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<HighlightColor, WebElementInfo> HighlightColorPickerMap =>
            this.highlightColorPickerMap = this.highlightColorPickerMap
                                     ?? EnumPropertyModelCache.GetMap<HighlightColor, WebElementInfo>(
                                         "Picker");

        /// <summary>
        /// Add contacts and click 'Save' button.
        /// </summary>
        /// <param name="contacts"> Contacts to add </param>
        /// <param name="groups"> Groups to add </param>
        /// <typeparam name="T">T</typeparam>
        /// <returns>The new instance of T page</returns>
        public T AddContactsToNote<T>(string[] contacts, string[] groups) where T : ICreatablePageObject
        {
            ContactsDialog sharedNotesContacts = this.ClickContactsLink();
            sharedNotesContacts.People.SelectContactsByContactName(contacts);
            sharedNotesContacts.Group.SelectGroupsByGroupName(groups);
            sharedNotesContacts.ClickInsertButton<EditNoteDialog>();
            return this.ClickSave<T>();
        }

        /// <summary>
        /// Enter text in text area and click 'Save'
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="note">Text to set in the note text box</param>
        /// <returns>The new instance of T page</returns>
        public T AddTextToNote<T>(string note) where T : ICreatablePageObject
        {
            this.TypeNote(note);
            DriverExtensions.WaitForJavaScript();
            return this.ClickElement<T>(SaveButtonLocator);
        }

        /// <summary>
        /// Select color and click 'Save'
        /// </summary>
         /// <typeparam name="T">T</typeparam>
        /// <param name="color">color</param>
        /// <returns> Document page </returns>
        public T SelectColor<T>(HighlightColor color) where T : ICreatablePageObject
        {

          DriverExtensions.WaitForElement(
                DriverExtensions.GetElement(NoteColorBoxContainerLocator),
                By.XPath(this.HighlightColorPickerMap[color].LocatorString)).Click();
       
            return this.ClickElement<T>(SaveButtonLocator);
        }

        /// <summary>
        /// Enter text in text area and click 'Save'
        /// </summary>
        /// <param name="noteText"> Text to set in the note text box </param>
        /// <param name="authorName"> The author Name. </param>
        /// <returns> NoteItemModel </returns>
        public NoteItemModel AddTextToNote(string noteText, string authorName = null)
        {
            this.TypeNote(noteText);
            DriverExtensions.WaitForJavaScript();
            this.ClickElement(SaveButtonLocator);

            return new NoteItemModel() { FirstName = authorName, NoteText = noteText };
        }

        /// <summary>
        /// Verifies close link is displayed
        /// </summary>
        /// <returns>True if 'Close' link is displayed, false otherwise.</returns>
        public bool IsCloseLinkDisplayed() => DriverExtensions.IsDisplayed(CloseLinkLocator, 5);

        /// <summary>
        /// Verify undo message appeared.
        /// </summary>
        /// <returns>True if undo message is displayed, false otherwise</returns>
        public bool IsUndoLinkDisplayed() => DriverExtensions.IsDisplayed(NotesUndoLinkLocator);

        /// <summary>
        /// Click 'Undo' link.
        /// </summary>
        public void ClickUndoLink() => this.ClickElement(NotesUndoLinkLocator);

        /// <summary>
        /// Clicks close link.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>The new instance of T page</returns>
        public T ClickCloseLink<T>() where T : ICreatablePageObject => this.ClickElement<T>(CloseLinkLocator);

        /// <summary>
        /// Click  'Add Contacts (+)'.  
        /// </summary>
        /// <returns> The <see cref="ContactsDialog"/>. </returns>
        public ContactsDialog ClickAddContactsButton() => this.ClickElement<ContactsDialog>(AddContactsButtonLocator);

        /// <summary>
        /// Clicks 'Cancel' button.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>The new instance of T page</returns>
        public T ClickCancel<T>() where T : ICreatablePageObject => this.ClickElement<T>(CancelButtonLocator);

        /// <summary>
        /// Click 'contacts' link
        /// </summary>
        /// <returns> The <see cref="ContactsDialog"/>. </returns>
        public ContactsDialog ClickContactsLink() => this.ClickElement<ContactsDialog>(ContactsLinkLocator);

        /// <summary>
        /// Click 'Delete' button.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>The new instance of T page</returns>
        public T ClickDelete<T>() where T : ICreatablePageObject => this.ClickElement<T>(DeleteButtonLocator);

        /// <summary>
        /// Verify that delete button is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsDeleteButtonDisplayed() => DriverExtensions.IsDisplayed(DeleteButtonLocator);

        /// <summary>
        /// Get Delete button name
        /// </summary>
        /// <returns> Delete button name </returns>
        public string GetDeleteButtonText() => DriverExtensions.WaitForElement(DeleteButtonLocator).Text;

        /// <summary>
        /// Click 'Previously Shared' link
        /// </summary>
        public void ClickPreviouslySharedLink() => this.ClickElement(PreviouslySharedLinkLocator);

        /// <summary>
        /// Click 'Remove All (X)' button. 
        /// </summary>
        public void ClickRemoveAll() => this.ClickElement(RemoveAllButtonLocator);

        /// <summary>
        /// Click 'remove(X) contacts' link.
        /// </summary>
        /// <param name="contactName"> The contact Name. </param>
        public void ClickRemoveByContactName(string contactName) => this.GetContactElementByName(contactName)?.Click();

        /// <summary>
        /// Save note
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>The new instance of T page</returns>
        public T ClickSave<T>() where T : ICreatablePageObject => this.ClickElement<T>(SaveButtonLocator);

        /// <summary>
        /// Save note
        /// </summary>
        public void ClickSave() => this.ClickElement(SaveButtonLocator);

        /// <summary>
        /// Click 'Shared With' link .  
        /// </summary>
        public void ClickShareWithLink() => this.ClickElement(SharedWithLinkLocator);

        /// <summary>
        /// Verify 'Add Contacts (+)' is displayed.  
        /// </summary>
        /// <returns> True if 'Add Contacts' button is displayed, false otherwise. </returns>
        public bool IsAddContactsButtonDisplayed() => DriverExtensions.IsDisplayed(AddContactsButtonLocator, 5);

        /// <summary>
        /// Verify 'added contacts' input is displayed. 
        /// </summary>
        /// <returns> True if 'added contacts' input is displayed, false otherwise </returns>
        public bool IsAddedContactsInputDisplayed() => DriverExtensions.IsDisplayed(AddedContactsInputLocator, 5);

        /// <summary>
        /// Verifies 'Cancel' button is displayed
        /// </summary>
        /// <returns> True if 'Cancel' button is displayed, false otherwise. </returns>
        public bool IsCancelButtonDisplayed() => DriverExtensions.IsDisplayed(CancelButtonLocator, 5);

        /// <summary>
        /// Verify 'contacts' link is displayed
        /// </summary>
        /// <returns> True if 'contacts' link is displayed, false otherwise </returns>
        public bool IsContactsLinkDisplayed() => DriverExtensions.IsDisplayed(ContactsLinkLocator, 5);

        /// <summary>
        /// Verify 'Previously Shared' link is displayed
        /// </summary>
        /// <returns> True if 'Previously Shared' link is displayed and enabled, false otherwise </returns>
        public bool IsPreviouslySharedLinkDisplayed()
            => DriverExtensions.IsDisplayed(PreviouslySharedLinkLocator, 5);

        /// <summary>
        /// Verifies 'Remove All (X)' button is displayed. 
        /// </summary>
        /// <returns> True if 'Remove All' button is displayed, false otherwise. </returns>
        public bool IsRemoveAllButtonDisplayed() => DriverExtensions.IsDisplayed(RemoveAllButtonLocator, 5);

        /// <summary>
        /// Verify 'Remove(X) contacts' is displayed is displayed
        /// </summary>
        /// <param name="contactName">contact Name</param>
        /// <returns> True if contact exists and is displayed. </returns>
        public bool IsRemoveByContactLinkDisplayed(string contactName)
        {
            IWebElement contactByName = this.GetContactElementByName(contactName);
            return contactByName != null && contactByName.Displayed;
        }

        /// <summary>
        /// Type text into note textbox
        /// </summary>
        /// <param name="noteText">Note text</param>
        public virtual void TypeNote(string noteText) => DriverExtensions.SetTextField(noteText, NoteInputLocator);

        /// <summary>
        /// Verify that note input is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsNoteInputDisplayed() => DriverExtensions.IsDisplayed(NoteInputLocator);

        /// <summary>
        /// Verify that 'Shared with' link is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsSharedWithLinkDisplayed() => DriverExtensions.IsDisplayed(SharedWithLinkLocator);

        /// <summary>
        /// Verify 'Shared With' count.  
        /// </summary>
        /// <param name="count"> Number of shared contacts </param>
        /// <returns> True if count of the 'Shared With' is equal to the input parameter, false otherwise. </returns>
        public bool VerifySharedWithCount(int count)
            => DriverExtensions.GetText(SharedWithLinkLocator).Contains("Shared With " + count);

        private IWebElement GetContactElementByName(string contactName)
            =>
                DriverExtensions.GetElements(RemoveByContactLinkLocator)
                                .FirstOrDefault(
                                    contact =>
                                        contact.Text.Contains(contactName, StringComparison.InvariantCultureIgnoreCase));
    }
}