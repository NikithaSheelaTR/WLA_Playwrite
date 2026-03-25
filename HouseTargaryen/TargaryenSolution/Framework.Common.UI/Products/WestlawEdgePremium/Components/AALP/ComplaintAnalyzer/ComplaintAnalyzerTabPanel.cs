namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.ComplaintAnalyzer
{
    using System.Collections.Generic;
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.AALP.ComplaintAnalyzer;

    /// <summary>
    /// Complaint Analyzer Tab Panel
    /// </summary>
    public class ComplaintAnalyzerTabPanel : TabPanel<ComplaintAnalyzerTabs>
    {
        private static readonly By CurrentActiveTabLocator = By.XPath("//li[(@class = 'Tab Tab--active')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecisionBrowseTabPanel"/> class. 
        /// </summary>
        public ComplaintAnalyzerTabPanel()
        {
            this.ActiveTab = new KeyValuePair<ComplaintAnalyzerTabs, BaseTabComponent>(ComplaintAnalyzerTabs.UploadADocument, new UploadADocumentTab());
            this.AllPossibleTabOptions = new Dictionary<ComplaintAnalyzerTabs, Type>
                                             {
                                                 {
                                                     ComplaintAnalyzerTabs.UploadADocument,
                                                     typeof(UploadADocumentTab)
                                                 },
                                                 {
                                                     ComplaintAnalyzerTabs.EnterComplaintText,
                                                     typeof(EnterComplaintTextTab)
                                                 }
                                             };
        }

        /// <summary>
        /// Browse Tab Tabs Mapper
        /// </summary>
        protected override EnumPropertyMapper<ComplaintAnalyzerTabs, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<ComplaintAnalyzerTabs, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/AALP/ComplaintAnalyzer");

        /// <summary>
        /// Is Tab Active
        /// </summary>
        /// <param name="tab">tab</param>
        /// <returns>true if expected tab is active</returns>
        public override bool IsActive(ComplaintAnalyzerTabs tab)
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
        public override bool IsDisplayed(ComplaintAnalyzerTabs tab) => DriverExtensions.IsDisplayed(By.Id(this.TabsMap[tab].Id));
    }
}
