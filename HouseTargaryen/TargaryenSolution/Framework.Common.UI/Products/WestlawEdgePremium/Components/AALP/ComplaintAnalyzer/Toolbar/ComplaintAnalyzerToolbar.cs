namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.ComplaintAnalyzer.Toolbar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.WestlawEdgePremium.DropDowns;
    using OpenQA.Selenium;

    /// <summary>
    /// Toolbar
    /// </summary>
    public class ComplaintAnalyzerToolbar : BaseModuleRegressionComponent
    {
        private static readonly By ToolbarContainerLocator = By.XPath("//div[contains(@class,'Results-module__deliveryMenu')]");
        private static readonly By DeliveryDropdownContainerLocator = By.XPath("//div[@data-testid='delivery']");

        /// <summary>
        /// Delivery dropdown
        /// </summary>
        public ComplaintAnalyzerDeliveryDropdown ComplaintAnalyzerDeliveryDropdown { get; } = new ComplaintAnalyzerDeliveryDropdown(DeliveryDropdownContainerLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ToolbarContainerLocator;
    }
}
