namespace Framework.Common.Api.Endpoints.Document.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The document profile.
    /// </summary>
    [DataContract]
    public class DocumentProfile
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [DataMember(Name = "Id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DataMember(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        [DataMember(Name = "Url")]
        public string Url { get; set; }
    }
}