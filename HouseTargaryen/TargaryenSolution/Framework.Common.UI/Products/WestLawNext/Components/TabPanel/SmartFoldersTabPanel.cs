namespace Framework.Common.UI.Products.WestLawNext.Components.TabPanel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Enums.Document;
    using Framework.Common.UI.Products.WestLawNext.Components.SearchResults;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Tab panel component for smart folders page
    /// </summary>
    public class SmartFoldersTabPanel : TabPanel<DocumentTypeTabs>
    {
        private static readonly By CurrentActiveTabLocator = By.XPath("//li[contains(@class, 'co_tabActive')]//div[@class='co_tabRight']//a[@class='co_tabLink ng-binding']");

        private static readonly By TabsLocator = By.XPath("//div[@class='co_genericBoxTabs']//div[@class='co_tabRight']/a[not(contains(text(), '(0)'))]");

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartFoldersTabPanel"/> class. 
        /// </summary>
        public SmartFoldersTabPanel()
        {
            this.ActiveTab = new KeyValuePair<DocumentTypeTabs, BaseTabComponent>(
                DocumentTypeTabs.Cases,
                new CasesTabComponent());
            this.AllPossibleTabOptions = new Dictionary<DocumentTypeTabs, Type>
                                             {
                                                 {
                                                     DocumentTypeTabs.Cases,
                                                     typeof(CasesTabComponent)
                                                 },
                                                 {
                                                     DocumentTypeTabs.SecondarySources,
                                                     typeof(SecondarySourcesTabComponent)
                                                 },
                                                 {
                                                     DocumentTypeTabs.StatutesAndCourtRules,
                                                     typeof(StatutesTabComponent)
                                                 }
                                             };
        }

        /// <summary>
        /// Get All Available Tabs
        /// </summary>
        /// <returns> Displayed tabs</returns>
        public List<DocumentTypeTabs> GetAvailableTabs() =>
                DriverExtensions.GetElements(TabsLocator)
                                .Select(el => el.Text.Remove(el.Text.LastIndexOf("(") - 1).GetEnumValueByText<DocumentTypeTabs>())
                                .ToList();

        /// <summary>
        /// Verifies if Statutes Tab with number is displayed and enabled
        /// </summary>
        /// <returns>The <see cref="int"/> of documents</returns>
        public int GetTabCount(DocumentTypeTabs tab) =>
            DriverExtensions.GetText(By.XPath(this.TabsMap[tab].LocatorString)).RetrieveCountFromBrackets();

        /// <summary>
        /// Is Tab Active
        /// </summary>
        /// <param name="tab">tab</param>
        /// <returns>true if expected tab is active</returns>
        public override bool IsActive(DocumentTypeTabs tab) =>
            DriverExtensions.GetText(CurrentActiveTabLocator).Contains(this.TabsMap[tab].Text);

        /// <summary>
        /// Is Tab Displayed
        /// </summary>
        /// <param name="tab"> tab </param>
        /// <returns> true if tab is displayed</returns>
        public override bool IsDisplayed(DocumentTypeTabs tab) =>
            DriverExtensions.IsDisplayed(By.XPath(this.TabsMap[tab].LocatorString));
    }
}
