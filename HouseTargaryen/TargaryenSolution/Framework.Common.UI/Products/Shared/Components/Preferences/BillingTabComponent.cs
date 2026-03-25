namespace Framework.Common.UI.Products.Shared.Components.Preferences
{
    using Framework.Common.UI.Products.Shared.Enums.Preferences;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Billing Tab Component in Preferences dialog
    /// </summary>
    public class BillingTabComponent : BaseTabComponent
    {
        private static readonly By EmailDetailedSessionSummaryCheckboxLocator = By.Id("coid_userSettingsDeliverHistoryAtSignOffOption");

        private static readonly By ContainerLocator = By.Id("coid_userSettingsTabPanel2");

        private EnumPropertyMapper<BillingTab, WebElementInfo> billingTabMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the BillingTab enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<BillingTab, WebElementInfo> BillingTabMap
            => this.billingTabMap = this.billingTabMap ?? EnumPropertyModelCache.GetMap<BillingTab, WebElementInfo>();

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Billing";

        /// <summary>
        /// Returns true if the specified element on the billing tab is displayed
        /// </summary>
        /// <param name="tabOption">the option to look for</param>
        /// <returns>If the option is visible</returns>
        public bool IsDisplayed(BillingTab tabOption)
        {
            By locator = By.Id(this.BillingTabMap[tabOption].Id);
            return DriverExtensions.IsDisplayed(locator);
        }

        /// <summary>
        /// Is Email Detailed Session Summary Displayed at sign off
        /// Now this checkbox is displayed on History tab
        /// </summary>
        /// <returns>true if checkbox is present</returns>
        public bool IsEmailDetailedSessionSummaryDisplayed()
        {
            return DriverExtensions.IsDisplayed(EmailDetailedSessionSummaryCheckboxLocator);
        }

        /// <summary>
        /// Returns true if the specified element on the billing tab is 
        ///  (checked for checkboxes)
        /// </summary>
        /// <param name="tabOption">the option to look for</param>
        /// <returns>true if selected, false otherwise</returns>
        public bool IsSelected(BillingTab tabOption)
        {
            By locator = By.Id(this.BillingTabMap[tabOption].Id);
            return DriverExtensions.IsCheckboxSelected(locator);
        }

        /// <summary>
        /// Selects the specified radio button option on the billing tab.
        /// </summary>
        /// <param name="tabOption"> the option to look for </param>
        /// <returns>
        /// The <see cref="BillingTabComponent"/>.BillingTabComponent </returns>
        public BillingTabComponent SelectRadioButton(BillingTab tabOption)
        {
            By locator = By.Id(this.BillingTabMap[tabOption].Id);
            DriverExtensions.GetElement(locator).Click();
            return this;
        }
    }
}