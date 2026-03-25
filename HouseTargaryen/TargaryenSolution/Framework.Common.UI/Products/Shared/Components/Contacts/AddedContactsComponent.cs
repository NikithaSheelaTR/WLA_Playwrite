namespace Framework.Common.UI.Products.Shared.Components.Contacts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Describe component 'Added contactsInfo' in Contacts dialog
    /// </summary>
    public class AddedContactsComponent : BaseModuleRegressionComponent
    {
        private static readonly By AddedContactAndGroupLink = By.XPath("//li[contains(@class,'co_contacts_addedContacts')]/button");

        private static readonly By ContainerLocator = By.ClassName("co_contacts_addedContactsInput");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Delete the user contacts added earlier (if any)
        /// </summary>
        /// <param name="usersToDelete"> User to delete </param>
        public void DeleteAddedUsersAndGroups(IEnumerable<string> usersToDelete)
        {
            if (DriverExtensions.IsElementPresent(AddedContactAndGroupLink))
            {
                IReadOnlyCollection<IWebElement> addedUsers = DriverExtensions.GetElements(AddedContactAndGroupLink);
                foreach (string user in usersToDelete)
                {
                    addedUsers.First(element => element.Text.Equals(user, StringComparison.InvariantCultureIgnoreCase)).Click();
                    DriverExtensions.WaitForJavaScript();
                }
            }
        }

        /// <summary>
        /// Check if contact or group is selected
        /// </summary>
        /// <param name="contactsInfo"> Contact's or group's name </param>
        /// <returns> True if contact or group is selected, false otherwise </returns>
        public bool IsContactAndGroupSelected(IEnumerable<string> contactsInfo)
        {
            bool isSelected = false;
            foreach (string contact in contactsInfo)
            {
                isSelected =
                    DriverExtensions.GetElements(AddedContactAndGroupLink)
                                    .Any(
                                        element =>
                                            element.Text.Equals(contact, StringComparison.InvariantCultureIgnoreCase));
            }

            return isSelected;
        }
    }
}