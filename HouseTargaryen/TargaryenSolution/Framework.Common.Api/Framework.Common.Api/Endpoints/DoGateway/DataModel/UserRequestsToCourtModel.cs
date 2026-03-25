namespace Framework.Common.Api.Endpoints.DoGateway.DataModel
{
    using Newtonsoft.Json;

    /// <summary>
    /// User Requests To Court Model
    /// </summary>
    public class UserRequestsToCourtModel
    {
        /// <summary>
        /// Gets or sets the request id.
        /// </summary>
        [JsonProperty("requestId")]
        public string RequestId { get; set; }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        [JsonProperty("host")]
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        [JsonProperty("port")]
        public string Port { get; set; }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }
}