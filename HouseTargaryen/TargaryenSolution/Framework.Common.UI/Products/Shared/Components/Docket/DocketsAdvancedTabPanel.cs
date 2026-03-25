namespace Framework.Common.UI.Products.Shared.Components.Docket
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Enums.Docket;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Dockets advanced tab panel
    /// </summary>
    public class DocketsAdvancedTabPanel : TabPanel<DocketsAdvancedTabs>
    {
        private static readonly By CurrentActiveTabLocator = By.XPath("//li[@id= 'co_search_advancedSearch_listItem_IXS']//li[contains(@class,'co_tabActive')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="DocketsAdvancedTabPanel"/> class. 
        /// </summary>
        public DocketsAdvancedTabPanel()
        {
            this.AllPossibleTabOptions = new Dictionary<DocketsAdvancedTabs, Type>
                                             {
                                                 {
                                                     DocketsAdvancedTabs.AdvancedSearch,
                                                     typeof(AdvancedSearchTab)
                                                 },
                                                 {
                                                     DocketsAdvancedTabs.SearchDocketsPdfsAndCourtFilings,
                                                     typeof(SearchDocketsPdfsAndCourtFilingsTab)
                                                 }
                                             };
        }

        /// <summary>
        /// Gets the DocketsAdvancedTabs enumeration
        /// </summary>
        protected override EnumPropertyMapper<DocketsAdvancedTabs, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<DocketsAdvancedTabs, WebElementInfo>();

        /// <summary>
        /// Is tab active
        /// </summary>
        /// <param name="tab">Tab option</param>
        /// <returns>True if active, false otherwise</returns>
        public override bool IsActive(DocketsAdvancedTabs tab)
            => DriverExtensions.WaitForElement(CurrentActiveTabLocator).Text.Contains(this.TabsMap[tab].Text);

        /// <summary>
        /// Is tab displayed
        /// </summary>
        /// <param name="tab">Tab option</param>
        /// <returns>True if displayed, false otherwise</returns>
        public override bool IsDisplayed(DocketsAdvancedTabs tab)
            => DriverExtensions.IsDisplayed(By.XPath(this.TabsMap[tab].LocatorString));
    }
}