namespace Framework.Common.UI.Raw.WestlawEdge.Models.TrDiscover
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The type ahead suggestion model.
    /// </summary>
    public class TrdSuggestionModel
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the jurisdiction.
        /// </summary>
        public string Jurisdiction { get; set; }

        /// <summary>
        /// Gets or sets the search term.
        /// </summary>
        public List<string> SearchTerm { get; set; }

        /// <summary>
        /// Gets or sets the search term.
        /// </summary>
        public string Predicate { get; set; }

        /// <summary>
        /// Is Predicate gray
        /// </summary>
        public bool IsPredicateGray { get; set; }

        /// <summary>
        /// Gets or sets the entity.
        /// </summary>
        public string Entity { get; set; }

        /// <summary>
        /// Is item currently Displayed in Typeahad 
        /// </summary>
        public bool IsDisplayed { get; set; }
    }
}
