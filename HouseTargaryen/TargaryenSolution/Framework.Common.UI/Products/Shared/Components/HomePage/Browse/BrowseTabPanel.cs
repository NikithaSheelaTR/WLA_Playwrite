namespace Framework.Common.UI.Products.Shared.Components.HomePage.Browse
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Enums.HomePage;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;
    using TabPanel;

    /// <summary>
    /// Browse Widget 
    /// Central part component on the Home Page
    /// </summary>
    public class BrowseTabPanel : TabPanel<BrowseTab>
    {
        private static readonly By ActiveTabLocator = By.CssSelector(".co_tabActive");

        private static readonly By BrowseComponentLocator = By.XPath("//div[@id='co_browseWidget' or @class='Browse-widget']");

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowseTabPanel"/> class. 
        /// </summary>
        public BrowseTabPanel()
        {
            this.ActiveTab = new KeyValuePair<BrowseTab, BaseTabComponent>(BrowseTab.AllContent, new AllContentTabPanel());
            this.AllPossibleTabOptions = new Dictionary<BrowseTab, Type>
                                             {
                                                 {
                                                     BrowseTab.AllContent,
                                                     typeof(AllContentTabPanel)
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
                                                     BrowseTab.Tools,
                                                     typeof(ToolsTabPanel)
                                                 },
                                                 {
                                                     BrowseTab.StateMaterials,
                                                     typeof(StateMaterialsTabPanel)
                                                 },
                                                 {
                                                     BrowseTab.FindAndKeyCite,
                                                     typeof(FindAndKeyCiteTabPanel)
                                                 }
                                             };
        }

        /// <summary>
        /// Is Tab active
        /// </summary>
        /// <param name="tab">The tab.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool IsActive(BrowseTab tab)
            => DriverExtensions.WaitForElement(ActiveTabLocator).GetText().Equals(this.TabsMap[tab].Text);

        /// <summary>
        /// Is Tab displayed
        /// </summary>
        /// <param name="tab">The tab.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool IsDisplayed(BrowseTab tab) => DriverExtensions.IsDisplayed(By.Id(this.TabsMap[tab].Id));

        /// <summary>
        /// Verify that Browse component is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsBrowseComponentDisplayed() => DriverExtensions.IsDisplayed(BrowseComponentLocator, 5);
    }
}