namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.DocumentAnalyzer
{
    using Newtonsoft.Json;
    
    /// <summary>
    /// Quote Items
    /// </summary>
    public class QuoteItems
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        [JsonProperty("content", NullValueHandling = NullValueHandling.Ignore)]
        public string Content { get; set; }

        /// <summary>
        /// Original content
        /// </summary>
        [JsonProperty("originalContent", NullValueHandling = NullValueHandling.Ignore)]
        public string OriginalContent { get; set; }

        /// <summary>
        /// The get hash code.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public override int GetHashCode() => base.GetHashCode();

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            return this.Equals((QuoteItems)obj);

        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected bool Equals(QuoteItems item)
        {
              return string.Equals(this.Id, item.Id) && string.Equals(this.Type, item.Type)
                                                         && string.Equals(this.Content, item.Content)
                                                         && string.Equals(this.OriginalContent, item.OriginalContent);
        }
    }
}