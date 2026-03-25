namespace Framework.Common.Api.Endpoints.Document.DataModel
{
    using Newtonsoft.Json;

    /// <summary>
    /// The image guids map.
    /// </summary>
    public class ImageGuidsMap
    {
        /// <summary>
        /// Gets or sets the gateway image guid.
        /// </summary>
        [JsonProperty("GatewayImageGuid")]
        public string GatewayImageGuid { get; set; }

        /// <summary>
        /// Gets or sets the novus image guid.
        /// </summary>
        [JsonProperty("NovusImageGuid")]
        public string NovusImageGuid { get; set; }
    }
}
