namespace Framework.Common.UI.Products.WestlawEdge.Components.TourComponent
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Tours;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Tour notification component
    /// </summary>
    public class TourNotificationComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.ClassName("co_onboardingTourBox_container");

        private static readonly By HomeTourNotificationLocator = By.Id("coid_homePageOnboardingTourBoxContentType");

        /// <summary>
        /// Tour Notification Dialog Tabs Map
        /// </summary>
        private EnumPropertyMapper<TourNotification, WebElementInfo> tourNotificationMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the TourNotification enumeration
        /// </summary>
        private EnumPropertyMapper<TourNotification, WebElementInfo> TourNotificationMap =>
            this.tourNotificationMap = this.tourNotificationMap
                                       ?? EnumPropertyModelCache.GetMap<TourNotification, WebElementInfo>(
                                           string.Empty,
                                           @"Resources/EnumPropertyMaps/WestlawEdge/Tours");

        /// <summary>
        /// Get notification content for different tab
        /// </summary>
        /// <param name="tourNotification"></param>
        /// <returns></returns>
        public bool IsNotificationDisplayed(TourNotification tourNotification)
        {
            return DriverExtensions.GetElement(By.XPath(this.TourNotificationMap[tourNotification].LocatorString)).
                Text.Equals(this.TourNotificationMap[tourNotification].Text);
        }

        /// <summary>
        /// Get notification for home tour for clicking on different tab
        /// </summary>
        /// <param name="tourNotification"></param>
        /// <returns></returns>
        public string GetNotificationContentForHomePage(TourNotification tourNotification)
            => DriverExtensions.WaitForElement(HomeTourNotificationLocator).Text;
    }
}