namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.Delivery
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Delivery;
    using Framework.Common.UI.Products.Shared.Components.Delivery.DeliveryTabs;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// What to deliver component.
    /// </summary>
    public class LitigationAnalyticsWhatToDeliverComponent : WhatToDeliverComponent
    {
        private static readonly By CurrentviewCheckBoxLocator = By.XPath("//input[contains(@id,'co_deliveryWhatToDeliverLegalAnalytics')]");
        private static readonly By CustomizedReport = By.XPath("//input[contains(@id,'co_deliveryWhatToDeliverLegalAnalyticsTabs')]");

        /// <summary>
        /// Only pages with terms checkbox
        /// </summary>
        public ICheckBox CurrentviewCheckBox => new CheckBox(CurrentviewCheckBoxLocator);

        ///<summary>
        /// Customized Reports Options component
        /// </summary>
        public CustomizedReportsOptionsComponent CustomizedReportsOptions { get; } = new CustomizedReportsOptionsComponent();

        /// <summary>
        /// Gets the WhatToDeliver enumeration to WebElementInfo map.
        /// </summary>
        protected new EnumPropertyMapper<WhatToDeliver, WebElementInfo> WhatToDeliverMap =>
            EnumPropertyModelCache.GetMap<WhatToDeliver, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/LitigationAnalytics");

        /// <summary>
        /// Select What to Deliver; List of Items/Documents
        /// </summary>
        /// <param name="format">Format of the delivery</param>
        /// <returns>The <see cref="TheBasicsTabComponent"/>.</returns>
        public TheBasicsTabComponent SelectOption(WhatToDeliver format)
        {
            DriverExtensions.WaitForElementDisplayed(By.XPath(this.WhatToDeliverMap[format].LocatorString)).Click();
            return new TheBasicsTabComponent();
        }
    }
}