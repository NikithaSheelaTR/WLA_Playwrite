namespace Framework.Common.UI.Products.Shared.Dialogs.Foldering
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs.Contacts;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Modal that pops up after you click 'add' on the folder share modal
    /// </summary>
    public class AddPeopleFolderShareDialog : BaseModuleRegressionDialog
    {
        private static readonly By AddNewInputLocator = By.CssSelector(".co_contacts_collector_addNew input");

        private static readonly By AddPeopleDialogLocator =
            By.CssSelector("#coid_lightboxOverlay .co_shareFolder_collector");

        private static readonly By ContactsBoxLocator = By.CssSelector("#coid_contacts_addedContactsInput");

        private static readonly By ContactsLinkLocator = By.XPath("//button[@class='co_folderingShareFolder_contacts']");

        private static readonly By ContinueButtonLocator = By.Id("co_folderingShareFolderContinue");

        private static readonly By PeopleSelectorLocator =
            By.XPath("//li[contains(@class, 'co_contacts_addedContacts')]//a");

        private static readonly By SuggestionLinkLocator = By.CssSelector("#co_searchSuggestion li a");

        private static readonly By ValidationMessageBoxLocator = By.XPath("//*[@id='co_shareFolder_createShare_message'] | //button[@class='co_infoBox_closeButton']/following-sibling::div[@class='co_infoBox_message']");

        private const string RemovePeopleLocator = "//li[@class='co_contacts_addedContactsPerson']/button[text()='{0}']";

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPeopleFolderShareDialog"/> class. 
        /// </summary>
        public AddPeopleFolderShareDialog()
        {
            DriverExtensions.WaitForElement(AddPeopleDialogLocator);
        }

        /// <summary>
        /// Adds an email address to the list
        /// </summary>
        /// <param name="email">Email to add</param>
        public void AddEmail(string email)
        {
            DriverExtensions.GetElement(ContactsBoxLocator).Click();
            IWebElement addNewInput = DriverExtensions.WaitForElement(AddNewInputLocator);
            addNewInput.SetTextField(email);
            addNewInput.SendKeys(Keys.Return);
        }

        /// <summary>
        /// Clear Email Text Field
        /// </summary>
        public void ClearEmailTextField()
        {
            DriverExtensions.GetElements(PeopleSelectorLocator).ToList().ForEach(x => x.Click());
            DriverExtensions.WaitForElement(AddNewInputLocator).Clear();
        } 

        /// <summary>
        /// Adds an internal contact email to the list
        /// </summary>
        /// <param name="searchTerm"> The search term </param>
        public void AddInternalEmail(string searchTerm)
        {
            DriverExtensions.GetElement(ContactsBoxLocator).Click();
            IWebElement addNewInput = DriverExtensions.WaitForElement(AddNewInputLocator);
            addNewInput.SetTextField(searchTerm);
            this.ClickElement(SuggestionLinkLocator);
        }

        /// <summary>
        /// Clicks the contacts link
        /// </summary>
        /// <returns> New instance of the contacts folder share modal </returns>
        public ContactsDialog ClickContactsLink() => this.ClickElement<ContactsDialog>(ContactsLinkLocator);

        /// <summary>
        /// Clicks the Continue button
        /// </summary>
        /// <returns> New instance of the T page </returns>
        public T ClickContinueButton<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(ContinueButtonLocator);

        /// <summary>
        /// Gets a list of people that have been selected to collaborate with
        /// </summary>
        /// <returns>List of strings</returns>
        public List<string> GetSelectedPeople()
            => DriverExtensions.GetElements(PeopleSelectorLocator).Select(e => e.Text).ToList();

            /// <summary>
        /// Gets any validation messages in the dialog
        /// </summary>
        /// <returns> Validation message </returns>
        public string GetValidationMessage()
            =>
                DriverExtensions.IsDisplayed(ValidationMessageBoxLocator, 5)
                    ? DriverExtensions.GetText(ValidationMessageBoxLocator)
                    : string.Empty;

        /// <summary>
        /// Removes a list of people from the selected box
        /// </summary>
        /// <param name="people"> People to remove </param>
        public void RemovePeople(params string[] people)
        {
            foreach (string person in people)
            {
                DriverExtensions.GetElement(By.XPath(string.Format(RemovePeopleLocator, people))).Click();
            }
        }
    }
}