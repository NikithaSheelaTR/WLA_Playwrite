namespace Framework.Common.Api.Endpoints.AlertProductService.DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// CaseTracking Subscription Data Model
    /// </summary>
    [DataContract]
    public class CaseTrackingSubscription
    {
        /// <summary>
        /// Case Tracking ClipId
        /// </summary>
        [DataMember(Name = "caseTrackingClipId")]
        public string CaseTrackingClipId { get; set; }

        /// <summary>
        /// Platform
        /// </summary>
        [DataMember(Name = "platform")]
        public string Platform { get; set; }

        /// <summary>
        /// Case Number
        /// </summary>
        [DataMember(Name = "caseNumber")]
        public string CaseNumber { get; set; }

        /// <summary>
        /// Court
        /// </summary>
        [DataMember(Name = "court")]
        public string Court { get; set; }

        /// <summary>
        /// Sign on
        /// </summary>
        [DataMember(Name = "signon")]
        public string Signon { get; set; }

        /// <summary>
        /// Frequency
        /// </summary>
        [DataMember(Name = "frequency")]
        public string Frequency { get; set; }

        /// <summary>
        /// Next Run Date
        /// </summary>
        [DataMember(Name = "nextRunDate")]
        public List<string> NextRunDate { get; set; }

        /// <summary>
        /// Additional Terms
        /// </summary>
        [DataMember(Name = "additionalTerms")]
        public List<string> AdditionalTerms { get; set; }

        /// <summary>
        /// List As Full Text
        /// </summary>
        [DataMember(Name = "listAsFullText")]
        public bool ListAsFullText { get; set; }

        /// <summary>
        /// Link Params
        /// </summary>
        [DataMember(Name = "linkParams")]
        public string LinkParams { get; set; }
    }
}
