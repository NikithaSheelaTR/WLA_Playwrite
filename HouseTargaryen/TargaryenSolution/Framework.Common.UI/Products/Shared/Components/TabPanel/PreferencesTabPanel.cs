namespace Framework.Common.UI.Products.Shared.Components.TabPanel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components.Preferences;
    using Framework.Common.UI.Products.Shared.Enums.Preferences;
    using Framework.Common.UI.Products.WestlawEdge.Components.Preferences;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Preferences Tab Panel
    /// </summary>
    public sealed class PreferencesTabPanel : TabPanel<PreferencesDialogTabs>
    {
        private static readonly By CurrentActiveTab =
            By.XPath("//li[contains(@class,'co_tabActive') and contains(@id, 'coid_userSettingsTab')]/div/h2/a");

        private static readonly By TabsLocator = By.XPath("//div[@id='coid_userSettingsTabs'] /ul/li");

        /// <summary>
        /// Initializes a new instance of the <see cref="PreferencesTabPanel"/> class. 
        /// </summary>
        public PreferencesTabPanel()
        {
            this.ActiveTab = new KeyValuePair<PreferencesDialogTabs, BaseTabComponent>(
                PreferencesDialogTabs.Profile,
                new ProfileTabComponent());
            this.AllPossibleTabOptions = new Dictionary<PreferencesDialogTabs, Type>
                                             {
                                                 {
                                                     PreferencesDialogTabs.Profile,
                                                     typeof(ProfileTabComponent)
                                                 },
                                                 {
                                                     PreferencesDialogTabs.Billing,
                                                     typeof(BillingTabComponent)
                                                 },
                                                 {
                                                     PreferencesDialogTabs.Search,
                                                     typeof(SearchTabComponent)
                                                 },
                                                 {
                                                     PreferencesDialogTabs.History,
                                                     typeof(HistoryTabComponent)
                                                 },
                                                 {
                                                     PreferencesDialogTabs
                                                         .Features,
                                                     typeof(FeaturesTabComponent
                                                     )
                                                 },
                                                 {
                                                     PreferencesDialogTabs
                                                         .Delivery,
                                                     typeof(DeliveryTabComponent
                                                     )
                                                 },
                                                 {
                                                     PreferencesDialogTabs
                                                         .Citations,
                                                     typeof(
                                                         CitationsTabComponent)
                                                 },
                                                 {
                                                     PreferencesDialogTabs.Test,
                                                     typeof(TestTabComponent)
                                                 },
                                                 {
                                                     PreferencesDialogTabs.Help,
                                                     typeof(EdgeHelpTabComponent)
                                                 }
                                             };
        }

        /// <summary>
        /// Get All Available Tabs
        /// </summary>
        /// <returns> Displayed tabs</returns>
        public List<PreferencesDialogTabs> GetAvailableTabs()
        {
            return
                DriverExtensions.GetElements(TabsLocator)
                                .Select(el => el.Text.GetEnumValueByText<PreferencesDialogTabs>())
                                .ToList();
        }

        /// <summary>
        /// Is Tab Active
        /// </summary>
        /// <param name="tab">tab</param>
        /// <returns>true if expected tab is active</returns>
        public override bool IsActive(PreferencesDialogTabs tab)
        {
            string activeTabText = DriverExtensions.WaitForElement(CurrentActiveTab).Text;
            string tabToCheckText = this.TabsMap[tab].Text;
            return activeTabText.Equals(tabToCheckText);
        }

        /// <summary>
        /// Is Tab Displayed
        /// </summary>
        /// <param name="tab"> tab </param>
        /// <returns> true if tab is displayed</returns>
        public override bool IsDisplayed(PreferencesDialogTabs tab)
        {
            return DriverExtensions.IsDisplayed(By.LinkText(this.TabsMap[tab].Text));
        }
    }
}