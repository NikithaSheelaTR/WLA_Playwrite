namespace Framework.Common.UI.Products.WestLawAnalytics.Components.Analytics
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.WestLawAnalytics.Dropdowns.Analytics;
    using Framework.Common.UI.Products.WestLawAnalytics.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// InPlanVsOutOfPlan Component on the Analytics page
    /// </summary>
    public class InPlanVsOutOfPlanTabComponent : BaseAnalyticsTabComponent
    {
        private static readonly By ContainerLocator = By.Id("wa_firmHealthStatisticPlanTab");
        private static readonly By LineGraphTypeDropdownLocator = By.XPath("//select[@id='wa_firmHealthLineChartGraphTypeDropDownOptions']");

        /// <summary>
        /// In Plan Graph Type Dropdown
        /// </summary>
        public IDropdown<InPlanLineChartGraphTypeOption> GraphTypeDropdown { get; }
            = new Dropdown<InPlanLineChartGraphTypeOption>(LineGraphTypeDropdownLocator);

        /// <summary>
        /// TabName
        /// </summary>
        protected override string TabName => "In Plan vs Out of Plan";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get Total values: Total Amount In Plan, Total Amount Out of Plan, Total Standard Charge, % Out of Plan
        /// </summary>
        /// <returns> Total values </returns>
        public Dictionary<string, string> GetInPlanTotalValues() => this.GetTotalValues();
    }
}