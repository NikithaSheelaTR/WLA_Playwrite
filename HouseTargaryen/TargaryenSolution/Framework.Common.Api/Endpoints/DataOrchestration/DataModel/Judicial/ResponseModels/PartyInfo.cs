namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.ResponseModels
{
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// PartyInfo
    /// </summary>
    public class PartyInfo
    {
        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// documents
        /// </summary>
        [JsonProperty("documents")]
        public List<DocInfo> Documents { get; set; }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj">Equals object</param>
        /// <returns>true if equals, false otherwise</returns>
        public override bool Equals(object obj)
        {
            var res = obj as PartyInfo;

            return this.Name.Equals(res.Name) && this.Id.Equals(res.Id) && this.Documents.Equals(res.Documents);
        }

        /// <summary>
        /// Hash code
        /// </summary>
        /// <returns>hash</returns>
        public override int GetHashCode()
        {
            return this.Name.GetHashCode() * this.Id.GetHashCode() + this.Documents.GetHashCode();
        }
    }
}
