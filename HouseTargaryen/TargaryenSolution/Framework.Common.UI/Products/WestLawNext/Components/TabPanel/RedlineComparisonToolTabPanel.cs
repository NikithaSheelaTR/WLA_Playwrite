namespace Framework.Common.UI.Products.WestLawNext.Components.TabPanel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenter;
    using Framework.Common.UI.Products.WestLawNext.Enums.BusinessLawCenter;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// RedlineComparisonToolTabPanel
    /// </summary>
    public class RedlineComparisonToolTabPanel : TabPanel<RedlineComparisonToolDialogTabs>
    {
        private static readonly By CurrentActiveTabLocator =
            By.XPath("//div[.//h3[contains(text(),'Redline Comparison Tool')]]//li[contains(@class,'co_tabActive')]");

        private static readonly By TabsLocator =
            By.XPath("//div[.//h3[contains(text(),'Redline Comparison Tool')]]//ul[@class='co_tabs']/li");

        /// <summary>
        /// Initializes a new instance of the <see cref="RedlineComparisonToolTabPanel"/> class. 
        /// </summary>
        public RedlineComparisonToolTabPanel()
            : this(
                new KeyValuePair<RedlineComparisonToolDialogTabs, BaseTabComponent>(
                    RedlineComparisonToolDialogTabs.SelectToCompare,
                    new SelectToCompareTabComponent()))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedlineComparisonToolTabPanel"/> class. 
        /// In some cases active tab is not SelectToCompare
        /// </summary>
        /// <param name="activeTab"> Active tab </param>
        public RedlineComparisonToolTabPanel(KeyValuePair<RedlineComparisonToolDialogTabs, BaseTabComponent> activeTab)
        {
            this.ActiveTab = activeTab;
            this.AllPossibleTabOptions = new Dictionary<RedlineComparisonToolDialogTabs, Type>
                                             {
                                                 {
                                                     RedlineComparisonToolDialogTabs.SelectToCompare,
                                                     typeof(SelectToCompareTabComponent)
                                                 },
                                                 {
                                                     RedlineComparisonToolDialogTabs.ViewSavedComparisons,
                                                     typeof(ViewSavedComparisonsTabComponent)
                                                 }
                                             };
        }

        /// <summary>
        /// Get All Available Tabs
        /// </summary>
        /// <returns> Displayed tabs</returns>
        public List<RedlineComparisonToolDialogTabs> GetAvailableTabs()
            =>
                DriverExtensions.GetElements(TabsLocator)
                                .Select(el => el.Text.GetEnumValueByText<RedlineComparisonToolDialogTabs>())
                                .ToList();

        /// <summary>
        /// Is tab active
        /// </summary>
        /// <param name="tab"> Tab option</param>
        /// <returns> True if active, false otherwise </returns>
        public override bool IsActive(RedlineComparisonToolDialogTabs tab)
            => DriverExtensions.WaitForElement(CurrentActiveTabLocator).Text.Equals(this.TabsMap[tab].Text);

        /// <summary>
        /// Is tab displayed
        /// </summary>
        /// <param name="tab"> Tab </param>
        /// <returns> True if displayed, false otherwise </returns>
        public override bool IsDisplayed(RedlineComparisonToolDialogTabs tab)
            => DriverExtensions.IsDisplayed(By.LinkText(this.TabsMap[tab].Text));
    }
}
