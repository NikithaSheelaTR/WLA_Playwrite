namespace Framework.Common.UI.Products.Shared.Models.CustomPages
{
    using System.Collections.Generic;

    /// <summary>
    /// The custom page contact model.
    /// </summary>
    public class CustomPageContactModel
    {
        /// <summary>
        /// Custom Page List
        /// </summary>
        public List<CustomPageModel> CustomPageList { get; set; }

        /// <summary>
        /// Gets or sets the Contact name.
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// Gets or sets the Custom Page Count.
        /// </summary>
        public int CustomPageCount { get; set; }

        /// <summary>
        /// Is Contact Active
        /// </summary>
        public bool IsContactActive { get; set; }

        /// <summary>
        /// Is Contact Expanded
        /// </summary>
        public bool IsContactExpanded { get; set; }
    }
}
