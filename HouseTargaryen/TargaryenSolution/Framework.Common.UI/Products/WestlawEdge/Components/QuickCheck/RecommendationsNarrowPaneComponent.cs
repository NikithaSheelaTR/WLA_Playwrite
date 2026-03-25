namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.Facets;
    using Framework.Common.UI.Products.WestlawEdge.Elements.QuickCheck;

    using OpenQA.Selenium;

    /// <summary>
    /// The pane with content types and facets on the recommendations tab.
    /// </summary>
    public class RecommendationsNarrowPaneComponent : BaseModuleRegressionComponent
    {
        private static readonly By ToggleArrowLocator = By.Id("co_collapseActionLeft");
        private static readonly By RecommendationTagFacetLocator = By.XPath("//section[contains(@class, 'SearchFacetMultipleXBoxes-RecommendationFeature')]");
        private static readonly By DepthOfDiscussionFacetLocator = By.XPath("//section[contains(@class, 'SearchFacetMultipleXBoxes-DepthOfDiscussion')]");
        private static readonly By JurisdictionFacetLocator = By.ClassName("SearchFacetHierarchy-Jurisdiction");
        private static readonly By KeyCiteFacetLocator = By.XPath("//section[contains(@class, 'KeyCite')]/parent::div");
        private static readonly By ClearButtonLocator = By.XPath(".//*[@class='co_undoAll']/button");     
        private static readonly By DocumentHeadingsFacetLocator = By.ClassName("SearchFacetco_docHeading-DocumentHeadings");
        private static readonly By ContainerLocator = By.XPath("//*[@class='co_innertube' and @tabindex='-1']");

        /// <summary>
        /// Clear button
        /// </summary>
        public IButton ClearButton => new Button(ClearButtonLocator);

        /// <summary>
        /// Toggle arrow button
        /// </summary>
        public ICheckBox ToggleArrow => new NarrowPaneExpandCheckbox(ToggleArrowLocator);

        /// <summary>
        /// The content type panel.
        /// </summary>
        public EdgeContentTypesFacetComponent ContentTypesTabPanel => new EdgeContentTypesFacetComponent();

        /// <summary>
        /// Search within facet component
        /// </summary>
        public EdgeSearchWithinFacetComponent SearchWithinFacetComponent => new EdgeSearchWithinFacetComponent();

        /// <summary>
        /// Jurisdiction facet component
        /// </summary>
        public BaseSearchHierarchyFacetComponent JurisdictionFacetComponent => new BaseSearchHierarchyFacetComponent(JurisdictionFacetLocator);

        /// <summary>
        /// Document Headings facet component
        /// </summary>
        public DocumentHeadingsFacetComponent DocumentHeadingsFacetComponent => new DocumentHeadingsFacetComponent(DocumentHeadingsFacetLocator);

        /// <summary>
        /// Recommendation tag facet component
        /// </summary>
        public BaseSearchHierarchyFacetComponent RecommendationTagFacetComponent => new BaseSearchHierarchyFacetComponent(RecommendationTagFacetLocator);

        /// <summary>
        /// Depth of discussion facet component
        /// </summary>
        public BaseSearchHierarchyFacetComponent DepthOfDiscussionFacetComponent => new BaseSearchHierarchyFacetComponent(DepthOfDiscussionFacetLocator);

        /// <summary>
        /// Date facet component
        /// </summary>
        public DateFacetComponent DateFacetComponent => new DateFacetComponent("Ri");

        /// <summary>
        /// The key cite treatment facet component
        /// </summary>
        public KeyCiteTreatmentFacetComponent KeyCiteTreatmentFacetComponent => new KeyCiteTreatmentFacetComponent(KeyCiteFacetLocator);

        /// <summary>
        /// Previously viewed facet component
        /// </summary>
        public PreviouslyViewedFacetComponent PreviouslyViewedFacetComponent => new PreviouslyViewedFacetComponent();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}