namespace Framework.Common.UI.Products.WestLawNext.Dialogs
{
    using System;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// Describe Preferences dialog on the client id page (appears after clicking on the 'Select practice area' link)
    /// </summary>
    public class PracticeAreaPreferencesDialog : BaseModuleRegressionDialog
    {
        private static readonly By PracticeAreaDropdownLocator = By.XPath("//select[@id='co_practiceAreasList']");

        private static readonly Random Randomiser = new Random();

        private static readonly By SaveButtonLocator = By.XPath("//input[@id='co_paLightboxSaveButton']");

        /// <summary>
        /// Click on the 'Save' button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickSaveButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(SaveButtonLocator);

        /// <summary>
        /// Verify that Select Practice Area dropdown is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsSelectPracticeAreaDropdownDisplayed()
            => DriverExtensions.IsDisplayed(PracticeAreaDropdownLocator, 5);

        /// <summary>
        /// Select random value for Practice Area
        /// </summary>
        public void SelectPracticeAreaByRandomIndex()
        {
            var practiceAreaDropdown = new SelectElement(DriverExtensions.WaitForElement(PracticeAreaDropdownLocator));
            practiceAreaDropdown.SelectByIndex(Randomiser.Next(practiceAreaDropdown.Options.Count));
            Console.WriteLine($"Selected Practice Area: {practiceAreaDropdown.SelectedOption.Text}");
        }
    }
}