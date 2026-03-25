namespace Framework.Common.Api.Endpoints.Gateway.DataModel.Docket
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// The docket updates response model.
    /// </summary>
    public class DocketUpdatesResponseModel
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
        public List<UpdateItemModel> RequestItems { get; set; }
    }
}