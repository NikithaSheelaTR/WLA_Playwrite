namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.Delivery
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System.Linq;

    /// <summary>
    /// Customized Reports Options Component
    /// </summary>
    public class CustomizedReportsOptionsComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//ol[@id = 'co_delivery_legalAnalyticsTabOptions']/custom-delivery-options/li/label/input[1]");
        private static readonly By AllCheckboxesLocator = By.XPath("//ol[@id = 'co_delivery_legalAnalyticsTabOptions']/custom-delivery-options/li/label/input[1]");
        private static readonly string CustomizedReportTabNameLocator = "//ol[@id = 'co_delivery_legalAnalyticsTabOptions']/custom-delivery-options/li/label/input[@name='{0}']";

        /// <summary>
        /// Customized Reports Options Component
        /// </summary>
        public CustomizedReportsOptionsComponent()
        {
            this.UnselectAllCheckboxes();
        }

        private static readonly EnumPropertyMapper<CustomDeliveryTabOptions, WebElementInfo> CustomDeliveryTabOptions =
          EnumPropertyModelCache.GetMap<CustomDeliveryTabOptions, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/LitigationAnalytics");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Select Include option on the Layout and Limits tab
        /// </summary>
        /// <param name="expectedTabOption"> Include option on the Customized Report tab </param>
        /// <param name="setTo"> The set To. </param>
        public void SetIncludeSectionOption(CustomDeliveryTabOptions expectedTabOption, bool setTo = true)
            => DriverExtensions.SetCheckbox(By.XPath(CustomDeliveryTabOptions[expectedTabOption].LocatorString), setTo);

        /// <summary>
        /// Select All Include option on the Layout and Limits tab
        /// </summary>
        public void SelectAllIncludeSectionOption()
            => DriverExtensions.GetElements(AllCheckboxesLocator).ToList().ForEach(elem => elem.SetCheckbox(true));

        /// <summary>
        /// Unselects all checkboxes of the Layouts and Limits tab
        /// </summary>
        public void UnselectAllCheckboxes()
            => DriverExtensions.GetElements(AllCheckboxesLocator).ToList().ForEach(elem => elem.SetCheckbox(false));

        /// <summary>
        /// select  Customized Report Checkbox Locators
        /// </summary>
        public void SelectCustomizedReportTabName(string itemTitle)
           => DriverExtensions.GetElements(By.XPath(string.Format(CustomizedReportTabNameLocator, itemTitle))).ToList().ForEach(elem => elem.SetCheckbox(true));
    }
}