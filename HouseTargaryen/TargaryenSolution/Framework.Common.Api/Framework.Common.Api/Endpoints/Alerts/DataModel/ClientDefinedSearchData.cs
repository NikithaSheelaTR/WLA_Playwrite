namespace Framework.Common.Api.Endpoints.Alerts.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The client defined search data.
    /// </summary>
    [DataContract]
    public class ClientDefinedSearchData
    {
        /// <summary>
        /// Gets or sets comments
        /// </summary>
        [DataMember(Name = "comments")]
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the citation
        /// </summary>
        [DataMember(Name = "citation")]
        public string Citation { get; set; }

        /// <summary>
        /// Gets or sets the privacy status
        /// </summary>
        [DataMember(Name = "privacyStatus")]
        public string PrivacyStatus { get; set; }

        /// <summary>
        /// Gets or sets the content
        /// </summary>
        [DataMember(Name = "content")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the alert with no results
        /// </summary>
        [DataMember(Name = "alertWithNoResults")]
        public string AlertWithNoResults { get; set; }
    }
}
