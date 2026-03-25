namespace Framework.Common.UI.Products.WestlawEdge.Components.TabPanel
{
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Components.CompareText;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.CompareText;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Compare text report tab
    /// </summary>
    public class CompareTextReportTabPanel : TabPanel<CompareTextReportTabs>
    {
        private static readonly By CurrentActiveTabLocator = By.XPath("//li[contains(@class,'Tab--active')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareTextReportTabPanel"/> class
        /// </summary>
        public CompareTextReportTabPanel()
        {
            this.ActiveTab = new KeyValuePair<CompareTextReportTabs, BaseTabComponent>(CompareTextReportTabs.SideBySideView, new SideBySideViewTab());
            this.AllPossibleTabOptions = new Dictionary<CompareTextReportTabs, Type>
                                             {
                                                 { CompareTextReportTabs.SideBySideView, typeof(SideBySideViewTab) },
                                                 { CompareTextReportTabs.CompareView, typeof(CompareViewTab) }
            };
        }

        /// <summary>
        /// Gets the CompareTextReportTabs enumeration
        /// </summary>
        protected override EnumPropertyMapper<CompareTextReportTabs, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<CompareTextReportTabs, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdge/CompareText");

        /// <summary>
        /// Is tab active
        /// </summary>
        /// <param name="tab">Tab option</param>
        /// <returns>True if active, false otherwise</returns>
        public override bool IsActive(CompareTextReportTabs tab)
            => DriverExtensions.WaitForElement(CurrentActiveTabLocator).Text.Equals(this.TabsMap[tab].Text);

        /// <summary>
        /// Is tab displayed
        /// </summary>
        /// <param name="tab">Tab option</param>
        /// <returns>True if displayed, false otherwise</returns>
        public override bool IsDisplayed(CompareTextReportTabs tab)
            => DriverExtensions.IsDisplayed(By.XPath(this.TabsMap[tab].LocatorString));
    }
}