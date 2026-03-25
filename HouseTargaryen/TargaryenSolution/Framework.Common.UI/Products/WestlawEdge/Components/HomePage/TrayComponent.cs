namespace Framework.Common.UI.Products.WestlawEdge.Components.HomePage
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Enums.HomePage;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// What's new tray component
    /// </summary>
    public class TrayComponent : BaseModuleRegressionComponent
    {
        private static readonly By ShowMoreButtonLocator = By.Id("coid_ShowMore");

        private static readonly By ShowLessButtonLocator = By.Id("coid_ShowLess");

        private static readonly By CollapsedOrExpandedViewLocator = By.XPath("./ancestor::div[contains(@class , 'Promo')]");

        private static readonly By StartPageTrayContainerLocator = By.Id("coid_website_whatIsNewBrowsePage");

        private static readonly By ContainerLocator = By.XPath("//*[@id='co_mainContainer']//div[contains(@class , 'Promo')]/*[@class = 'Main-wrapper']");

        private EnumPropertyMapper<TrayContent, WebElementInfo> trayMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the Tray Content to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<TrayContent, WebElementInfo> TrayMap =>
            this.trayMap = this.trayMap ?? EnumPropertyModelCache.GetMap<TrayContent, WebElementInfo>(
                               string.Empty,
                               @"Resources/EnumPropertyMaps/WestlawEdge/HomePage");

        /// <summary>
        /// Verify is content displayed
        /// </summary>
        /// <param name="content">
        /// Content type
        /// </param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsContentDisplayed(TrayContent content)
            => DriverExtensions.IsDisplayed(By.XPath(this.TrayMap[content].LocatorString), 5);

        /// <summary>
        /// Click Show More button
        /// </summary>
        public void ClickShowMoreButton()
        {
            DriverExtensions.Click(ShowMoreButtonLocator);
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Click Show Less button
        /// </summary>
        public void ClickShowLessButton() => DriverExtensions.Click(ShowLessButtonLocator);

        /// <summary>
        /// Click content type
        /// </summary>
        /// <typeparam name="T">page to return</typeparam>
        /// <param name="option">option to click</param>
        /// <returns>new page</returns>
        public T ClickContent<T>(TrayContent option)
            where T : ICreatablePageObject
        {
            DriverExtensions.Click(By.XPath(this.TrayMap[option].LocatorString));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click content type and open new browser tab
        /// </summary>
        /// <typeparam name="T">Page to return</typeparam>
        /// <param name="option">Option to click</param>
        /// <param name="newTabName">The new Tab Name.</param>
        /// <returns> New Page Object</returns>
        public T ClickContentAndOpenNewBrowserTab<T>(TrayContent option, string newTabName)
            where T : ICreatablePageObject =>
            this.ClickAndOpenNewBrowserTab<T>(By.XPath(this.TrayMap[option].LocatorString), newTabName);

        /// <summary>
        /// Verify is show more button displayed
        /// </summary>
        /// <returns>True, if show more button is displayed</returns>
        public bool IsShowMoreButtonDisplayed() => DriverExtensions.IsDisplayed(ShowMoreButtonLocator);

        /// <summary>
        /// Verify is show more button clickable
        /// </summary>
        /// <returns>True, if show more button is clickable</returns>
        public bool IsShowMoreButtonClickable() => !DriverExtensions
            .WaitForElement(ShowMoreButtonLocator).GetAttribute("class").Contains("co_disabled");

        /// <summary>
        /// Verify is show less button displayed
        /// </summary>
        /// <returns>True, if show less button is displayed</returns>
        public bool IsShowLessButtonDisplayed() => DriverExtensions.IsDisplayed(ShowLessButtonLocator);

        /// <summary>
        /// Verify is show less button clickable
        /// </summary>
        /// <returns>True, if show less button is clickable</returns>
        public bool IsShowLessButtonClickable() => !DriverExtensions
            .WaitForElement(ShowLessButtonLocator).GetAttribute("class").Contains("co_disabled");

        /// <summary>
        /// Verify is tray hidden for any page that isn't Home page
        /// </summary>
        /// <returns>True, if tray is hidden</returns>
        public bool IsTrayHidden() =>
            DriverExtensions.WaitForElement(StartPageTrayContainerLocator).GetAttribute("class").Contains("co_hideState");

        /// <summary>
        /// Verify is default view displayed
        /// </summary>
        /// <returns>True, if default view is displayed</returns>
        public bool IsDefaultViewDisplayed() => this.IsContentDisplayed(TrayContent.LitigationAnalytics)
                   && this.IsContentDisplayed(TrayContent.QuickCheck)
                   && this.IsContentDisplayed(
                       TrayContent.JurisdictionSurveys);

        /// <summary>
        /// Verify is expanded view displayed
        /// </summary>
        /// <returns>True, if expanded view is displayed</returns>
        public bool IsExpandedViewDisplayed() =>
            DriverExtensions.GetAttribute("class", this.ComponentLocator, CollapsedOrExpandedViewLocator).Contains("expanded");
    }
}