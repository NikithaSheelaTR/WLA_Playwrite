namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.NarrowPane
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Toggles;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Products.WestlawEdge.Elements.NarrowPanel;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.NarrowPane.Facets;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// Litigation Analytics narrow Tab Panel
    /// </summary>
    public class LitigationAnalyticsNarrowPanel : EdgeNarrowPaneComponent
    {
        private static readonly By NarrowComponentLocator = By.XPath("//div[contains(@class,'la-Layout-leftCol ')]");
        private static readonly By ToggleButtonLocator = By.Id("co_collapseActionLeft");
        private static readonly By ToggleStateLocator = By.XPath(".//div[@class = 'co_innertube']");
        private static readonly By IncludeMultidistrictLitigationToggleLocator = By.XPath(".//div[contains(@class,'la-Layout-leftCol ')]//div[@class='SlideToggle-thumb']");
        private static readonly By IncludeMultidistrictLitigationToggleStateLocator = By.XPath(".//div[@class='SlideToggle-bar']");
        private static readonly By FacetGrantedLocator = By.XPath("//button[./span[text() = 'Granted']]");
        private static readonly By ApplicationsdButtonLocator = By.XPath("//button[./span[text() = 'Applications']]");
        private static readonly By AssignmentsButtonLocator = By.XPath("//button[./span[text() = 'Assignments']]");
        private static readonly By SearchCriteriaButtonLocator = By.XPath("//button[./span[text() = 'Search criteria']]");
        private static readonly By ClearAllFilterButtonLocator = By.XPath(".//button[@class='co_primaryBtn la-Button-outcome isActive']");
        private static readonly By SelectMultipleFiltersToggleLocator = By.XPath(".//div[contains(@class,'la-Layout-leftCol ')]//div[@class='MultipleFilter-controls']//div[@class='SlideToggle-thumb']");
        private static readonly By ApplyButtonLocator = By.XPath(".//button[@class='co_multifacet_apply']");
        private static readonly By SelectMultipleFiltersToggleStateLocator = By.XPath(".//div[@class ='MultipleFilter-controls']//div[@class='SlideToggle-thumb-container']");
        private static readonly By ClearButtonLocator = By.XPath(".//button[contains(text(),'Clear')]");

        /// <summary>
        /// Litigation Analytics narrow Tab Panel
        /// </summary>
        public LitigationAnalyticsNarrowPanel()
        {
        }

        /// <summary>
        /// Search Facets
        /// </summary>
        public LitigationAnalyticsSearchFacetsFilterComponent SearchFacets { get; } = new LitigationAnalyticsSearchFacetsFilterComponent();

        /// <summary>
        /// Narrow Panel Toggle      
        /// </summary>
        public IToggle NarrowPanelToggle => new NarrowPanelToggle(ToggleButtonLocator, new ByChained(NarrowComponentLocator, ToggleStateLocator), "display", "block");

        /// <summary>
        /// Narrow Panel Toggle Include Multidistrict Litigation Toggle
        /// </summary>
        public IToggle IncludeMultidistrictLitigationToggle => new Toggle(IncludeMultidistrictLitigationToggleLocator, new ByChained(NarrowComponentLocator, IncludeMultidistrictLitigationToggleStateLocator), "SlideToggle-thumb-container", "#218321");

        /// <summary>
        /// Granted button   
        /// </summary>
        public IButton GrantedButton => new Button(FacetGrantedLocator);

        /// <summary>
        /// Granted button   
        /// </summary>
        public IButton ApplicationsdButton => new Button(ApplicationsdButtonLocator);

        /// <summary>
        /// Granted button   
        /// </summary>
        public IButton AssignmentsButton => new Button(AssignmentsButtonLocator);

        /// <summary>
        /// Search Criteria button   
        /// </summary>
        public IButton SearchCriteriaButton => new Button(SearchCriteriaButtonLocator);

        /// <summary>
        /// Clear All Filter button   
        /// </summary>
        public IButton ClearAllFilterButton => new Button(ClearAllFilterButtonLocator);

        /// <summary>
        /// Apply button   
        /// </summary>
        public IButton ApplyButton => new Button(ApplyButtonLocator);

        /// <summary>
        /// Clear button   
        /// </summary>
        public IButton ClearButton => new Button(ClearButtonLocator);

        /// <summary>
        /// Select Multiple Filters Toggle Locator
        /// </summary>
        public IToggle SelectMultipleFiltersToggle => new Toggle(SelectMultipleFiltersToggleLocator, new ByChained(NarrowComponentLocator, SelectMultipleFiltersToggleStateLocator), "background", "#218321");
    }
}