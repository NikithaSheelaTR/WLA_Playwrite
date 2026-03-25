namespace Framework.Common.Api.Endpoints.Gateway.DataModel.Docket
{
    using Newtonsoft.Json;

    /// <summary>
    /// The delete requests response model.
    /// </summary>
    public class DeleteRequestsResponseModel
    {
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        [JsonProperty("statusCode")]
        public string StatusCode { get; set; }
    }
}