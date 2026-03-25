namespace Framework.Common.Api.Endpoints.Report.DataModel.Judicial.ResponseModels
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// OrchestrationDataInfo
    /// </summary>
    public class OrchestrationDataInfo
    {
        /// <summary>
        /// uploadSummary
        /// </summary>
        [JsonProperty("uploadSummary")]
        public UploadSummaryInfo UploadSummary { get; set; }
        
        /// <summary>
        /// cases
        /// </summary>
        [JsonProperty("cases")]
        public List<ExtractedCitationInfo> Cases { get; set; }

        /// <summary>
        /// InvalidCitations
        /// </summary>
        [JsonProperty("invalidCitations")]
        public List<JudicialInvalidCitationsInfo> InvalidCitations { get; set; }

        /// <summary>
        /// others
        /// </summary>
        [JsonProperty("others")]
        public List<ExtractedCitationInfo> Others { get; set; }

        /// <summary>
        /// regulations
        /// </summary>
        [JsonProperty("regulations")]
        public List<ExtractedCitationInfo> Regulations { get; set; }

        /// <summary>
        /// statutes
        /// </summary>
        [JsonProperty("statutes")]
        public List<ExtractedCitationInfo> Statutes { get; set; }

        /// <summary>
        /// trialCourtOrders
        /// </summary>
        [JsonProperty("trialCourtOrders")]
        public List<ExtractedCitationInfo> TrialCourtOrders { get; set; }
        
        /// <summary>
        /// quotationsCitationSummaries
        /// </summary>
        [JsonProperty("quotationsCitationSummaries")]
        public List<QuotationsCitationSummaryInfo> QuotationsCitationSummaries { get; set; }
    }
}
