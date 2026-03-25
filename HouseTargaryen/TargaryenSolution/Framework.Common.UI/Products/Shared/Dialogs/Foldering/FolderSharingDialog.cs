namespace Framework.Common.UI.Products.Shared.Dialogs.Foldering
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components.Folder;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.Foldering;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Raw.WestlawEdge.Utils.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// View and Edit Folder Sharing widget: this widget appears when selecting the folder sharing option on a folder that is already shared
    /// </summary>
    public class FolderSharingDialog : BaseModuleRegressionDialog
    {
        private static readonly By AddButtonLocator = By.XPath("//button[@class='co_shareFolder_addCollaborators co_defaultBtn']");

        private static readonly By CloseButtonLocator = By.Id("co_folderingShareFolderCommit");

        private static readonly By CollabsAndRolesLocator = By.Id("co_shareFolder_collaboratorsAndRoles");

        private static readonly By DetailsTableLocator = By.ClassName("co_detailsTable_content");

        private static readonly By EndSharingButtonLocator = By.XPath("//button[text()='End Sharing']");

        private static readonly By FolderShareDialogLocator = By.Id("coid_lightboxOverlay");

        private static readonly By MessageLocator =
            By.XPath("//div[@id ='co_infoBox_message_editRoles']//div[@class='co_infoBox_message']");

        private static readonly By RemoveCollaboratorLocator = By.XPath("//td[@class = 'co_detailsTable_remove']/a");

        private static readonly By RemoveSelfDialogLocator = By.Id("co_folderingShareFolder_removeSelf_modal");

        private static readonly By RolesLocator = By.ClassName("co_detailsTable_roles");

        private static readonly By RootLocator = By.Id("co_shareFolder_folderView");

        private static readonly By SecondEndSharingButtonLocator = By.XPath("//input[@value='End Sharing']");

        private static readonly By SubfolderCheckboxLocator = By.XPath("//input[@id='co_shareFolder_inclSubfolders']");

        /// <summary>
        /// Initializes a new instance of the <see cref="FolderSharingDialog"/> class. 
        /// ViewAndEditFolderSharingWidget Constructor
        /// </summary>
        public FolderSharingDialog()
        {
            DriverExtensions.WaitForElementDisplayed(FolderShareDialogLocator);
        }

        /// <summary>
        /// The _folder tree.
        /// </summary>
        public FolderTreeComponent FolderTree { get; } = new FolderTreeComponent(RootLocator);

        /// <summary>
        /// Sharing Roles dropdowns of dialog
        /// </summary>
        private Dictionary<string, Dropdown<SharingRoles>> SharingRolesDropdowns =>
            DriverExtensions.GetElements(CollabsAndRolesLocator, By.TagName("tr"))
                            .Where(e => e.InnerHtml().Contains("select")).ToDictionary(
                                e => DriverExtensions.WaitForElement(e, DetailsTableLocator).Text.ToUpper(),
                                e => new Dropdown<SharingRoles>(e, By.TagName("select")));
        

        /// <summary>
        /// Clicks the Add button.  We aren't exposing the add button element because it can only return one possible page to eliminate confusion.
        /// Dialog with Add button is displayed if the folder is already shared and you want to add more contacts
        /// </summary>
        /// <returns>The add people folder share modal</returns>
        public virtual AddPeopleFolderShareDialog ClickAddButton()
            => this.ClickElement<AddPeopleFolderShareDialog>(AddButtonLocator);

        /// <summary>
        /// Changes the role of a participant indicated by users name(Last Name,First Name)
        /// </summary>
        /// <param name="firstName"> 
        /// First Name
        /// </param>
        /// <param name="lastName"> 
        /// Last Name
        /// </param>
        /// <param name="role"> Sharing role </param>
        public void ChangeParticipantRoleByName(string firstName, string lastName, SharingRoles role) =>
            this.SharingRolesDropdowns[$"{lastName}, {firstName}"].SelectOption(role);

        /// <summary>
        /// Click "Apply changes to subfolders" checkbox
        /// </summary>
        public void ClickApplyToSubFolderCheckbox() => this.ClickElement(SubfolderCheckboxLocator);

        /// <summary>
        /// Click the Close Button
        /// </summary>
        /// <returns>
        /// The <see cref="ResearchOrganizerPage"/>.
        /// </returns>
        public ResearchOrganizerPage ClickClose() => this.ClickElement<ResearchOrganizerPage>(CloseButtonLocator);

        /// <summary>
        /// Click the end collaboration button. If the clickFinalEndSharing parameter is false, the final confirmation is not clicked
        /// and just returns the  final confirmation displayed. 
        /// </summary>
        public void ClickEndSharing()
        {
            DriverExtensions.WaitForElementDisplayed(EndSharingButtonLocator);
            this.ClickElement(EndSharingButtonLocator);
            this.ClickElement(SecondEndSharingButtonLocator);
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
        public virtual SharingRoles GetRolesByName(string firstName, string lastName) =>
            DriverExtensions.GetElements(CollabsAndRolesLocator, By.TagName("tr"))
            .Where(e => DriverExtensions.WaitForElement(e, DetailsTableLocator).Text
            .Equals($"{lastName}, {firstName}", StringComparison.OrdinalIgnoreCase))
            .Select(e => DriverExtensions.WaitForElement(e, RolesLocator).Text.GetEnumValueByText<SharingRoles>())
            .First();

        /// <summary>
        /// Get Folder Sharing message
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetMessage() => DriverExtensions.WaitForElement(MessageLocator).Text;

        /// <summary>
        /// Removes a the participant indicated by it name(Last Name,First Name)
        /// </summary>
        /// <param name="firstName"> The first name <see cref="string"/></param>
        /// <param name="lastName"> The last name <see cref="string"/></param>
        public virtual void RemoveParticipantByName(string firstName, string lastName)
        {
            DriverExtensions.GetElements(CollabsAndRolesLocator, By.TagName("tr"))
                            .Where(e => DriverExtensions.WaitForElement(e, DetailsTableLocator).Text
                            .Equals($"{lastName}, {firstName}", StringComparison.OrdinalIgnoreCase))
                            .ToList()
                            .ForEach(e => DriverExtensions.WaitForElement(e, RemoveCollaboratorLocator).Click());

            if (DriverExtensions.IsDisplayed(RemoveSelfDialogLocator))
            {
                new ConfirmationDialog().ClickOkButton<FolderSharingDialog>();
            }
        }
    }
}