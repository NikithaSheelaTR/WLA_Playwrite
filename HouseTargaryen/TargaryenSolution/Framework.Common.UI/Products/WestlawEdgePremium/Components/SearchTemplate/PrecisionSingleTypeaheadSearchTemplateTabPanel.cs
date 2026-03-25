namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.NewSearchTemplate
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Precision single typeahead search template Tab Panel
    /// </summary>
    public class PrecisionSingleTypeaheadSearchTemplateTabPanel : TabPanel<PrecisionSingleTypeaheadSearchTemplateTab>
    {
        private static readonly By CurrentActiveTabLocator =
           By.XPath("//div[@id='legalSimilaritySearchModal']//li[@class='Tab Tab--active']");

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecisionSingleTypeaheadSearchTemplateTabPanel"/> class. 
        /// </summary>
        public PrecisionSingleTypeaheadSearchTemplateTabPanel()
        {
            AllPossibleTabOptions =
                new Dictionary<PrecisionSingleTypeaheadSearchTemplateTab, Type>
                    {
                        { PrecisionSingleTypeaheadSearchTemplateTab.SearchAll, typeof(PrecisionSearchAllTabComponent) },
                        { PrecisionSingleTypeaheadSearchTemplateTab.SearchByAttribute, typeof(PrecisionSearchByAttributeTabComponent) }
                    };
        }

        /// <summary>
        /// Is tab active
        /// </summary>
        /// <param name="tab">Tab to verify</param>
        /// <returns>True if tab is active, false otherwise</returns>
        public override bool IsActive(PrecisionSingleTypeaheadSearchTemplateTab tab)
        {
            var activeTab = DriverExtensions.SafeGetElement(CurrentActiveTabLocator);
            if (activeTab == null)
            {
                return false;
            }
            else
            {
                string activeTabText = activeTab.Text;
                string tabToCheckText = TabsMap[tab].Text;
                return activeTabText.Equals(tabToCheckText);
            }
        }

        /// <summary>
        /// Is tab displayed
        /// </summary>
        /// <param name="tab">Tab to verify</param>
        /// <returns>True if tab is displayed, false otherwise</returns>
        public override bool IsDisplayed(PrecisionSingleTypeaheadSearchTemplateTab tab) =>
            DriverExtensions.IsDisplayed(By.XPath(TabsMap[tab].LocatorString), 5);

        /// <summary>
        /// Precision filters tab map
        /// </summary>
        protected override EnumPropertyMapper<PrecisionSingleTypeaheadSearchTemplateTab, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<PrecisionSingleTypeaheadSearchTemplateTab, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/");
    }
}
