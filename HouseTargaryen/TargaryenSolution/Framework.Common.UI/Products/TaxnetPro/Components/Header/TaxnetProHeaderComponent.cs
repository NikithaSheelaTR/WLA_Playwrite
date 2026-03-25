namespace Framework.Common.UI.Products.TaxnetPro.Components.Header
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.TaxnetPro.Enums.Header;
    using Framework.Common.UI.Products.WestlawEdge.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Taxnet Pro Header Component
    /// </summary>
    public class TaxnetProHeaderComponent : EdgeHeaderComponent
    {
        private static readonly By ExpandedTabLocator = By.XPath(".//ancestor::div[@class='co_dropdownTabExpanded']");
        private static readonly By TaxnetProLogoLocator = By.Id("coid_website_logo");

        private EnumPropertyMapper<TaxnetProHeaderTabs, WebElementInfo> headerTabsMap;
        private EnumPropertyMapper<TaxnetProMainMenuTabs, WebElementInfo> mainMenuTabsMap;
        private EnumPropertyMapper<SearchMenuBarDropdown, WebElementInfo> searchMenuBarMap;
        private EnumPropertyMapper<NewsMenuBarDropdown, WebElementInfo> newsMenuBarMap;

        private EnumPropertyMapper<TaxnetProHeaderTabs, WebElementInfo> HeaderTabsMap =>
            this.headerTabsMap = this.headerTabsMap
                                 ?? EnumPropertyModelCache.GetMap<TaxnetProHeaderTabs, WebElementInfo>(
                                     string.Empty,
                                     @"Resources/EnumPropertyMaps/TaxnetPro");

        private EnumPropertyMapper<TaxnetProMainMenuTabs, WebElementInfo> MainMenuTabsMap =>
            this.mainMenuTabsMap = this.mainMenuTabsMap
                                 ?? EnumPropertyModelCache.GetMap<TaxnetProMainMenuTabs, WebElementInfo>(
                                     string.Empty,
                                     @"Resources/EnumPropertyMaps/TaxnetPro");

        private EnumPropertyMapper<SearchMenuBarDropdown, WebElementInfo> SearchMenuBarDropdownMap =>
            this.searchMenuBarMap = this.searchMenuBarMap
                                 ?? EnumPropertyModelCache.GetMap<SearchMenuBarDropdown, WebElementInfo>(
                                     string.Empty,
                                     @"Resources/EnumPropertyMaps/TaxnetPro");

        private EnumPropertyMapper<NewsMenuBarDropdown, WebElementInfo> NewsMenuBarDropdownMap =>
            this.newsMenuBarMap = this.newsMenuBarMap
                                 ?? EnumPropertyModelCache.GetMap<NewsMenuBarDropdown, WebElementInfo>(
                                     string.Empty,
                                     @"Resources/EnumPropertyMaps/TaxnetPro");

        /// <summary>
        /// Clicks Header tab
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tab"></param>
        /// <returns>New instance of T</returns>
        public T ClickHeaderTab<T>(TaxnetProHeaderTabs tab)
            where T : ICreatablePageObject
        {
            if (!this.IsHeaderTabExpanded(tab))
            {
                DriverExtensions.Click(
                    DriverExtensions.WaitForElement(By.XPath(this.HeaderTabsMap[tab].LocatorString)));
            }

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Checks if Header tab is expanded
        /// </summary>
        /// <param name="tab"></param>
        /// <returns>true if displayed</returns>
        public bool IsHeaderTabExpanded(TaxnetProHeaderTabs tab) =>
            DriverExtensions.IsDisplayed(By.XPath(this.HeaderTabsMap[tab].LocatorString), ExpandedTabLocator);

        /// <summary>
        /// Hovers on Main menu tab
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tab"></param>
        /// <returns>Returns new page instance</returns>
        public T HoverOnMainMenuTab<T>(TaxnetProMainMenuTabs tab) where T : ICommonSearchHomePage
        {
            DriverExtensions.GetElement(By.XPath(MainMenuTabsMap[tab].LocatorString)).Hover();
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForPageLoad();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on Main menu tab
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tab"></param>
        /// <returns>Returns new page instance</returns>
        public T ClickOnMainMenuTab<T>(TaxnetProMainMenuTabs tab) where T : IBrowseCategoryPage
        {
            DriverExtensions.GetElement(By.XPath(MainMenuTabsMap[tab].LocatorString)).Click();
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForPageLoad();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks on Search menu bar value
        /// </summary>
        /// <param name="searchMenu"></param>
        /// <returns>Returns new page instance</returns>
        public T ClickOnSearchMenuBarValue<T>(SearchMenuBarDropdown searchMenu) where T : ICreatablePageObject
        {
            DriverExtensions.Click(By.Id(SearchMenuBarDropdownMap[searchMenu].Id));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks on News menu bar value
        /// </summary>
        /// <param name="newsMenu"></param>
        /// <returns>Returns new page instance</returns>
        public T ClickOnNewsMenuBarValue<T>(NewsMenuBarDropdown newsMenu) where T : ICreatablePageObject
        {
            DriverExtensions.Click(By.Id(NewsMenuBarDropdownMap[newsMenu].Id));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks the Taxnet Pro logo in the header to return to the Homepage
        /// </summary>
        /// <typeparam name="T">Page Object</typeparam>
        /// <returns>Home Page object</returns>
        public new T ClickLogo<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(TaxnetProLogoLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}