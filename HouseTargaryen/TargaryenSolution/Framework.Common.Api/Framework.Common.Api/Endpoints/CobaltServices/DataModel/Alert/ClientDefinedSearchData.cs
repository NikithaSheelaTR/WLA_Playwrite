namespace Framework.Common.Api.Endpoints.CobaltServices.DataModel.Alert
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Client Defined Search Data model
    /// </summary>
    [DataContract]
    public class ClientDefinedSearchData
    {
        /// <summary>
        /// Comments
        /// </summary>
        [DataMember(Name = "comments")]
        public string Comments { get; set; }

        /// <summary>
        /// Privacy Status
        /// </summary>
        [DataMember(Name = "privacyStatus")]
        public string PrivacyStatus { get; set; }

        /// <summary>
        /// Alert With No Results
        /// </summary>
        [DataMember(Name = "alertWithNoResults")]
        public string AlertWithNoResults { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        [DataMember(Name = "content")]
        public string Content { get; set; }

        /// <summary>
        /// Docket Number
        /// </summary>
        [DataMember(Name = "docketNumber")]
        public string DocketNumber { get; set; }
    }
}
