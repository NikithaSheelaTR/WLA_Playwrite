namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Indigo Recent Filters Facet Component
    /// </summary>
    public class EdgeRecentFiltersFacetComponent : BaseModuleRegressionComponent
    {
        private static readonly By SelectedContentTypeLocator = By.XPath("//div[@id = 'co_reapplyFacets']/p");
        private static readonly By RestoreFiltersButtonLocator = By.XPath(".//*[@class='co_defaultBtn']");
        private static readonly By RestorePreviousButtonTooltipLocator = By.XPath(".//*[contains(@id,'infoBox')]");
        private static readonly By CloseRestorePreviousStateButtonTooltipLocator = By.XPath(".//*[contains(@class,'closeButton')]");
        private static readonly By ContainerLocator = By.Id("co_reapplyFacets");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get Selected Content Type
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetSelectedContentType() => DriverExtensions.GetElement(SelectedContentTypeLocator).Text;

        /// <summary>
        /// Click Restore Previous Filters Button
        /// </summary>
        /// <typeparam name="T"> Page </typeparam>
        /// <returns> T </returns>
        public T ClickRestorePreviousFiltersButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(RestoreFiltersButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Closing Restore Previous Button Tooltip
        /// </summary>      
        public void CloseRestorePreviousButtonTooltip()
        {
            if (DriverExtensions.IsDisplayed(ContainerLocator, CloseRestorePreviousStateButtonTooltipLocator))
            {
                DriverExtensions.GetElement(ContainerLocator, CloseRestorePreviousStateButtonTooltipLocator).Click();
            }
        }

        /// <summary>
        /// Closing Restore Previous Button Tooltip
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsRestorePreviousStateButtonTooltipDisplayed() =>
           DriverExtensions.IsDisplayed(ContainerLocator, RestorePreviousButtonTooltipLocator);

        /// <summary>
        /// Gets Restore Previous Button Popup Message Text
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string RestorePreviousFilterPopUpMessage() =>
            DriverExtensions.GetElement(ContainerLocator, RestorePreviousButtonTooltipLocator).Text;

        /// <summary>
        /// IsRestorePreviousFiltersButtonEnable
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsRestorePreviousFiltersButtonEnable() =>
            DriverExtensions.WaitForElement(RestoreFiltersButtonLocator).Enabled;
    }
}