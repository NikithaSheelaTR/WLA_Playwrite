namespace Framework.Common.UI.Raw.WestlawEdge.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Indigo Result List Item Model
    /// </summary>
    public class EdgeCasesResultListItemModel : BaseDocumentResultListItemModel
    {
        /// <summary>
        /// The get court level.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string CourtLevel { get; set; }

        /// <summary>
        /// Check if there are highlighted terms in Summary
        /// </summary>
        /// <returns></returns>
        public bool IsHighlightedInSnippets { get; set; }

        /// <summary>
        /// Check if there are highlighted terms in Summary
        /// </summary>
        /// <returns></returns>
        public bool IsHighlightedInSummary { get; set; }

        /// <summary>
        /// Check if there are highlighted terms in Synopsis
        /// </summary>
        /// <returns></returns>
        public bool IsHighlightedInSynopsis { get; set; }

        /// <summary>
        /// The disclosed synopsis link.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSynopsisExpanded { get; set; }

        /// <summary>
        /// The present synopsis link.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSynopsisDisplayed { get; set; }

        /// <summary>
        /// The get Snippets text.
        /// </summary>
        /// <returns></returns>
        public List<string> SnippetsText { get; set; }

        /// <summary>
        /// The get synopsis text.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string SynopsisText { get; set; }
    }
}