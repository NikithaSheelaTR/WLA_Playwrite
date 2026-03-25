namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.DocumentAnalyzer
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    /// <summary>
    /// Original quote items
    /// </summary>
    public class OriginalQuoteItems
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
        /// Text
        /// </summary>
        [JsonProperty("text")]
        public List<TextItem> Text { get; set; }

        /// <summary>
        /// DocumentGuid
        /// </summary>
        [JsonProperty("documentGuid")]
        public string DocumentGuid { get; set; }

        /// <summary>
        /// Pincite
        /// </summary>
        [JsonProperty("pincite")]
        public string Pincite { get; set; }

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

            return this.Equals((OriginalQuoteItems)obj);

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
        protected bool Equals(OriginalQuoteItems item)
        {
            return string.Equals(this.Id, item.Id) && string.Equals(this.Type, item.Type)
                                                   && this.Text.SequenceEqual(item.Text);
        }
    }
}