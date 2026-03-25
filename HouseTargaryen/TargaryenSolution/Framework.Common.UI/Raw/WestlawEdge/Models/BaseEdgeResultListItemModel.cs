namespace Framework.Common.UI.Raw.WestlawEdge.Models
{
    using Framework.Core.CommonTypes.Enums;

    /// <summary>
    /// Base Indigo Result List Item Model
    /// </summary>
    public class BaseEdgeResultListItemModel
    {
        /// <summary>
        /// The KeyCite present.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsKeyCitePresent { get; set; }

        /// <summary>
        /// The Implied Overruling icon is present.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsImpliedOverrulingPresent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is citing references link present.
        /// </summary>
        public bool IsCitingReferencesLinkDisplayed { get; set; }

        /// <summary>
        /// Gets or sets the citing references count.
        /// </summary>
        public int CitingReferencesCount { get; set; }

        /// <summary>
        /// The Flag
        /// </summary>
        public KeyCiteFlag KeyCiteFlag { get; set; }

        /// <summary>
        /// The get item title text
        /// </summary>
        /// <returns></returns>
        public string TitleText { get; set; }
    }
}