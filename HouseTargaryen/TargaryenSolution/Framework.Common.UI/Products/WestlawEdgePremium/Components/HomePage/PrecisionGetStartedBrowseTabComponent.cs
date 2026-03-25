namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage
{
    using System;
    using System.Collections.Generic;
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Get Started Browse Component
    /// </summary>
    public class PrecisionGetStartedBrowseTabComponent : TabPanel<PrecisionGetStartedBrowseTab>
    {
        private static readonly By CurrentActiveTabLocator = By.XPath("//li[@class = 'co_tabLeft co_tabActive']");

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecisionGetStartedBrowseTabComponent"/> class. 
        /// </summary>
        public PrecisionGetStartedBrowseTabComponent()
        {
            this.ActiveTab = new KeyValuePair<PrecisionGetStartedBrowseTab, BaseTabComponent>(PrecisionGetStartedBrowseTab.ContentTypes, new PrecisionGetStartedContentTypesTabComponent());
            this.AllPossibleTabOptions = new Dictionary<PrecisionGetStartedBrowseTab, Type>
                                             {
                                                 {
                                                     PrecisionGetStartedBrowseTab.Tools,
                                                     typeof(PrecisionGetStartedToolsTabComponent)
                                                 },
                                                 {
                                                     PrecisionGetStartedBrowseTab.ContentTypes,
                                                     typeof(PrecisionGetStartedContentTypesTabComponent)
                                                 },
                                                 {
                                                     PrecisionGetStartedBrowseTab.StateMaterials,
                                                     typeof(PrecisionGetStartedStateMaterialsTabComponent)
                                                 },
                                                 {
                                                     PrecisionGetStartedBrowseTab.FederalMaterials,
                                                     typeof(PrecisionGetStartedFederalMaterialsTabComponent)
                                                 },
                                                 {
                                                     PrecisionGetStartedBrowseTab.PracticeAreas,
                                                     typeof(PrecisionGetStartedPracticeAreasTabComponent)
                                                 },
                                                 {
                                                     PrecisionGetStartedBrowseTab.CustomPages,
                                                     typeof(PrecisionGetStartedCustomPagesTabComponent)
                                                 },
                                                 {
                                                     PrecisionGetStartedBrowseTab.Favorites,
                                                     typeof(PrecisionGetStartedFavoritesTabComponent)
                                                 }
                                             };
        }

        /// <summary>
        /// Get Started Browse Tab Tabs Mapper
        /// </summary>
        protected override EnumPropertyMapper<PrecisionGetStartedBrowseTab, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<PrecisionGetStartedBrowseTab, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/BrowseComponent");

        /// <summary>
        /// Is Get Started Tab Active
        /// </summary>
        /// <param name="tab">tab</param>
        /// <returns>true if expected tab is active</returns>
        public override bool IsActive(PrecisionGetStartedBrowseTab tab)
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
        public override bool IsDisplayed(PrecisionGetStartedBrowseTab tab) => DriverExtensions.IsDisplayed(By.Id(this.TabsMap[tab].Id));
    }
}
