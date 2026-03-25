namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.ResponseModels
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

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
        [JsonProperty("previouslyViewed")]
        public PreviouslyViewedDocument PreviouslyViewed { get; set; }

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
    }
}
