namespace Framework.Common.UI.Products.Shared.Pages.Alerts
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Components.Alerts;
    using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// The manage documents and groups.
    /// </summary>
    public class ManageDocumentsAndGroupsPage : BaseModuleRegressionPage
    {
        private const string DeleteButtonLctMask =
            "//div[@class='co_listItem'][contains(.,{0})]/ul/li/button[@class='co_alertGroup_trash']";

        private const string GroupListItemLctMask =
            "//div[@class='co_listItem']/button[text()={0}]|//div[@class='co_listItem']/span[text()={0}]";

        private const string MoveButtonLctMask =
            "//div[@class='co_listItem'][contains(.,{0})]/ul/li/button[@class='co_alertGroup_move']";

        private const string RenameButtonLctMask =
            "//div[@class='co_listItem'][contains(.,{0})]/ul/li/button[@class='co_alertGroup_rename']";

        private static readonly By ConfirmDeleteButtonLocator = By.Id("co_alerts_groupDeleteYes");

        private static readonly By GroupRootLocator = By.Id("breadcrumbRoot");

        private static readonly By IgnoredDocumentsLinkLocator = By.Id("co_alertManageIgnoresLink");

        private static readonly By ManageAlertGroupsLinkLocator = By.Id("co_alertManageGroupsLink");

        private static readonly By SelectAllItemsLocator = By.Id("checkbox_all_items_select");

        private static readonly By UnignoreSelected = By.Id("coid_alerts_capitolWatch_ignoreDocument_unignoreSelected");

        /// <summary> 
        /// Gets Common Westlaw Next Header Section 
        /// </summary>
        public WestlawNextHeaderComponent Header { get; private set; } = new WestlawNextHeaderComponent();

        /// <summary> 
        /// Gets Manage alert group link 
        /// </summary>
        public ILink ManageAlertGroupLink => new Link(ManageAlertGroupsLinkLocator);

        /// <summary>
        /// Delete Group
        /// </summary>
        /// <param name="groupName"> group Name </param>
        public void DeleteGroup(string groupName)
        {
            DriverExtensions.GetElement(SafeXpath.BySafeXpath(DeleteButtonLctMask, groupName)).Click();
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForElement(ConfirmDeleteButtonLocator).Click();
        }

        /// <summary>
        /// Verify group is displayed.
        /// Before verifying it is needed to wait when page will be loaded
        /// </summary>
        /// <param name="displayedGroup"> group name. </param>
        /// <returns> True if group displayed, false otherwise. </returns>
        public bool IsGroupDisplayed(string displayedGroup)
        {
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(GroupListItemLctMask, displayedGroup), 5);
        }

        /// <summary>
        /// verify group is moved.
        /// </summary>
        /// <param name="groupName"> The from group. </param>
        /// <param name="groupMovedTo"> The to group. </param>
        /// <returns> true if group moved, false otherwise </returns>
        public bool IsGroupMoved(string groupName, string groupMovedTo)
        {
            bool isGroupDisplayedInRootFolder = this.IsGroupDisplayed(groupName);
            DriverExtensions.GetElement(SafeXpath.BySafeXpath(GroupListItemLctMask, groupMovedTo)).Click();
            DriverExtensions.WaitForJavaScript();
            bool isGroupMoved = DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(GroupListItemLctMask, groupName));
            DriverExtensions.GetElement(GroupRootLocator).Click();

            return !isGroupDisplayedInRootFolder && isGroupMoved;
        }

        /// <summary>
        /// Move Group
        /// </summary>
        /// <param name="from"> from folder </param>
        /// <param name="to"> to folder </param>
        public void MoveGroup(string from, string to)
        {
            // Click On Manage Alert Groups
            DriverExtensions.WaitForElement(ManageAlertGroupsLinkLocator).Click();
            DriverExtensions.WaitForJavaScript();

            // Click move button.
            DriverExtensions.GetElement(SafeXpath.BySafeXpath(MoveButtonLctMask, from)).Click();
            var alertGroupsDialog = new AlertGroupsDialog();

            // Select group and assign.
            alertGroupsDialog.SelectGroupButton(to).Click();
            alertGroupsDialog.AssignButton.Click<CreateAlertPage>();
        }

        /// <summary>
        /// Rename Group
        /// </summary>
        /// <param name="from"> from group  </param>
        /// <param name="to"> to group  </param>
        public void RenameGroup(string from, string to)
        {
            // Click On Manage Alert Groups
            DriverExtensions.GetElement(ManageAlertGroupsLinkLocator).Click();
            DriverExtensions.WaitForJavaScript();

            // Click on rename button.
            DriverExtensions.GetElement(SafeXpath.BySafeXpath(RenameButtonLctMask, from)).Click();
            DriverExtensions.WaitForJavaScript();

            var renameDialog = new RenameAlertGroupDialog();
            renameDialog.GroupNameTextbox.SetText(to);
            renameDialog.SaveButton.Click<ManageDocumentsAndGroupsPage>();
        }

        /// <summary>
        /// Un ignore All Documents
        /// </summary>
        public void UnignoreAllDocuments()
        {
            DriverExtensions.GetElement(IgnoredDocumentsLinkLocator).Click();
            DriverExtensions.WaitForJavaScript();

            DriverExtensions.GetElement(SelectAllItemsLocator).Click();
            DriverExtensions.GetElement(UnignoreSelected).Click();
        }

        /// <summary>
        /// Clicks the left pane manage alert group link
        /// </summary>
        /// <returns>
        /// The <see cref="ManageAlertGroupsComponent"/>.
        /// </returns>
        public ManageAlertGroupsComponent ClickOnManageAlertGroupLink()
        {
            ManageAlertGroupLink.Click();
            return new ManageAlertGroupsComponent();
        }
    }
}