namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts.SubCharts.TransactionStatus
{
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Transaction Status Sub Chart Panel
    /// </summary>
    public class TransactionStatusSubChartPanel : TabPanel<LitigationAnalyticsContainerSubcategories>
    {
        private static readonly By CurrentActiveTabLocator = By.XPath("//div[contains(@class, 'la-ToggleButton')]//button[contains(@class,'active')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionStatusSubChartPanel"/> class
        /// </summary>
        public TransactionStatusSubChartPanel()
        {
            this.AllPossibleTabOptions = new Dictionary<LitigationAnalyticsContainerSubcategories, Type>
            {
                { LitigationAnalyticsContainerSubcategories.Distribution, typeof(DistributionSubChartTab) },
                { LitigationAnalyticsContainerSubcategories.CountTrend, typeof(CountTrendSubChartTab) }

            };
        }

        /// <summary>
        /// History tabs map
        /// </summary>
        protected override EnumPropertyMapper<LitigationAnalyticsContainerSubcategories, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<LitigationAnalyticsContainerSubcategories, WebElementInfo>(
                string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/LitigationAnalytics");

        /// <summary>
        /// Is tab active
        /// </summary>
        /// <param name="tab">Tab option</param>
        /// <returns>True if active, false otherwise</returns>
        public override bool IsActive(LitigationAnalyticsContainerSubcategories tab)
            => DriverExtensions.WaitForElement(CurrentActiveTabLocator).Text.Contains(this.TabsMap[tab].Text);

        /// <summary>
        /// Is tab displayed
        /// </summary>
        /// <param name="tab">Tab option</param>
        /// <returns>True if displayed, false otherwise</returns>
        public override bool IsDisplayed(LitigationAnalyticsContainerSubcategories tab)
            => DriverExtensions.IsDisplayed(By.XPath(this.TabsMap[tab].LocatorString));
    }
}