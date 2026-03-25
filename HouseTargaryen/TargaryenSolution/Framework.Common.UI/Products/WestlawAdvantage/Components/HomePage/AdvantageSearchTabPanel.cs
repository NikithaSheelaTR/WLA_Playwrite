using Framework.Common.UI.Products.Shared.Components.TabPanel;
using Framework.Common.UI.Products.Shared.Models.EnumProperties;
using Framework.Common.UI.Products.WestlawAdvantage.Enums;
using Framework.Common.UI.Products.WestLawNext.Components;
using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
using Framework.Core.Utils.Enums;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Framework.Common.UI.Products.WestlawAdvantage.Components.HomePage
{
    /// <summary>
    /// Tab punnel for DeepResearch and Global search
    /// </summary>
    public class AdvantageSearchTabPanel : TabPanel<AdvantageSearchTabs>
    {

        private static readonly By CurrentActiveTabLocator = By.XPath("//saf-tab[@aria-selected='true']");

        /// <summary>
        /// Advantage Browse Tab Panel
        /// </summary>
        public AdvantageSearchTabPanel()
        {
            this.ActiveTab = new KeyValuePair<AdvantageSearchTabs, BaseTabComponent>(AdvantageSearchTabs.AIDeepResearch, new AIDeepResearchTabPanel());
            this.AllPossibleTabOptions = new Dictionary<AdvantageSearchTabs, Type>
                                             {
                                                 {
                                                     AdvantageSearchTabs.AIDeepResearch,
                                                     typeof(AIDeepResearchTabPanel)
                                                 },
                                                 {
                                                     AdvantageSearchTabs.KeywordAndBooleanSearch,
                                                     typeof(KeywordAndBooleanSearchTabPanel)
                                                 }
                                             };
        }

        /// <summary>
        /// Browse Tab Tabs Mapper
        /// </summary>
        protected override EnumPropertyMapper<AdvantageSearchTabs, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<AdvantageSearchTabs, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawAdvantage");

        /// <summary>
        /// IsActive
        /// </summary>
        /// <param name="tab"></param>
        /// <returns>true if active false if not</returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool IsActive(AdvantageSearchTabs tab)
        {
            string activeTabText = DriverExtensions.WaitForElement(CurrentActiveTabLocator).Text;
            string tabToCheckText = this.TabsMap[tab].Text;
            return activeTabText.Contains(tabToCheckText);
        }

        /// <summary>
        /// IsDisplayed
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool IsDisplayed(AdvantageSearchTabs tab) => DriverExtensions.IsDisplayed(By.Id(this.TabsMap[tab].LocatorString));
    }
}
