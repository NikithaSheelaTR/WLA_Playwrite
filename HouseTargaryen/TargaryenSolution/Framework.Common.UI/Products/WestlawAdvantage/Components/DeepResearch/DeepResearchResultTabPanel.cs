namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
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
    /// Deep Research Results Tab Panel
    /// </summary>
    public class DeepResearchResultTabPanel : TabPanel<DeepResearchResultTabs>
    {
        private static readonly By CurrentActiveTabLocator = By.XPath("//saf-tab-v3[(@aria-selected='true')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="DeepResearchResultTabPanel"/> class. 
        /// </summary>
        public DeepResearchResultTabPanel()
        {
            this.ActiveTab = new KeyValuePair<DeepResearchResultTabs, BaseTabComponent>(DeepResearchResultTabs.Report, new ReportTab());
            this.AllPossibleTabOptions = new Dictionary<DeepResearchResultTabs, Type>
                                             {
                                                 {
                                                     DeepResearchResultTabs.Report,
                                                     typeof(ReportTab)
                                                 },
                                                 {
                                                     DeepResearchResultTabs.Verify,
                                                     typeof(VerifyTab)
                                                 },
                                                 {
                                                     DeepResearchResultTabs.FollowUp,
                                                     typeof(FollowUpTab)
                                                 },
                                                 {
                                                     DeepResearchResultTabs.Sources,
                                                     typeof(SourcesTab)
                                                 },
                                                 {
                                                    DeepResearchResultTabs.CasesOnBothSides,
                                                    typeof(CasesOnBothSidesTab)
                                                 },
                                                 { 
                                                    DeepResearchResultTabs.Enhance,
                                                    typeof(EnhanceTab)
                                                 },
                                                 {
                                                    DeepResearchResultTabs.ResearchSteps,
                                                    typeof(ResearchStepsTab)
                                                 }
                                             };
        }

        /// <summary>
        /// Result tabs Mapper
        /// </summary>
        protected override EnumPropertyMapper<DeepResearchResultTabs, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<DeepResearchResultTabs, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawAdvantage");

        /// <summary>
        /// Is Tab Active
        /// </summary>
        /// <param name="tab">tab</param>
        /// <returns>true if expected tab is active</returns>
        public override bool IsActive(DeepResearchResultTabs tab)
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
        public override bool IsDisplayed(DeepResearchResultTabs tab) => DriverExtensions.IsDisplayed(By.Id(this.TabsMap[tab].Id));
    }
}

