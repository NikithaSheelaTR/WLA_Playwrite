namespace Framework.Common.UI.Products.Shared.Pages.Alerts
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;

    using OpenQA.Selenium;

    /// <summary>
    /// Westlaw Today alert creation page
    /// </summary>
    public class WestlawTodayCreateAlertPage : BaseModuleRegressionPage
    {
        private static readonly By AlertNameLocator = By.XPath("//input[@id='co_legalNewsSubscriptionName']");
        private static readonly By EmailAddressLocator = By.XPath("//input[@id='co_legalNewsEmailAddress']");
        private static readonly By SaveButtonLocator = By.XPath("//button[@id='coid_LegalNewsAlertsSubscribe']");
        private static readonly By CancelButtonLocator = By.XPath("//button[@id='coid_LegalNewsAlertsCancel']");
        private static readonly By PracticeAreaCheckboxLocator = By.XPath("//ul[@class = 'PracticeArea-alert-list']//input");
        private static readonly By DescriptionLocator = By.XPath("//textarea[@id = 'optionsAlertDescription']");

        /// <summary>
        /// Name of alert text box
        /// </summary>
        public ITextbox AlertName => new CustomTextbox(AlertNameLocator);

        /// <summary>
        /// Email address text box
        /// </summary>
        public ITextbox EmailAddress => new CustomTextbox(EmailAddressLocator);

        /// <summary>
        /// Description text box
        /// </summary>
        public ITextbox Description => new Textbox(DescriptionLocator);

        /// <summary>
        /// Practice area checkboxes
        /// </summary>
        public IReadOnlyCollection<ICheckBox> PracticeAreasCheckboxes => new ElementsCollection<CheckBox>(PracticeAreaCheckboxLocator);

        /// <summary>
        /// Save button
        /// </summary>
        public IButton SaveButton => new Button(SaveButtonLocator);

        /// <summary>
        /// Cancel button
        /// </summary>
        public IButton CancelButton => new Button(CancelButtonLocator);
    }
}