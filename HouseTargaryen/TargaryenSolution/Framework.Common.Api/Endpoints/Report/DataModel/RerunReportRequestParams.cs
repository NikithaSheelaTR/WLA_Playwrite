namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.DocumentAnalyzer
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Rerun report request parameters
    /// </summary>
    public class RerunReportRequestParams
    {
        /// <summary>
        /// Court Level
        /// </summary>
        [JsonProperty("courtLevel")]
        public string CourtLevel { get; set; }

        /// <summary>
        /// Procedural Posture
        /// </summary>
        [JsonProperty("proceduralPosture")]
        public string ProceduralPosture { get; set; }

        /// <summary>
        /// Moving Party
        /// </summary>
        [JsonProperty("movingParty")]
        public string MovingParty { get; set; }

        /// <summary>
        /// Desired Outcome
        /// </summary>
        [JsonProperty("desiredOutcome")]
        public string DesiredOutcome { get; set; }

        /// <summary>
        /// Initial Report Id
        /// </summary>
        [JsonProperty("initialReportId")]
        public string InitialReportId { get; set; }
    }
}

