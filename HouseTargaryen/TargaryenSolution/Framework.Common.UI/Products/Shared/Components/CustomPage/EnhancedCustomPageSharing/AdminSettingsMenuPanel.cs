namespace Framework.Common.UI.Products.Shared.Components.CustomPage.EnhancedCustomPageSharing
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Enums.EnhancedCustomPageSharing;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Admin Settings Menu Panel
    /// </summary>
    public class AdminSettingsMenuPanel : TabPanel<AdminSettingsTabs>
    {
        private static readonly By CurrentActiveTab =
            By.XPath("//ul[@id='co_adminMenuTabList']/li[contains(@class, 'active')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminSettingsMenuPanel"/> class. 
        /// </summary>
        public AdminSettingsMenuPanel()
        {
            this.AllPossibleTabOptions = new Dictionary<AdminSettingsTabs, Type>
                                             {
                                                     { AdminSettingsTabs.PageSetup, typeof(PageSetupTabComponent) },
                                                     { AdminSettingsTabs.Assign, typeof(AssignTabComponent) }
                                             };
        }

        /// <summary>
        /// Is Tab Active
        /// </summary>
        /// <param name="tab">tab</param>
        /// <returns>true if expected tab is active</returns>
        public override bool IsActive(AdminSettingsTabs tab)
            => DriverExtensions.WaitForElement(CurrentActiveTab).Text.Equals(this.TabsMap[tab].Text);

        /// <summary>
        /// Is Tab Displayed
        /// </summary>
        /// <param name="tab"> tab </param>
        /// <returns> true if tab is displayed</returns>
        public override bool IsDisplayed(AdminSettingsTabs tab)
            => DriverExtensions.IsDisplayed(By.Id(this.TabsMap[tab].Id));

        /// <summary>
        /// Is Tab Disabled
        /// </summary>
        /// <param name="tab"> tab </param>
        /// <returns> true if tab is displayed</returns>
        public bool IsDisabled(AdminSettingsTabs tab)
            => DriverExtensions.GetElement(By.Id(this.TabsMap[tab].Id)).GetAttribute("class").Contains("co_disabled");
    }
}
