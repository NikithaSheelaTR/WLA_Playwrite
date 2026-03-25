namespace Framework.Common.UI.Raw.WestlawEdge.Models.RelatedDocuments
{
    /// <summary>
    /// Describe Secondary Sources in the Related Document Widget
    /// </summary>
    public class RelatedDocumentsBaseItemModel
    {
        /// <summary>
        /// Link name
        /// </summary>
        public string LinkName { get; set; }

        /// <summary>
        /// Verifies that Highlighted Search Term is displayed
        /// </summary>
        public bool IsHighlightedSearchTermDisplayed { get; set; }
    }
}
