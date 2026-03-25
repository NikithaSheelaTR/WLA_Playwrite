namespace Framework.Common.UI.Products.WestlawEdge.Components.LegalAnalytics
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawAdvantage.Components.LitigationAnalytics;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.LegalAnalytics;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Litigation Analytics Profile Tab
    /// </summary>
    public sealed class LitigationAnalyticsProfileTabPanel : TabPanel<ProfileComponentTab>
    {
        private static readonly By CurrentActiveTab =
            By.XPath("//*[@class='la-Layout-tabWrap']//*[@class='Tab Tab--active']");

        /// <summary>
        /// LitigationAnalyticsProfileTabPanel ctor
        /// </summary>
        public LitigationAnalyticsProfileTabPanel()
        {
            this.ActiveTab = new KeyValuePair<ProfileComponentTab, BaseTabComponent>(ProfileComponentTab.Overview, new ProfileTabComponent());
            this.AllPossibleTabOptions = new Dictionary<ProfileComponentTab, Type>
            {
                {
                    ProfileComponentTab.KnowYourJudge,
                    typeof(KnowYourJudgeTabComponent)
                },
                {
                    ProfileComponentTab.Overview,
                    typeof(ProfileTabComponent)
                },
                {
                    ProfileComponentTab.Experience,
                    typeof(HistoryTabComponent)
                },
                {
                    ProfileComponentTab.Motions,
                    typeof(MotionsTabComponent)
                },
                {
                    ProfileComponentTab.References,
                    typeof(ReferencesTabComponent)
                }
            };
        }

        /// <summary>
        /// Graph Tab Tabs Mapper
        /// </summary>
        protected override EnumPropertyMapper<ProfileComponentTab, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<ProfileComponentTab, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdge/LegalAnalytics");

        /// <summary>
        /// Is Tab Active
        /// </summary>
        /// <param name="tab">tab</param>
        /// <returns>true if expected tab is active</returns>
        public override bool IsActive(ProfileComponentTab tab)
            => this.TabsMap[tab].Text.Equals(DriverExtensions.WaitForElement(CurrentActiveTab).Text);

        /// <summary>
        /// Is Tab Displayed
        /// </summary>
        /// <param name="tab"> tab </param>
        /// <returns> true if tab is displayed</returns>
        public override bool IsDisplayed(ProfileComponentTab tab) =>
            DriverExtensions.IsDisplayed(By.Id(this.TabsMap[tab].LocatorString));
    }
}
