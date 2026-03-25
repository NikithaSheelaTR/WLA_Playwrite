namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.RequestModels
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Filter infi
    /// </summary>
    public class FilterInfo
    {
        /// <summary>
        /// list of results
        /// </summary>
        [JsonProperty("results")]
        public List<ResultInfo> Results { get; set; }
    }
}
