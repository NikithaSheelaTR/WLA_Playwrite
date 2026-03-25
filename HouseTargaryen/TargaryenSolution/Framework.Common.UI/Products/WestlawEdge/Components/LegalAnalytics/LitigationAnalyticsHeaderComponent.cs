namespace Framework.Common.UI.Products.WestlawEdge.Components.LegalAnalytics
{
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Enums.LegalAnalytics;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Litigation Analytics Header Component
    /// </summary>
    public class LitigationAnalyticsHeaderComponent : TabPanel<LitigationAnalyticsTabs>
    {     
        private static readonly By CurrentActiveTab =
            By.XPath("//*[@class='co_tabs Tab-list']//li[@aria-selected='true']");       

        /// <summary>
        /// Gets the Litigation Analytics tabs enumeration to WebElementInfo map.
        /// </summary>
        protected override EnumPropertyMapper<LitigationAnalyticsTabs, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<LitigationAnalyticsTabs, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdge/LegalAnalytics");

        /// <summary>
        /// LitigationAnalyticsHeaderComponent ctor
        /// </summary>
        public LitigationAnalyticsHeaderComponent()
        {
            this.AllPossibleTabOptions = new Dictionary<LitigationAnalyticsTabs, Type>
            {
                {
                    LitigationAnalyticsTabs.Judges,
                    typeof(JudgeTabComponent)
                }
            };
        }

        /// <summary>
        /// IsActive
        /// </summary>
        /// <param name="tab"></param>
        /// <returns>bool</returns>
        public override bool IsActive(LitigationAnalyticsTabs tab) =>        
           this.TabsMap[tab].Text.Equals(DriverExtensions.WaitForElement(CurrentActiveTab).Text);

        /// <summary>
        /// IsDisplayed
        /// </summary>
        /// <param name="tab"></param>
        /// <returns>bool</returns>
        public override bool IsDisplayed(LitigationAnalyticsTabs tab) =>
          DriverExtensions.IsDisplayed(By.Id(this.TabsMap[tab].LocatorString));
    }
}