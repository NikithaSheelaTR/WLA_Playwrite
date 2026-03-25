namespace Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo
{
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;

    /// <summary>
    /// Related Opinion Page
    /// </summary>
    public class EdgeRelatedOpinionPage : RelatedOpinionPage
    {
        /// <summary>
        ///  Gets or sets The toolbar across the top
        /// </summary>
        public new EdgeToolbarComponent Toolbar { get; set; } = new EdgeToolbarComponent();
    }
}