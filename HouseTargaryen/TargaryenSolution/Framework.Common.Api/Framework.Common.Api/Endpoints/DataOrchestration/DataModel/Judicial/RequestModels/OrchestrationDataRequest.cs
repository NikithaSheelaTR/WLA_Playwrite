namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.RequestModels
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Report info
    /// </summary>
    public class OrchestrationDataRequest
    {
        /// <summary>
        /// Report name
        /// </summary>
        [JsonProperty("reportName")]
        public string ReportName { get; set; }

        /// <summary>
        /// Report name
        /// </summary>
        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// Parties info
        /// </summary>
        [JsonProperty("parties")]
        public List<PartyInfoRequest> Parties { get; set; }
    }
}
