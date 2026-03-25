namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.MasterTaxonomy
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Products.WestLawNextCanada.Components;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.Miscellaneous;
    using OpenQA.Selenium;

    /// <summary>
    /// Browse Legal Topics Page
    /// </summary>
    public class MasterTaxonomyPage : CustomDigestBrowsePage
    {
        private static readonly By FilterLeftRailLabelLocator = By.XPath("//div[@id='co_website_searchFacets']");
        private static readonly By OverviewLabelLocator = By.XPath("//*[@id='co_search_contentNav_count_ALL_li']");

        /// <summary>
        /// View Results Facet
        /// </summary>
        public BrowseLegalTopicsComponent ViewLegalTopics { get; } = new BrowseLegalTopicsComponent();

        /// <summary>
        /// New Narrow Pane Component (left side of search results page)
        /// </summary>
        public EdgeNarrowPaneComponent NarrowPanel { get; } = new EdgeNarrowPaneComponent();

        /// <summary>
        /// Miscellaneous Component
        /// </summary>
        public CanadaMiscellaneousComponent Miscellaneous { get; } = new CanadaMiscellaneousComponent();

        /// <summary>
        /// Overview Label
        /// </summary>
        public ILabel OverviewLabel => new Label(OverviewLabelLocator);

        /// <summary>
        /// View FilterLeft Rail Label
        /// </summary>
        public ILabel FilterLeftRailLabel => new Label(FilterLeftRailLabelLocator);
    }
}
