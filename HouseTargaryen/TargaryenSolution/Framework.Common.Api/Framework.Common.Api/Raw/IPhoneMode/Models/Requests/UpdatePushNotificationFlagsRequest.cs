namespace Framework.Common.Api.Raw.IPhoneMode.Models.Requests
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;

    using Framework.Core.Utils;

    /// <summary>
    /// UpdatePushNotificationFlagsRequest
    /// </summary>
    [DataContract]
    public class UpdatePushNotificationFlagsRequest : IRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePushNotificationFlagsRequest"/> class. 
        /// </summary>
        /// <param name="alertGuid"> The _alert GUID. </param>
        /// <param name="isPushNotification"> The _is Push Notification. </param>
        /// <param name="alertTypeIdentifier"> The _alert Type Identifier. </param>
        public UpdatePushNotificationFlagsRequest(
            string alertGuid,
            bool isPushNotification,
            string alertTypeIdentifier = "WestClip")
        {
            var update = new AlertsNotificationUpdates
                             {
                                 AlertGuid = alertGuid,
                                 IsPushNotification = isPushNotification,
                                 AlertTypeIdentifier = alertTypeIdentifier
                             };
            this.NotificationAlerts = new List<AlertsNotificationUpdates>() { update };
        }

        /// <summary>
        /// notificationAlerts
        /// </summary>
        [DataMember(Name = "notificationAlerts")]
        public List<AlertsNotificationUpdates> NotificationAlerts { get; set; }

        /// <summary>
        /// Get Request Body
        /// </summary>
        /// <returns> Request Body  </returns>
        public string GetRequestBody()
        {
            return ObjectSerializer.SerializeObject<DataContractJsonSerializer, UpdatePushNotificationFlagsRequest>(
                this);
        }
    }

    /// <summary>
    /// Alerts Notification Updates
    /// </summary>
    [DataContract]
    public class AlertsNotificationUpdates
    {
        /// <summary>
        /// Gets or sets the alert GUID.
        /// </summary>
        [DataMember(Name = "alertGuid")]
        public string AlertGuid { get; set; }

        /// <summary>
        /// Gets or sets the alert type identifier.
        /// </summary>
        [DataMember(Name = "alertTypeIdentifier")]
        public string AlertTypeIdentifier { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is push notification.
        /// </summary>
        [DataMember(Name = "isPushNotification")]
        public bool IsPushNotification { get; set; }
    }
}