namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.DocumentAnalyzer
{
    using Newtonsoft.Json;

    /// <summary>
    /// The input quotation.
    /// </summary>
    public class InputQuotation
    {
        /// <summary>
        /// User quote text
        /// </summary>
        [JsonProperty("quoteText")]
        public string QuoteText { get; set; }

        /// <summary>
        /// User Pre quote text
        /// </summary>
        [JsonProperty("preQuoteText")]
        public string PreQuoteText { get; set; }

        /// <summary>
        /// User Post quote text
        /// </summary>
        [JsonProperty("postQuoteText")]
        public string PostQuoteText { get; set; }
    }
}
