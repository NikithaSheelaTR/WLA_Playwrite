namespace Framework.Common.Api.Endpoints.Search.DataModel
{
    using Newtonsoft.Json;

    /// <summary>
    /// Attributes for Search Update endpoint
    /// </summary>
  
   public class Attribute
    {
        /// <summary>
        ///  Gets or sets Id
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
