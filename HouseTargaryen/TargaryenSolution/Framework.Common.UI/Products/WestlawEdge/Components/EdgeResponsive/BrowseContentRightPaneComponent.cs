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
    /// Browse content right pane component
    /// </summary>
    public class BrowseContentRightPaneComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@id = 'coid_browseTabContents']");

        private EnumPropertyMapper<BrowseContentRightPaneLinks, WebElementInfo> browseContentRightPaneLinks;

        /// <summary>
        /// Gets browse content right pane links enumeration to WebElementInfo map.
        /// </summary>
        private EnumPropertyMapper<BrowseContentRightPaneLinks, WebElementInfo> BrowseContentRightPaneLinksMap =>
            this.browseContentRightPaneLinks = this.browseContentRightPaneLinks
                                               ?? EnumPropertyModelCache.GetMap<BrowseContentRightPaneLinks, WebElementInfo>(
                                                   string.Empty,
                                                   @"Resources/EnumPropertyMaps/WestlawEdge/Header");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Clicks one of the browse category
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="link">The category.</param>
        /// <returns>New instance of T page.</returns>
        public T ClickBrowseContentLink<T>(BrowseContentRightPaneLinks link) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(By.XPath(this.BrowseContentRightPaneLinksMap[link].LocatorString)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}

