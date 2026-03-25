namespace Framework.Common.UI.Products.WestlawAdvantage.Components.KnowYourJudge
{
    using System;
    using System.Collections.Generic;
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawAdvantage.Enums;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// Know your judge Tab Panel
    /// </summary>
    public class KnowYourJudgeReportTabPanel : TabPanel<KnowYourJudgeReportTabs>
    {
        private static readonly By CurrentActiveTabLocator = By.XPath("//saf-tab-v3[(@aria-selected='true')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="KnowYourJudgeReportTabPanel"/> class. 
        /// </summary>
        public KnowYourJudgeReportTabPanel()
        {
            this.ActiveTab = new KeyValuePair<KnowYourJudgeReportTabs, BaseTabComponent>(KnowYourJudgeReportTabs.ClaimBased, new ClaimsBasedTab());
            this.AllPossibleTabOptions = new Dictionary<KnowYourJudgeReportTabs, Type>
                                             {
                                                 {
                                                     KnowYourJudgeReportTabs.ClaimBased,
                                                     typeof(ClaimsBasedTab)
                                                 },
                                                 {
                                                     KnowYourJudgeReportTabs.FactBased,
                                                     typeof(FactBasedTab)
                                                 },
                                                 {
                                                    KnowYourJudgeReportTabs.PrecedentBased,
                                                    typeof(PrecedentBasedTab)
                                                 },
                                                 {
                                                    KnowYourJudgeReportTabs.OrderSummary,
                                                    typeof(OrderSummaryTab)
                                                 }
                                             };
        }

        /// <summary>
        /// Result tabs Mapper
        /// </summary>
        protected override EnumPropertyMapper<KnowYourJudgeReportTabs, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<KnowYourJudgeReportTabs, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawAdvantage");

        /// <summary>
        /// Is Tab Active
        /// </summary>
        /// <param name="tab">tab</param>
        /// <returns>true if expected tab is active</returns>
        public override bool IsActive(KnowYourJudgeReportTabs tab)
        {
            string activeTabText = DriverExtensions.WaitForElement(CurrentActiveTabLocator).Text;
            string tabToCheckText = this.TabsMap[tab].Text;
            return activeTabText.Contains(tabToCheckText);
        }

        /// <summary>
        /// Is Tab Displayed
        /// </summary>
        /// <param name="tab"> tab </param>
        /// <returns> true if tab is displayed</returns>
        public override bool IsDisplayed(KnowYourJudgeReportTabs tab) => DriverExtensions.IsDisplayed(By.Id(this.TabsMap[tab].Id));
    }
}

