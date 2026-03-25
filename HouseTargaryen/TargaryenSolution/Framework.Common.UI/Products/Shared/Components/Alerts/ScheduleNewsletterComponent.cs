namespace Framework.Common.UI.Products.Shared.Components.Alerts
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;

    using OpenQA.Selenium;

    /// <summary>
    /// Schedule Newsletter Component
    /// </summary>
    public class ScheduleNewsletterComponent : ScheduleAlertComponent
    {
        private static readonly By IncludeAlertsWithNoResultCheckBoxLocator = By.XPath("//label[contains(text(),'Include alerts with no results')]/input");

        private static readonly By IncludeNewsletterWithNoResultCheckBoxLocator = By.XPath("//label[contains(text(),'Send Newsletter if no results')]/input");

        private static readonly By ContinueSeparateEmailDeliveryCheckBoxLocator = By.XPath("//label[contains(text(),'Continue separate email delivery')]/preceding-sibling::input");

        /// <summary>
        /// Include Alerts With No Result CheckBox
        /// </summary>
        public ICheckBox IncludeAlertsWithNoResultCheckBox => new CheckBox(IncludeAlertsWithNoResultCheckBoxLocator);

        /// <summary>
        /// Include Newsletter With No Result CheckBox
        /// </summary>
        public ICheckBox IncludeNewsletterWithNoResultCheckBox => new CheckBox(IncludeNewsletterWithNoResultCheckBoxLocator);

        /// <summary>
        /// Continue Separate Email Delivery CheckBox
        /// </summary>
        public ICheckBox ContinueSeparateEmailDeliveryCheckBox => new CheckBox(ContinueSeparateEmailDeliveryCheckBoxLocator);
    }
}