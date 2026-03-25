namespace Framework.Common.UI.Products.WestlawEdge.Components.NotificationCenter
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.HomePage;
    using Framework.Common.UI.Products.WestlawEdge.Items.NotificationsCenter;
    using Framework.Common.UI.Products.WestlawEdge.Models.NotificationsCenter;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// List of notifications on the Notifications center View All page
    /// </summary>
    public class NotificationCenterResultListComponent : BaseModuleRegressionComponent
    {
        private static readonly By NotificationContainerLocator = By.XPath(".//li[contains(@class,'NotificationItem')]");
        private static readonly By NotificationPreferenceLocator = By.XPath(".//a[@class = 'NotificationTabPanelHeader-preference']");

        private static readonly By ContainerLocator = By.XPath("//div[@class='NotificationTabPanel']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets list of notifications
        /// </summary>
        /// <returns> list of notifications </returns>
        public List<NotificationCenterPageModel> GetListOfNotifications() => DriverExtensions
            .GetElements(this.ComponentLocator, NotificationContainerLocator)
            .Select(item => new NotificationCenterPageItem(item).ToModel<NotificationCenterPageModel>())
            .ToList();

        /// <summary>
        /// Click on Notification Preference link
        /// </summary>
        /// <returns> A new instance of EdgePreferencesDialog class</returns>
        public EdgePreferencesDialog ClickOnNotificationPreference()
        {
            DriverExtensions.WaitForElement(NotificationPreferenceLocator).Click();
            return DriverExtensions.CreatePageInstance<EdgePreferencesDialog>();
        }
    }
}
