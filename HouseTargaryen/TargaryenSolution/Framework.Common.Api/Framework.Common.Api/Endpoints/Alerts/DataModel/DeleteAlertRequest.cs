namespace Framework.Common.Api.Endpoints.Alerts.DataModel
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Delete alert request
    /// </summary>
    public class DeleteAlertRequest
    {
        /// <summary>
        /// Gets or sets the alert type
        /// </summary>
        [JsonProperty("alertType")]
        public string AlertType { get; set; }

        /// <summary>
        /// Gets or sets the alert guids
        /// </summary>
        [JsonProperty("alertGuids")]
        public List<string> AlertGuids { get; set; }
    }
}
