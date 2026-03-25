namespace Framework.Common.Api.Endpoints.Document.DataModel.DeliveryFoUriPathInfo
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The size .
    /// </summary>
    [DataContract]
    public class Size
    {
        /// <summary>
        /// Gets or sets the bottom margin.
        /// </summary>
        [DataMember(Name = "BottomMargin")]
        public int BottomMargin { get; set; }

        /// <summary>
        /// Gets or sets the left margin.
        /// </summary>
        [DataMember(Name = "LeftMargin")]
        public int LeftMargin { get; set; }

        /// <summary>
        /// Gets or sets the page height.
        /// </summary>
        [DataMember(Name = "PageHeight")]
        public int PageHeight { get; set; }

        /// <summary>
        /// Gets or sets the page width.
        /// </summary>
        [DataMember(Name = "PageWidth")]
        public int PageWidth { get; set; }

        /// <summary>
        /// Gets or sets the right margin.
        /// </summary>
        [DataMember(Name = "RightMargin")]
        public int RightMargin { get; set; }

        /// <summary>
        /// Gets or sets the top margin.
        /// </summary>
        [DataMember(Name = "TopMargin")]
        public int TopMargin { get; set; }
    }
}