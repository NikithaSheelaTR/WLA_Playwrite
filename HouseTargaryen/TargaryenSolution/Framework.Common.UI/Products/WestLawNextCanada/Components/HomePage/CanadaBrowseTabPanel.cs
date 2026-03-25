namespace Framework.Common.UI.Products.WestLawNextCanada.Components.HomePage
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.AssistedResearch;
    using Framework.Common.UI.Products.WestLawNextCanada.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Browse Component
    /// </summary>
    public class CanadaBrowseTabPanel : TabPanel<CanadaBrowseTabs>
    {
        private static readonly By CurrentActiveTabLocator = By.XPath("//li[(@class = 'Tab Tab--active')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="CanadaBrowseTabPanel"/> class. 
        /// </summary>
        public CanadaBrowseTabPanel()
        {
            this.ActiveTab = new KeyValuePair<CanadaBrowseTabs, BaseTabComponent>(CanadaBrowseTabs.AiAssistedResearch, new CanadaAiArTabPanel());
            this.AllPossibleTabOptions = new Dictionary<CanadaBrowseTabs, Type>
                                             {
                                                 {
                                                     CanadaBrowseTabs.AiAssistedResearch,
                                                     typeof(CanadaAiArTabPanel)
                                                 },
                                                 {
                                                     CanadaBrowseTabs.AllContent,
                                                     typeof(CanadaContentType)
                                                 },
                                                 {
                                                     CanadaBrowseTabs.International,
                                                     typeof(InternationalTabPanel)
                                                 },
                                                 {
                                                     CanadaBrowseTabs.ProductsFeatures,
                                                     typeof(ProductsAndFeaturesTabPanel)
                                                 },
                                                 {
                                                     CanadaBrowseTabs.Products,
                                                     typeof(ProductsTabPanel)
                                                 }
                                             };
        }

        /// <summary>
        /// Browse Tab Tabs Mapper
        /// </summary>
        protected override EnumPropertyMapper<CanadaBrowseTabs, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<CanadaBrowseTabs, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawNextCanada");

        /// <summary>
        /// Is Tab Active
        /// </summary>
        /// <param name="tab">tab</param>
        /// <returns>true if expected tab is active</returns>
        public override bool IsActive(CanadaBrowseTabs tab)
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
        public override bool IsDisplayed(CanadaBrowseTabs tab) => DriverExtensions.IsDisplayed(By.XPath(this.TabsMap[tab].LocatorString));


        /// <summary>
        /// Set Active Tab
        /// </summary>
        /// <param name="tab"> tab option </param>
        /// <typeparam name="TTab">BaseTabComponent instance</typeparam>
        /// <returns> The <see cref="BaseTabComponent"/>.Tab component </returns>
        public override TTab SetActiveTab<TTab>(CanadaBrowseTabs tab)
        {
            Type searchedType;

            if (!this.AllPossibleTabOptions.TryGetValue(tab, out searchedType))
            {
                throw new NotFoundException("Tab is not found");
            }

            if (!this.ActiveTab.Key.Equals(tab) || this.ActiveTab.Value == null)
            {
                this.ActiveTab = new KeyValuePair<CanadaBrowseTabs, BaseTabComponent>(tab, this.ClickTab<TTab>(tab));
            }

            return (TTab)this.ActiveTab.Value;
        }

        /// <summary>
        /// Click Tab
        /// </summary>
        /// <param name="tab">tab option</param>
        /// <typeparam name="TTab">BaseTabComponent instance</typeparam>
        /// <returns>ITAB object</returns>
        protected override TTab ClickTab<TTab>(CanadaBrowseTabs tab)
        {
            DriverExtensions.WaitForElementDisplayed(By.XPath(this.TabsMap[tab].LocatorString)).Click();
            return DriverExtensions.CreatePageInstance<TTab>();
        }
    }
}