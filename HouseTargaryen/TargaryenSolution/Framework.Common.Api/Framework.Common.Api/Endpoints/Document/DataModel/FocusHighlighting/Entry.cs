namespace Framework.Common.Api.Endpoints.Document.DataModel.FocusHighlighting
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Entry
    /// </summary>
    [DataContract]
    public class Entry
    {
        /// <summary>
        /// Indent
        /// </summary>
        [DataMember(Name = "indent")]
        public int Indent { get; set; }

        /// <summary>
        /// SectionId
        /// </summary>
        [DataMember(Name = "secondId")]
        public string SectionId { get; set; }

        /// <summary>
        /// Text
        /// </summary>
        [DataMember(Name = "text")]
        public string Text { get; set; }

        /// <summary>
        /// WordCount
        /// </summary>
        [DataMember(Name = "wordCount")]
        public int WordCount { get; set; }

        /// <summary>
        /// Concepts
        /// </summary>
        [DataMember(Name = "concepts")]
        public List<Concept> Concepts { get; set; }

        /// <summary>
        /// LeaderText
        /// </summary>
        [DataMember(Name = "leaderText")]
        public string LeaderText { get; set; }

        /// <summary>
        /// Head Text
        /// </summary>
        [DataMember(Name = "headText")]
        public string HeadText { get; set; }

        /// <summary>
        /// Preview Text
        /// </summary>
        [DataMember(Name = "previewText")]
        public string PreviewText { get; set; }
    }
}
