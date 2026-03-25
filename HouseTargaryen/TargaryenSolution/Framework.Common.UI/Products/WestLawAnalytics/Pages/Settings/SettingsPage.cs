namespace Framework.Common.UI.Products.WestLawAnalytics.Pages.Settings
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawAnalytics.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// WLN Analytics Settings Page
    /// </summary>
    public class SettingsPage : BaseModuleRegressionPage
    {
        private static readonly By CurrentActiveTabLocator =
            By.XPath("//ul[@class='wa_settingsNavigation wa_tabs']/li[@class='active']");

        /// <summary>
        /// Westlaw Analytics Settings Tabs Map
        /// </summary>
        private EnumPropertyMapper<WestlawAnalyticsSettingsTabs, WebElementInfo> settingsTabsMap;

        /// <summary>
        /// Gets the Westlaw Analytics Settings Tabs enumeration
        /// </summary>
        private EnumPropertyMapper<WestlawAnalyticsSettingsTabs, WebElementInfo> SettingsTabsMap
            =>
                this.settingsTabsMap =
                    this.settingsTabsMap
                    ?? EnumPropertyModelCache.GetMap<WestlawAnalyticsSettingsTabs, WebElementInfo>();

        /// <summary>
        /// Clicks settings tabs
        /// </summary>
        /// <typeparam name="T"> T page </typeparam>
        /// <param name="tab"> Tab </param>
        /// <returns> The T page </returns>
        public T ClickSettingsTab<T>(WestlawAnalyticsSettingsTabs tab) where T : ICreatablePageObject
        {
            DriverExtensions.Click(By.XPath(this.SettingsTabsMap[tab].LocatorString));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Verify that tab is active
        /// </summary>
        /// <param name="tab"> Tab to verify </param>
        /// <returns> True if tab is active, false otherwise </returns>
        public bool IsSettingsTabActive(WestlawAnalyticsSettingsTabs tab)
        {
            string activeTabText = DriverExtensions.WaitForElement(CurrentActiveTabLocator).Text;
            string tabToCheckText = this.SettingsTabsMap[tab].Text;
            return activeTabText.Equals(tabToCheckText);
        }

        /// <summary>
        /// Verify that tab is displayed
        /// </summary>
        /// <param name="tab"> Tab to verify </param>
        /// <returns> True if tab is active, false otherwise </returns>
        public bool IsTabDisplayed(WestlawAnalyticsSettingsTabs tab) =>
            DriverExtensions.IsDisplayed(By.XPath(this.SettingsTabsMap[tab].LocatorString));
    }
}
