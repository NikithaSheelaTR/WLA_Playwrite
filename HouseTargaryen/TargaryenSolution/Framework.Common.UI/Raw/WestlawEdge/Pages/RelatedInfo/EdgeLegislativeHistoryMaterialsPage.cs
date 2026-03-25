namespace Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo
{
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;
    using Framework.Common.UI.Products.WestlawEdge.Components.StatutesHistory;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;

    /// <summary>
    /// Indigo Legislative History Materials Page
    /// </summary>
    public class EdgeLegislativeHistoryMaterialsPage : LegislativeHistoryMaterialsPage
    {
        /// <summary>
        ///  Gets or sets The toolbar across the top
        /// </summary>
        public new StatuteHistoryToolbarComponent Toolbar { get; set; } = new StatuteHistoryToolbarComponent();

        /// <summary>
        /// Statute History Content Types Navigation Component
        /// </summary>
        public StatuteHistoryContentTypesNavigationComponent ContentType { get; set; } = new StatuteHistoryContentTypesNavigationComponent();
    }
}