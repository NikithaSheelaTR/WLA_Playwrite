namespace Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo
{
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;

    /// <summary>
    /// Indigo Other History Page
    /// </summary>
    public class EdgeOtherHistoryPage : OtherHistoryPage
    {
        /// <summary>
        ///  Gets or sets The toolbar across the top
        /// </summary>
        public new EdgeToolbarComponent Toolbar { get; set; } = new EdgeToolbarComponent();

        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeOtherHistoryPage"/> class. 
        /// </summary>
        public EdgeOtherHistoryPage()
        {
            this.Toolbar.DetailDropdown = new HistoryPageDetailDropdown();
        }
    }
}