namespace Framework.Common.UI.Products.Shared.Pages.Browse
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// Extends CommonBrowsePage with smart preference checkbox functionality for Virginia State Cases page
    /// </summary>
    public class VirginiaStateCasesBrowsePage : CommonBrowsePage
    {
        private static readonly By SmartPreferenceCheckboxLocator = By.Id("co_smartPreferenceCheckbox");

        private static readonly By SmartPreferenceCheckboxLabelLocator = By.XPath("//label[@for='co_smartPreferenceCheckbox']");

        private static readonly By SmartPreferenceCheckboxInfoIconLocator = By.Id("co_moreInfoLink");

        /// <summary>
        /// Smart preference checkbox element
        /// </summary>
        public ICheckBox SmartPreferenceCheckbox => new CheckBox(SmartPreferenceCheckboxLocator);

        /// <summary>
        /// Smart preference checkbox label element
        /// </summary>
        public ILabel SmartPreferenceCheckboxLabel => new Label(SmartPreferenceCheckboxLabelLocator);

        /// <summary>
        /// Smart preference checkbox info icon element
        /// </summary>
        public IButton SmartPreferenceCheckboxInfoIcon => new Button(SmartPreferenceCheckboxInfoIconLocator);
    }
}

