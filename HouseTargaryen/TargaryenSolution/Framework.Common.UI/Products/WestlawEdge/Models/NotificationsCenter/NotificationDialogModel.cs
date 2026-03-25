namespace Framework.Common.UI.Products.WestlawEdge.Models.NotificationsCenter
{
    using System;

    /// <summary>
    /// Describe notifications on the 'Notification dialog'
    /// </summary>
    public class NotificationDialogModel
    {
        /// <summary>
        /// Document name
        /// </summary>
        public string LinkName { get; set; }

        /// <summary>
        /// Date of the notification
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// User name of the notification
        /// </summary>
        public string Contact { get; set; }
    }
}