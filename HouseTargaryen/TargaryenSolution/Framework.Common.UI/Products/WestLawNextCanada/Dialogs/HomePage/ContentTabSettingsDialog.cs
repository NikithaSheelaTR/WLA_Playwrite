namespace Framework.Common.UI.Products.WestLawNextCanada.Dialogs.HomePage
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNextCanada.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// Content Tab Settings Dialog
    /// </summary>
    public class ContentTabSettingsDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//div[contains(@class, 'Select-default-tab-modal')]");
        private static readonly By DialogTitleLocator = By.XPath("//h2[contains(@id , 'coid_lightboxAriaLabel')]");
        private static readonly By DialogTagLineLocator = By.XPath("//fieldset/legend");
        private static readonly By SaveButtonLocator = By.XPath("//button[text()='Save']");

        private EnumPropertyMapper<PreferenceTabOptions, WebElementInfo> preferenceTabOptionMap;

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<PreferenceTabOptions, WebElementInfo> PreferenceTabOptionMap =>
            this.preferenceTabOptionMap = this.preferenceTabOptionMap
                                       ?? EnumPropertyModelCache
                                           .GetMap<PreferenceTabOptions, WebElementInfo>(
                                               string.Empty,
                                               @"Resources/EnumPropertyMaps/WestlawNextCanada");

        /// <summary>
        /// Gets the dialog title.
        /// </summary>
        public ILabel DialogTitle => new Label(ContainerLocator, DialogTitleLocator);

        /// <summary>
        /// Get the dialog tag line
        /// </summary>
        public ILabel DialogTagLine => new Label(ContainerLocator, DialogTagLineLocator);

        /// <summary>
        /// Save button.
        /// </summary>
        public IButton SaveButton => new Button(ContainerLocator, SaveButtonLocator);

        /// <summary>
        /// Select option on the Content tab settings dialog
        /// </summary>
        /// <param name="optionToSelect"> Preference option to select </param>
        public void SetPreferenceTabOption(PreferenceTabOptions optionToSelect) =>
            DriverExtensions.WaitForElementDisplayed(By.Id(PreferenceTabOptionMap[optionToSelect].Id)).Click();

        /// <summary>
        /// Check if the preference tab option is selected
        /// </summary>
        /// <param name="optionToSelect">Preference option</param>
        /// <returns>true if selected, otherwise false</returns>
        public bool IsPreferenceTabOptionSelected(PreferenceTabOptions optionToSelect) =>
            DriverExtensions.WaitForElementDisplayed(By.Id(PreferenceTabOptionMap[optionToSelect].Id)).Selected;
    }
}