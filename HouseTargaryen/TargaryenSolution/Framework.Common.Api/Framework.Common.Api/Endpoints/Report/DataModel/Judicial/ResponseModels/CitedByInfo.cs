namespace Framework.Common.Api.Endpoints.Report.DataModel.Judicial.ResponseModels
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// CitedByInfo
    /// </summary>
    public class CitedByInfo
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
        /// docIndex
        /// </summary>
        [JsonProperty("docIndex")]
        public int DocIndex { get; set; }

        /// <summary>
        /// partyId
        /// </summary>
        [JsonProperty("partyId")]
        public string PartyId { get; set; }

        /// <summary>
        /// partyName
        /// </summary>
        [JsonProperty("partyName")]
        public string PartyName { get; set; }
    }
}
