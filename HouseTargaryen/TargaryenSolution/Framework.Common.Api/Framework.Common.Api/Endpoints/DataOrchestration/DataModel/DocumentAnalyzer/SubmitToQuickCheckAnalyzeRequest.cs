namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.DocumentAnalyzer
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// SubmitToQuickCheckAnalyzeRequest
    /// </summary>
    public class SubmitToQuickCheckAnalyzeRequest
    {
        /// <summary>
        /// clientId
        /// </summary>
        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// contentType
        /// </summary>
        [JsonProperty("contentType")]
        public string ContentType { get; set; }

        /// <summary>
        /// documentId
        /// </summary>
        [JsonProperty("documentId")]
        public string DocumentId { get; set; }

        /// <summary>
        /// documentTitle
        /// </summary>
        [JsonProperty("documentTitle")]
        public string DocumentTitle { get; set; }

        /// <summary>
        /// reportType
        /// </summary>
        [JsonProperty("reportType")]
        public string ReportType { get; set; }
    }
}
