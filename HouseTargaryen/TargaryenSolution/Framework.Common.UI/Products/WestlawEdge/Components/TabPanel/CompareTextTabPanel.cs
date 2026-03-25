namespace Framework.Common.UI.Products.WestlawEdge.Components.TabPanel
{
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Components.CompareText;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.CompareText;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Compare text tab panel
    /// </summary>
    public class CompareTextTabPanel : TabPanel<CompareTextTabs>
    {
        private static readonly By CurrentActiveTabLocator = By.XPath("//ul[@aria-label= 'Compare text tabs.']//li[contains(@class,'Tab--active')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareTextTabPanel"/> class
        /// </summary>
        public CompareTextTabPanel()
        {
            this.AllPossibleTabOptions = new Dictionary<CompareTextTabs, Type>
                                             {
                                                 { CompareTextTabs.SelectToCompare, typeof(SelectToCompareTab) },
                                                 { CompareTextTabs.ViewSavedComparisons, typeof(ViewSavedComparisonsTab) }
                                             };
        }

        /// <summary>
        /// Gets the CompareTextTabs enumeration
        /// </summary>
        protected override EnumPropertyMapper<CompareTextTabs, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<CompareTextTabs, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdge/CompareText");

        /// <summary>
        /// Is tab active
        /// </summary>
        /// <param name="tab">Tab option</param>
        /// <returns>True if active, false otherwise</returns>
        public override bool IsActive(CompareTextTabs tab)
            => DriverExtensions.WaitForElement(CurrentActiveTabLocator).Text.Equals(this.TabsMap[tab].Text);

        /// <summary>
        /// Is tab displayed
        /// </summary>
        /// <param name="tab">Tab option</param>
        /// <returns>True if displayed, false otherwise</returns>
        public override bool IsDisplayed(CompareTextTabs tab)
            => DriverExtensions.IsDisplayed(By.XPath(this.TabsMap[tab].LocatorString));
    }
}