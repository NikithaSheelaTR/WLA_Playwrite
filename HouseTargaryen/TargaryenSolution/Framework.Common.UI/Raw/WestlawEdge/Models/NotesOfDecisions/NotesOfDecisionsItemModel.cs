namespace Framework.Common.UI.Raw.WestlawEdge.Models.NotesOfDecisions
{
    /// <summary>
    /// The NOD item from the NOD tab
    /// </summary>
    public class NotesOfDecisionsItemModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether is checkbox selected.
        /// </summary>
        public bool IsCheckboxSelected { get; set; }

        /// <summary>
        /// Gets or sets the nod heading.
        /// </summary>
        public string NodHeading { get; set; }

        /// <summary>
        /// Gets or sets the nod text.
        /// </summary>
        public string NodText { get; set; }

        /// <summary>
        /// Gets or sets the nod subitem count.
        /// </summary>
        public int NodSubItemCount { get; set; }

        /// <summary>
        /// Gets or sets the nod key number icon count.
        /// </summary>
        public int NodKeyNumberIconCount { get; set; }
    }
}
