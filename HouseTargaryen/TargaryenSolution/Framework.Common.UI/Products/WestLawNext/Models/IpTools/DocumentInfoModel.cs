namespace Framework.Common.UI.Products.WestLawNext.Models.IpTools
{
    using Framework.Core.CommonTypes.Enums;

    /// <summary>
    /// The document info model.
    /// </summary>
    public class DocumentInfoModel
    {
        /// <summary>
        /// The citation.
        /// </summary>
        public string Citation { get; set; }

        /// <summary>
        /// The citation.
        /// </summary>
        public KeyCiteFlag Flag { get; set; }

        /// <summary>
        /// The GUID.
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// The long title.
        /// </summary>
        public string LongTitle { get; set; }

        /// <summary>
        /// Gets or sets the snippet.
        /// </summary>
        public string Snippet { get; set; }

        /// <summary>
        /// The title.
        /// </summary>
        public string Title { get; set; }
    }
}