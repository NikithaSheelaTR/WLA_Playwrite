namespace Framework.Common.UI.Raw.WestlawEdge.Pages.Document
{
    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;

    /// <summary>
    /// Edge Super Browse Page
    /// </summary>
    public class EdgeSuperBrowsePage : BaseSuperBrowsePage
    {
        /// <summary> Toolbar component  </summary>
        public EdgeToolbarComponent Toolbar { get; protected set; } = new EdgeToolbarComponent();
    }
}
