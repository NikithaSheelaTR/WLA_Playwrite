namespace Framework.Common.UI.Products.WestLawAnalytics.Pages.Settings
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawAnalytics.Models.BusinessObjects;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The features settings page.
    /// </summary>
    public class FeaturesSettingsPage : BaseModuleRegressionPage
    {
        private static readonly By AllowAttorneysToChooseChargeableNonchargeableCheckboxLocator = By.XPath("//input[@id = 'wa_settingsUserSettingsShowNonChargeable']");

        private static readonly By AllowAttorneysToSubmitResearchDescriptionCheckboxLocator = By.XPath("//input[@id = 'wa_settingsUserSettingsShowResearchDescription']");

        private static readonly By EnableClientsAndMattersCheckboxLocator = By.XPath("//input[@id = 'wa_settingsUserSettingsShowDescriptiveNames']");

        private static readonly By EnablePracticeAreaDesignationCheckboxLocator = By.XPath("//input[@id = 'wa_settingsUserSettingsShowPracticeArea']");

        private static readonly By InfoMessageLocator = By.XPath("//div[@class='co_infoBox_message']");

        private static readonly By SelectAllTopLinkLocator = By.XPath("//a[@id='wa_settingsUserSettingsSelectAllStorageIdsTop']");

        private static readonly By SaveButtonLocator = By.XPath("//button[@id='wa_settingsUserSettingsSaveFeaturePreferencesButton']");

        private static readonly By EnableFeaturseContentLocator = By.XPath("//div[@id='wa_settingsMainContent']");

        private static readonly By ChargeableLocator = By.XPath("//div[contains(@class, 'Chargeable')]");

        private static readonly By ClientMatterLocator = By.XPath("//div[contains(@class, 'ClientMatterDescription')]");

        private static readonly By PracticeAreaLocator = By.XPath("//div[contains(@class, 'PracticeArea')]");

        private static readonly By ResearchDescriptionLocator = By.XPath("//div[contains(@class, 'ResearchDescription')]");
        
        /// <summary>
        /// Initializes a new instance of the <see cref="FeaturesSettingsPage"/> class. 
        /// </summary>
        public FeaturesSettingsPage()
        {
           DriverExtensions.WaitForElement(EnableFeaturseContentLocator);
        }

        /// <summary>
        /// This method selects all the features and clicks select all button, then clicks the save features button.
        /// </summary>
        /// <param name="featuresModel"> The features Model. </param>
        public void SetFeatures(AnalyticsFeaturesModel featuresModel)
        {
            DriverExtensions.SetCheckbox(EnableClientsAndMattersCheckboxLocator, featuresModel.ClientMatter);
            DriverExtensions.SetCheckbox(EnablePracticeAreaDesignationCheckboxLocator, featuresModel.PracticeArea);
            DriverExtensions.SetCheckbox(AllowAttorneysToSubmitResearchDescriptionCheckboxLocator, featuresModel.ResearchDescription);
            DriverExtensions.SetCheckbox(AllowAttorneysToChooseChargeableNonchargeableCheckboxLocator, featuresModel.ChargeableToClient);

            DriverExtensions.WaitForElement(SelectAllTopLinkLocator).Click();
            DriverExtensions.WaitForElement(SaveButtonLocator).Click();
        }

        /// <summary>
        /// Get Features List
        /// </summary>
        /// <param name="accountNumber"> Account number (start with 1) </param>
        /// <returns> The <see cref="AnalyticsFeaturesModel"/>. </returns>
        public AnalyticsFeaturesModel GetFeatureByIndex(int accountNumber)
            =>
                new AnalyticsFeaturesModel
                    {
                        ClientMatter = this.IsFeatureEnabled(ClientMatterLocator, accountNumber),
                        PracticeArea = this.IsFeatureEnabled(PracticeAreaLocator, accountNumber),
                        ResearchDescription = this.IsFeatureEnabled(ResearchDescriptionLocator, accountNumber),
                        ChargeableToClient = this.IsFeatureEnabled(ChargeableLocator, accountNumber)
                    };

        /// <summary>
        /// This method verifies that when a firm is selected the correct message appears showing that their feature setting were saved
        /// </summary>
        /// <returns>True if the correct message appears when saving feature settings</returns>
        public string GetInfoMessageText() => DriverExtensions.WaitForElementDisplayed(InfoMessageLocator).GetText();

        private bool IsFeatureEnabled(By featureLocator, int accountNumber)
            => DriverExtensions.GetElements(featureLocator).ElementAt(accountNumber + 1).Text.Contains("Enabled");
    }
}