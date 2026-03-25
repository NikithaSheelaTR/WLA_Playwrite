namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.ResponseModels
{
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Issue info
    /// </summary>
    public class Issue
    {
        /// <summary>
        /// Issue Title
        /// </summary>
        [JsonProperty("issueTitle")]
        public string IssueTitle { get; set; }

        /// <summary>
        /// guid
        /// </summary>
        [JsonProperty("guid")]
        public string Guid { get; set; }

        /// <summary>
        /// additionalCasesCount
        /// </summary>
        [JsonProperty("additionalCasesCount")]
        public int AdditionalCasesCount { get; set; }

        /// <summary>
        /// recommendationsByTypes
        /// </summary>
        [JsonProperty("recommendationsByTypes")]
        public List<RecommendationsByTypeInfo> RecommendationsByTypes { get; set; }

        /// <summary>
        /// recommendations
        /// </summary>
        [JsonProperty("additionalCases")]
        public List<RecommendationInfo> AdditionalCases { get; set; }
    }
}
