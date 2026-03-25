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
    /// Narrow Tab Panel
    /// </summary>
    public class NarrowTabPanel : TabPanel<NarrowTab>
    {
        private static readonly By CurrentActiveTabLocator =
           By.XPath(".//li[contains(@role, 'tab') and @class = 'Tab Tab--active']");

        private static readonly By NarrowComponentLocator = By.XPath("//*[@id = 'co_leftColumn']");
        private static readonly By ToggleButtonLocator = By.Id("co_collapseActionLeft");
        private static readonly By ToggleStateLocator = By.XPath(".//div[@class = 'co_innertube']");

        /// <summary>
        /// Initializes a new instance of the <see cref="NarrowTabPanel"/> class. 
        /// </summary>
        public NarrowTabPanel()
        {          
            this.AllPossibleTabOptions =
                new Dictionary<NarrowTab, Type>
                    {
                        { NarrowTab.ContentTypes, typeof(ContentTypesTabComponent) },
                        { NarrowTab.Filters, typeof(FiltersTabComponent) }
                    };
        }

        /// <summary>
        /// Narrow Panel Toggle      
        /// </summary>
        public IToggle NarrowPanelToggle => new NarrowPanelToggle(ToggleButtonLocator, new ByChained(NarrowComponentLocator, ToggleStateLocator),"display", "block");

        /// <summary>
        /// Improved Filter Tooltip
        /// </summary>
        public EdgeImprovedFilterTooltipComponent FilterTooltip => new EdgeImprovedFilterTooltipComponent();

        /// <summary>
        /// Verify that tab is active
        /// </summary>
        /// <param name="tab"> Tab to verify </param>
        /// <returns> True if tab is active, false otherwise </returns>
        public override bool IsActive(NarrowTab tab)
        {
            var activeTab = DriverExtensions.SafeGetElement(DriverExtensions.GetElement(NarrowComponentLocator), CurrentActiveTabLocator);
            if(activeTab == null)
            {
                return false;
            }
            else
            {
                string activeTabText = activeTab.Text;
                string tabToCheckText = this.TabsMap[tab].Text;
                return activeTabText.Equals(tabToCheckText);
            }            
        }

        /// <summary>
        /// Verify that tab is displayed
        /// </summary>
        /// <param name="tab"> Tab to verify </param>
        /// <returns> True if tab is displayed, false otherwise </returns>
        public override bool IsDisplayed(NarrowTab tab) =>
            DriverExtensions.IsDisplayed(By.XPath(this.TabsMap[tab].LocatorString), 5);

        /// <summary>
        /// Browse Tab Tabs Mapper
        /// </summary>
        protected override EnumPropertyMapper<NarrowTab, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<NarrowTab, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdge/NarrowPanel");
    }
}
