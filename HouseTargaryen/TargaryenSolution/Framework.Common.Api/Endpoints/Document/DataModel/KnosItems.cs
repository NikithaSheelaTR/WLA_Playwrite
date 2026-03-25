namespace Framework.Common.Api.Endpoints.Document.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The knos items.
    /// </summary>
    [DataContract]
    public class KnosItems
    {
        /// <summary>
        /// Gets or sets the knos code.
        /// </summary>
        [DataMember(Name = "knosCode")]
        public string KnosCode { get; set; }

        /// <summary>
        /// Gets or sets the knos levels.
        /// </summary>
        [DataMember(Name = "knosLevels")]
        public string[] KnosLevels { get; set; }
    }
}