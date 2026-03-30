namespace Framework.Common.UI.Products.WestlawAdvantage.Components.HomePage
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawAdvantage.Enums;
    using Framework.Common.UI.Products.WestlawAdvantage.Pages.DeepResearch;
    using Framework.Common.UI.Products.WestlawEdge.Components.BrowseWidget;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage.HomePageTabs;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Advantage Browse Tab Panel
    /// </summary>
    public class AdvantageBrowseTabPanel : TabPanel<AdvantageBrowseTab>
    {
        private static readonly By CurrentActiveTabLocator = By.XPath("//li[contains(@class, 'Tab--active') and contains(@class, 'f1-phase1-tab')]");
        private static readonly By TabLabelLocator = By.XPath("//*[@id='coid_browseTabs']//*[contains(@id, 'tab')]");
        private static readonly By GearButtonLocator = By.XPath("//*[@class='icon25 icon_gear-gray']");

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecisionBrowseTabPanel"/> class. 
        /// </summary>
        public AdvantageBrowseTabPanel()
        {
            this.ActiveTab = new KeyValuePair<AdvantageBrowseTab, BaseTabComponent>(AdvantageBrowseTab.PrecisionResearch, new PrecisionResearchTabPanel());
            this.AllPossibleTabOptions = new Dictionary<AdvantageBrowseTab, Type>
                                             {
                                                 {
                                                     AdvantageBrowseTab.DeepAIResearch,
                                                     typeof(DeepResearchTabPanel)
                                                 },
                                                 {
                                                     AdvantageBrowseTab.Tools,
                                                     typeof(AdvantageToolsTabPanel)
                                                 },
                                                 {
                                                     AdvantageBrowseTab.ContentTypes,
                                                     typeof(ContentTypesTabPanel)
                                                 },
                                                 {
                                                     AdvantageBrowseTab.StateMaterials,
                                                     typeof(StateMaterialsTabPanel)
                                                 },
                                                 {
                                                     AdvantageBrowseTab.FederalMaterials,
                                                     typeof(FederalMaterialsTabPanel)
                                                 },
                                                 {
                                                     AdvantageBrowseTab.PracticeAreas,
                                                     typeof(PracticeAreasTabPanel)
                                                 },
                                                 {
                                                     AdvantageBrowseTab.MyContent,
                                                     typeof(MyContentTabPanel)
                                                 },
                                                 {
                                                     AdvantageBrowseTab.PrecisionResearch,
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
        protected override EnumPropertyMapper<AdvantageBrowseTab, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<AdvantageBrowseTab, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawAdvantage");

        /// <summary>
        /// Is Tab Active
        /// </summary>
        /// <param name="tab">tab</param>
        /// <returns>true if expected tab is active</returns>
        public override bool IsActive(AdvantageBrowseTab tab)
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
        public override bool IsDisplayed(AdvantageBrowseTab tab) => DriverExtensions.IsDisplayed(By.Id(this.TabsMap[tab].LocatorString));
    }
}
