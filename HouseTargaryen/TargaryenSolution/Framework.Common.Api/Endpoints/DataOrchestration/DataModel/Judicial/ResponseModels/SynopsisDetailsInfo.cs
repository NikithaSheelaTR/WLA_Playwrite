namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.ResponseModels
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// SynopsisDetailsInfo
    /// </summary>
    public class SynopsisDetailsInfo
    {
        /// <summary>
        /// Paragraph
        /// </summary>
        [JsonProperty("paragraph")]
        public string Paragraph { get; set; }

        /// <summary>
        /// HoldingsHeading
        /// </summary>
        [JsonProperty("holdingsHeading")]
        public string HoldingsHeading { get; set; }

        /// <summary>
        /// OverallHolding
        /// </summary>
        [JsonProperty("overallHolding")]
        public List<string> OverallHolding { get; set; }

        /// <summary>
        /// Holdings
        /// </summary>
        [JsonProperty("holdings")]
        public List<HoldingInfo> Holdings { get; set; }
    }
}
