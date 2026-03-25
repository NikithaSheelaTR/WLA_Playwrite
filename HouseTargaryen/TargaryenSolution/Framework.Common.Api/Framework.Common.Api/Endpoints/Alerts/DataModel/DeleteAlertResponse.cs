namespace Framework.Common.Api.Endpoints.Alerts.DataModel
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    /// <summary>
    /// Delete Alert Response
    /// </summary>
    public class DeleteAlertResponse
    {
        /// <summary>
        /// List of success guids
        /// </summary>
        [JsonProperty("successGuids")]
        public List<AlertGuid> SuccessGuids { get; set; }

        /// <summary>
        /// List of failure guids
        /// </summary>
        [JsonProperty("failureGuids")]
        public List<AlertGuid> FailureGuids { get; set; }
    }

    /// <summary>
    /// Alert Guid
    /// </summary>
    public class AlertGuid
    {
        /// <summary>
        /// Guid
        /// </summary>
        [JsonProperty("guid")]
        public string Guid { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
