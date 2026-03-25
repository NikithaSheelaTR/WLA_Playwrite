namespace Framework.Common.Api.Raw.IPhoneMode.Models.Responses
{
    using System.Collections.Generic;

    using Framework.Common.Api.Raw.IPhoneMode.Utilities;

    /// <summary>
    /// Update Push Notification Flags Request
    /// </summary>
    public class NotifcationsResponse : IResponse
    {
        /// <summary>
        /// Gets or sets Update Push Notification Flags Request
        /// </summary>
        public List<RelatedUserNotifications> UserNotifications { get; set; }

        /// <summary>
        /// Verify Is Valid
        /// </summary>
        /// <returns>true if valid, false otherwise</returns>
        public bool IsValid()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Get Contract Validations
        /// </summary>
        /// <returns>Contract Validations</returns>
        public ContractValidations Validations()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Update Push Notification Flags Request
        /// </summary>
        public class DataPayload
        {
            /// <summary>
            /// Gets or sets the alert GUID.
            /// </summary>
            public string AlertGuid { get; set; }

            /// <summary>
            /// Gets or sets the alert name.
            /// </summary>
            public string AlertName { get; set; }

            /// <summary>
            /// Gets or sets the alert result GUID.
            /// </summary>
            public string AlertResultGuid { get; set; }

            /// <summary>
            /// Gets or sets the alert type.
            /// </summary>
            public string AlertType { get; set; }

            /// <summary>
            /// Gets or sets the docket track next info.
            /// </summary>
            public object DocketTrackNextInfo { get; set; }

            /// <summary>
            /// Gets or sets the document count.
            /// </summary>
            public int DocumentCount { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether is push notification.
            /// </summary>
            public bool IsPushNotification { get; set; }

            /// <summary>
            /// Gets or sets the key cite next info.
            /// </summary>
            public object KeyCiteNextInfo { get; set; }

            /// <summary>
            /// Gets or sets the last complete run date.
            /// </summary>
            public object LastCompleteRunDate { get; set; }

            /// <summary>
            /// Gets or sets the message.
            /// </summary>
            public string Message { get; set; }

            /// <summary>
            /// Gets or sets the prism user GUID.
            /// </summary>
            public string PrismUserGuid { get; set; }

            /// <summary>
            /// Gets or sets the product value.
            /// </summary>
            public string ProductValue { get; set; }

            /// <summary>
            /// Gets or sets the track company id.
            /// </summary>
            public object TrackCompanyId { get; set; }

            /// <summary>
            /// Gets or sets the track company location.
            /// </summary>
            public object TrackCompanyLocation { get; set; }

            /// <summary>
            /// Gets or sets the track company name.
            /// </summary>
            public object TrackCompanyName { get; set; }

            /// <summary>
            /// Gets or sets the track practice area.
            /// </summary>
            public string TrackPracticeArea { get; set; }
        }

        /// <summary>
        /// The properties.
        /// </summary>
        public class Properties
        {
            /// <summary>
            /// Gets or sets the alert name.
            /// </summary>
            public string AlertableName { get; set; }

            /// <summary>
            /// Gets or sets the key.
            /// </summary>
            public string Key { get; set; }

            /// <summary>
            /// Gets or sets the product name.
            /// </summary>
            public string ProductName { get; set; }

            /// <summary>
            /// Gets or sets the resource type.
            /// </summary>
            public string ResourceType { get; set; }
        }

        /// <summary>
        /// The related user notifications.
        /// </summary>
        public class RelatedUserNotifications
        {
            /// <summary>
            /// Gets or sets the alert name.
            /// </summary>
            public string AlertableName { get; set; }

            /// <summary>
            /// Gets or sets the data payload.
            /// </summary>
            public DataPayload DataPayload { get; set; }

            /// <summary>
            /// Gets or sets the date.
            /// </summary>
            public long Date { get; set; }

            /// <summary>
            /// Gets or sets the document id.
            /// </summary>
            public string DocumentId { get; set; }

            /// <summary>
            /// Gets or sets the firm id.
            /// </summary>
            public string FirmId { get; set; }

            /// <summary>
            /// Gets or sets the notification GUID.
            /// </summary>
            public string NotificationGuid { get; set; }

            /// <summary>
            /// Gets or sets the notification id.
            /// </summary>
            public string NotificationId { get; set; }

            /// <summary>
            /// Gets or sets the product name.
            /// </summary>
            public string ProductName { get; set; }

            /// <summary>
            /// Gets or sets the properties.
            /// </summary>
            public Properties Properties { get; set; }

            /// <summary>
            /// Gets or sets the related users.
            /// </summary>
            public List<string> RelatedUsers { get; set; }

            /// <summary>
            /// Gets or sets the resource.
            /// </summary>
            public Resource Resource { get; set; }

            /// <summary>
            /// Gets or sets the result GUID.
            /// </summary>
            public string ResultGuid { get; set; }
        }

        /// <summary>
        /// The resource.
        /// </summary>
        public class Resource
        {
            /// <summary>
            /// Gets or sets the resource event name.
            /// </summary>
            public string ResourceEventName { get; set; }

            /// <summary>
            /// Gets or sets the resource id.
            /// </summary>
            public string ResourceId { get; set; }

            /// <summary>
            /// Gets or sets the resource type.
            /// </summary>
            public string ResourceType { get; set; }
        }
    }
}