namespace Framework.Common.UI.Products.Shared.Dialogs.Contacts
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Contacts dialog in shared notes
    /// </summary>
    public class ContactsDialog : BaseContactsManagedDialog
    {
        private static readonly By ContinueButtonLocator = By.Id("co_folderingShareFolderContinue");

        private static readonly By PeopleSelectorLocator =
            By.XPath("//li[contains(@class, 'co_contacts_addedContacts')]//a");
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactsDialog"/> class.  
        /// </summary>
        public ContactsDialog()
        {
            this.InsertButton = By.Id("coid_contacts_closeButton");
            this.CancelButton = By.Id("coid_contacts_cancelLink");
            this.CloseDialogButton = By.Id("coid_contacts_closeImage");
            DriverExtensions.WaitForJavaScript();
        }

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
        /// Removes a list of people from the selected box
        /// </summary>
        /// <param name="people"> People to remove </param>
        public void RemovePeople(params string[] people)
        {
            foreach (string person in people)
            {
                DriverExtensions.GetElement(By.LinkText(person)).Click();
            }
        }
    }
}