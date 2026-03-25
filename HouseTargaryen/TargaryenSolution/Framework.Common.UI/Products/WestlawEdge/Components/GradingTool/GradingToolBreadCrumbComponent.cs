namespace Framework.Common.UI.Products.WestlawEdge.Components.GradingTool
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.GradingTool;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// The indigo grading tool bread crumb component.
    /// </summary>
    public class GradingToolBreadCrumbComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("coid_website_breadCrumbTrail");

        private EnumPropertyMapper<GradingToolBreadCrumbLink, WebElementInfo> gradingToolBreadCrumbMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the Bread Crumb enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<GradingToolBreadCrumbLink, WebElementInfo> GradingToolBreadCrumbMap =>
            this.gradingToolBreadCrumbMap = this.gradingToolBreadCrumbMap
                                            ?? EnumPropertyModelCache.GetMap<GradingToolBreadCrumbLink, WebElementInfo>(
                                                string.Empty,
                                                @"Resources/EnumPropertyMaps/WestlawEdge/GradingTool");

        /// <summary>
        /// Clicks bread crumb link.
        /// </summary>
        /// <param name="linkName"> The link name. </param>
        /// <typeparam name="T"> Tpage </typeparam>
        /// <returns> T page </returns>
        public T ClickBreadCrumbLink<T>(GradingToolBreadCrumbLink linkName) where T : ICreatablePageObject
        {
            DriverExtensions.Click(By.XPath(this.GradingToolBreadCrumbMap[linkName].LocatorString));
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
