namespace Framework.Common.UI.Products.WestlawEdge.Components.Preferences
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Components.Preferences;
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Components.NotificationCenter;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Preferences;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Indigo Preferences Tab Panel
    /// </summary>
    public class EdgePreferencesTabPanel : TabPanel<EdgePreferencesDialogTabs>
    {
        private static readonly By CurrentActiveTabLocator =
            By.XPath("//li[contains(@class,'Tab Tab--active')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="EdgePreferencesTabPanel"/> class. 
        /// </summary>
        public EdgePreferencesTabPanel()
        {
            this.ActiveTab = new KeyValuePair<EdgePreferencesDialogTabs, BaseTabComponent>(
                EdgePreferencesDialogTabs.Profile,
                new EdgeProfileTabComponent());
            this.AllPossibleTabOptions = new Dictionary<EdgePreferencesDialogTabs, Type>
            {
                {
                    EdgePreferencesDialogTabs.Profile, typeof(EdgeProfileTabComponent)
                },
                {
                    EdgePreferencesDialogTabs.Billing, typeof(EdgeBillingTabComponent)
                },
                {
                    EdgePreferencesDialogTabs.Search, typeof(EdgeSearchTabComponent)
                },
                {
                    EdgePreferencesDialogTabs.History, typeof(EdgeHistoryTabComponent)
                },
                {
                    EdgePreferencesDialogTabs.Features, typeof(EdgeFeaturesTabComponent)
                },
                {
                    EdgePreferencesDialogTabs.Delivery, typeof(EdgeDeliveryTabComponent)
                },
                {
                    EdgePreferencesDialogTabs.Citations, typeof(EdgeCopyWithReferenceTabComponent)
                },
                {
                    EdgePreferencesDialogTabs.CopyWithReference, typeof(EdgeCopyWithReferenceTabComponent)
                },
                {
                    EdgePreferencesDialogTabs.Test, typeof(TestTabComponent)
                },
                {
                    EdgePreferencesDialogTabs.Notifications, typeof(NotificationsPreferencesTabComponent)
                },
                {
                    EdgePreferencesDialogTabs.Help, typeof(EdgeHelpTabComponent)
                }
            };
        }

        /// <summary>
        /// Gets the EdgePreferencesDialogTabs enumeration
        /// </summary>
        protected override EnumPropertyMapper<EdgePreferencesDialogTabs, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<EdgePreferencesDialogTabs, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdge/Preferences");

        /// <summary>
        /// Is Tab Active
        /// </summary>
        /// <param name="tab">tab</param>
        /// <returns>true if expected tab is active</returns>
        public override bool IsActive(EdgePreferencesDialogTabs tab)
        {
            string activeTabText = DriverExtensions.WaitForElement(CurrentActiveTabLocator).Text;
            string tabToCheckText = this.TabsMap[tab].Text;
            return activeTabText.Equals(tabToCheckText);
        }

        /// <summary>
        /// Is Tab Displayed
        /// </summary>
        /// <param name="tab"> tab </param>
        /// <returns> true if tab is displayed</returns>
        public override bool IsDisplayed(EdgePreferencesDialogTabs tab)
            => DriverExtensions.IsDisplayed(By.XPath(this.TabsMap[tab].LocatorString));
    }  
}
