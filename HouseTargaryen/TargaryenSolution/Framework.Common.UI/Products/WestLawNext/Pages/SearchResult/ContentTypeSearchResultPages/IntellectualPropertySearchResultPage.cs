namespace Framework.Common.UI.Products.WestLawNext.Pages.SearchResult.ContentTypeSearchResultPages
{
	using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets;
    using Framework.Common.UI.Products.Shared.Components.ResultList;
    using Framework.Common.UI.Products.Shared.Components.Toolbar.CustomToolbars;
	using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.WestLawNext.Components.SearchResults;
	using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

	/// <summary>
	/// Common Search Result Page for Intellectual Property content
	/// </summary>
	/// <typeparam name="TItem">ResultListItem</typeparam>
	public class IntellectualPropertySearchResultPage<TItem> : BaseContentTypeSearchResultPage<TItem>
		where TItem : ResultListItem
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IntellectualPropertySearchResultPage{TItem}"/> class. 
		/// Extra wait in case of external AWS Image search functionality invocation
		/// </summary>
		public IntellectualPropertySearchResultPage()
		{
			DriverExtensions.WaitForPageLoad(60000);
			DriverExtensions.WaitForJavaScript(60000);
		}

		/// <summary> 
		/// Get Graph Facet.
		/// </summary>
		public GraphsComponent Graphs { get; } = new GraphsComponent();

        /// <summary>
        /// image Variants tab
        /// </summary>
        public ImageVariantsComponent ImageVariants { get; } = new ImageVariantsComponent();

        /// <summary>
        /// Ip narrow pane Component
        /// </summary>
        public IpNarrowPaneComponent IpNarrowPane { get; } = new IpNarrowPaneComponent();

        /// <summary>
        /// IpToolbar
        /// </summary>
        public new IpToolbar Toolbar { get; } = new IpToolbar();

		/// <summary>
		/// Table View Component
		/// </summary>
		public TableViewComponent TableViewComponent { get; } = new TableViewComponent();

		/// <summary>
		/// Tile View Component
		/// </summary>
		public TileViewComponent TileViewComponent { get; } = new TileViewComponent();
    }
}