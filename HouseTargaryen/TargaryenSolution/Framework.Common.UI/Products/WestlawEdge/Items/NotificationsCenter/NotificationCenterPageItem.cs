namespace Framework.Common.UI.Products.WestlawEdge.Items.NotificationsCenter
{
    using System;

    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Describe notifications on the 'Notifications Center View All page'
    /// </summary>
    public class NotificationCenterPageItem : BaseItem
    {
        private static readonly By NotificationTextLocator = By.XPath(".//span[contains(@class, 'metaText--firstSection') or contains(@class, 'metaText--thirdSection')]");

        private static readonly By NotificationLinkLocator = By.XPath(".//a[contains(@class, 'metaText--link')]");

        private static readonly By NotificationDateLocator = By.XPath(".//span[contains(@class, 'metaText--date')]");

        private static readonly By NotificationContactLocator = By.XPath(".//span[contains(@class, 'metaText--message')]");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="NotificationCenterPageItem"/> class. 
        /// </summary>
        /// <param name="notificationItemContainer"> The Notification Item Container. </param>
        public NotificationCenterPageItem(IWebElement notificationItemContainer) : base(notificationItemContainer)
        {
        }

        /// <summary>
        /// Text of the notification
        /// </summary>
        public string Text => DriverExtensions.GetElement(this.Container, NotificationTextLocator).Text.Trim();

        /// <summary>
        /// Name of the Link in the notification
        /// </summary>
        public string LinkName => DriverExtensions.SafeGetElement(this.Container, NotificationLinkLocator)?.Text.Trim() ?? string.Empty;

        /// <summary>
        /// Date of the notification
        /// </summary>
        public DateTime DateTime
        {
            get
            {
                DateTime date;
                string dateTime = DriverExtensions.SafeGetElement(this.Container, NotificationDateLocator)?.Text.Replace("at", "") ?? string.Empty;
                DateTime.TryParse(dateTime, out date);
                return date;
            }
        }

        /// <summary>
        /// Contact of the notification
        /// </summary>
        public string Contact => DriverExtensions.SafeGetElement(this.Container, NotificationContactLocator)?.Text.Trim() ?? string.Empty;
    }
}