namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.RequestModels
{
    using System;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Expire report info
    /// </summary>
    public class ExpiryReportInfo
    {
        /// <summary>
        /// date time report duration
        /// </summary>
        [JsonProperty("duration")]
        public DateTime Duration { get; set; }

        /// <summary>
        /// type of current report
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
