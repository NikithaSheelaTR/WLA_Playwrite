namespace Framework.Common.Api.Endpoints.Report.DataModel
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Quote Check data
    /// </summary>
    public class DocAnalyzerQuoteCheckData
    {
        /// <summary>
        /// User quote text
        /// </summary>
        [JsonProperty("userQuoteText")]
        public string UserQuoteText { get; set; }

        /// <summary>
        /// QuoteCheck Citations
        /// </summary>
        [JsonProperty("citations")]
        public List<QuoteCheckCitation> Citations { get; set; }

        /// <summary>
        /// QuoteCheck Best Citation
        /// </summary>
        [JsonProperty("bestCitation")]
        public QuoteCheckCitation BestCitation { get; set; }
    }
}
