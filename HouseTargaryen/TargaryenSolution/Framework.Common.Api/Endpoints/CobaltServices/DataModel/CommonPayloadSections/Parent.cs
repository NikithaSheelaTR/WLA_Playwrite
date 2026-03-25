namespace Framework.Common.Api.Endpoints.CobaltServices.DataModel.CommonPayloadSections
{
    using Newtonsoft.Json;

    /// <summary>
    /// Parent folder
    /// </summary>
    public class Parent
    {
        /// <summary>
        /// Gets or sets the parent type.
        /// </summary>
        [JsonProperty("parentType")]
        public string ParentType { get; set; }

        /// <summary>
        /// Gets or sets the parent id.
        /// </summary>
        [JsonProperty("parentId")]
        public string ParentId { get; set; }
    }
}