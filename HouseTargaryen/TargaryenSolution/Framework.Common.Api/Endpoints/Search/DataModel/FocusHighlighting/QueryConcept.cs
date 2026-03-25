namespace Framework.Common.Api.Endpoints.Search.DataModel.FocusHighlighting
{
   using Newtonsoft.Json;

    /// <summary>
    /// The query concept
    /// </summary>
    public class QueryConcept
    {
        /// <summary>
        /// Gets or sets the concept
        /// </summary>
        [JsonProperty("concept")]
        public string Concept { get; set; }

        /// <summary>
        /// Gets or sets the offsets
        /// </summary>
        [JsonProperty("offsets")]
        public string Offsets { get; set; }
    }
}