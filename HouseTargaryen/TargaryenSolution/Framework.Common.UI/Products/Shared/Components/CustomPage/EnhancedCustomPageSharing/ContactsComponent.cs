namespace Framework.Common.UI.Products.Shared.Components.CustomPage.EnhancedCustomPageSharing
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.CustomPage;
    using Framework.Common.UI.Products.Shared.Items.EnhancedCustomPageSharing;
    using Framework.Common.UI.Products.Shared.Models.CustomPages.EnhancedCustomPageSharing;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Contacts Component
    /// </summary>
    public class ContactsComponent : BaseModuleRegressionComponent
    {
        /// <summary>
        /// The contact locator.
        /// </summary>
        private static readonly By ContactLocator = By.XPath("//tbody/tr[contains(@id,'co_shareCustomPageUserRow')]");

        private static readonly By ContainerLocator = By.Id("sharedPageContactsTable");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// The get contact model list.
        /// </summary>
        public List<CustomPageAdminContactModel> GetContactModelList()
            => this.GetContactList().Select(item => item.ToModel<CustomPageAdminContactModel>()).ToList();

        /// <summary>
        /// The remove contact by name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        public void RemoveContactByName(string name)
            => this.GetContactList().First(c => c.ContactName.Equals(name)).ClickRemoveContact();

        /// <summary>
        /// The remove contact by name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="selected"></param>
        public void SetRemoveContactCheckboxByName(string name, bool selected)
            => this.GetContactList().First(c => c.ContactName.Equals(name)).SetRemoveContactCheckbox(selected);

        /// <summary>
        /// The set role for contact by name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="role">
        /// The role.
        /// </param>
        public void SetRoleForContactByName(string name, CustomPageSharingRole role)
            => this.GetContactList().First(c => c.ContactName.Equals(name)).SelectRole(role);

        /// <summary>
        /// The get contact list.
        /// </summary>
        private List<CustomPageAdminContactItem> GetContactList()
            => DriverExtensions.GetElements(ContactLocator).Select(c => new CustomPageAdminContactItem(c)).ToList();
    }
}
