namespace Framework.Common.UI.Products.WestlawEdge.Components.History
{
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.History;
    using Framework.Core.Utils.Enums;

    /// <summary>
    /// History tab panel
    /// </summary>
    public class HistoryTabPanel : TabPanel<HistoryTabs>
    {
        private static readonly By CurrentActiveTabLocator = By.XPath("//ul[contains(@class, 'HistoryTabHeader')]//li[contains(@class,'co_tabActive')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoryTabPanel"/> class
        /// </summary>
        public HistoryTabPanel()
        {
            this.AllPossibleTabOptions = new Dictionary<HistoryTabs, Type>
                                             {
                                                 { HistoryTabs.ListView, typeof(CurrentHistoryTabComponent) },
                                                 { HistoryTabs.GraphicalView, typeof(GraphicalHistoryTabComponent) }
                                             };
        }

        /// <summary>
        /// History tabs map
        /// </summary>
        protected override EnumPropertyMapper<HistoryTabs, WebElementInfo> TabsMap 
            => EnumPropertyModelCache.GetMap<HistoryTabs, WebElementInfo>(string.Empty,
            @"Resources/EnumPropertyMaps/WestlawEdge");

        /// <summary>
        /// Is tab active
        /// </summary>
        /// <param name="tab">Tab option</param>
        /// <returns>True if active, false otherwise</returns>
        public override bool IsActive(HistoryTabs tab)
            => DriverExtensions.WaitForElement(CurrentActiveTabLocator).Text.Equals(this.TabsMap[tab].Text);

        /// <summary>
        /// Is tab displayed
        /// </summary>
        /// <param name="tab">Tab option</param>
        /// <returns>True if displayed, false otherwise</returns>
        public override bool IsDisplayed(HistoryTabs tab)
            => DriverExtensions.IsDisplayed(By.XPath(this.TabsMap[tab].LocatorString));
        }
}