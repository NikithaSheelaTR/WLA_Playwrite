namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts.Tabs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// ChartHeaderComponent
    /// </summary>
    public class HeaderCategoryTabPanel : TabPanel<LitigationAnalyticsChartHeaderTab>
    {
        private static readonly By CurrentActiveTabLocator = By.XPath("//div[@class= 'la-Layout-chartContainer']");
        private static readonly By TabLocator = By.XPath("//li[contains(@id, 'TableLabel')]");
        private static readonly By NextNavigationButtonLocator = By.XPath("//button[contains(@class,'co_next Tab-navigation-button')]");
        private static readonly By PreviousNavigationButtonLocator = By.XPath("//button[@class, 'co_prev Tab-navigation-button'");
       
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderCategoryTabPanel"/> class
        /// </summary>
        public HeaderCategoryTabPanel()
        {
            DriverExtensions.WaitForElementDisplayed(CurrentActiveTabLocator, 400000);
            this.AllPossibleTabOptions = new Dictionary<LitigationAnalyticsChartHeaderTab, Type>
            {
                { LitigationAnalyticsChartHeaderTab.Participants, typeof(ParticipantsTabComponent) },
                { LitigationAnalyticsChartHeaderTab.CaseType, typeof(CaseTypeTabComponent) },
                { LitigationAnalyticsChartHeaderTab.Volume, typeof(VolumeTabComponent) },
                { LitigationAnalyticsChartHeaderTab.TransactionStatus, typeof(TransactionStatusTabComponent) },
                { LitigationAnalyticsChartHeaderTab.Distribution, typeof(DistributionTabComponent) },
                { LitigationAnalyticsChartHeaderTab.FilingRole, typeof(FilingRoleTabComponent) },
                { LitigationAnalyticsChartHeaderTab.Year, typeof(YearTabComponent) },
                { LitigationAnalyticsChartHeaderTab.Lawfirm, typeof(LawfirmTabComponent) },
                { LitigationAnalyticsChartHeaderTab.ConsiderationOffered, typeof(ConsiderationOfferedTabComponent) }
        };
        }

        /// <summary>
        /// Previous Navigation Button
        /// </summary>
        public IButton PreviousNavigationButton => new Button(PreviousNavigationButtonLocator);

        /// <summary>
        /// Previous Navigation Button
        /// </summary>
        public IButton NextNavigationButton => new Button(NextNavigationButtonLocator);

        /// <summary>
        /// Chart tabs map
        /// </summary>
        protected override EnumPropertyMapper<LitigationAnalyticsChartHeaderTab, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<LitigationAnalyticsChartHeaderTab, WebElementInfo>(
                string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/LitigationAnalytics");

        /// <summary>
        /// Is tab active
        /// </summary>
        /// <param name="tab">Tab option</param>
        /// <returns>True if active, false otherwise</returns>
        public override bool IsActive(LitigationAnalyticsChartHeaderTab tab)
            => DriverExtensions.WaitForElement(CurrentActiveTabLocator).Text.Contains(this.TabsMap[tab].Text);

        /// <summary>
        /// Is tab displayed
        /// </summary>
        /// <param name="tab">Tab option</param>
        /// <returns>True if displayed, false otherwise</returns>
        public override bool IsDisplayed(LitigationAnalyticsChartHeaderTab tab)
            => DriverExtensions.IsDisplayed(By.XPath(this.TabsMap[tab].LocatorString));

        /// <summary>
        /// Get list of displayed Tabs
        /// </summary>
        /// <returns>True if active, false otherwise</returns>
        public List<String> GetListOfDisplayedTabs
            => DriverExtensions.GetElements(TabLocator).Select(e => e.Text).ToList();
    }
}