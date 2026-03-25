namespace Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo
{
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.WestlawEdge.Components.Document;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;

    /// <summary>
    /// Caselaw Appellate History Page
    /// </summary>
    public class EdgeCaselawAppellateHistoryPage : CaselawAppellateHistoryPage
    {
        /// <summary>
        ///  Gets or sets The toolbar across the top
        /// </summary>
        public new EdgeToolbarComponent Toolbar { get; set; } = new EdgeToolbarComponent();

        /// <summary>
        /// Edge fixed header
        /// </summary>
        public new EdgeDocumentFixedHeaderComponent FixedHeader { get; } = new EdgeDocumentFixedHeaderComponent();

        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeCaselawAppellateHistoryPage"/> class. 
        /// </summary>
        public EdgeCaselawAppellateHistoryPage()
        {
            Toolbar.DetailDropdown = new HistoryPageDetailDropdown();
        }
    }
}