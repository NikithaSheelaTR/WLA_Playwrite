namespace Framework.Common.UI.Products.WestLawNext.Models.EnumProperties
{
    using Framework.Core.DataModel;

    /// <summary>
    /// The content type info.
    /// </summary>
    public class ContentTypeInfo : BaseTextModel
    {
        /// <summary>
        /// string used in ids for content type specific items in the narrow pane
        /// </summary>
        public string NarrowPaneLinkLocatorString { get; set; }

        /// <summary>
        /// string used in ids for content type specific search results
        /// </summary>
        public string SearchResultsLocatorString { get; set; }
    }
}