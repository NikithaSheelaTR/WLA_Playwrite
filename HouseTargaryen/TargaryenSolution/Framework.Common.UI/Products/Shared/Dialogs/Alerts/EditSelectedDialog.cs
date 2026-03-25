namespace Framework.Common.UI.Products.Shared.Dialogs.Alerts
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    /// <summary>
    /// The dialog that comes when you click the edit selected link 
    /// </summary>
    public class EditSelectedDialog : BaseModuleRegressionDialog
    {
        private const string GroupToAssignLctMask =
            "//ul[@id='coid_alerts_widgets_alertGroups_listItems']/li/div/button[@title={0}]";

        private static readonly By AlertAssignedToGroupWarningYesButtonLocator =
            By.Id("co_alertAssignedToGroupWarning_buttonYes");

        private static readonly By AssignAlertGroupsLinkLocator = By.XPath("//*[@id='AlertGroups_multiple_edit_facet_selector_item']");

        private static readonly By SaveButtonLocator = By.Id("co_multiple_edit_save");

        private static readonly By SearchCategoryTextBoxLocator = By.Id("AddaddCategoryText");

        private static readonly By AddCategoryButtonLocator = By.Id("AddaddCategoryButton");

        private static readonly By AddCategoriesTextLocator = By.XPath(".//*[contains(text(),'Add categories')]");

        /// <summary>
        /// Alert assigned to group warning yes button
        /// </summary>
        public IButton AlertAssignedToGroupWarningYesButton => new Button(AlertAssignedToGroupWarningYesButtonLocator);

        /// <summary>
        /// Save button
        /// </summary>
        public IButton SaveButton => new Button(SaveButtonLocator);

        /// <summary>
        /// Group to assign button
        /// </summary>
        public IButton GroupToAssignButton(string groupName) => 
            new Button(SafeXpath.BySafeXpath(GroupToAssignLctMask, groupName));

        /// <summary>
        /// Assign alert groups Link
        /// </summary>
        public ILink AssignAlertGroupsLink => new Link(AssignAlertGroupsLinkLocator);

        /// <summary>
        /// Search Category box
        /// </summary>
        public ITextbox SearchCategoryBox => new Textbox(SearchCategoryTextBoxLocator);

        /// <summary>
        /// Search Category box
        /// </summary>
        public IButton AddCategoryButton => new Button(AddCategoryButtonLocator);

        /// <summary>
        /// Add Categories Link
        /// </summary>
        public IWebElement AddCategoriesText  => DriverExtensions.WaitForElement(AddCategoriesTextLocator);

        /// <summary>
        /// Assign alert group.
        /// </summary>
        /// <param name="groupName"> Group name. </param>
        public void AssignAlertGroup(string groupName)
        {
            this.AssignAlertGroupsLink.Click();
            this.GroupToAssignButton(groupName).Click();
            this.SaveButton.Click();
        }

        /// <summary>
        /// Add Category into selected alerts.
        /// </summary>
        /// <param name="categoryName"> GrCategory Name. </param>
        public void AddCategoryInSelectedAlerts(string categoryName)
        {
            SearchCategoryBox.Clear();
            SearchCategoryBox.SendKeys(categoryName);
            AddCategoryButton.Click();
            SaveButton.Click();
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
        }
    }
}