namespace Framework.Common.Api.Endpoints.Document.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The document delivery info.
    /// </summary>
    [DataContract]
    public class DocumentDeliveryInfo
    {
        /// <summary>
        /// Gets or sets the session id.
        /// </summary>
        [DataMember(Name = "DocStarPagesRanges")]
        public string DocStarPagesRanges { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether has abridgment classification.
        /// </summary>
        [DataMember(Name = "hasAbridgmentClassification")]
        public bool HasAbridgmentClassification { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether has case annotation.
        /// </summary>
        [DataMember(Name = "hasCaseAnnotation")]
        public bool HasCaseAnnotation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether has headnotes.
        /// </summary>
        [DataMember(Name = "hasHeadnotes")]
        public bool HasHeadnotes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether has inline footnotes.
        /// </summary>
        [DataMember(Name = "hasInlineFootnotes")]
        public bool HasInlineFootnotes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether has non west headnotes.
        /// </summary>
        [DataMember(Name = "hasNonWestHeadnotes")]
        public bool HasNonWestHeadnotes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether has original image.
        /// </summary>
        [DataMember(Name = "hasOriginalImage")]
        public bool HasOriginalImage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether has star pages.
        /// </summary>
        [DataMember(Name = "hasStarPages")]
        public bool HasStarPages { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether has table of contents.
        /// </summary>
        [DataMember(Name = "hasTableOfContents")]
        public bool HasTableOfContents { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether hide inline key cite flags.
        /// </summary>
        [DataMember(Name = "hideInlineKeyCiteFlags")]
        public bool HideInlineKeyCiteFlags { get; set; }
    }
}