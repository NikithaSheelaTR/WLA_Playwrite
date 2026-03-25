namespace Framework.Common.UI.Products.WestlawEdge.Components.Preferences
{
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Preferences;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Indigo Billing Tab Component in Preferences dialog
    /// </summary>
    public class EdgeBillingTabComponent : BaseTabComponent
    {
        private static readonly By EmailDetailedSessionSummaryCheckboxLocator =
            By.XPath("//input[./parent::label/span[contains(.,'Email detailed')]]");

        private static readonly By ContainerLocator = By.Id("panel_Billing");

        private EnumPropertyMapper<EdgeBillingTab, WebElementInfo> billingTabMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the BillingTab enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<EdgeBillingTab, WebElementInfo> BillingTabMap =>
            this.billingTabMap = this.billingTabMap ?? EnumPropertyModelCache.GetMap<EdgeBillingTab, WebElementInfo>(
                                     string.Empty,
                                     @"Resources/EnumPropertyMaps/WestlawEdge/Preferences");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Billing";

        /// <summary>
        /// Returns true if the specified element on the billing tab is displayed
        /// </summary>
        /// <param name="tabOption">the option to look for</param>
        /// <returns>true if the option is visible</returns>
        public bool IsRadiobuttonDisplayed(EdgeBillingTab tabOption)
            => DriverExtensions.IsDisplayed(By.XPath(this.BillingTabMap[tabOption].LocatorString));

        /// <summary>
        /// Returns true if the specified element on the billing tab is 
        ///  (checked for checkboxes)
        /// </summary>
        /// <param name="tabOption">the option to look for</param>
        /// <returns>true if selected, false otherwise</returns>
        public bool IsRadiobuttonSelected(EdgeBillingTab tabOption)
            => DriverExtensions.IsCheckboxSelected(By.XPath(this.BillingTabMap[tabOption].LocatorString));

        /// <summary>
        /// Is Email Detailed Session Summary Displayed at sign off
        /// Now this checkbox is displayed on History tab
        /// </summary>
        /// <returns>true if checkbox is present</returns>
        public bool IsEmailDetailedSessionSummaryDisplayed() => DriverExtensions.IsDisplayed(EmailDetailedSessionSummaryCheckboxLocator);

        /// <summary>
        /// Selects the specified radio button option on the billing tab.
        /// </summary>
        /// <param name="tabOption"> the option to look for </param>
        /// <returns>
        /// The <see cref="EdgeBillingTabComponent"/>.BillingTabComponent </returns>
        public EdgeBillingTabComponent SelectRadioButton(EdgeBillingTab tabOption)
        {
            DriverExtensions.WaitForElement(By.XPath(this.BillingTabMap[tabOption].LocatorString)).Click();
            return this;
        }
    }
}
