namespace Framework.Common.Api.Endpoints.Report.DataModel
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// The legal issues container.
    /// </summary>
    public class LegalIssuesContainer
    {
        /// <summary>
        /// Gets or sets the issues.
        /// </summary>
        [JsonProperty("issues")]
        public List<LegalIssue> Issues { get; set; }
    }
}