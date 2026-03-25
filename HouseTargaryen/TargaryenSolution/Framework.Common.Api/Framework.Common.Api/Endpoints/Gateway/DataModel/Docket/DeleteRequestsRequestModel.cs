namespace Framework.Common.Api.Endpoints.Gateway.DataModel.Docket
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// The delete requests request model.
    /// </summary>
    public class DeleteRequestsRequestModel
    {
        /// <summary>
        /// Gets or sets the request ids.
        /// </summary>
        [JsonProperty("requestIds")]
        public List<string> RequestIds { get; set; }
    }
}