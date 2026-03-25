namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.ResponseModels
{
    using System;
    using System.Collections.Generic;

    using Framework.Core.Utils;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// RecommendationsByTypeInfo
    /// </summary>
    public class RecommendationsByTypeInfo
    {
        /// <summary>
        /// type
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// recommendations
        /// </summary>
        [JsonProperty("recommendations")]
        public List<RecommendationInfo> Recommendations { get; set; }

        /// <summary>
        /// return recommendations for current info
        /// </summary>
        /// <returns></returns>
        public List<RecommendationInfo> GetRecommendations()
        {
            try
            {
                return this.Recommendations;
            }
            catch (NullReferenceException)
            {
                Logger.LogInfo("No recommendations for this issue");
                return new List<RecommendationInfo>();
            }
        }
    }
}
