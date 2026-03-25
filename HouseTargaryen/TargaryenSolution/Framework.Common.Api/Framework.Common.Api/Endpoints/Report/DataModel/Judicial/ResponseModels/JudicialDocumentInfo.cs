namespace Framework.Common.Api.Endpoints.Report.DataModel.Judicial.ResponseModels
{
    using Newtonsoft.Json;

    /// <summary>
    /// JudicialDocumentInfo
    /// </summary>
    public class JudicialDocumentInfo
    {
        /// <summary>
        /// Document status
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// DocumentId
        /// </summary>
        [JsonProperty("documentId")]
        public string DocumentId { get; set; }

        /// <summary>
        /// Current status of document processing
        /// </summary>
        [JsonProperty("currentStep")]
        public string CurrentStep { get; set; }

        /// <summary>
        /// Is document converted
        /// </summary>
        [JsonProperty("ocrConverted")]
        public bool OcrConverted { get; set; }

        /// <summary>
        /// ErrorCode
        /// </summary>
        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }

    }
}
