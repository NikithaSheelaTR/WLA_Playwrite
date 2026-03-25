namespace Framework.Common.UI.Products.TaxnetPro.Components.Preferences
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.TaxnetPro.Enums.HomePage;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Taxnet Pro Preferences Tab Panel
    /// </summary>
    public class TaxnetProPreferencesTabPanel : TabPanel<TaxnetProPreferencesDialogTabs>
    {
        private static readonly By CurrentActiveTabLocator = By.XPath("//li[contains(@class,'co_tabActive')]");


        /// <summary>
        /// Initializes a new instance of the <see cref="TaxnetProPreferencesTabPanel"/> class. 
        /// </summary>
        public TaxnetProPreferencesTabPanel()
        {
            this.AllPossibleTabOptions = new Dictionary<TaxnetProPreferencesDialogTabs, Type>
            {
                { TaxnetProPreferencesDialogTabs.LanguageSearch, typeof(LanguageSearchTabComponent) }
            };
        }

        /// <summary>
        /// Gets the EdgePreferencesDialogTabs enumeration
        /// </summary>
        protected override EnumPropertyMapper<TaxnetProPreferencesDialogTabs, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<TaxnetProPreferencesDialogTabs, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/TaxnetPro");

        /// <summary>
        /// Is Tab Active
        /// </summary>
        /// <param name="tab">tab</param>
        /// <returns>true if expected tab is active</returns>
        public override bool IsActive(TaxnetProPreferencesDialogTabs tab)
        {
            string activeTabText = DriverExtensions.WaitForElement(CurrentActiveTabLocator).Text;
            string tabToCheckText = this.TabsMap[tab].Text;
            return activeTabText.Equals(tabToCheckText);
        }

        /// <summary>
        /// Is Tab Displayed
        /// </summary>
        /// <param name="tab"> tab </param>
        /// <returns> true if tab is displayed</returns>
        public override bool IsDisplayed(TaxnetProPreferencesDialogTabs tab)
            => DriverExtensions.IsDisplayed(By.XPath(this.TabsMap[tab].LocatorString));
    }
}