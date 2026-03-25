namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Header
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.HomePage;
    using Framework.Common.UI.Products.WestlawEdge.Items.NotificationsCenter;
    using Framework.Common.UI.Products.WestlawEdge.Models.NotificationsCenter;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// The notification center dialog.
    /// </summary>
    public class EdgeNotificationsDialog : BaseEdgeHeaderDialog
    {
        private static readonly By PreferencesLinkLocator = By.Id("notificationPreferenceLink");
        private static readonly By AlertsLinkLocator = By.Id("notificationEditAlertsLink");
        private static readonly By NotificationItemLocator = By.XPath(".//li[contains(@class, 'NotificationItem')]");
        private static readonly By ContainerLocator = By.Id("co_notificationContainer");

        /// <summary>
        /// Container
        /// </summary>
        protected override IWebElement Container => DriverExtensions.WaitForElement(ContainerLocator);

        /// <summary>
        /// Clicks 'View Alerts page' link.
        /// </summary>
        /// <typeparam name="T">page</typeparam>
        /// <returns>T page instance</returns>
        public T ClickViewAlertsLink<T>() where T : ICreatablePageObject => this.ClickElement<T>(AlertsLinkLocator);

        /// <summary>
        /// Clicks 'Preferences' link.
        /// </summary>
        /// <returns> The <see cref="EdgePreferencesDialog"/>. </returns>
        public EdgePreferencesDialog ClickPreferencesLink() => this.ClickElement<EdgePreferencesDialog>(PreferencesLinkLocator);

        /// <summary>
        /// Get count of notifications
        /// </summary>
        /// <returns>Count of notifications</returns>
        public int GetNotificationsCount() => this.GetNotificationsList().Count();

        /// <summary>
        /// Gets the list of Notification items
        /// </summary>
        /// <returns> List of NotificationCenterPageModel items</returns>
        public List<NotificationCenterPageModel> GetAllNotificationsItems() =>
            this.GetNotificationsList().Select(
                notificationItem => new NotificationCenterPageItem(notificationItem).ToModel<NotificationCenterPageModel>()).ToList();

        /// <summary>
        /// GetNotificationItemModel
        /// </summary>
        /// <param name="index">index</param>
        /// <returns> The <see cref="NotificationCenterPageModel"/>. </returns>
        public NotificationCenterPageModel GetNotificationModel(int index = 0)
            => new NotificationCenterPageItem(this.GetNotificationsList().ElementAt(index)).ToModel<NotificationCenterPageModel>();

        /// <summary>
        /// Is notification displayed at index in dialog
        /// </summary>
        /// <param name="notificationModel">
        /// The notification model. 
        /// </param>
        /// <param name="index">
        /// Notification item index 
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsNotificationDisplayed(NotificationDialogModel notificationModel, int index = 0)
            => this.GetNotificationsCount() == 0 ? false : this.AreFieldsMatched(this.GetNotificationModel(index), notificationModel);

        /// <summary>
        /// Checks if Alerts link is displayed
        /// </summary>
        /// <returns> true if displayed, else false</returns>
        public bool IsAlertsLinkDisplayed() => DriverExtensions.IsDisplayed(AlertsLinkLocator);

        /// <summary>
        /// Checks if Preferences link is displayed
        /// </summary>
        /// <returns> true if displayed, else false</returns>
        public bool IsPreferencesLinkDisplayed() => DriverExtensions.IsDisplayed(PreferencesLinkLocator);

        /// <summary>
        /// Are fields matched expected data
        /// </summary>
        /// <param name="item"> Notification item</param>
        /// <param name="notificationModel">Notification model</param>
        /// <returns>True if fields are matched, false otherwise</returns>
        private bool AreFieldsMatched(NotificationCenterPageModel item, NotificationDialogModel notificationModel)
            => item.LinkName.Contains(notificationModel.LinkName)
                && item.DateTime.IsInRange(notificationModel.DateTime, 180)
                && item.Contact.Contains(notificationModel.Contact);

        private IEnumerable<IWebElement> GetNotificationsList() => DriverExtensions.GetElements(this.Container, NotificationItemLocator);
    }
}