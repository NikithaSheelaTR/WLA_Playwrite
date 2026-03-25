namespace Framework.Common.Api.Endpoints.Report.DataModel.Judicial.ResponseModels
{
    using Newtonsoft.Json;

    /// <summary>
    /// CurrentPuiInfo
    /// </summary>
    public class CurrentPuiInfo
    {
        /// <summary>
        /// inFolder
        /// </summary>
        [JsonProperty("inFolder")]
        public bool InFolder { get; set; }

        /// <summary>
        /// hasAnnotations
        /// </summary>
        [JsonProperty("hasAnnotations")]
        public bool HasAnnotations { get; set; }

        /// <summary>
        /// previouslyViewed
        /// </summary>
        [JsonProperty("isPreviouslyViewed")]
        public bool IsPreviouslyViewed { get; set; }

        /// <summary>
        /// hasHighlights
        /// </summary>
        [JsonProperty("hasHighlights")]
        public bool HasHighlights { get; set; }

        /// <summary>
        /// hasTextNotes
        /// </summary>
        [JsonProperty("hasTextNotes")]
        public bool HasTextNotes { get; set; }

        /// <summary>
        /// InPlan
        /// </summary>
        [JsonProperty("inPlan")]
        public bool InPlan { get; set; }
    }
}
