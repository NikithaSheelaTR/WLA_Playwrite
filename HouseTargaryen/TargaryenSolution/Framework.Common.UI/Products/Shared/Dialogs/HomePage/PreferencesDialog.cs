namespace Framework.Common.UI.Products.Shared.Dialogs.HomePage
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;

    /// <summary>
    /// Base Preferences Dialog
    /// </summary>
    public class PreferencesDialog : BaseModuleRegressionDialog
    {
        private static readonly By SaveButtonLocator = By.XPath("//*[@id='coid_userSettingsSave'] | //button/span[text()='Save']");

        private static readonly By CloseButtonLocator = By.Id("coid_userSettingsClose");

        private static readonly By HeaderTitleLocator = By.Id("coid_userSettingsHeading");

        private static readonly By CancelButtonLocator = By.Id("coid_userSettingsCancel");

        private static readonly By InfoMessageTextLocator = By.XPath(".//div[@data-role='SuccessBanner']//p");

        /// <summary>
        /// Preferences Tab Panel
        /// </summary>
        public PreferencesTabPanel TabPanel { get; set; } = new PreferencesTabPanel();

        /// <summary>
        /// Save button
        /// </summary>
        public IButton SaveButton => new Button(SaveButtonLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(CloseButtonLocator);

        /// <summary>
        /// Cancel button
        /// </summary>
        public IButton CancelButton => new Button(CancelButtonLocator);

        /// <summary>
        /// Header title
        /// </summary>
        public ILabel HeaderTitleLabel => new Label(HeaderTitleLocator);

        /// <summary>
        /// InfoMessage Text 
        /// </summary>
        public ILabel InfoMessageTextLabel => new Label(InfoMessageTextLocator);
    }
}