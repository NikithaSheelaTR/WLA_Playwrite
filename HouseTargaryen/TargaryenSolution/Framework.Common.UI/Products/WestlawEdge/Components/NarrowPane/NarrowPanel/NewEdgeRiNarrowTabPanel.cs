namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Elements.NarrowPanel;
    using Framework.Common.UI.Products.WestlawEdge.Enums.NarrowPanel;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// New Ri Narrow Pane affects only the following Ri tabs in Edge:
    /// Citing References,
    /// Professional References, 
    /// Court Docs,
    /// Medical References, 
    /// Filings, 
    /// References Cites,
    /// Cited With
    /// </summary>
    public class NewEdgeRiNarrowTabPanel : TabPanel<RiNarrowTab>
    {
        private static readonly By CurrentActiveTabLocator =
           By.XPath(".//li[contains(@role, 'tab') and @class = 'Tab Tab--active ']");

        private static readonly By NarrowComponentLocator = By.XPath("//*[@id = 'co_leftColumn']");
        private static readonly By ToggleButtonLocator = By.Id("co_collapseActionLeft");
        private static readonly By ToggleStateLocator = By.XPath(".//div[@class = 'co_innertube']");

        /// <summary>
        /// Initializes a new instance of the <see cref="NewEdgeRiNarrowTabPanel"/> class. 
        /// </summary>
        public NewEdgeRiNarrowTabPanel()
        {
            this.AllPossibleTabOptions =
                new Dictionary<RiNarrowTab, Type>
                    {
                        { RiNarrowTab.RiContentTypes, typeof(RiContentTypesTabComponent) },
                        { RiNarrowTab.RiFilters, typeof(RiFiltersTabComponent) }
                    };
        }

        /// <summary>
        /// Narrow Panel Toggle      
        /// </summary>
        public IToggle NarrowPanelToggle =>
            new NarrowPanelToggle(
                ToggleButtonLocator,
                new ByChained(NarrowComponentLocator, ToggleStateLocator),
                "display",
                "block");

        /// <summary>
        /// Browse Tab Tabs Mapper
        /// </summary>
        protected override EnumPropertyMapper<RiNarrowTab, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<RiNarrowTab, WebElementInfo>(
                string.Empty,
                @"Resources/EnumPropertyMaps/WestlawEdge/NarrowPanel");

        /// <summary>
        /// Verify that tab is active
        /// </summary>
        /// <param name="tab"> Tab to verify </param>
        /// <returns> True if tab is active, false otherwise </returns>
        public override bool IsActive(RiNarrowTab tab)
        {
            IWebElement activeTab = DriverExtensions.SafeGetElement(
                DriverExtensions.GetElement(NarrowComponentLocator),
                CurrentActiveTabLocator);

            return activeTab?.Text.Equals(this.TabsMap[tab].Text) ?? false;
        }

        /// <summary>
        /// Verify that tab is displayed
        /// </summary>
        /// <param name="tab"> Tab to verify </param>
        /// <returns> True if tab is displayed, false otherwise </returns>
        public override bool IsDisplayed(RiNarrowTab tab) =>
            DriverExtensions.IsDisplayed(By.XPath(this.TabsMap[tab].LocatorString), 5);
    }
}