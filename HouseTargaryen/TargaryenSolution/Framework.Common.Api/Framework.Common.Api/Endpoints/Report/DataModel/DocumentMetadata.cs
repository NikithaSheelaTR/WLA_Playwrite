namespace Framework.Common.Api.Endpoints.Report.DataModel
{
    using Framework.Common.Api.Endpoints.DataOrchestration.DataModel.DocumentAnalyzer;

    using Newtonsoft.Json;

    /// <summary>
    /// The document meta data.
    /// </summary>
    public class DocumentMetadata
    {
        /// <summary>
        /// Gets or sets the current step.
        /// </summary>
        [JsonProperty("currentStep")]
        public string CurrentStep { get; set; }

        /// <summary>
        /// Gets or sets the input type.
        /// </summary>
        [JsonProperty("inputType")]
        public string InputType { get; set; }

        /// <summary>
        /// Gets or sets original report info.
        /// </summary>
        [JsonProperty("isOriginalReport")]
        public string IsOriginalReport { get; set; }

        /// <summary>
        /// Gets or sets the error response.
        /// </summary>
        [JsonProperty("errorResponse")]
        public object ErrorResponse { get; set; }

        /// <summary>
        /// Gets or sets the orch Data Guid
        /// </summary>
        [JsonProperty("orchDataGuid")]
        public string OrchDataGuid { get; set; }

        /// <summary>
        /// Gets or sets the ocr job id
        /// </summary>
        [JsonProperty("ocrJobId")]
        public string OcrJobId { get; set; }

        /// <summary>
        /// Gets or sets the quote chech data guid
        /// </summary>
        [JsonProperty("quoteCheckDocGuid")]
        public string QuoteCheckDocGuid { get; set; }

        /// <summary>
        /// Gets or sets the quotations summary
        /// </summary>
        [JsonProperty("quotationsSummary")]
        public QuotationsCountSummary QuotationsSummary { get; set; }
    }
}
