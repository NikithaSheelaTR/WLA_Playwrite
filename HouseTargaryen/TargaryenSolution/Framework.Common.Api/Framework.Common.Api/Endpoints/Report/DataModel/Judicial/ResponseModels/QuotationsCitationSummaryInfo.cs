namespace Framework.Common.Api.Endpoints.Report.DataModel.Judicial.ResponseModels
{
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// QuotationsCitationSummaryInfo
    /// </summary>
    public class QuotationsCitationSummaryInfo
    {
        /// <summary>
        /// guid
        /// </summary>
        [JsonProperty("guid")]
        public string Guid { get; set; }

        /// <summary>
        /// title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// courtline
        /// </summary>
        [JsonProperty("courtline")]
        public string Courtline { get; set; }

        /// <summary>
        /// primaryCitation
        /// </summary>
        [JsonProperty("primaryCitation")]
        public string PrimaryCitation { get; set; }

        /// <summary>
        /// quoteCountsByParty
        /// </summary>
        [JsonProperty("quoteCountsByParty")]
        public QuoteCountsByParty QuoteCountsByParty { get; set; }

        /// <summary>
        /// citedBy
        /// </summary>
        [JsonProperty("citedBy")]
        public List<CitedByInfo> CitedBy { get; set; }
    }
}
