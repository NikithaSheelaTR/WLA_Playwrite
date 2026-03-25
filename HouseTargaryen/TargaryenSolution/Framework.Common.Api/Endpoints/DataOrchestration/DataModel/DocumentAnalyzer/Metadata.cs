namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.DocumentAnalyzer
{
    using System;
    using Newtonsoft.Json;
    
    /// <summary>
    /// Metadata
    /// </summary>
    public class Metadata
    {
        /// <summary>
        /// Gets or sets the treatment phrase.
        /// </summary>
        [JsonProperty("treatmentPhrase")]
        public string TreatmentPhrase { get; set; }

        /// <summary>
        /// Gets or sets the doc guid.
        /// </summary>
        [JsonProperty("docGuid")]
        public string DocGuid { get; set; }

        /// <summary>
        /// Gets or sets the doc family guid.
        /// </summary>
        [JsonProperty("docFamilyGuid")]
        public string DocFamilyGuid { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the title link.
        /// </summary>
        [JsonProperty("titleLink")]
        public Uri TitleLink { get; set; }

        /// <summary>
        /// Gets or sets the court.
        /// </summary>
        [JsonProperty("court")]
        public string Court { get; set; }

        /// <summary>
        /// Gets or sets the live date.
        /// </summary>
        [JsonProperty("liveDate")]
        public string LiveDate { get; set; }

        /// <summary>
        /// Gets or sets the citation.
        /// </summary>
        [JsonProperty("citation", NullValueHandling = NullValueHandling.Ignore)]
        public string Citation { get; set; }
    }
}
