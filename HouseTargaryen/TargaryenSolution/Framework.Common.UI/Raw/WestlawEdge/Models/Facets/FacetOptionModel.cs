namespace Framework.Common.UI.Raw.WestlawEdge.Models.Facets
{
    /// <summary>
    /// The facet option model.
    /// </summary>
    public class FacetOptionModel
    {
        /// <summary>
        /// Title of item
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Count of item
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Is checkbox displayed
        /// </summary>
        public bool IsDisplayed { get; set; }

        /// <summary>
        /// The is enabled.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Is checkbox selected
        /// </summary>
        public bool IsSelected { get; set; }
    }
}