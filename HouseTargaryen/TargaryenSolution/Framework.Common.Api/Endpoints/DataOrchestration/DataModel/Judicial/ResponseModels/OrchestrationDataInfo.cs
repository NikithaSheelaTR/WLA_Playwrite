namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.ResponseModels
{
    using System.Collections.Generic;

    using Framework.Common.Api.Endpoints.Report.DataModel.Judicial.ResponseModels;

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
        /// partyId
        /// </summary>
        [JsonProperty("partyId")]
        public string PartyId { get; set; }

        /// <summary>
        /// partyName
        /// </summary>
        [JsonProperty("partyName")]
        public string PartyName { get; set; }

        /// <summary>
        /// CasesOmittedByBothCount
        /// </summary>
        [JsonProperty("casesOmittedByBothCount")]
        public int CasesOmittedByBothCount { get; set; }
        
        /// <summary>
        /// documents
        /// </summary>
        [JsonProperty("documents")]
        public List<UploadedDocumentInfo> Documents { get; set; }

        /// <summary>
        /// Quotes
        /// </summary>
        [JsonProperty("quotes")]
        public List<QuoteInfo> Quotes { get; set; }

        /// <summary>
        /// recommendations
        /// </summary>
        [JsonProperty("recommendations")]
        public List<RecommendationInfo> Recommendations { get; set; }
    }
}
