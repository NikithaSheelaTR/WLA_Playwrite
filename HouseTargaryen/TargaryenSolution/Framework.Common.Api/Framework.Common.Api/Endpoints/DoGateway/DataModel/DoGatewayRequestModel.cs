namespace Framework.Common.Api.Endpoints.DoGateway.DataModel
{
    using Newtonsoft.Json;

    /// <summary>
    /// DoGateway Request Model
    /// </summary>
    public class DoGatewayRequestModel
    {
        /// <summary>
        /// Gets or sets the last n days.
        /// </summary>
        [JsonProperty("lastNDays")]
        public string LastNDays { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether errors only.
        /// </summary>
        [JsonProperty("errorsOnly")]
        public bool ErrorsOnly { get; set; }

        /// <summary>
        /// Gets or sets the required keywords.
        /// </summary>
        [JsonProperty("requiredKeywords")]
        public string[] RequiredKeywords { get; set; }
    }
}