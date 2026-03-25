namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.NarrowPane.Facets
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items.LitigationAnalytics;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Litigation Analytics component with facets for search
    /// </summary>
    public class LitigationAnalyticsSearchFacetsFilterComponent : EdgeSearchFacetsFilterComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class = 'SearchFacet-general-group']");
        private static readonly By FilterByCategoryLocator = By.XPath(".//h3[@class = 'co_genericBoxHeader co_narrow_boxHeader']");
        private static readonly By ClearAllFiltersButtonLocator = By.XPath(".//button[contains(@id, 'Filter_clearAllFilters_button')]");
        private static readonly By FacetItemLocator = By.ClassName("SearchFacet-label");

        /// <summary>
        /// Litigation Analytics component with facets for search
        /// </summary>
        public LitigationAnalyticsSearchFacetsFilterComponent()
        {
            DriverExtensions.WaitForElementDisplayed(By.ClassName("co_navSelect"));
        }

        /// <summary>
        /// Date facet.
        /// </summary>
        public new LitigationAnalyticsDateFacetDialog DateFacet => new LitigationAnalyticsDateFacetDialog();

        /// <summary>
        /// Get filter by name
        /// </summary>
        protected EnumPropertyMapper<LitigationAnalyticsFacets, WebElementInfo> Facets =>
            EnumPropertyModelCache.GetMap<LitigationAnalyticsFacets, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/LitigationAnalytics");

        /// <summary>
        /// Get cascading filter by name
        /// </summary>
        protected EnumPropertyMapper<LitigationAnalyticsCascadingFacets, WebElementInfo> CascadingFacets =>
            EnumPropertyModelCache.GetMap<LitigationAnalyticsCascadingFacets, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/LitigationAnalytics");

        /// <summary>
        /// Select Facet Item By Name.
        /// </summary>
        public LitigationAnalyticsFacetDialog SelectFacet(LitigationAnalyticsFacets facetItemName)
        {
            DriverExtensions.ScrollTo(By.XPath(this.Facets[facetItemName].LocatorString));
            DriverExtensions.GetElement(By.XPath(this.Facets[facetItemName].LocatorString)).JavascriptClick();
            return new LitigationAnalyticsFacetDialog();
        }

        /// <summary>
        /// Select Facet Item By Name.
        /// </summary>
        public IButton CascadingFacetButton(LitigationAnalyticsCascadingFacets facetItemName) =>
            new Button(By.XPath(this.CascadingFacets[facetItemName].LocatorString));

        /// <summary>
        /// Selected facet items.
        /// </summary>
        public List<LitigationAnalyticsFacetItem> FacetResultItems(LitigationAnalyticsFacets facetItemName) => new ItemsCollection<LitigationAnalyticsFacetItem>(By.XPath(this.Facets[facetItemName].LocatorString + "/ancestor::section"), FacetItemLocator).Where(item => item.IsCurrentItemDisplayed()).ToList();

        /// <summary>
        /// Selected facet items.
        /// </summary>
        public List<LitigationAnalyticsFacetItem> CascadingFacetResultItems(LitigationAnalyticsCascadingFacets facetItemName) => new ItemsCollection<LitigationAnalyticsFacetItem>(By.XPath(this.CascadingFacets[facetItemName].LocatorString + "/ancestor::section"), FacetItemLocator).Where(item => item.IsCurrentItemDisplayed()).ToList();

        /// <summary>
        /// Narrow Panel Toggle      
        /// </summary>
        public ILabel FilterByCategoryLabel => new Label(this.ComponentLocator, FilterByCategoryLocator);

        /// <summary>
        /// Clear all filters button   
        /// </summary>
        public IButton ClearAllaFiltersButton => new Button(this.ComponentLocator, ClearAllFiltersButtonLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}