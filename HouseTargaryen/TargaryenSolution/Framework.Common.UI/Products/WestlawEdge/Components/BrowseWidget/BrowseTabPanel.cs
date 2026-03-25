namespace Framework.Common.UI.Products.WestlawEdge.Components.BrowseWidget
{
    using System;
    using System.Collections.Generic;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Products.WestLawNextCanada.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.BrowseComponent;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Browse Component
    /// </summary>
    public class BrowseTabPanel : TabPanel<BrowseTab>
    {
        private static readonly By CurrentActiveTabLocator = By.XPath("//li[(@class = 'Tab Tab--active')]");
        private static readonly By GearButtonLocator = By.ClassName("icon_gear-gray");
        private static readonly By TabLabelLocator = By.XPath("//*[@id='coid_browseTabs']//*[contains(@id, 'tab')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowseTabPanel"/> class. 
        /// </summary>
        public BrowseTabPanel()
        {
            this.ActiveTab = new KeyValuePair<BrowseTab, BaseTabComponent>(BrowseTab.ContentTypes, new ContentTypesTabPanel());
            this.AllPossibleTabOptions = new Dictionary<BrowseTab, Type>
                                             {
                                                 {
                                                     BrowseTab.Tools,
                                                     typeof(ToolsTabPanel)
                                                 },
                                                 {
                                                     BrowseTab.ContentTypes,
                                                     typeof(ContentTypesTabPanel)
                                                 },
                                                 {
                                                     BrowseTab.StateMaterials,
                                                     typeof(StateMaterialsTabPanel)
                                                 },
                                                 {
                                                     BrowseTab.FederalMaterials,
                                                     typeof(FederalMaterialsTabPanel)
                                                 },
                                                 {
                                                     BrowseTab.PracticeAreas,
                                                     typeof(PracticeAreasTabPanel)
                                                 },
                                                 {
                                                     BrowseTab.MyContent,
                                                     typeof(MyContentTabPanel)
                                                 },
                                                 {
                                                     BrowseTab.ProductsAndFeatures,
                                                    typeof(ProductsAndFeaturesTabPanel)
                                                 },
                                                 {
                                                     BrowseTab.AiAssistedResearch,
                                                    typeof(AiAssistedResearchTabPanel)
                                                 },
                                                 {
                                                     BrowseTab.International,
                                                    typeof(InternationalTabPanel)
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
        public IButton GearButton => new Button(GearButtonLocator);

        /// <summary>
        /// Browse Tab Tabs Mapper
        /// </summary>
        protected override EnumPropertyMapper<BrowseTab, WebElementInfo> TabsMap => 
            EnumPropertyModelCache.GetMap<BrowseTab, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdge/BrowseComponent");

        /// <summary>
        /// Is Tab Active
        /// </summary>
        /// <param name="tab">tab</param>
        /// <returns>true if expected tab is active</returns>
        public override bool IsActive(BrowseTab tab)
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
        public override bool IsDisplayed(BrowseTab tab) => DriverExtensions.IsDisplayed(By.XPath(this.TabsMap[tab].LocatorString));

        /// <summary>
        /// Clicks a given browse tab
        /// </summary>
        /// <typeparam name="T">Browse Tab</typeparam>
        /// <param name="tab"> Tab to click</param>
        /// <returns>
        /// This object, for fluent interfaces
        /// </returns>
        public T ClickBrowseTab<T>(BrowseTab tab) where T : ICommonSearchHomePage
        {
            DriverExtensions.WaitForElementDisplayed(By.Id(this.TabsMap[tab].Id)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }        
    }
}
