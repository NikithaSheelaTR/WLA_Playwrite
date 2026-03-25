namespace Framework.Common.UI.Products.WestlawEdge.Components.ResultList
{
    using Framework.Common.UI.Products.WestLawNext.Components.SearchResults;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;

    /// <summary>
    /// The base folder analysis tab component.
    /// </summary>
    public abstract class EdgeBaseFolderAnalysisTabComponent : BaseFolderAnalysisTabComponent
    {
        /// <summary>
        /// The toolbar for smart folders
        /// </summary>
        public new EdgeToolbarComponent Toolbar { get; set; } = new EdgeToolbarComponent();
    }
}
