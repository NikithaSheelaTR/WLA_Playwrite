namespace Framework.Common.UI.Products.Shared.Dialogs.Foldering
{
    using System;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs.Contacts;
    using Framework.Common.UI.Products.Shared.Enums.Foldering;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// works with the folder sharing widget actions
    /// </summary>
    public class ShareFolderDialog : BaseModuleRegressionDialog
    {
        private static readonly By AddNewTextBoxLocator = By.CssSelector(".co_contacts_collector_addNew input");

        private static readonly By CollaboratorRolesLocator =
            By.XPath("//div[@class = 'SharedWithTableCell']/select");

        private static readonly By ContactsBoxLocator = By.CssSelector("#coid_contacts_addedContactsInput");

        private static readonly By ContactsLinkLocator = By.XPath("//button[@class='co_folderingShareFolder_contacts']");

        private static readonly By ContinueButtonLocator = By.Id("co_folderingShareFolderContinue");

        private static readonly By NotificationMessageLocator = By.XPath("//div[@id='co_infoBox_message']//div[contains(@class, 'co_infoBox_message')]");

        private static readonly By SearchSuggestionLocator = By.CssSelector("#co_searchSuggestion li a");

        private static readonly By ShareButtonLocator = By.Id("co_folderingShareFolderCommit");

        /// <summary>
        /// Initializes a new instance of the <see cref="ShareFolderDialog"/> class. 
        /// constructs the folder sharing widget
        /// </summary>
        public ShareFolderDialog()
        {
            DriverExtensions.WaitForElementDisplayed(ContinueButtonLocator);
        }

        /// <summary>
        /// Add email
        /// </summary>
        /// <param name="email">
        /// Email <see cref="string"/>
        /// </param>
        /// <param name="name">
        /// First name <see cref="string"/>
        /// </param>
        public void AddExternalEmail(string email, string name)
        {
            this.ClickElement(ContactsBoxLocator);
            DriverExtensions.SetTextField(email, AddNewTextBoxLocator);
            DriverExtensions.WaitForElementDisplayed(SearchSuggestionLocator);
            this.ClickElement(
                DriverExtensions.GetElements(SearchSuggestionLocator)
                                .FirstOrDefault(e => e.Text.Equals(name, StringComparison.OrdinalIgnoreCase)));
        }

        /// <summary>
        /// Adds an internal contact email to the list
        /// </summary>
        /// <param name="searchTerm"> The search term </param>
        public void AddInternalEmail(string searchTerm)
        {
            DriverExtensions.GetElement(ContactsBoxLocator).Click();
            IWebElement addNewInput = DriverExtensions.WaitForElement(AddNewTextBoxLocator);
            addNewInput.SetTextField(searchTerm);
            this.ClickElement(SearchSuggestionLocator);
        }

        /// <summary>
        /// Adds an email address to the list
        /// </summary>
        /// <param name="email">Email to add</param>
        public void AddEmail(string email)
        {
            DriverExtensions.GetElement(ContactsBoxLocator).Click();
            IWebElement addNewInput = DriverExtensions.WaitForElement(AddNewTextBoxLocator);
            addNewInput.SetTextField(email);
            addNewInput.SendKeys(Keys.Return);
        }

        /// <summary>
        /// Clicks on the contacts link display contacts widget
        /// </summary>
        /// <returns> The <see cref="ContactsDialog"/>. </returns>
        public ContactsDialog ClickContactsLinkOnSharingWidget() => this.ClickElement<ContactsDialog>(ContactsLinkLocator);

        /// <summary>
        /// Click continue on the first widget screen on the folder sharing process
        /// </summary>
        public void ClickContinueToViewUserRoles()
        {
            this.ClickElement(ContinueButtonLocator);
            DriverExtensions.WaitForElement(ShareButtonLocator);
        }

        /// <summary>
        /// Clicks the Continue button
        /// </summary>
        /// <returns> New instance of the T page </returns>
        public T ClickContinueButton<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(ContinueButtonLocator);

        /// <summary>
        ///  Change Collaborator Role on the Folder sharing widget
        /// </summary>
        /// <param name="role">Role of the collaborator. This can be either Contributor or Reviewer</param>
        /// <param name="index"> The index of collaborator to make selection to. </param>
        public void SelectColoboratorRole(SharingRoles role, int index = 0) 
            => DriverExtensions.SelectElementInListByText(CollaboratorRolesLocator, role.ToString());

        /// <summary>
        /// Click on the share button on the sharing widget as final step to share
        /// </summary>
        /// <returns>confirmation message upon save</returns>
        public string ShareFolderAndGetMessage()
        {
            this.ClickElement(ShareButtonLocator);
            return DriverExtensions.WaitForElement(NotificationMessageLocator).Text;
        }
    }
}