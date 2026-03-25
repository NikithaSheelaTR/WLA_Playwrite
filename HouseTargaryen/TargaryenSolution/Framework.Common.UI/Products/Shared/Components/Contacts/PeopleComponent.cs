namespace Framework.Common.UI.Products.Shared.Components.Contacts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Dialogs.Foldering;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Describe component 'People' in Contacts dialog
    /// </summary>
    public class PeopleComponent : BaseModuleRegressionComponent
    {

        private static string peopleInListLocatorMask =
            ".//ul[@class='co_shrub co_contacts_data']//li[position() > 1]//div[contains(@class,'co_contacts_name co_gridCellItem')][contains(., '{0}')]";

        private static readonly By ContainerLocator
            = By.XPath("//div[@aria-hidden='false']//div[contains(@id, 'coid_contacts_peopleList_contacts')]");

        private static readonly By ContactsInfoLinkLocator =
            By.XPath("//ul[@class='co_shrub co_contacts_data']/li/div[@class='co_listItem hover']/a[contains(@class,'co_contacts_name')]/following-sibling::button");

        private static readonly By MembershipsInfoLinkLocator = By.ClassName("co_contacts_membershipsInfo");

        private static readonly By PeopleInListLocator =
            By.XPath(".//ul[@class='co_shrub co_contacts_data']//li[position() > 1]//div[contains(@class,'co_contacts_name co_gridCellItem')] | .//ul[@id='coid_contacts_peopleListItems_contacts']//li[position() > 1]//div[contains(@class,'co_contacts_name co_contacts')]");

        private static readonly By SearchPeopleInputLocator = By.XPath("./div[@class='co_widgetSearchBox']/label/input");
        
        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Filter the person list
        /// </summary>
        /// <param name="filter">String to filter the list by</param>
        public void ApplyPersonFilter(string filter)
        {
            IWebElement filterTextBox = DriverExtensions.GetElement(this.ComponentLocator, SearchPeopleInputLocator);
            filterTextBox.Click();
            filterTextBox.Focus();
            filterTextBox.Clear();
            filterTextBox.SendKeysSlow(filter);
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForPageLoad();
        }

        /// <summary>
        /// Get a list of people currently in the people list
        /// </summary>
        /// <returns>List of people</returns>
        public List<string> GetPeopleList()
            => DriverExtensions.GetElements(this.ComponentLocator, PeopleInListLocator).Select(e => e.Text).ToList();

        /// <summary>
        /// Select contact in the contact widget by contact name. 
        /// </summary>
        /// <param name="contactList">contact list</param>
        public void SelectContactsByContactName(IEnumerable<string> contactList) => contactList.ToList().ForEach(this.SelectContactByContactName);

        /// <summary>
        /// Select contact by name. 
        /// </summary>
        /// <param name="contactName"> Contact name </param>
        public void SelectContactByContactName(string contactName)
        {
            DriverExtensions.GetElements(this.ComponentLocator, PeopleInListLocator)
                            .First(contact => contact.Text.Replace("\r\n", " ").Equals(contactName, StringComparison.InvariantCultureIgnoreCase)).Click();

            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Show the group memberships for a given person
        /// </summary>
        /// <param name="personName"> Person to show the group memberships </param>
        /// <returns> The <see cref="ContactsMembershipsDialog"/>. </returns>
        public ContactsMembershipsDialog ShowGroupMemberships(string personName)
        {
            IWebElement contactIWebElement = this.HoverContactByName(personName);
            DriverExtensions.WaitForElement(contactIWebElement, ContactsInfoLinkLocator).Click();
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForElement(MembershipsInfoLinkLocator).Click();
            return new ContactsMembershipsDialog();
        }

        /// <summary>
        /// Get list of people marked green check mark
        /// </summary>
        /// <returns></returns>
        public List<string> GetPersonListMarkedGreenCheckMark() =>
            DriverExtensions.GetElements(this.ComponentLocator, PeopleInListLocator)
                            .Where(i => i.GetAttribute("class").Contains("co_checkbox_selected")).Select(i => i.Text).ToList();


        private IWebElement HoverContactByName(string groupName)
        {
            By xpath = By.XPath(string.Format(peopleInListLocatorMask, groupName));
            IWebElement groupIWebElement = DriverExtensions.GetElement(this.ComponentLocator, xpath);
            groupIWebElement.SeleniumHover();
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForPageLoad();

            return groupIWebElement;
        }
    }
}