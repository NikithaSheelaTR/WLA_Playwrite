namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.Trillium
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Components.HomePage;
    using Framework.Common.UI.Products.Shared.Components.Toolbar.FooterToolbar;
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Products.WestLawNextCanada.Components;

    /// <summary>
    /// Trillium Page
    /// </summary>
    public class TrilliumPage : CommonBrowsePage
    {
        /// <summary>
        /// Trillium Component
        /// </summary>
        public TrilliumComponent Trillium { get; } = new TrilliumComponent();

        /// <summary>
        /// Narrow Pane  
        /// </summary>
        public NarrowPaneComponent NarrowPane { get; } = new NarrowPaneComponent();

        /// <summary>
        /// Header
        /// </summary>
        public WestlawNextHeaderComponent HeaderComponent { get; } = new WestlawNextHeaderComponent();

        /// <summary>
        /// Footer Toolbar
        /// </summary>
        public FooterToolbarComponent FooterToolbar { get; } = new FooterToolbarComponent();

        /// <summary>
        /// Trillium page Narrow Pane  
        /// </summary>
        public TrilliumLeftNarrowPane LeftNarrowPane { get; } = new TrilliumLeftNarrowPane();

        /// <summary>
        /// Favorites Component 
        /// </summary>
        public FavoritesComponent Favorite { get; } = new FavoritesComponent();       

        /// <summary>
        /// Header Component 
        /// </summary>
        public TrilliumHeader TrilliumHeader { get; } = new TrilliumHeader();
    }
}
