namespace Framework.Common.Api.Endpoints.Gateway.DataModel.Docket
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// The docket requests response model.
    /// </summary>
    public class DocketRequestsResponseModel
    {
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        [JsonProperty("statusCode")]
        public string StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the request items.
        /// </summary>
        [JsonProperty("requestItems")]
        public List<RequestItemModel> RequestItems { get; set; }
    }
}
