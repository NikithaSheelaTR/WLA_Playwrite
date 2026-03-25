namespace Framework.Common.UI.Products.WestlawEdge.Components.LegalAnalytics
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.LegalAnalytics;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// The LegalAnalyticsTabPanel
    /// </summary>
    public sealed class LegalAnalyticsTabPanel : TabPanel<GraphComponentTab>
    {
        private static readonly By CurrentActiveTab =
          By.ClassName("active");

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphComponentTab"/> class. 
        /// </summary>
        public LegalAnalyticsTabPanel()
        {
            this.AllPossibleTabOptions = new Dictionary<GraphComponentTab, Type>
                                             {
                                                 {
                                                     GraphComponentTab.Outcome,
                                                     typeof(OutcomeTabComponent)
                                                 },
                                                 {
                                                     GraphComponentTab.TimeToRule,
                                                     typeof(TimeToRuleTabComponent)
                                                 }
                                             };
        }

        /// <summary>
        /// Graph Tab Tabs Mapper
        /// </summary>
        protected override EnumPropertyMapper<GraphComponentTab, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<GraphComponentTab, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdge/LegalAnalytics");

        /// <summary>
        /// Is Tab Active
        /// </summary>
        /// <param name="tab">tab</param>
        /// <returns>true if expected tab is active</returns>
        public override bool IsActive(GraphComponentTab tab)
        {
            string activeTabText = DriverExtensions.WaitForElement(CurrentActiveTab).Text;
            string tabToCheckText = this.TabsMap[tab].Text;
            return activeTabText.Equals(tabToCheckText);
        }

        /// <summary>
        /// Is Tab Displayed
        /// </summary>
        /// <param name="tab"> tab </param>
        /// <returns> true if tab is displayed</returns>
        public override bool IsDisplayed(GraphComponentTab tab) =>
                DriverExtensions.IsDisplayed(By.Id(this.TabsMap[tab].Id));       
    }
}
