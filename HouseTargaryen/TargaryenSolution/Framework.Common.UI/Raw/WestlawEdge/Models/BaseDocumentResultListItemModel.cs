namespace Framework.Common.UI.Raw.WestlawEdge.Models
{
    using System;

    /// <summary>
    /// The Document Item Model
    /// </summary>
    public abstract class BaseDocumentResultListItemModel : BaseEdgeResultListItemModel
    {
        /// <summary>
        /// The get date.
        /// </summary>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        public DateTime Date { get; set; }

        /// <summary>
        /// The Index
        /// </summary>
        public int Index { get; set; }
    }
}