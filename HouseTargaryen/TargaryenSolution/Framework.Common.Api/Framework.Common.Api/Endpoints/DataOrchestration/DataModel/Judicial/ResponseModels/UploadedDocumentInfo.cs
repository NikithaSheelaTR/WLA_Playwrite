namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.ResponseModels
{
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// UploadedDocumentInfo
    /// </summary>
    public class UploadedDocumentInfo
    {
        /// <summary>
        /// docId
        /// </summary>
        [JsonProperty("docId")]
        public string DocId { get; set; }

        /// <summary>
        /// docName
        /// </summary>
        [JsonProperty("docName")]
        public string DocName { get; set; }

        /// <summary>
        /// issues
        /// </summary>
        [JsonProperty("issues")]
        public List<Issue> Issues { get; set; }
    }
}
