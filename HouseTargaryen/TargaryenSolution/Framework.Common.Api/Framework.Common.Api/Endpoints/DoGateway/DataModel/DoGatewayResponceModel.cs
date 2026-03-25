namespace Framework.Common.Api.Endpoints.DoGateway.DataModel
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// DoGateway Response Model
    /// </summary>
    public class DoGatewayResponseModel
    {
        /// <summary>
        /// Gets or sets the requests.
        /// </summary>
        [JsonProperty("requests")]
        public List<UserRequestsToCourtModel> Requests { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the search succeeded.
        /// </summary>
        [JsonProperty("searchSucceeded")]
        public string SearchSucceeded { get; set; }

        /// <summary>
        /// Gets or sets the search time millis.
        /// </summary>
        [JsonProperty("searchTimeMillis")]
        public string SearchTimeMillis { get; set; }
    }
}