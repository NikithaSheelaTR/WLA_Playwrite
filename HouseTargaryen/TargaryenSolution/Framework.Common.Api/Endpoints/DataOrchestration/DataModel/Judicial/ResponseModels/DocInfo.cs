namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.ResponseModels
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Doc info
    /// </summary>
    public class DocInfo
    {
        /// <summary>
        /// doc Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// index
        /// </summary>
        [JsonProperty("index")]
        public int Index { get; set; }

        /// <summary>
        /// quotationsId
        /// </summary>
        [JsonProperty("quotationsId")]
        public string QuotationsId { get; set; }

        /// <summary>
        /// segmentDocGuid
        /// </summary>
        [JsonProperty("segmentDocGuid")]
        public string SegmentDocGuid { get; set; }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>true if equals, false otherwise</returns>
        public override bool Equals(object obj)
        {
            var res = obj as DocInfo;
            return res.Name.Equals(this.Name) && res.Id.Equals(this.Id) && res.Index.Equals(this.Index);
        }

        /// <summary>
        /// hash code
        /// </summary>
        /// <returns>hash</returns>
        public override int GetHashCode()
        {
            return 31 * this.Name.GetHashCode() * this.Id.GetHashCode() / this.Index.GetHashCode();
        }
    }
}
