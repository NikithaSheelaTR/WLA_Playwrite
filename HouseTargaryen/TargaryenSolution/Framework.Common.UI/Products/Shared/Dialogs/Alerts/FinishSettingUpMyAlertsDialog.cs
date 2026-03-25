namespace Framework.Common.UI.Products.Shared.Dialogs.Alerts
{
    using Framework.Common.UI.Interfaces.Elements;

    using OpenQA.Selenium;

    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    /// <summary>
    /// Finish Setting Up My Alerts Dialog 
    /// </summary>
    public class FinishSettingUpMyAlertsDialog : BaseModuleRegressionDialog
    {
        private static readonly By UseAnExistingAlertButtonLocator = By.Id("ExistingAlertsButton");

        private static readonly By CreateAlertButtonLocator = By.Id("CreateAlertButton"); 

        private static readonly By CreateCovid19LegalMaterialsAndNewsAlertButtonLocator = By.Id("CreateInsightsAlertButton");

        private static readonly By MyContactsFieldLocator = By.ClassName("co_contacts_addedContactsInput"); 

        private static readonly By MyContactsEmailFieldLocator = By.Id("coid_contacts_autoSuggest_input");

        /// <summary>
        /// Use An Existing Alert Button
        /// </summary>
        public IButton UseAnExistingAlertButton { get; } = new Button(UseAnExistingAlertButtonLocator);

        /// <summary>
        /// Create Alert Button
        /// </summary>
        public IButton CreateAlertButton { get; } = new Button(CreateAlertButtonLocator);

        /// <summary>
        /// Create A Covid19 Legal Materials And News Alert Button
        /// </summary>
        public IButton CreateCovid19LegalMaterialsAndNewsAlertButton { get; } = new Button(CreateCovid19LegalMaterialsAndNewsAlertButtonLocator);

        /// <summary>
        /// Set My Contacts
        /// </summary>
        public FinishSettingUpMyAlertsDialog SetMyContacts(string text)
        {
            DriverExtensions.WaitForElement(MyContactsFieldLocator).Click();
            DriverExtensions.WaitForElement(MyContactsEmailFieldLocator).SendKeys(text);

            return new FinishSettingUpMyAlertsDialog();
        }
    }
}