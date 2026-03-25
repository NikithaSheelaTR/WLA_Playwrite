namespace Framework.Common.Api.Endpoints.Foldering.DataModel
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// The pui request.
    /// </summary>
    public class PuiRequest
    {
        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the icon flags.
        /// </summary>
        [JsonProperty("iconFlags")]
        public List<string> IconFlags { get; set;}

        /// <summary>
        /// Gets or sets the documents guids.
        /// </summary>
        [JsonProperty("documentGuids")]
        public List<string> DocumentsGuids { get; set; }
    }
}
