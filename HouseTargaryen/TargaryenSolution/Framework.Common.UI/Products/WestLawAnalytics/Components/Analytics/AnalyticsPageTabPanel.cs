namespace Framework.Common.UI.Products.WestLawAnalytics.Components.Analytics
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.WestLawAnalytics.Enums;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Analytics Page Tab (Panel)
    /// </summary>
    public class AnalyticsPageTabPanel : TabPanel<AnalyticsPageTabs>
    {
        private static readonly By CurrentActiveTab =
            By.XPath("//ul[@class='wa_firmHealthStatisticNavigation wa_tabs']/li[@class='active']");

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsPageTabPanel"/> class. 
        /// </summary>
        public AnalyticsPageTabPanel()
        {
            this.ActiveTab = new KeyValuePair<AnalyticsPageTabs, BaseTabComponent>(AnalyticsPageTabs.InPlanVsOutOfPlan, new InPlanVsOutOfPlanTabComponent());
            this.AllPossibleTabOptions = new Dictionary<AnalyticsPageTabs, Type>
                                             {
                                                 {
                                                     AnalyticsPageTabs.InPlanVsOutOfPlan,
                                                     typeof(InPlanVsOutOfPlanTabComponent)
                                                 },
                                                 {
                                                     AnalyticsPageTabs.ChargeableToClientVsNotChargeableToClient,
                                                     typeof(ChargeableToClientVsNotChargeableToClientTabComponent)
                                                 }
                                             };
        }

        /// <summary>
        /// Verify that tab is active
        /// </summary>
        /// <param name="tab"> Tab to verify </param>
        /// <returns> True if tab is active, false otherwise </returns>
        public override bool IsActive(AnalyticsPageTabs tab)
        {
            string activeTabText = DriverExtensions.WaitForElement(CurrentActiveTab).Text;
            string tabToCheckText = this.TabsMap[tab].Text;
            return activeTabText.Equals(tabToCheckText);
        }

        /// <summary>
        /// Verify that tab is displayed
        /// </summary>
        /// <param name="tab"> Tab to verify </param>
        /// <returns> True if tab is displayed, false otherwise </returns>
        public override bool IsDisplayed(AnalyticsPageTabs tab)
            => DriverExtensions.IsDisplayed(By.XPath(this.TabsMap[tab].LocatorString), 5);
    }
}
