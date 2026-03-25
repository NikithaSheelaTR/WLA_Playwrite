namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.DocumentAnalyzer
{
    using Newtonsoft.Json;

    /// <summary>
    /// The identified quotations.
    /// </summary>
    public class IdentifiedQuotations
    {
        /// <summary>
        /// Category
        /// </summary>
        [JsonProperty("category")]
        public string Category { get; set; }

        /// <summary>
        /// Quotations
        /// </summary>
        [JsonProperty("citationId", NullValueHandling = NullValueHandling.Ignore)]
        public string CitationId { get; set; }
        
        /// <summary>
        /// Quotations
        /// </summary>
        [JsonProperty("quotationId")]
        public string QuotationsId { get; set; }

        /// <summary>
        /// quotationsDocId
        /// </summary>
        [JsonProperty("quotationsDocId")]
        public string QuotationsDocId { get; set; }

        /// <summary>
        /// verify is object equals
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var identifiedQuote = obj as IdentifiedQuotations;

            return this.Category.Equals(identifiedQuote.Category)
                   && this.QuotationsDocId.Equals(identifiedQuote.QuotationsDocId)
                   && this.QuotationsId.Equals(identifiedQuote.QuotationsId);
        }

        /// <summary>
        /// GetHashCode of element
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.Category.GetHashCode() * this.QuotationsDocId.GetHashCode() + 3;
        }
    }
}
