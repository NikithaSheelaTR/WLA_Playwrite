namespace Framework.Common.UI.Products.WestlawEdge.Models.NotificationsCenter
{
    using System;

    /// <summary>
    /// Describe notifications on the 'Notifications Center View All page'
    /// </summary>
    public class NotificationCenterPageModel
    {
        /// <summary>
        /// Text of the notification
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Link in the notification
        /// </summary>
        public string LinkName { get; set; }

        /// <summary>
        /// Date of the notification
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Contact of the notification
        /// </summary>
        public string Contact { get; set; }
    }
}