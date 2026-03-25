namespace Framework.Common.UI.Products.Shared.Components.CustomPage
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Items.CustomPageAdmin;
    using Framework.Common.UI.Products.Shared.Models.CustomPages;
    using Framework.Common.UI.Products.Shared.Pages.CustomPages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Contacts Component on Edit assigned page 
    /// </summary>
    public class ContactsComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContactLocator = By.XPath("//div[@class = 'cp_section co_genericBox styled']");
        private static readonly By ExpandOrCollapseButtonLocator = By.XPath("//*[@id = 'co_sa_expandCollapseAll']");

        private static readonly By ContainerLocator = By.ClassName("co_sa_content");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get Custom Page Contacts
        /// </summary>
        /// <returns></returns>
        public List<CustomPageContactModel> GetCustomPageContacts() =>
            this.GetCustomPageContactsItems().Select(item => item.ToModel<CustomPageContactModel>()).ToList();

        /// <summary>
        /// Get Custom Page Contact
        /// </summary>
        /// <returns></returns>
        public CustomPageContactModel GetCustomPageContact(string contactName) =>
            this.GetCustomPageContactsItems().Single(item => item.ContactName.Equals(contactName)).ToModel<CustomPageContactModel>();

        /// <summary>
        /// get button text
        /// </summary>
        /// <returns></returns>
        public string GetExpandOrCollapseButtonText() => DriverExtensions.GetText(ExpandOrCollapseButtonLocator);

        /// <summary>
        /// Click on Custom page by name for certain contact
        /// </summary>
        /// <param name="contactName"></param>
        /// <param name="customPageName"></param>
        /// <returns></returns>
        public T ClickContactCustomPageByName<T>(string contactName, string customPageName) where T : ICreatablePageObject =>
            this.GetCustomPageConatactsByName(contactName, customPageName).ClickCustomPageName<T>();

        /// <summary>
        /// Click on View Assigned Contacts link by name
        /// </summary>
        /// <param name="contactName"></param>
        /// <param name="customPageName"></param>
        /// <returns></returns>
        public AdminSettingsPage ClickViewAssignedContactsLinkByName(string contactName, string customPageName) =>
            this.GetCustomPageConatactsByName(contactName, customPageName).ClickViewAssignedContactsLink();

        /// <summary>
        /// Click on ExpandOrCollapse Button
        /// </summary>
        public void ClickExpandOrCollapseButton() => DriverExtensions.GetElement(ExpandOrCollapseButtonLocator).Click();

        /// <summary>
        /// Are Items Expanded And Have Minus Button
        /// </summary>
        /// <returns></returns>
        public bool AreItemsExpandedAndHaveMinusButton() =>
            this.GetCustomPageContactsItems().All(c => c.IsMinusButtonDisplayed() && c.IsContactExpanded);

        /// <summary>
        /// Are Items Collapsed And Have Plus Button
        /// </summary>
        /// <returns></returns>
        public bool AreItemsCollapsedAndHavePlusButton() =>
            this.GetCustomPageContactsItems().All(c => c.IsPlusButtonDisplayed() && !c.IsContactExpanded);

        /// <summary>
        /// Is focus set on the custom page
        /// </summary>
        public bool IsFocusSetOnCustomPage(string pageName) =>
            DriverExtensions.GetFocusedElement().GetParentElement().Text.StartsWith(pageName);

        /// <summary>
        /// Is focus set on the contact
        /// </summary>
        public bool IsFocusSetOnContact(string contactName) =>
            DriverExtensions.GetFocusedElement().Text.StartsWith(contactName);

        private List<CustomPageContactItem> GetCustomPageContactsItems() => DriverExtensions.GetElements(ContactLocator).Select(c => new CustomPageContactItem(c)).ToList();

        private CustomPageItem GetCustomPageConatactsByName(string contactName, string customPageName) =>
            this.GetCustomPageContactsItems().First(c => c.ContactName.Equals(contactName)).CustomPageList
                        .First(page => page.CustomPageName.Equals(customPageName));
    }
}
