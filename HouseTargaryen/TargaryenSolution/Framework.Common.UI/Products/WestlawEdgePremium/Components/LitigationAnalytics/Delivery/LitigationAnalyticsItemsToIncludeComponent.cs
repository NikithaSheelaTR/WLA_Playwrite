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
    /// Items to include component.
    /// </summary>
    public class LitigationAnalyticsItemsToIncludeComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//li//ol[contains(@id, 'co_delivery_legalAnalytics') and not(contains(@style, 'display: none;')) and not(contains(@id, 'co_delivery_legalAnalyticsTabOptions'))]");
        private static readonly By AllCheckboxesLocator = By.XPath("//li//ol[contains(@id, 'co_delivery_legalAnalytics') and not(contains(@style, 'display: none;')) and not(contains(@id, 'co_delivery_legalAnalyticsTabOptions'))]/li/input[1]");

        private static readonly EnumPropertyMapper<ItemsToInclude, WebElementInfo> ItemsToIncludeMap =
          EnumPropertyModelCache.GetMap<ItemsToInclude, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/LitigationAnalytics");

        /// <summary>
        /// Items to include component.
        /// </summary>
        public LitigationAnalyticsItemsToIncludeComponent()
        {
            this.UnselectAllCheckboxes();
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Select Include option on the Layout and Limits tab
        /// </summary>
        /// <param name="expectedTabOption"> Include option on the Layout and Limits tab </param>
        /// <param name="setTo"> The set To. </param>
        public void SetIncludeSectionOption(ItemsToInclude expectedTabOption, bool setTo = true)
            => DriverExtensions.SetCheckbox(By.XPath(ItemsToIncludeMap[expectedTabOption].LocatorString), setTo);

        /// <summary>
        /// Unselects all checkboxes of the Layouts and Limits tab
        /// </summary>
        public void SelectAllCheckboxes()
            => DriverExtensions.GetElements(AllCheckboxesLocator).ToList().ForEach(elem => elem.SetCheckbox(true));

        /// <summary>
        /// Unselects all checkboxes of the Layouts and Limits tab
        /// </summary>
        public void UnselectAllCheckboxes()
            => DriverExtensions.GetElements(AllCheckboxesLocator).ToList().ForEach(elem => elem.SetCheckbox(false));

    }
}