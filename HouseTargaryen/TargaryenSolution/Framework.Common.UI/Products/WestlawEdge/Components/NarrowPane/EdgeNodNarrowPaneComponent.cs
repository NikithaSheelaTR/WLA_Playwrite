namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane
{
    using Framework.Common.UI.Products.WestlawEdge.Components.ToC;

    /// <summary>
    /// Indigo Notes Of Decision Narrow Pane Component
    /// </summary>
    public class EdgeNodNarrowPaneComponent
    {
        /// <summary>
        /// Notes Of Decision Filter facet
        /// </summary>
        public EdgeNotesOfDecisionsFacetsFilterComponent NodFilter { get; } =
            new EdgeNotesOfDecisionsFacetsFilterComponent();

        /// <summary>
        /// Notes Of Decisions TOC component
        /// </summary>
        public EdgeNotesOfDecisionsTocComponent NodToc { get; } = new EdgeNotesOfDecisionsTocComponent();
    }
}
