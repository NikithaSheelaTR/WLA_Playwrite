namespace Framework.Common.UI.Raw.WestlawEdge.Models
{
    /// <summary>
    /// The base folder analysis result list item model.
    /// </summary>
    public class BaseFolderAnalysisResultListItemModel : BaseEdgeResultListItemModel
    {
        /// <summary>
        /// The Index
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Returns true if Document has 'NEW' icon
        /// </summary>
        public bool IsItemNew { get; set; }
    }
}
