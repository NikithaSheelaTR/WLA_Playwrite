namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.Facets;
    
    using OpenQA.Selenium;

    /// <summary>
    /// Quotation analysis narrow pane
    /// </summary>
    public class QuotationAnalysisNarrowPaneComponent : RecommendationsNarrowPaneComponent
    {
        private static readonly By InfoIconLocator = By.ClassName("co_scopeIcon");
        private static readonly By DifferencesFacetLocator = By.XPath("//section[contains(@class, 'SearchFacetHierarchy-Differences')]");
        private static readonly By MischaracterizationIdentificationFacetLocator = By.XPath("//section[contains(@class, 'SearchFacetMultipleXBoxes-StatementMischaracterization')]");
        private static readonly By ContentFacetLocator = By.XPath("//section[contains(@class, 'SearchFacetMultipleXBoxes-Content')]");
        private static readonly By DocumentFacetLocator = By.XPath("//section[contains(@class, 'PartyDocument')]");
        private static readonly By PinCiteErrorFacetLocator = By.XPath("//section[contains(@class, 'SearchFacetMultipleXBoxes-PinciteError')]");
        private static readonly By TitleSearchFacetContainerLocator = By.XPath("//section[contains(@class, 'SearchFacetSearchByTitle-SearchByTitle')]");
        private static readonly By ParaphrasesLocator = By.XPath("//a[contains(text(), 'Paraphrases')]");
        private static readonly By UnmatchedQuotationsLocator = By.XPath("//a[contains(text(), 'Unmatched quotations')]");
        private static readonly By ViewAllQuotationsLocator = By.XPath("//a[contains(text(), 'View all')]");

        /// <summary>
        /// Info button
        /// </summary>
        public IButton InfoIconButton => new Button(InfoIconLocator);

        /// <summary>
        /// The Paraphrases Link.
        /// </summary>
        public ILink ParaphrasesLink => new Link(ParaphrasesLocator);

        /// <summary>
        /// The Unmatched Quotations Link.
        /// </summary>
        public ILink UnmatchedQuotationsLink => new Link(UnmatchedQuotationsLocator);

        /// <summary>
        /// The View All Quotations Link.
        /// </summary>
        public ILink ViewAllLink => new Link(ViewAllQuotationsLocator);

        /// <summary>
        /// Quotation type
        /// </summary>
        public QuotationTypeFacetComponent QuotationType => new QuotationTypeFacetComponent();

        /// <summary>
        /// Mischaracterization Identification facet
        /// </summary>
        public BaseSearchHierarchyFacetComponent MischaracterizationIdentificationFacet =>
        new BaseSearchHierarchyFacetComponent(MischaracterizationIdentificationFacetLocator);

        /// <summary>
        /// Differences facet
        /// </summary>
        public BaseSearchHierarchyFacetComponent DifferencesFacet =>
        new BaseSearchHierarchyFacetComponent(DifferencesFacetLocator);

        /// <summary>
        /// Content facet
        /// </summary>
        public BaseSearchHierarchyFacetComponent ContentFacet =>
            new BaseSearchHierarchyFacetComponent(ContentFacetLocator);

        /// <summary>
        /// The document facet.
        /// </summary>
        public BaseSearchHierarchyFacetComponent DocumentFacet =>
            new BaseSearchHierarchyFacetComponent(DocumentFacetLocator);

        /// <summary>
        /// The pin cite error facet.
        /// </summary>
        public BaseSearchHierarchyFacetComponent PinCiteErrorFacet =>
            new BaseSearchHierarchyFacetComponent(PinCiteErrorFacetLocator);

        /// <summary>
        /// The title search facet.
        /// </summary>
        public EdgeBaseFacetWithAppearingDialogComponent TitleSearchFacet =>
            new EdgeBaseFacetWithAppearingDialogComponent(TitleSearchFacetContainerLocator);
    }
}