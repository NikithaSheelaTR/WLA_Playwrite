namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsHomePageComponents.Tabs
{
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsHomePageComponents.Entities;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Litigation Analytics home page Entities Tab Component.
    /// </summary>
    public class LitigationAnalyticsEntitiesTabsComponent : TabPanel<LitigationAnalyticsProfiles>
    {
        private static readonly By CurrentActiveTab =
            By.XPath("//*[@class='co_tabs Tab-list']//li[@aria-selected='true']");

        /// <summary>
        /// Gets the Litigation Analytics tabs enumeration to WebElementInfo map.
        /// </summary>
        protected override EnumPropertyMapper<LitigationAnalyticsProfiles, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<LitigationAnalyticsProfiles, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/LitigationAnalytics");

        /// <summary>
        /// LitigationAnalyticsHeaderComponent ctor
        /// </summary>
        public LitigationAnalyticsEntitiesTabsComponent()
        {
            this.AllPossibleTabOptions = new Dictionary<LitigationAnalyticsProfiles, Type>
            {
                {
                    LitigationAnalyticsProfiles.Attorneys,
                    typeof(LitigationAnalyticsAttorneysEntityTabComponent)
                },
                {
                    LitigationAnalyticsProfiles.Companies,
                    typeof(LitigationAnalyticsCompaniesEntityTabComponent)
                },
                {
                    LitigationAnalyticsProfiles.Damages,
                    typeof(LitigationAnalyticsDamagesEntityTabComponent)
                },
                {
                    LitigationAnalyticsProfiles.Judges,
                    typeof(LitigationAnalyticsJudgesEntityTabComponent)
                },
                {
                    LitigationAnalyticsProfiles.LawFirms,
                    typeof(LitigationAnalyticsLawFirmsEntityTabComponent)
                },
                {
                    LitigationAnalyticsProfiles.CaseTypes,
                    typeof(LitigationAnalyticsCaseTypeEntityTabComponent)
                },
                {
                    LitigationAnalyticsProfiles.Courts,
                    typeof(LitigationAnalyticsCourtsEntityTabComponent)
                },
                {
                    LitigationAnalyticsProfiles.OpportunityFinder,
                    typeof(LitigationAnalyticsOpportunityFinderEntityTabComponent)
                }
            };
        }

        /// <summary>
        /// IsActive
        /// </summary>
        /// <param name="tab"></param>
        /// <returns>bool</returns>
        public override bool IsActive(LitigationAnalyticsProfiles tab) =>
           this.TabsMap[tab].Text.Equals(DriverExtensions.WaitForElement(CurrentActiveTab).Text);

        /// <summary>
        /// IsDisplayed
        /// </summary>
        /// <param name="tab"></param>
        /// <returns>bool</returns>
        public override bool IsDisplayed(LitigationAnalyticsProfiles tab) =>
          DriverExtensions.IsDisplayed(By.Id(this.TabsMap[tab].LocatorString));
    }
}