namespace Framework.Common.Api.Endpoints.Report.DataModel.Judicial.ResponseModels
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// JudicialQuotationsOrchestrationData
    /// </summary>
    public class JudicialQuotationsOrchestrationData
    {
        /// <summary>
        /// OverallQuoteCounts
        /// </summary>
        [JsonProperty("overallQuoteCounts")]
        public OverallQuoteCountsInfo OverallQuoteCounts { get; set; }

        /// <summary>
        /// UploadSummary
        /// </summary>
        [JsonProperty("uploadSummary")]
        public UploadSummaryInfo UploadSummary { get; set; }

        /// <summary>
        /// PartyId
        /// </summary>
        [JsonProperty("partyId")]
        public string PartyId { get; set; }

        /// <summary>
        /// PartyName
        /// </summary>
        [JsonProperty("partyName")]
        public string PartyName { get; set; }

        /// <summary>
        /// Quotes
        /// </summary>
        [JsonProperty("quotes")]
        public object[] Quotes { get; set; }

        /// <summary>
        /// QuotationsCitationSummaries
        /// </summary>
        [JsonProperty("quotationsCitationSummaries")]
        public List<QuotationsCitationSummaryInfo> QuotationsCitationSummaries { get; set; }

        /// <summary>
        /// InvalidCitations
        /// </summary>
        [JsonProperty("invalidCitations")]
        public JudicialInvalidCitationsInfo InvalidCitations { get; set; }
    }
}
