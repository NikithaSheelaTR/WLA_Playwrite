namespace Framework.Common.UI.Products.WestLawNextCanada.Pages
{
    using Framework.Common.UI.Products.WestLawNextCanada.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;

    /// <summary>
    /// EdgeCanadaCommonAuthenticatedWestlawNextPage can be used as a base page for EdgeCanadaCommonSearchHome and other WestlawNext pages
    /// </summary>
    public class CanadaEdgeCommonAuthenticatedWestlawNextPage : EdgeCommonAuthenticatedWestlawNextPage
    {
        /// <summary>
        /// Gets the header.
        /// </summary>
        public new CanadaEdgeHeaderComponent Header { get; } = new CanadaEdgeHeaderComponent();

        /// <summary>
        /// Gets the header.
        /// </summary>
        public new CanadaEdgeFooterComponent Footer { get; } = new CanadaEdgeFooterComponent();
    }
}
