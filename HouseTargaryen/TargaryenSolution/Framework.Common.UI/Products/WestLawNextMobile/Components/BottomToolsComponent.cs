namespace Framework.Common.UI.Products.WestLawNextMobile.Components
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Enums.Mobile;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Tools component in the bottom of the Mobile page
    /// </summary>
    public class BottomToolsComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[contains(text(), 'Tools') and contains(@class, 'hdrTwo')]/following-sibling::ul[1]");

        private EnumPropertyMapper<MobileDocMenuTool, WebElementInfo> toolsMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the tabs Map.
        /// </summary>
        protected EnumPropertyMapper<MobileDocMenuTool, WebElementInfo> ToolsMap
            => this.toolsMap = this.toolsMap ?? EnumPropertyModelCache.GetMap<MobileDocMenuTool, WebElementInfo>();

        /// <summary>
        /// Clicks on the Tool link at the bottom of the page for the given tool
        /// </summary>
        /// <typeparam name="T">The class of the page object to return</typeparam>
        /// <param name="tool">The tool to click on</param>
        /// <returns>The page-object</returns>
        public T ClickToolLink<T>(MobileDocMenuTool tool) where T : BaseModuleRegressionPage
        {
            DriverExtensions.WaitForElement(
                DriverExtensions.WaitForElement(this.ComponentLocator),
                By.XPath(this.ToolsMap[tool].LocatorString)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Determines if the specified Tool link is present in the Tools section at the bottom of the page
        /// </summary>
        /// <param name="tool">The tool to look for</param>
        /// <returns>True if the link was found</returns>
        public bool IsToolLinkDisplayed(MobileDocMenuTool tool)
            => DriverExtensions.IsDisplayed(
                    DriverExtensions.WaitForElement(this.ComponentLocator),
                    By.XPath(this.ToolsMap[tool].LocatorString));
    }
}
