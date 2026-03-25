namespace Framework.Common.Api.Endpoints.AlertProductService.DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Partner Defined Properties data model
    /// </summary>
    [DataContract]
    public class PartnerDefinedProperties
    {
        /// <summary>
        /// Trackable Code
        /// </summary>
        [DataMember(Name = "trackableCode")]
        public string TrackableCode { get; set; }

        /// <summary>
        /// Search Info Json
        /// </summary>
        [DataMember(Name = "searchInfoJson")]
        public string SearchInfoJson { get; set; }

        /// <summary>
        /// Prism Registration Key
        /// </summary>
        [DataMember(Name = "prismRegistrationKey")]
        public string PrismRegistrationKey { get; set; }

        /// <summary>
        /// Enabled Deliveries
        /// </summary>
        [DataMember(Name = "enabledDeliveries")]
        public string EnabledDeliveries { get; set; }

        /// <summary>
        /// Notification Flag
        /// </summary>
        [DataMember(Name = "notificationFlag")]
        public string NotificationFlag { get; set; }

        /// <summary>
        /// Firm Id
        /// </summary>
        [DataMember(Name = "firmId")]
        public string FirmId { get; set; }

        /// <summary>
        /// Enabled Portal Delivery
        /// </summary>
        [DataMember(Name = "enabledPortalDelivery")]
        public string EnabledPortalDelivery { get; set; }

        /// <summary>
        /// Owner Name
        /// </summary>
        [DataMember(Name = "ownerName")]
        public string OwnerName { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        [DataMember(Name = "createdBy")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Content Docket Guid For Link Creation
        /// </summary>
        [DataMember(Name = "content_docketGuidForLinkCreation")]
        public string ContentDocketGuidForLinkCreation { get; set; }

        /// <summary>
        /// Content Content Data
        /// </summary>
        [DataMember(Name = "content_contentData")]
        public string ContentContentData { get; set; }

        /// <summary>
        /// Case Tracking Attempt Counter
        /// </summary>
        [DataMember(Name = "caseTrackingAttemptCounter")]
        public int CaseTrackingAttemptCounter { get; set; }

        /// <summary>
        /// Search Search UI Json
        /// </summary>
        [DataMember(Name = "search_searchUIJson")]
        public string SearchSearchUIJson { get; set; }

        /// <summary>
        /// Globalization Json
        /// </summary>
        [DataMember(Name = "globalizationJson")]
        public string GlobalizationJson { get; set; }

        /// <summary>
        /// Case Tracking Subscription
        /// </summary>
        [DataMember(Name = "caseTrackingSubscription")]
        public string CaseTrackingSubscription { get; set; }

        /// <summary>
        /// Mobile Properties
        /// </summary>
        [DataMember(Name = "mobileProperties")]
        public string MobileProperties { get; set; }

        /// <summary>
        /// User Classification
        /// </summary>
        [DataMember(Name = "userClassification")]
        public string UserClassification { get; set; }

        /// <summary>
        /// Alert Custom Json
        /// </summary>
        [DataMember(Name = "alertCustomJson")]
        public string AlertCustomJson { get; set; }

        /// <summary>
        /// Search Content Reporting Name
        /// </summary>
        [DataMember(Name = "search_contentReportingName")]
        public string SearchContentReportingName { get; set; }

        /// <summary>
        /// Alert With No Results
        /// </summary>
        [DataMember(Name = "alertWithNoResults")]
        public string AlertWithNoResults { get; set; }

        /// <summary>
        /// Unsubscribed Addresses
        /// </summary>
        [DataMember(Name = "unsubscribedAddresses")]
        public string UnsubscribedAddresses { get; set; }
    }
}
