namespace Framework.Common.UI.Products.WestLawNext.Pages.IphoneMode
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Enums.IphoneMode;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// PreferencesPage class
    /// </summary>
    public class PreferencesPage : BaseModuleRegressionPage
    {
        private static readonly By SelectedAlertNameLocator = By.XPath("//div[@id = 'coid_notifications_preferences_alerts_list']//input[@checked]/../b");

        private static readonly By SaveButtonLocator = By.Id("coid_notifications_preferences_saveButton");
        
        private static readonly By ProgressIndicatorLocator = By.Id("coid_ProgressIndicator");

        private EnumPropertyMapper<IphoneModePreferences, WebElementInfo> preferenceModeMap;
        

        /// <summary>
        /// Initializes a new instance of the <see cref="PreferencesPage"/> class. 
        /// </summary>
        public PreferencesPage()
        {
            DriverExtensions.WaitForElement(By.XPath(this.PreferenceModeMap[IphoneModePreferences.Alerts].LocatorString));
            DriverExtensions.WaitForElement(SaveButtonLocator);
        }

        /// <summary>
        /// Preference mode map
        /// </summary>
        protected EnumPropertyMapper<IphoneModePreferences, WebElementInfo> PreferenceModeMap
            =>
                this.preferenceModeMap =
                    this.preferenceModeMap ?? EnumPropertyModelCache.GetMap<IphoneModePreferences, WebElementInfo>();

        /// <summary>
        /// Gets the selected alerts.
        /// </summary>
        /// <returns>selected alerts</returns>
        public List<string> GetSelectedAlerts() =>
            DriverExtensions.GetElements(SelectedAlertNameLocator).Select(a => a.Text).ToList();

        /// <summary>
        /// Saves the preferences.
        /// </summary>
        public void SavePreferences()
        {
            DriverExtensions.WaitForElementDisplayed(SaveButtonLocator).Click();
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForElementNotDisplayed(ProgressIndicatorLocator);
        }

        /// <summary>
        /// Sets the preference.
        /// </summary>
        /// <param name="preference"> The preference </param>
        /// <param name="state"> The state </param>
        /// <returns> The <see cref="PreferencesPage"/>. </returns>
        public PreferencesPage SetPreference(IphoneModePreferences preference, bool state = true)
        {
            DriverExtensions.SetCheckbox(By.XPath(this.PreferenceModeMap[preference].LocatorString), state);
            return this;
        }
    }
}