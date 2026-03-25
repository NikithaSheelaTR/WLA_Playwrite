namespace Framework.Common.UI.Products.WestLawNext.Pages.IphoneMode
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Updates Page
    /// </summary>
    public class UpdatesPage : BaseModuleRegressionPage
    {
        private static readonly By DatePickerLocator = By.Id("ui-datepicker-div");
        
        private static readonly By NotificationsListLocator = By.XPath("//*[@id='co_notificationsList']//a");
        
        private static readonly By ShowNotificationsButtonLocator = By.Id("co_notifications_show");
        
        private static readonly By StartDateLocator = By.Id("co_notifications_startDate");
        

        /// <summary>
        /// Clicks the show notifications.
        /// </summary>
        public void ClickShowNotifications()
        {
            DriverExtensions.Click(ShowNotificationsButtonLocator);
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Searches the notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <returns>searched notifications</returns>
        public string SearchNotification(string notification)
            =>
                DriverExtensions.GetElements(NotificationsListLocator)
                                .Where(n => n.Text.Contains(notification))
                                .Select(n => n.GetAttribute("data-guid"))
                                .FirstOrDefault();

        /// <summary>
        /// Sets the start date.
        /// </summary>
        /// <param name="date">The date.</param>
        public void SetStartDate(string date)
        {
            DriverExtensions.SetTextField(date, StartDateLocator);
            DriverExtensions.GetElement(StartDateLocator).SendKeys(Keys.Enter);
            DriverExtensions.GetElement(DatePickerLocator).WaitForElementNotDisplayed();
            DriverExtensions.GetElement(ShowNotificationsButtonLocator).WaitForElementDisplayed();
        }
    }
}