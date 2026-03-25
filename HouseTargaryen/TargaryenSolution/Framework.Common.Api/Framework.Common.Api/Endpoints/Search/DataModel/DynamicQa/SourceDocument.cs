namespace Framework.Common.Api.Endpoints.Search.DataModel.DynamicQa
{
    using Newtonsoft.Json;

    /// <summary>
    /// The source document.
    /// </summary>
    public class SourceDocument
    {
        /// <summary>
        /// Gets or sets the doc guid.
        /// </summary>
        [JsonProperty("DocGuid")]
        public string DocGuid { get; set; }
    }
}
