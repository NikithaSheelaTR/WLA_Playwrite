namespace Framework.Common.Api.Endpoints.Report.DataModel
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// The document state info.
    /// </summary>
    public class DocAnalyzerAnalyzedData
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }
        
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        [JsonProperty("createdDate")]
        public string CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        [JsonProperty("updatedDate")]
        public string UpdatedDate { get; set; }
        
        /// <summary>
        /// Gets or sets the section count.
        /// </summary>
        [JsonProperty("sectionCount")]
        public int SectionCount { get; set; }
        
        /// <summary>
        /// Gets or sets the document metadata.
        /// </summary>
        [JsonProperty("documentMetadata")]
        public DocumentMetadata DocumentMetadata { get; set; }
        /// <summary>
        /// Gets or sets the sections.
        /// </summary>
        [JsonProperty("sections", NullValueHandling = NullValueHandling.Ignore)]
        public List<Section> Sections { get; set; }
    }
}
