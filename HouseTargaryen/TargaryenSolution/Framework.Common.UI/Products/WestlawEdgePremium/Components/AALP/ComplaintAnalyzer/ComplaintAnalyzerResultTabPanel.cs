namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.ComplaintAnalyzer
{
    using System;
    using System.Collections.Generic;
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.AALP.ComplaintAnalyzer;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// Complaint Analyzer Results Tab Panel
    /// </summary>
    public class ComplaintAnalyzerResultTabPanel : TabPanel<ComplaintAnalyzerResultTabs>
    {
        private static readonly By CurrentActiveTabLocator = By.XPath("//li[(@class = 'Tab Tab--active')]");

        private readonly IWebElement containerElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecisionBrowseTabPanel"/> class. 
        /// </summary>
        public ComplaintAnalyzerResultTabPanel(IWebElement containerElement)
        {
            this.containerElement = containerElement;
            this.ActiveTab = new KeyValuePair<ComplaintAnalyzerResultTabs, BaseTabComponent>(ComplaintAnalyzerResultTabs.Summary, new SummaryTab());
            this.AllPossibleTabOptions = new Dictionary<ComplaintAnalyzerResultTabs, Type>
                                             {
                                                 {
                                                     ComplaintAnalyzerResultTabs.Summary,
                                                     typeof(SummaryTab)
                                                 },
                                                 {
                                                     ComplaintAnalyzerResultTabs.Claims,
                                                     typeof(ClaimsTab)
                                                 },
                                                 {
                                                     ComplaintAnalyzerResultTabs.EventTimeline,
                                                     typeof(EventTimelineTab)
                                                 },
                                                 {
                                                     ComplaintAnalyzerResultTabs.Defenses,
                                                     typeof(DefensesTab)
                                                 }
                                             };
        }

        /// <summary>
        /// Browse Tab Tabs Mapper
        /// </summary>
        protected override EnumPropertyMapper<ComplaintAnalyzerResultTabs, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<ComplaintAnalyzerResultTabs, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/AALP/ComplaintAnalyzer");

        /// <summary>
        /// Is Tab Active
        /// </summary>
        /// <param name="tab">tab</param>
        /// <returns>true if expected tab is active</returns>
        public override bool IsActive(ComplaintAnalyzerResultTabs tab)
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
        public override bool IsDisplayed(ComplaintAnalyzerResultTabs tab) => DriverExtensions.IsDisplayed(By.Id(this.TabsMap[tab].Id));
    }
}
