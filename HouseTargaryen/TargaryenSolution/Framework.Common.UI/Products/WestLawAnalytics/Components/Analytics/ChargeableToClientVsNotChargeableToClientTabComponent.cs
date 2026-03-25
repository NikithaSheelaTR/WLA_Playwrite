namespace Framework.Common.UI.Products.WestLawAnalytics.Components.Analytics
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.WestLawAnalytics.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// ChargeableToClientVsNotChargeableToClient Component on the Analytics page
    /// </summary>
    public class ChargeableToClientVsNotChargeableToClientTabComponent : BaseAnalyticsTabComponent
    {
        private static readonly By ContainerLocator = By.Id("wa_firmHealthStatisticBillingTab");
        private static readonly By LineGraphTypeDropdownLocator = By.XPath("//select[@id='wa_firmHealthLineChartGraphTypeDropDownOptions']");

        /// <summary>
        /// ChargeableToClientChartGraphTypeDropdown
        /// </summary>
        public IDropdown<ChargeableToClientChartGraphTypeOption> ChargeableToClientGraphTypeDropdown { get; }
            = new Dropdown<ChargeableToClientChartGraphTypeOption>(LineGraphTypeDropdownLocator);

        /// <summary>
        /// TabName
        /// </summary>
        protected override string TabName => "Chargeable to Client vs Not Chargeable to Client";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get Total values: Total Amount Chargeable to Client, Total Amount Not Chargeable to Client, Total Standard Charge, % Chargeable to Client
        /// </summary>
        /// <returns> Total values </returns>
        public Dictionary<string, string> GetChargeableTotalValues() => this.GetTotalValues();
    }
}