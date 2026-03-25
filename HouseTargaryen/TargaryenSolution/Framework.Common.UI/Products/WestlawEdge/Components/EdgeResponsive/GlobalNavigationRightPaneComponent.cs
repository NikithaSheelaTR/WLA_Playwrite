namespace Framework.Common.UI.Products.WestlawEdge.Components.EdgeResponsive
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Header;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// Global navigation right pane component
    /// </summary>
    public class GlobalNavigationRightPaneComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//ul[@id = 'co_mainNav']");

        private EnumPropertyMapper<GlobalNavigationRightPaneLinks, WebElementInfo> globalNavigationRightPaneLinks;

        /// <summary>
        /// Gets global navigation right pane links enumeration to WebElementInfo map.
        /// </summary>
        private EnumPropertyMapper<GlobalNavigationRightPaneLinks, WebElementInfo> GlobalNavigationRightPaneLinks =>
            this.globalNavigationRightPaneLinks = this.globalNavigationRightPaneLinks
                                     ?? EnumPropertyModelCache.GetMap<GlobalNavigationRightPaneLinks, WebElementInfo>(
                                         string.Empty,
                                         @"Resources/EnumPropertyMaps/WestlawEdge/Header");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Clicks global navigation right pane links
        /// </summary>
        /// <typeparam name="T">Page returned from clicking the link.</typeparam>
        /// <param name="link">Global navigation right pane link.</param>
        /// <returns>New instance of T</returns>
        public T ClickLink<T>(GlobalNavigationRightPaneLinks link) where T : ICreatablePageObject
        {
           DriverExtensions.Click(DriverExtensions.WaitForElement(By.XPath(this.GlobalNavigationRightPaneLinks[link].LocatorString)));
           return DriverExtensions.CreatePageInstance<T>();
        }   
    }
}
