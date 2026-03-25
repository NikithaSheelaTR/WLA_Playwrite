namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage
{
    using System;
    using System.Collections.Generic;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;
    using ContentTypesTabPanel = WestlawEdge.Components.BrowseWidget.ContentTypesTabPanel;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage.HomePageTabs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP;

    /// <summary>
    /// Browse Component
    /// </summary>
    public class PrecisionBrowseTabPanel : TabPanel<PrecisionBrowseTab>
    {
        private static readonly By CurrentActiveTabLocator = By.XPath("//li[(@class = 'Tab Tab--active')]");
        private static readonly By TabLabelLocator = By.XPath("//*[@id='coid_browseTabs']//*[contains(@id, 'tab')]");
        private static readonly By GearButtonLocator = By.XPath("//*[@class='icon25 icon_gear-gray']");

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecisionBrowseTabPanel"/> class. 
        /// </summary>
        public PrecisionBrowseTabPanel()
        {
            this.ActiveTab = new KeyValuePair<PrecisionBrowseTab, BaseTabComponent>(PrecisionBrowseTab.PrecisionResearch, new PrecisionResearchTabPanel());
            this.AllPossibleTabOptions = new Dictionary<PrecisionBrowseTab, Type>
                                             {
                                                 {
                                                     PrecisionBrowseTab.AiAssistedResearch,
                                                     typeof(AiAssistedResearchTabPanel)
                                                 },
                                                 {
                                                     PrecisionBrowseTab.Tools,
                                                     typeof(PrecisionToolsTabPanel)
                                                 },
                                                 {
                                                     PrecisionBrowseTab.ContentTypes,
                                                     typeof(ContentTypesTabPanel)
                                                 },
                                                 {
                                                     PrecisionBrowseTab.StateMaterials,
                                                     typeof(StateMaterialsTabPanel)
                                                 },
                                                 {
                                                     PrecisionBrowseTab.FederalMaterials,
                                                     typeof(FederalMaterialsTabPanel)
                                                 },
                                                 {
                                                     PrecisionBrowseTab.PracticeAreas,
                                                     typeof(PracticeAreasTabPanel)
                                                 },
                                                 {
                                                     PrecisionBrowseTab.MyContent,
                                                     typeof(MyContentTabPanel)
                                                 },
                                                 {
                                                     PrecisionBrowseTab.PrecisionResearch,
                                                     typeof(PrecisionResearchTabPanel)
                                                 }
                                             };
        }

        /// <summary>
        /// Tab labels
        /// </summary>
        public IReadOnlyCollection<ILabel> TabLabels => new ElementsCollection<Label>(TabLabelLocator);
        
        /// <summary>
        /// Gear button
        /// </summary>
        public IButton DefaultTabGearButtonLocator => new Button(GearButtonLocator);

        /// <summary>
        /// Browse Tab Tabs Mapper
        /// </summary>
        protected override EnumPropertyMapper<PrecisionBrowseTab, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<PrecisionBrowseTab, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/BrowseComponent");

        /// <summary>
        /// Is Tab Active
        /// </summary>
        /// <param name="tab">tab</param>
        /// <returns>true if expected tab is active</returns>
        public override bool IsActive(PrecisionBrowseTab tab)
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
        public override bool IsDisplayed(PrecisionBrowseTab tab) => DriverExtensions.IsDisplayed(By.Id(this.TabsMap[tab].LocatorString));
    }
}
