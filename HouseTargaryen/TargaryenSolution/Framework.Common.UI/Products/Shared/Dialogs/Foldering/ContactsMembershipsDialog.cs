namespace Framework.Common.UI.Products.Shared.Dialogs.Foldering
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using OpenQA.Selenium;

    /// <summary>
    /// Dialog that opens up when you click the more info link when viewing a contact
    /// </summary>
    public class ContactsMembershipsDialog : BaseModuleRegressionDialog
    {
        private const string GroupCheckboxLocator = "//label[@class='co_contacts_membershipsItem' and text()={0}]/input";

        private static readonly By CloseButtonLocator = By.Id("coid_contacts_cancelButton");

        private static readonly By GroupLocator = By.CssSelector(".co_contacts_membershipsItem");

        private static readonly By LoadingSpinnerLocator =
            By.XPath("//div[contains(@class, 'co_loading') and not(contains(@class, 'co_hideState'))]");

        private static readonly By RemoveButtonLocator = By.Id("coid_contacts_groupsMembershipsRemoveFrom");

        private static readonly By RemoveConfirmButtonLocator = By.Id("co_confirmRemovalButton");
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactsMembershipsDialog"/> class.
        /// </summary>
        public ContactsMembershipsDialog()
            
        {
        }

        /// <summary>
        /// Clicks the Close button
        /// </summary>
        /// <returns> New instance of the T page </returns>
        public T ClickCloseButton<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(CloseButtonLocator);

        /// <summary>
        /// Gets a list of the groups this user is in
        /// </summary>
        /// <returns>List of groups</returns>
        public List<string> GetGroups()
            =>
                DriverExtensions.GetElements(GroupLocator)
                                .Select(e => e.Text.Split(new[] { "\r\n" }, StringSplitOptions.None)[1])
                                .ToList();

        /// <summary>
        /// Removes a person from a given group
        /// </summary>
        /// <param name="groupName"> Group to remove the person from </param>
        public void RemoveGroupMembership(string groupName)
        {
            DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(GroupCheckboxLocator, groupName)).Click();
            this.ClickElement(RemoveButtonLocator);
            this.ClickElement(RemoveConfirmButtonLocator);
            DriverExtensions.WaitForElementNotPresent(SafeXpath.BySafeXpath(GroupCheckboxLocator, groupName));
        }
    }
}