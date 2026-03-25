namespace Framework.Common.UI.Products.TaxnetPro.Pages.RelatedInfo
{
    using Framework.Common.UI.Products.TaxnetPro.Components.Document.RI;
    using Framework.Common.UI.Products.TaxnetPro.Components.NarrowPane;
    using Framework.Common.UI.Products.TaxnetPro.Components.Toolbar.Footer;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo;

    /// <summary>
    /// TaxnetPro Tab Page
    /// </summary>
    public class TaxnetProTabPage : EdgeTabPage
    {
        private static readonly string CrossReferencesGridLocator = "#co_relatedInfo_table_xrefs";

        /// <summary>
        /// Reference grid
        /// </summary>
        new public CrossReferencesGridComponent ReferenceGrid { get; } = new CrossReferencesGridComponent(CrossReferencesGridLocator);

        /// <summary>
        /// Document Language Facet
        /// </summary>
        public DocumentLanguageFacetComponent DocumentLanguageFacet { get; } = new DocumentLanguageFacetComponent();

        /// <summary>
        /// Narrow Pane Component
        /// </summary>
        new public EdgeNarrowPaneComponent NarrowPane { get; } = new EdgeNarrowPaneComponent();

        /// <summary>
        /// Pagination component
        /// </summary>
        public TaxnetProPaginationFooterComponent PaginationComponent { get; } = new TaxnetProPaginationFooterComponent();
    }
}
