namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.ResponseModels
{
    using Newtonsoft.Json;
    
    /// <summary>
    /// Uploaded document info
    /// </summary>
    public class UploadingDocumentInfo
    {
        /// <summary>
        /// DocumentId
        /// </summary>
        [JsonProperty("documentId")]
        public string DocumentId { get; set; }

        /// <summary>
        /// Document status
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

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
        /// Error code
        /// </summary>
        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }
    }
}
