namespace Framework.Common.UI.Products.Shared.Dialogs.Document.Notes
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs.Contacts;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Dialog that appears after clicking on highlight text on the Document page
    /// </summary>
    public class ManageHighlightDialog : BaseModuleRegressionDialog
    {
        private static readonly By AddNoteLinkLocator = By.Id("co_highlightMenuAddNote");

        private static readonly By DeleteHighlightLinkLocator = By.Id("co_highlightMenuDeleteHighlight");

        private static readonly By ShareWithContactsLinkLocator = By.XPath("//*[@id='co_shareWithContacts']");

        private static readonly By ShareWithPreviousContactsLinkLocator = By.XPath("//*[@id='co_previousContacts']");

        private static readonly By StopSharingHighlightLinkLocator = By.XPath("//*[@id='co_stopSharing']");

        private static readonly By ViewEditReviewersLinkLocator = By.XPath("//*[@id='co_editReviewers']");

        private static readonly By CopyWithReferenceLinkLocator = By.XPath("//div[@class='co_dropBoxInner']//li[contains(@id,'CopyWithRef')]");

        private static readonly By CopyWithoutReferenceLinkLocator =
            By.XPath("//div[@class='co_dropBoxInner']//li[contains(@id,'CopyWithoutRef')]");

        private static readonly By HighlightColorBoxContainerLocator = By.Id("co_highlightMenuEditHighlightColor");

        private EnumPropertyMapper<HighlightColor, WebElementInfo> highlightColorPickerMap;

        /// <summary>
        /// Gets the Flag enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<HighlightColor, WebElementInfo> HighlightColorPickerMap =>
            this.highlightColorPickerMap = this.highlightColorPickerMap
                                     ?? EnumPropertyModelCache.GetMap<HighlightColor, WebElementInfo>(
                                         "Picker");

        /// <summary>
        /// Click 'Add a note' link
        /// </summary>
        /// <returns> The <see cref="EditNoteDialog"/>. </returns>
        public EditNoteDialog ClickAddNoteLink()
        {
            DriverExtensions.WaitForElement(AddNoteLinkLocator);
            DriverExtensions.ScrollTo(AddNoteLinkLocator);
            return this.ClickElement<EditNoteDialog>(AddNoteLinkLocator);
        }

        /// <summary>
        /// Verify 'Add Note' link displayed
        /// </summary>
        /// <returns>True if 'Add Note' link is displayed, false otherwise.</returns>
        public bool IsAddNoteDisplayed() => DriverExtensions.IsDisplayed(AddNoteLinkLocator, 5);

        /// <summary>
        /// Click 'Delete highlight' link 
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickDeleteButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(DeleteHighlightLinkLocator);

        /// <summary>
        /// Verify 'Delete highlight' link is displayed
        /// </summary>
        /// <returns> True if 'Delete highlight' link is displayed, false otherwise </returns>
        public bool IsDeleteHighlightDisplayed() => DriverExtensions.IsDisplayed(DeleteHighlightLinkLocator, 5);

        /// <summary>
        /// Click 'Share with Contacts' link.
        /// </summary>
        /// <returns> The <see cref="ContactsDialog"/>. </returns>
        public ContactsDialog ClickShareWithContacts() => this.ClickElement<ContactsDialog>(ShareWithContactsLinkLocator);

        /// <summary>
        /// Click 'Share With Previous Contacts' link
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>The new instance of T page</returns>
        public T ClickShareWithPreviousContacts<T>() where T : ICreatablePageObject => this.ClickElement<T>(ShareWithPreviousContactsLinkLocator);

        /// <summary>
        /// Verify 'Share with Contacts' link is displayed
        /// </summary>
        /// <returns>True if 'Share with Contacts' link is displayed, false otherwise</returns>
        public bool IsShareWithContactsDisplayed() => DriverExtensions.IsDisplayed(ShareWithContactsLinkLocator, 5);

        /// <summary>
        /// Check 'Share with Previous Contacts' is displayed.
        /// </summary>
        /// <returns> True if Share with previous link displayed </returns>
        public bool IsShareWithPreviousContactsDisplayed() => DriverExtensions.IsDisplayed(ShareWithPreviousContactsLinkLocator, 5);

        /// <summary>
        /// Click 'Stop Sharing highlights' link.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>The new instance of T page</returns>
        public T ClickStopSharingHighlight<T>() where T : ICreatablePageObject => this.ClickElement<T>(StopSharingHighlightLinkLocator);

        /// <summary>
        /// Verify 'Stop sharing highlights' link is displayed.
        /// </summary>
        /// <returns>True if 'Stop Sharing Highlight' link is displayed, false otherwise.</returns>
        public bool IsStopSharingHighlightDisplayed() => DriverExtensions.IsDisplayed(StopSharingHighlightLinkLocator, 5);

        /// <summary>
        /// Click 'View/Edit Reviewers' link.
        /// </summary>
        /// <returns> The <see cref="ContactsDialog"/>. </returns>
        public ContactsDialog ClickViewEditReviewers() => this.ClickElement<ContactsDialog>(ViewEditReviewersLinkLocator);

        /// <summary>
        /// Verify 'View/Edit Reviewers' link is displayed
        /// </summary>
        /// <returns>True if 'View/Edit Reviewers' link is displayed, false otherwise. </returns>
        public bool IsViewEditReviewersDisplayed() => DriverExtensions.IsDisplayed(ViewEditReviewersLinkLocator, 5);

        /// <summary>
        /// Verify 'Copy with Reference' link displayed
        /// </summary>
        /// <returns>True if 'Copy with Reference' link is displayed, false otherwise </returns>
        public bool IsCopyWithReferenceDisplayed() => DriverExtensions.IsDisplayed(CopyWithReferenceLinkLocator, 5);

        /// <summary>
        /// Verify 'Copy without Reference' link displayed
        /// </summary>
        /// <returns>True if link is displayed, false otherwise </returns>
        public bool IsCopyWithoutReferenceDisplayed() => DriverExtensions.IsDisplayed(CopyWithoutReferenceLinkLocator, 5);

        /// <summary>
        /// Click 'Copy With Reference' Link
        /// </summary>
        /// <typeparam name="T">Page Type</typeparam>
        /// <returns> New instance of the object </returns>
        public T ClickCopyWithReference<T>() where T : ICreatablePageObject => this.ClickElement<T>(CopyWithReferenceLinkLocator);


        /// <summary>
        /// Select color and click 'Save'
        /// </summary>
        /// <param name="color">color</param>
        /// <returns> NoteItemModel </returns>
        public T SelectColor<T>(HighlightColor color)
            where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(
                DriverExtensions.GetElement(HighlightColorBoxContainerLocator),
                By.XPath(this.HighlightColorPickerMap[color].LocatorString)).CustomClick();

            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
