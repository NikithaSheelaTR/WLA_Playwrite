namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage.SelectDefaultTabComponents
{
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Select default tab panel
    /// </summary>
    public class PrecisionSelectDefaultTabPanel : TabPanel<HomePageSelectDefaultTab>
    {
        private static readonly By CurrentActiveTabLocator = By.XPath("//li[(@class = 'Tab Tab--active')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecisionSelectDefaultTabPanel"/> class. 
        /// </summary>
        public PrecisionSelectDefaultTabPanel()
        {
            this.AllPossibleTabOptions =
                new Dictionary<HomePageSelectDefaultTab, Type>
                    {
                        { HomePageSelectDefaultTab.ForMe, typeof(ForMeTabComponent) },
                        { HomePageSelectDefaultTab.ForMyOrganization, typeof(ForMyOrganizationTabComponent) }
                    };
        }

        /// <summary>
        /// Browse Tab Tabs Mapper
        /// </summary>
        protected override EnumPropertyMapper<HomePageSelectDefaultTab, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<HomePageSelectDefaultTab, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/HomePage");

        /// <summary>
        /// Is tab displayed
        /// </summary>
        /// <param name="tab">Tab to verify</param>
        /// <returns>True if tab is displayed, false otherwise</returns>
        public override bool IsDisplayed(HomePageSelectDefaultTab tab) =>
            DriverExtensions.IsDisplayed(By.XPath(this.TabsMap[tab].LocatorString), 5);

        /// <summary>
        /// Is Tab Active
        /// </summary>
        /// <param name="tab">tab</param>
        /// <returns>true if expected tab is active</returns>
        public override bool IsActive(HomePageSelectDefaultTab tab)
        {
            string activeTabText = DriverExtensions.WaitForElement(CurrentActiveTabLocator).Text;
            string tabToCheckText = this.TabsMap[tab].Text;
            return activeTabText.Equals(tabToCheckText);
        }
    }
}
