namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.ResponseModels
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// MetadataInfo
    /// </summary>
    public class MetadataInfo
    {
        /// <summary>
        /// treatmentPhrase
        /// </summary>
        [JsonProperty("treatmentPhrase")]
        public string TreatmentPhrase { get; set; }

        /// <summary>
        /// docGuid
        /// </summary>
        [JsonProperty("docGuid")]
        public string DocGuid { get; set; }

        /// <summary>
        /// docFamilyGuid
        /// </summary>
        [JsonProperty("docFamilyGuid")]
        public string DocFamilyGuid { get; set; }

        /// <summary>
        /// title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// titleLink
        /// </summary>
        [JsonProperty("titleLink")]
        public string TitleLink { get; set; }

        /// <summary>
        /// court
        /// </summary>
        [JsonProperty("court")]
        public string Court { get; set; }

        /// <summary>
        /// liveDate
        /// </summary>
        [JsonProperty("liveDate")]
        public string LiveDate { get; set; }
    }
}
