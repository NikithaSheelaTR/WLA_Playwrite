
namespace Framework.Common.Api.Endpoints.Report.DataModel.Judicial.ResponseModels
{
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// DocumentsInfo
    /// </summary>
    public class DocumentsInfo
    {
        /// <summary>
        /// InvalidCitation
        /// </summary>
        [JsonProperty]
        public List<InvalidCitation> Items { get; set; }

        /// <summary>
        /// DocId
        /// </summary>
        [JsonProperty("docId")]
        public string DocId { get; set; }
    }
}
