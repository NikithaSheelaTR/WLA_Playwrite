namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Foldering
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs.Foldering;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Enums.Foldering;
    using Framework.Common.UI.Products.WestlawEdge.Components.Folders;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using OpenQA.Selenium;
    using System;
    using System.Linq;


    /// <summary>
    /// EdgeFolderSharingDialog(used for already shared folder)
    /// </summary>
    public class EdgeFolderSharingDialog : FolderSharingDialog
    {
        private static readonly By StopSharingFolderButtonLocator = By.XPath("//button[@class = 'co_shareFolder_stopSharing co_secondaryBtn']");

        private static readonly By CloseButtonLocator = By.XPath("//button[@id='co_folderingManageSharesFooter' and normalize-space(text() = 'Close')]");

        private static readonly By AddButtonLocator = By.CssSelector("button.co_shareFolder_addCollaborators");

        private static readonly By AddNewTextBoxLocator = By.CssSelector(".co_contacts_collector_addNew input");

        private static readonly By ContactsBoxLocator = By.CssSelector("#coid_contacts_addedContactsInput");

        private static readonly By ContinueButtonLocator = By.Id("co_folderingShareFolderContinue");

        private static readonly By NotificationMessageLocator = By.XPath("//div[@id='co_infoBox_message']//div[@class='co_infoBox_message']/text()");

        private static readonly By SearchSuggestionLocator = By.CssSelector("#co_searchSuggestion li a");

        private static readonly By ShareButtonLocator = By.Id("co_folderingShareFolderCommit");

        private static readonly By RemoveSelfDialogLocator = By.Id("co_folderingShareFolder_removeSelf_modal");

        private const string RemoveParticipantButtonLctMask= "//div[@id='co_shareFolder_collaboratorsAndRoles']//*[contains(text(),'{0}')]//following::button[@class='SharedWithTableRemoveButton']";

        private const string RolesLctMask = "//div[@id='co_shareFolder_collaboratorsAndRoles']//*[contains(text(),'{0}')]//following::div[@class='SharedWithTableCell']";

        /// <summary>
        /// Collaborators and roles component
        /// </summary>
        public EdgeCollaboratorsAndRolesComponent CollaboratorsAndRolesComponent { get; } = new EdgeCollaboratorsAndRolesComponent();

        /// <summary>
        /// Stop sharing folder button
        /// </summary>
        public IButton StopSharingFolderButton { get; } = new Button(StopSharingFolderButtonLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton { get; } = new Button(CloseButtonLocator);

        /// <summary>
        /// Clicks the Add button.  We aren't exposing the add button element because it can only return one possible page to eliminate confusion.
        /// Dialog with Add button is displayed if the folder is already shared and you want to add more contacts
        /// </summary>
        /// <returns>The add people folder share modal</returns>
        public override AddPeopleFolderShareDialog ClickAddButton()
            => this.ClickElement<AddPeopleFolderShareDialog>(AddButtonLocator);

        /// <summary>
        /// Add email
        /// </summary>
        /// <param name="email">
        /// Email <see cref="string"/>
        /// </param>
        /// <param name="name">
        /// First name <see cref="string"/>
        /// </param>
        public void AddExternalEmail(string email, string name)
        {
            this.ClickElement(ContactsBoxLocator);
            DriverExtensions.SetTextField(email, AddNewTextBoxLocator);
            DriverExtensions.WaitForElementDisplayed(SearchSuggestionLocator);
            this.ClickElement(
                DriverExtensions.GetElements(SearchSuggestionLocator)
                                .FirstOrDefault(e => e.Text.Equals(name, StringComparison.OrdinalIgnoreCase)));
        }

        /// <summary>
        /// Click continue on the first widget screen on the folder sharing process
        /// </summary>
        public void ClickContinueToViewUserRoles()
        {
            this.ClickElement(ContinueButtonLocator);
            DriverExtensions.WaitForElement(ShareButtonLocator);
        }

        /// <summary>
        /// Get Sharing Roles
        /// </summary>
        /// <param name="firstName"> 
        /// First Name
        /// </param>
        /// <param name="lastName"> 
        /// Last Name
        /// </param>
        /// <returns>
        /// The <see cref="SharingRoles"/>.
        /// </returns>
        public override SharingRoles GetRolesByName(string firstName, string lastName) =>
            DriverExtensions.WaitForElement(By.XPath(string.Format(RolesLctMask, $"{lastName}, {firstName}"))).
                             Text.GetEnumValueByText<SharingRoles>();

        /// <summary>
        /// Click on the share button on the sharing widget as final step to share
        /// </summary>
        /// <returns>confirmation message upon save</returns>
        public string ShareFolderAndGetMessage()
        {
            this.ClickElement(ShareButtonLocator);
            return DriverExtensions.WaitForElement(NotificationMessageLocator).Text;
        }

        /// <summary>
        /// Removes a the participant indicated by it name(Last Name,First Name)
        /// </summary>
        /// <param name="firstName"> The first name <see cref="string"/></param>
        /// <param name="lastName"> The last name <see cref="string"/></param>
        public override void RemoveParticipantByName(string firstName, string lastName)
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(RemoveParticipantButtonLctMask, $"{lastName}, {firstName}"))).Click();

            if (DriverExtensions.IsDisplayed(RemoveSelfDialogLocator))
            {
                new ConfirmationDialog().ClickOkButton<FolderSharingDialog>();
            }
        }
    }
}