namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.NotificationCenter;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.NotificationCenter;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.DataModel;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The notification type facet on the Notifications Center View All page.
    /// </summary>
    public class NotificationTypeFacetComponent : BaseModuleRegressionComponent
    {
        private const string NotificationTypeLctMask = "//ul[@class='NotificationTabFacetGroup-list']//button[text()='{0}']/parent::li/button";

        private const string NotificationTypeStatusLctMask
            = "//ul[@class='NotificationTabFacetGroup-list']//button[text()='{0}']/parent::li";

        private const string SelectedTypeLctMask
            = "//ul[@class='NotificationTabFacetGroup-list']//button[text()='{0}']/parent::li[@class='co_leftColumn_activePage']";

        private const string NotificationTypeCountLctMask
            = "//ul[@class='NotificationTabFacetGroup-list']//button[text()='{0}']/preceding-sibling::span";

        private static readonly By FacetTitleLocator = By.ClassName("co_genericBoxHeader");

        private static readonly By ContainerLocator = By.Id("coid_contentTypesContainer");

        private static readonly By NotificationTypeLocator = By.XPath("//ul[@class='NotificationTabFacetGroup-list']/li/button");

        private EnumPropertyMapper<NotificationTypes, BaseTextModel> notificationTypesMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the notification type enumeration to BaseTextModel map.
        /// </summary>
        protected EnumPropertyMapper<NotificationTypes, BaseTextModel> NotificationTypesMap
            => this.notificationTypesMap
                  = this.notificationTypesMap
                   ?? EnumPropertyModelCache.GetMap<NotificationTypes, BaseTextModel>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdge/NotificationCenter");

        /// <summary>
        /// Selects Notification type
        /// </summary>
        /// <param name="notificationType"> Notification type to select  </param>
        /// <returns> The <see cref="NotificationCenterPage"/>. </returns>
        public NotificationCenterPage SelectNotificationType(NotificationTypes notificationType)
        {
            DriverExtensions.ScrollTo(By.XPath(string.Format(NotificationTypeLctMask, this.NotificationTypesMap[notificationType].Text)));
            DriverExtensions.WaitForElement(
                By.XPath(string.Format(NotificationTypeLctMask, this.NotificationTypesMap[notificationType].Text))).Click();
            return new NotificationCenterPage();
        }

        /// <summary>
        /// Verifies that the notification type is enabled.
        /// </summary>
        /// <param name="notificationType"> The notification type. </param>
        /// <returns> The <see cref="bool"/>. True if notification type is disabled. </returns>
        public bool IsNotificationTypeDisabled(NotificationTypes notificationType)
            => DriverExtensions.GetElement(
                By.XPath(string.Format(NotificationTypeStatusLctMask, this.NotificationTypesMap[notificationType].Text)))
            .GetAttribute("class").Equals("disabled");

        /// <summary>
        /// The is notification type displayed.
        /// </summary>
        /// <param name="notificationType">
        /// The notification type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsNotificationTypeDisplayed(NotificationTypes notificationType)
            => DriverExtensions.IsDisplayed(
                By.XPath(string.Format(NotificationTypeCountLctMask, this.NotificationTypesMap[notificationType].Text)));

        /// <summary>
        /// Gets List of Notification types.
        /// </summary>
        /// <returns> List of Notification types </returns>
        public List<NotificationTypes> GetNotificationTypesFacetOptions() => DriverExtensions
                                                                             .GetElements(NotificationTypeLocator)
                                                                             .Select(
                                                                                 elem =>
                                                                                     elem
                                                                                         .Text.GetEnumValueByText<
                                                                                             NotificationTypes>(
                                                                                             string.Empty,
                                                                                             @"Resources/EnumPropertyMaps/WestlawEdge/NotificationCenter"))
                                                                             .ToList();

        /// <summary>
        /// Gets List of Notification types counts.
        /// </summary>
        /// <returns> List of Notification types  counts </returns>
        public Dictionary<NotificationTypes, int> GetNotificationTypesCounts() =>
            this.GetNotificationTypesFacetOptions().ToDictionary(
                key => key,
                notificationType =>
                    DriverExtensions.GetText(
                                        By.XPath(string.Format(
                                                NotificationTypeCountLctMask,
                                                this.NotificationTypesMap[notificationType].Text)))
                                    .ConvertCountToInt());

        /// <summary>
        /// Verifies of Notification types counts.
        /// </summary>
        /// <returns> List of Notification types  counts </returns>
        public bool AreNotificationTypesCountsDisplayed()
            => this.GetNotificationTypesFacetOptions()
            .TrueForAll(type => DriverExtensions.IsDisplayed(By.XPath(string.Format(NotificationTypeCountLctMask, this.NotificationTypesMap[type].Text))));

        /// <summary>
        /// Verify that the facet title is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the facet title is displayed. </returns>
        public bool IsFacetTitleDisplayed() => DriverExtensions.IsDisplayed(FacetTitleLocator);

        /// <summary>
        /// gets the facet title name.
        /// </summary>
        /// <returns> The <see cref="string"/>. The facet title name. </returns>
        public string GetFacetTitleName() => DriverExtensions.GetText(FacetTitleLocator);

        /// <summary>
        /// Verifies that the type is selected.
        /// </summary>
        /// <param name="notificationType"> The notification type. </param>
        /// <returns> The <see cref="bool"/>. True if type is selected. </returns>
        public bool IsTypeSelected(NotificationTypes notificationType)
            => DriverExtensions.IsDisplayed(By.XPath(string.Format(SelectedTypeLctMask, this.NotificationTypesMap[notificationType].Text)));
    }
}
