namespace Framework.Common.Api.Endpoints.Document.DataModel.FocusHighlighting
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Document V2 Indigo Toc
    /// </summary>
    [DataContract]
    public class DocumentV2EdgeToc
    {
        /// <summary>
        /// Doc Guid
        /// </summary>
        [DataMember(Name = "docGuid")]
        public string DocGuid { get; set; }

        /// <summary>
        /// Entries
        /// </summary>
        [DataMember(Name = "entries")]
        public List<Entry> Entries { get; set; }
    }
}
