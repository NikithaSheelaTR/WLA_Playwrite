namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.BuildContract
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.WestlawEdge.Components;
    using Framework.Common.UI.Products.WestLawNext.Pages;   
    using Framework.Common.UI.Products.Shared.Dialogs.HomePage;
   
    /// <summary>
    /// Represents a page for building contacts.
    /// </summary>
    public class BuildContractPage : CommonAdvancedSearchPage
    {
        private static readonly By ShowWelcomeCheckBoxInContractExpressWill = By.XPath("//mat-checkbox[@formcontrolname='showWelcomeScreen']/label/div");      

        private static readonly By CheckboxLocatorwelcome = By.ClassName("Welcome-option");

        private static readonly By MyaccountLocator = By.XPath("//button[@data-role='MyAccountBtn']");
        
        /// <summary>
        /// Header
        /// </summary>
        public new EdgeHeaderComponent Header { get; } = new EdgeHeaderComponent();

        /// <summary>
        /// Header
        /// </summary>
        public PreferencesDialog Dialog { get; } = new PreferencesDialog();

        /// <summary>
        /// Clicks on the profile dropdown and opens the profile dialog.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="time">.</param>
        /// <returns></returns>
        public T GetMyccountDetails<T>(int time = 5000) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementDisplayed(MyaccountLocator);
            DriverExtensions.Click(MyaccountLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// The is checkbox selected.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsCheckboxSelected() => DriverExtensions.GetElement(ShowWelcomeCheckBoxInContractExpressWill).Selected;

        /// <summary>
        /// Check if the checkbox is not selected.
        /// </summary>
        /// <returns>True if the checkbox is selected; otherwise, false.</returns>
        public bool Clickcheckbox()
        {
            IWebElement checkboxElement = DriverExtensions.GetElement(ShowWelcomeCheckBoxInContractExpressWill);
            DriverExtensions.GetElement(checkboxElement).Click();
            return checkboxElement.Selected;
        }

        /// <summary>
        /// Check if the checkbox is displayed.
        /// </summary>
        /// <returns>True if the checkbox is displayed; otherwise, false.</returns>
        public bool IsCheckboxDisplayed()
        {
            return DriverExtensions.IsDisplayed(CheckboxLocatorwelcome);
        }

    }

}