namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.DocumentAnalyzer
{
    using Newtonsoft.Json;
    
    /// <summary>
    /// MostNegativeCitation
    /// </summary>
    public class MostNegativeCitation
    {
        /// <summary>
        /// Gets or sets the doc guid.
        /// </summary>
        [JsonProperty("docGuid")]
        public string DocGuid { get; set; }

        /// <summary>
        /// Gets or sets the flags.
        /// </summary>
        [JsonProperty("flags")]
        public Flag[] Flags { get; set; }
    }

}
