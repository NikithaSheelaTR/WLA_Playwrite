namespace Framework.Common.Api.Endpoints.Search.DataModel.DocumentAnalyzer.RecommendationsV5
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// The issues container
    /// </summary>
    public class IssuesContainer
    {
        /// <summary>
        /// Gets or sets Issues
        /// </summary>
        [JsonProperty("issues")]
        public List<Issue> Issues { get; set; }
    }
}