namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane
{
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Indigo Notes of Decisions facets filter component
    /// </summary>
    public class EdgeNotesOfDecisionsFacetsFilterComponent : EdgeRiFacetsFilterComponent
    {
        private static readonly By FilterLeftRailContainerLocator = By.XPath("//div[@id='co_website_searchFacets']");
        private static readonly By NodFilterLocator = By.XPath(".//div[@id='coid_nod_FacetsContainer']");
        private static readonly By FilterSectionExpandButtonLocator = By.XPath(".//button[@aria-controls='coid_nod_FacetsContainer']");
        private static readonly By FilterInfoMessageLocator = By.XPath(".//div[@class='co_infoBox_message']");
        private static readonly By AppliedFilterCheckmarkLocator = By.XPath(".//span[@class='la_icon icon_green_checkmark']");

        private static readonly By KeyNumberFacetContainerLocator = By.XPath("//section[contains(@class,'-KeyNumber')]");

        /// <summary>
        /// Key Number Facet Component
        /// </summary>
        public new EdgeBaseFacetWithAppearingDialogComponent KeyNumberFacet => new EdgeBaseFacetWithAppearingDialogComponent(KeyNumberFacetContainerLocator);

        /// <summary>
        /// Recent Filters facet component
        /// </summary>
        public EdgeRecentFiltersFacetComponent RecentFiltersFacet { get; } = new EdgeRecentFiltersFacetComponent();

        /// <summary>
        /// Container
        /// </summary>
        private IWebElement Container => DriverExtensions.WaitForElement(FilterLeftRailContainerLocator);

        /// <summary>
        /// Verifies that the Filter section is displayed on the left rail on NOD tab.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if notes of decisions left rail is displayed </returns>
        public bool IsNodFilterSectionDisplayed()
            => DriverExtensions.IsDisplayed(FilterLeftRailContainerLocator);

        /// <summary>
        /// Verifies that the Facets section is collapsed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if notes of decisions left rail is displayed </returns>
        public bool IsNodFilterSectionCollapsed() => DriverExtensions.GetElement(this.Container, NodFilterLocator)
                                                                     .GetAttribute("class").Contains("co_hideState");

        /// <summary>
        /// Verifies that the applied filter checkmark is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the applied filter checkmark is displayed </returns>
        public bool IsNodAppliedFilterCheckmarkDisplayed() => DriverExtensions.IsDisplayed(this.Container,AppliedFilterCheckmarkLocator);

        /// <summary>
        /// Verifies that the Facets are displayed on the Filter section of NOD tab.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if notes of decisions Filter section contains facets displayed </returns>
        public bool AreNodFacetsDisplayed()
            => DriverExtensions.IsDisplayed(this.Container, NodFilterLocator);

        /// <summary>
        /// Clicks the show/hide button for Filter section
        /// </summary>
        public void ClickFilterSectionExpandButton() =>
            DriverExtensions.GetElement(this.Container, FilterSectionExpandButtonLocator).Click();

        /// <summary>
        /// Get text of warning message on Filters pane.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if notes of decisions left rail is displayed </returns>
        public string GetInfoMessageText() => DriverExtensions.GetTextSafe(this.Container, FilterInfoMessageLocator);
    }
}