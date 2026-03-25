namespace Framework.Common.UI.Products.WestlawEdge.Components.Miscellaneous
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Raw.WestlawEdge.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Miscellaneous Tab Panel
    /// </summary>
    public class MiscellaneousTabPanel : TabPanel<MiscellaneousTabs>
    {
        private static readonly By CurrentActiveTabLocator =
            By.XPath("//div[@id = 'widget_div_miscellaneous']//li[contains(@class, 'active')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="MiscellaneousTabPanel"/> class. 
        /// </summary>
        public MiscellaneousTabPanel()
        {
            this.AllPossibleTabOptions =
                new Dictionary<MiscellaneousTabs, Type>
                    {
                        { MiscellaneousTabs.History, typeof(HistoryTabComponent) },
                        { MiscellaneousTabs.CustomPages, typeof(CustomPagesTabComponent) },
                        { MiscellaneousTabs.Favorites, typeof(FavoritesTabComponent) },
                        { MiscellaneousTabs.Folders, typeof(FoldersTabComponent) }
                    };
        }

        /// <summary>
        /// Verify that tab is active
        /// </summary>
        /// <param name="tab"> Tab to verify </param>
        /// <returns> True if tab is active, false otherwise </returns>
        public override bool IsActive(MiscellaneousTabs tab)
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
        public override bool IsDisplayed(MiscellaneousTabs tab) =>
            DriverExtensions.IsDisplayed(By.XPath(this.TabsMap[tab].LocatorString), 5);
    }
}
