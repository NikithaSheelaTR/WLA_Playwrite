namespace Framework.Common.UI.Products.Shared.Components.Contacts
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Dialogs.Contacts;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Utils.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Describe component 'Group' in Contacts dialog
    /// </summary>
    public class GroupComponent : BaseModuleRegressionComponent
    {
        private static readonly string GroupInListLctMask = "//*[contains(@class,'co_contacts_name') and (text()='{0}' or normalize-space(text())='{0}')] | //*[contains(@class,'co_contacts_name') and contains(text(),'{0}')]//parent::li";

        private static readonly By AddGroupButton = By.ClassName("co_contacts_addNew");

        private static readonly By AllGroups =
            By.XPath(".//li//div[contains(@class,'co_listItem')]//span[@class='co_accessibilityLabel' or @class='co_contacts_name']  | //li//span[contains(@class, 'co_contacts_name')]");

        private static readonly By InfoBoxLocator =
            By.XPath("//li[@id='coid_contacts_groupDeleteInfoBoxListItem']");

        private static readonly By CreateFirstGroupLocator =
           By.XPath("//div[@id='coid_contacts_groupList']//ul[@class='co_contacts_createFirst co_contacts_data']");

        private static readonly By DeleteGroupButton = By.ClassName("co_contacts_delete");

        private static readonly By EditableGroups =
            By.XPath("./li/div[@class='co_listItem' and ./a[@class='co_contacts_delete']]");

        private static readonly By EditGroupButton = By.ClassName("co_contacts_edit");

        private static readonly By GroupMemberBy = By.XPath("//div[@class='co_overlayBox_content']//ul[@id='coid_group_member']/li");

        private static readonly By InfoGroupButton = By.ClassName("co_contacts_info");

        private static readonly By SearchGroupInput =
            By.XPath("//div[@id='coid_contacts_groupList']/div[@class='co_widgetSearchBox']/label/input");

        private static readonly By GroupsInListLocator = By.XPath(".//a[contains(@id, 'groupListBox')]");

        private static readonly By ContainerLocator = By.Id("coid_contacts_lightbox");

        private static readonly By NextPageButton = By.Id("co_NextPageContacts_href");
        private static readonly By CloseButtonLocator = By.XPath("//button[@class='co_overlayBox_buttonCancel']");

        private EnumPropertyMapper<MyContactsGroupSearchScope, WebElementInfo> myContactsGroupSearchScopeMap;

       /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

       /// <summary>
       /// GroupDropdown instance
       /// </summary>
       public GroupSearchScopeDropdown ContactGroupDropdown => new GroupSearchScopeDropdown();

       /// <summary>
       /// InfoBox 
       /// </summary>
       public IInfoBox InfoBox => new InfoBox(DriverExtensions.WaitForElement(InfoBoxLocator));

       /// <summary>
        /// Gets the MyContactsGroupSearchScope enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<MyContactsGroupSearchScope, WebElementInfo> MyContactsGroupSearchScopeMap
            =>
                this.myContactsGroupSearchScopeMap =
                    this.myContactsGroupSearchScopeMap
                    ?? EnumPropertyModelCache.GetMap<MyContactsGroupSearchScope, WebElementInfo>();

        private IWebElement GroupComponentContainer =>
            DriverExtensions.GetElement(
                By.XPath("//div[@id='coid_contacts_groupList']/ul[@class='co_shrub co_contacts_data']"));

        /// <summary>
        /// Filters the group list
        /// </summary>
        /// <param name="filter">String to filter the list by</param>
        public void ApplyGroupFilter(string filter)
        {
            IWebElement filterTextBox = DriverExtensions.WaitForElement(SearchGroupInput);
            filterTextBox.Click();
            filterTextBox.Focus();
            filterTextBox.Clear();
            filterTextBox.SendKeysSlow(filter);
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForPageLoad();
        }

        /// <summary>
        /// Click 'Add Group'
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>New Contacts dialog</returns>
        public T ClickAddGroupButton<T>() where T : BaseContactsGroupsDialog
        {
            DriverExtensions.WaitForElement(AddGroupButton).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Edit existing group
        /// </summary>
        /// <typeparam name="T">T </typeparam>
        /// <param name="groupName"> Name group for deleting  </param>
        /// <returns> The <see cref="CustomContactsGroupsDialog"/>. </returns>
        public T ClickEditGroupButton<T>(string groupName) where T : BaseContactsGroupsDialog
        {
            IWebElement groupIWebElement = this.HoverGroupByName(groupName);
            IWebElement editIcon = DriverExtensions.WaitForElement(groupIWebElement, EditGroupButton);
            editIcon.WaitForElementDisplayed();
            editIcon.JavascriptClick();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Create new contact's group
        /// </summary>
        /// <typeparam name="TGroupDialog"> Type of the return page</typeparam>
        /// <typeparam name="TContactDialog"> Type of the group dialog </typeparam> 
        /// <param name="groupName"> Group name </param> 
        /// <param name="usersToAdd"> User to add </param>
        /// <returns> New instance of the page </returns>
        public TGroupDialog CreateGroup<TGroupDialog, TContactDialog>(string groupName, IEnumerable<string> usersToAdd)
            where TGroupDialog : BaseModuleRegressionDialog where TContactDialog : BaseContactsGroupsDialog
        {
            var contactsGroups = this.ClickAddGroupButton<TContactDialog>();
            contactsGroups.NewGroup.EditGroupName(groupName);
            contactsGroups.People.SelectContactsByContactName(usersToAdd);
            return contactsGroups.ClickSaveGroupButton<TGroupDialog>();
        }

        /// <summary>
        /// Create new contact's group for sharing notes or folders
        /// </summary>
        /// <param name="groupName"> Group Name </param>
        /// <param name="usersToAdd"> User to add </param>
        /// <typeparam name="TContactDialog"> Type of the return page </typeparam>
        /// <returns> New instance of the page</returns>
        public TContactDialog CreateGroup<TContactDialog>(string groupName, IEnumerable<string> usersToAdd)
            where TContactDialog : BaseModuleRegressionDialog =>
            this.CreateGroup<TContactDialog, ContactsGroupsDialog>(groupName, usersToAdd);

        /// <summary>
        /// Delete all existing group of contacts
        /// </summary>
        public void DeleteAllContactGroups()
        {
            IEnumerable<string> deletingGroups =
                DriverExtensions.GetElements(this.GroupComponentContainer, EditableGroups).Select(e => e.Text);
            foreach (string group in deletingGroups)
            {
                this.DeleteGroup(group);
            }
        }

        /// <summary>
        /// Delete existing group
        /// </summary>
        /// <param name="groupName"> Group name for deleting </param>
        public void DeleteGroup(string groupName)
        {
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForPageLoad();
            By xpath = By.XPath(string.Format(GroupInListLctMask, groupName));
            IWebElement groupIWebElement = DriverExtensions.GetElement(xpath);
            IWebElement deleteIcon = DriverExtensions.WaitForElement(groupIWebElement, DeleteGroupButton);
            deleteIcon.WaitForElementDisplayed();
            deleteIcon.JavascriptClick();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Delete group if it is editable (only owner can delete group)
        /// </summary>
        /// <param name="groupName"> Group Name </param>
        public void DeleteGroupIfEditable(string groupName)
        {
            if (this.IsGroupEditable(groupName))
            {
                this.DeleteGroup(groupName);
            }
        }

        /// <summary>
        /// Edit existing group: add/delete users from group, rename group
        /// </summary>
        /// <typeparam name="TGroupDialog"> Type of the return page </typeparam>
        /// /// <typeparam name="TContactDialog"> Type of the group dialog </typeparam>
        /// <param name="groupName"> Name group for editing </param>
        /// <param name="usersToAdd"> List of users for adding </param>
        /// <param name="usersToDelete"> List of users for deleting from group </param>
        /// <param name="newGroupName"> New group name(optional) </param>
        /// <returns>New instance of the page</returns>
        public TGroupDialog EditExistingGroup<TGroupDialog, TContactDialog>(
            string groupName,
            IEnumerable<string> usersToAdd,
            IEnumerable<string> usersToDelete,
            string newGroupName = null) where TGroupDialog : ICreatablePageObject
                                        where TContactDialog : BaseContactsGroupsDialog
        {
            var customPageGroupDialog = this.ClickEditGroupButton<TContactDialog>(groupName);
            if (!string.IsNullOrEmpty(newGroupName))
            {
                customPageGroupDialog.NewGroup.EditGroupName(groupName);
            }

            if (usersToAdd != null)
            {
                customPageGroupDialog.People.SelectContactsByContactName(usersToAdd);
            }

            if (usersToDelete != null)
            {
                customPageGroupDialog.NewGroup.DeleteUsers(usersToDelete);
            }

            return customPageGroupDialog.ClickSaveGroupButton<TGroupDialog>();
        }

        /// <summary>
        /// Get a list of groups
        /// </summary>
        /// <returns>List of groups</returns>
        public List<string> GetGroupList() => DriverExtensions.IsDisplayed(CreateFirstGroupLocator) ? new List<string>()
            : DriverExtensions.GetElements(this.GroupComponentContainer, AllGroups).Select(el => el.InnerHtml()).ToList();

        /// <summary>
        /// Gets a list of members of a given group.
        /// </summary>
        /// <param name="groupName">Group to look at</param>
        /// <returns>List of strings</returns>
        public List<string> GetMembersOfGroup(string groupName)
        {
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForPageLoad();
            By xpath = By.XPath(string.Format(GroupInListLctMask, groupName));
            IWebElement groupIWebElement = DriverExtensions.GetElement(xpath);
            DriverExtensions.WaitForElement(groupIWebElement, InfoGroupButton).Click();
            DriverExtensions.WaitForJavaScript();
            List<string> MembersOfGroup = DriverExtensions.GetElements(GroupMemberBy).Select(e => e.Text).ToList();
            DriverExtensions.WaitForElement(groupIWebElement, CloseButtonLocator).Click();
            DriverExtensions.WaitForJavaScript();
            return MembersOfGroup;
        }

        /// <summary>
        /// Get list of groups marked green check mark
        /// </summary>
        /// <returns></returns>
        public List<string> GetGroupsListMarkedGreenCheckMark() =>
            DriverExtensions.GetElements(this.GroupComponentContainer, GroupsInListLocator)
                            .Where(i => i.GetAttribute("class").Contains("co_checkbox_selected")).Select(i => i.Text).ToList();

        /// <summary>
        /// Verify is group displayed
        /// </summary>
        /// <param name="groupName"> Group name to verify </param>
        /// <returns> True, if group with this name is already exist </returns>
        public bool IsContactGroupDisplayed(string groupName) => DriverExtensions.IsDisplayed(CreateFirstGroupLocator) ? false : this.GetGroupList().Contains(groupName);

        /// <summary>
        /// Select Group in the contact widget by group name.
        /// </summary>
        /// <param name="groups"> Group name </param>
        public void SelectGroupsByGroupName(IEnumerable<string> groups) =>
            groups.ToList().ForEach(this.SelectGroupByGroupName);

        /// <summary>
        /// Select Group in the contact widget by group name.
        /// </summary>
        /// <param name="groupName"> Group name</param>
        public void SelectGroupByGroupName(string groupName) => DriverExtensions.WaitForElementDisplayed(By.XPath(string.Format(GroupInListLctMask, groupName))).Click();

       
        /// <summary>
        /// Hover Group
        /// </summary>
        /// <param name="groupName"> Group Name</param>
        /// <returns> Hovered element </returns>
        private IWebElement HoverGroupByName(string groupName)
        {
            IWebElement groupIWebElement = null;
            By xpath = By.XPath(string.Format(GroupInListLctMask, groupName));
            if (DriverExtensions.IsDisplayed(xpath, 5))
            {
                groupIWebElement = DriverExtensions.GetElement(xpath);
                groupIWebElement.SeleniumHover();
                DriverExtensions.WaitForJavaScript();
                DriverExtensions.WaitForPageLoad();
            }

            return groupIWebElement;
        }

        /// <summary>
        /// Verify that group is editable
        /// </summary>
        /// <param name="groupName"> Group Name </param>
        /// <returns> True if displayed, false otherwise </returns>
        private bool IsGroupEditable(string groupName)
            => DriverExtensions.GetElements(this.GroupComponentContainer, EditableGroups)
            .Select(e => e.Text).Contains(groupName);


        /// <summary>
        /// Create new contact's group Click on Next Page
        /// </summary>
        /// <typeparam name="TGroupDialog"> Type of the return page</typeparam>
        /// <typeparam name="TContactDialog"> Type of the group dialog </typeparam> 
        /// <param name="groupName"> Group name </param> 
        /// <param name="usersToAdd"> User to add </param>
        /// <returns> New instance of the page </returns>
        public TGroupDialog CreateGroupNextPage<TGroupDialog, TContactDialog>(string groupName, IEnumerable<string> usersToAdd)
            where TGroupDialog : BaseModuleRegressionDialog where TContactDialog : BaseContactsGroupsDialog
        {
            var contactsGroups = this.ClickAddGroupButton<TContactDialog>();
            contactsGroups.NewGroup.EditGroupName(groupName);
            IWebElement nextPage = DriverExtensions.WaitForElement(NextPageButton);
            nextPage.Click();
            contactsGroups.People.SelectContactsByContactName(usersToAdd);
            return contactsGroups.ClickSaveGroupButton<TGroupDialog>();
        }
    }
}