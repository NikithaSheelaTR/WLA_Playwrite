namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Execution;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Litigation Analytics Profile Tab
    /// </summary>
    public sealed class LitigationAnalyticsProfileTabPanel : TabPanel<ProfileComponentTab>
    {
        private static readonly By CurrentActiveTab = By.XPath("//*[@class='la-Layout-tabWrap']//*[@class='Tab Tab--active']//span");

        /// <summary>
        /// LitigationAnalyticsProfileTabPanel ctor
        /// </summary>
        public LitigationAnalyticsProfileTabPanel()
        {            
            this.ActiveTab = new KeyValuePair<ProfileComponentTab, BaseTabComponent>(ProfileComponentTab.Overview, new OverviewProfileTabComponent());
            this.AllPossibleTabOptions = new Dictionary<ProfileComponentTab, Type>
            {
                {
                    ProfileComponentTab.Overview,
                    typeof(OverviewProfileTabComponent)
                },
                {
                    ProfileComponentTab.Experience,
                    typeof(ExperienceProfileTabComponent)
                },
                {
                    ProfileComponentTab.Motions,
                    typeof(MotionsProfileTabComponent)
                },
                {
                    ProfileComponentTab.Damages,
                    typeof(DamagesProfileTabComponent)
                },
                {
                    ProfileComponentTab.References,
                    typeof(ReferencesProfileTabComponent)
                },
                {
                    ProfileComponentTab.Outcomes,
                    typeof(OutcomesProfileTabComponent)
                },
                {
                    ProfileComponentTab.ExpertChallenges,
                    typeof(ExpertChallengesTabComponent)
                },
                 {
                    ProfileComponentTab.Deals,
                    typeof(DealsProfileTabComponent)
                },
                 {
                    ProfileComponentTab.Patents,
                    typeof(PatentsProfileTabComponent)
                }
            };
        }
       
        /// <summary>
        /// Graph Tab Tabs Mapper
        /// </summary>
        protected override EnumPropertyMapper<ProfileComponentTab, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<ProfileComponentTab, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/LitigationAnalytics");

        /// <summary>
        /// Is Tab Active
        /// </summary>
        /// <param name="tab">tab</param>
        /// <returns>true if expected tab is active</returns>
        public override bool IsActive(ProfileComponentTab tab)
            => this.TabsMap[tab].Text.Equals(DriverExtensions.WaitForElement(CurrentActiveTab).Text);

        /// <summary>
        /// ProfileTabPanelTitle
        /// </summary>
        public string CurrentTabName => DriverExtensions.GetText(CurrentActiveTab);

        /// <summary>
        /// Is Tab Displayed
        /// </summary>
        /// <param name="tab"> tab </param>
        /// <returns> true if tab is displayed</returns>
        public override bool IsDisplayed(ProfileComponentTab tab) =>
            DriverExtensions.IsDisplayed(By.Id(this.TabsMap[tab].LocatorString));
    }
}