namespace Framework.Common.UI.Products.Shared.Components.FolderHistory
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Enums.History;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Section on the left panel on the history page
    /// </summary>
    public class LeftHistoryComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("co_researchOrganizerNavigationContainer");

       /// <summary>
        /// The Narrow Pane (left side of search results page)
        /// </summary>
        public NarrowPaneComponent NarrowPane { get; set; } = new NarrowPaneComponent();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the historyType enumeration to WebElementInfo map.
        /// </summary>
        private EnumPropertyMapper<HistoryType, WebElementInfo> HistoryTypeMap
           => EnumPropertyModelCache.GetMap<HistoryType, WebElementInfo>();

        /// <summary>
        /// Choose History Type if not selected
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="historyType">
        /// History Type
        /// </param>
        /// <returns>
        /// new History Page 
        /// </returns>
        public T ChooseHistoryType<T>(HistoryType historyType) where T : ICommonHistoryPage
        {
            IWebElement historyTypeElement = DriverExtensions.WaitForElement(By.Id(this.HistoryTypeMap[historyType].Id));
            if (!DriverExtensions.GetAttribute("class", By.Id(this.HistoryTypeMap[historyType].Id)).Contains("co_hideState"))
            {
                historyTypeElement.Click();
                DriverExtensions.WaitForAnimation();
            }

            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
