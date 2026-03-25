namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.DocumentAnalyzer
{
    using Framework.Common.Api.Endpoints.Report.DataModel;

    using Newtonsoft.Json;

    /// <summary>
    /// Quotations data
    /// </summary>
    public class QuotationsData
    {
        /// <summary>
        /// Inut quotation
        /// </summary>
        [JsonProperty("inputQuotation")]
        public InputQuotation InputQuotation { get; set; }

        /// <summary>
        /// Best citation
        /// </summary>
        [JsonProperty("citation")]
        public QuoteCheckCitation BestCitation { get; set; }

        /// <summary>
        /// Westlaw matched quote
        /// </summary>
        [JsonProperty("matchedQuotation")]
        public WLMatchedQuotation MatchedQuotation { get; set; }
    }
}
