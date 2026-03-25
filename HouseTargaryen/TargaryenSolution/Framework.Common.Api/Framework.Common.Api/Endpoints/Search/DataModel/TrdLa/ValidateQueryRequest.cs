namespace Framework.Common.Api.Endpoints.Search.DataModel.TrdLa
{
    using Newtonsoft.Json;

    /// <summary>
    /// Request from Search Validate endpoint
    /// </summary>
    public class ValidateQueryRequest
    {
        /// <summary>
        /// Gets or sets query
        /// </summary>
        [JsonProperty("query")]
        public string Query { get; set; }
    }
}