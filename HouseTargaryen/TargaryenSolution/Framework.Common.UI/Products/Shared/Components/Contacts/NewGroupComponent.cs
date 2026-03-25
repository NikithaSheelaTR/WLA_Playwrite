namespace Framework.Common.UI.Products.Shared.Components.Contacts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Describe component 'New Group' in Contacts dialog
    /// </summary>
    public class NewGroupComponent : BaseModuleRegressionComponent
    {
        private static readonly By GroupMember = By.XPath(".//li[@class='co_contacts_addedContactsPerson']/button");

        private static readonly By GroupNameInput = By.XPath("//input[contains(@id,'newGroupName')]");

        private static readonly By ContainerLocator
            = By.XPath("//div[@class='co_overlayBox_rightContent' and .//ul[contains(@class,'co_contacts_groupData')]]");

        private static readonly By MakeThisGroupAvailableForOrganizationCheckBoxLocator = By.Id("coid_contacts_newGroupPublic");

        /// <summary>
        /// MakeThisGroupAvailableForOrganizationCheckBox
        /// </summary>
        public ICheckBox MakeThisGroupAvailableForOrganizationCheckBox =>
            new CheckBox(MakeThisGroupAvailableForOrganizationCheckBoxLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Delete added users
        /// </summary>
        /// <param name="usersToDelete"> Users to delete</param>
        public void DeleteUsers(IEnumerable<string> usersToDelete)
        {
            if (usersToDelete != null)
            {
                usersToDelete = usersToDelete.ToArray();
                if (usersToDelete.First().Contains(","))
                {
                    usersToDelete = usersToDelete.Select(this.ConvertStringToGroupDisplayingFormat);
                }

                foreach (string user in usersToDelete)
                {
                    IList<IWebElement> addedUsers = DriverExtensions.GetElements(this.ComponentLocator, GroupMember);
                    addedUsers.First(u => u.Text.Equals(user, StringComparison.InvariantCultureIgnoreCase)).Click();
                }
            }
        }

        /// <summary>
        /// Edit current group name
        /// </summary>
        /// <param name="name"> Group for editing</param>
        public void EditGroupName(string name)
        {
            DriverExtensions.WaitForElement(GroupNameInput);
            DriverExtensions.SetTextField(name, GroupNameInput);
        }

        /// <summary>
        /// Get a list of selected people
        /// </summary>
        /// <returns> List of people</returns>
        public List<string> GetSelectedPeople()
            => DriverExtensions.GetElements(this.ComponentLocator, GroupMember).Select(e => e.Text).ToList();

        /// <summary>
        /// Verify people is in group
        /// </summary>
        /// <param name="people">The people To Verify.</param>
        /// <returns> True if people is in group </returns>
        public bool IsPeopleInGroup(IEnumerable<string> people)
            => !people.Any(person => !this.IsPersonInGroup(person));

        /// <summary>
        /// Verify person is in group
        /// </summary>
        /// <param name="person"> The person To Verify. </param>
        /// <returns> True if person is in group </returns>
        public bool IsPersonInGroup(string person)
        {
            // List of the group in people format (like "Tom, Mickey")
            List<string> convertedGroupList =
                this.GetSelectedPeople().Select(this.ConvertStringToPeopleDisplayingFormat).ToList();
            return convertedGroupList.Any(elem => elem.Equals(person, StringComparison.InvariantCultureIgnoreCase));
        }

        // Convert string from format like "Tom, Mickey" to format like "Mickey Tom"
        private string ConvertStringToGroupDisplayingFormat(string stringToConvert)
        {
            string[] tokens = stringToConvert.Split(',');
            stringToConvert = tokens[1].Trim() + " " + tokens[0].Trim();
            return stringToConvert.Trim();
        }

        // Convert string from format like "Mickey Tom" to format like "Tom, Mickey"
        private string ConvertStringToPeopleDisplayingFormat(string stringToConvert)
        {
            string[] tokens = stringToConvert.Split(' ');
            stringToConvert = tokens[1].Trim() + ", " + tokens[0].Trim();
            return stringToConvert;
        }
    }
}