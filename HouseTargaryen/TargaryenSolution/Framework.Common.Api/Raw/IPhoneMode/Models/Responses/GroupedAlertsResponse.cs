namespace Framework.Common.Api.Raw.IPhoneMode.Models.Responses
{
    using System.Collections.Generic;

    /// <summary>
    /// The grouped alerts response.
    /// </summary>
    public class GroupedAlertsResponse
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the tracked items.
        /// </summary>
        public List<TrackedItemsItem> TrackedItems { get; set; }

        /// <summary>
        /// The alerts item.
        /// </summary>
        public class AlertsItem
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
            /// Gets or sets the alert type.
            /// </summary>
            public string AlertType { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether push notification.
            /// </summary>
            public bool PushNotification { get; set; }

            /// <summary>
            /// Gets or sets the search term.
            /// </summary>
            public string SearchTerm { get; set; }
        }

        /// <summary>
        /// The tracked items item.
        /// </summary>
        public class TrackedItemsItem
        {
            /// <summary>
            /// Gets or sets the grouping name.
            /// </summary>
            public string GroupingName { get; set; }

            /// <summary>
            /// Gets or sets the group name.
            /// </summary>
            public string GroupName { get; set; }

            /// <summary>
            /// Gets or sets the tracking group.
            /// </summary>
            public List<TrackingGroupItem> TrackingGroup { get; set; }
        }

        /// <summary>
        /// The tracking group item.
        /// </summary>
        public class TrackingGroupItem
        {
            /// <summary>
            /// Gets or sets the alerts.
            /// </summary>
            public List<AlertsItem> Alerts { get; set; }

            /// <summary>
            /// Gets or sets the search term.
            /// </summary>
            public string SearchTerm { get; set; }

            /// <summary>
            /// Gets or sets the tracking name.
            /// </summary>
            public string TrackingName { get; set; }
        }
    }
}