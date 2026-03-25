namespace Framework.Common.Api.Endpoints.Document.DataModel
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// The image guids map request model.
    /// </summary>
    public class ImageGuidsMapRequestModel
    {
        /// <summary>
        /// Gets or sets the gateway image guids.
        /// </summary>
        [JsonProperty("GatewayImageGuids")]
        public List<string> GatewayImageGuids { get; set; }
    }
}
