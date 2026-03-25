namespace Framework.Common.UI.Products.WestlawEdge.Components.NotificationCenter
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.NotificationCenter;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Notification Center Tab Panel
    /// </summary>
    public class NotificationCenterTabPanel : TabPanel<NotificationCenterTabs>
    {
        private static readonly By CurrentActiveTabLocator =
            By.XPath("//ul[contains(@class, 'NotificationTabHeader')]//li[contains(@class,'co_tabActive')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationCenterTabPanel"/> class. 
        /// </summary>
        public NotificationCenterTabPanel()
        {
            this.ActiveTab = new KeyValuePair<NotificationCenterTabs, BaseTabComponent>();
            this.AllPossibleTabOptions = new Dictionary<NotificationCenterTabs, Type>
                                             {
                                                 {
                                                     NotificationCenterTabs.Alerts,
                                                     typeof(AlertsTabComponent)
                                                 },
                                                 {
                                                     NotificationCenterTabs.Notifications,
                                                     typeof(NotificationsTabComponent)
                                                 }
                                             };
        }

        /// <summary>
        /// Gets the PreferencesDialogTabs enumeration
        /// </summary>
        protected override EnumPropertyMapper<NotificationCenterTabs, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<NotificationCenterTabs, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdge/NotificationCenter");

        /// <summary>
        /// Verify that tab is active
        /// </summary>
        /// <param name="tab"> Tab to verify </param>
        /// <returns> True if tab is active, false otherwise </returns>
        public override bool IsActive(NotificationCenterTabs tab)
        {
            string activeTabText = DriverExtensions.WaitForElement(CurrentActiveTabLocator).Text;
            string tabToCheckText = this.TabsMap[tab].Text;
            return activeTabText.Equals(tabToCheckText);
        }

        /// <summary>
        /// Verify that tab is displayed
        /// </summary>
        /// <param name="tab"> Tab to verify </param>
        /// <returns> True if tab is displayed, false otherwise </returns>
        public override bool IsDisplayed(NotificationCenterTabs tab)
            => DriverExtensions.IsDisplayed(By.XPath(this.TabsMap[tab].LocatorString), 5);
    }
}
