namespace Framework.Common.Api.Endpoints.Report.DataModel.Judicial.ResponseModels
{
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// JudicialInvalidCitationsInfo
    /// </summary>
    public class JudicialInvalidCitationsInfo
    {
        /// <summary>
        /// DocumentsInfo
        /// </summary>
        [JsonProperty("documents")]
        public List<DocumentsInfo> Documents { get; set; }

        /// <summary>
        /// PartyId
        /// </summary>
        [JsonProperty("partyId")]
        public int PartyId { get; set; } 
    }
}
