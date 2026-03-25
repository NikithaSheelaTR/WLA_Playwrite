namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.DocumentAnalyzer
{
    using Newtonsoft.Json;

    /// <summary>
    /// The wl matched quotation.
    /// </summary>
    public class WLMatchedQuotation
    {
        /// <summary>
        /// User quote text
        /// </summary>
        [JsonProperty("quoteText")]
        public string QuoteText { get; set; }
        
        /// <summary>
        /// User quote text
        /// </summary>
        [JsonProperty("preQuoteText")]
        public string PreQuoteText { get; set; }

        /// <summary>
        /// User quote text
        /// </summary>
        [JsonProperty("postQuoteText")]
        public string PostQuoteText { get; set; }
    }
}
