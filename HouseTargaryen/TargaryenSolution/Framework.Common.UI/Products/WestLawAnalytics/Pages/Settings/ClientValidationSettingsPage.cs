namespace Framework.Common.UI.Products.WestLawAnalytics.Pages.Settings
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawAnalytics.Enums;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// Client Validation Settings page
    /// </summary>
    public class ClientValidationSettingsPage : BaseModuleRegressionPage
    {
        private const string OptionTexLctMask = "//label[text()='{0}']";

        private static readonly By SaveButtonLocator = By.XPath("//ul[contains(@class,'Column') and not(contains(@style,'display: none'))]//button[@id='submitSaveAccount']");

        private static readonly By ReturnToLinkLocator = By.XPath("//ul[contains(@class,'Column') and not(contains(@style,'display: none'))]//a[@id='cancel']");

        private static readonly By DeleteButtonLocator = By.XPath("//*[@id='deleteClientMattersButton']");

        private static readonly By InfoMessageLocator = By.XPath("//div[@class='co_infoBox_message']");

        private static readonly By ClientValidationDropDownLocator = By.XPath("//ul[contains(@class,'Column') and not(contains(@style,'display: none'))]//select[@id='clientValidationType']");

        private static readonly By WestHostedClientValidationDropdownLocator = By.XPath("//*[@id='westHostedType']");

        private EnumPropertyMapper<AnalyticsClientValidationOptions, WebElementInfo> settingsOptionsMap;

        /// <summary>
        /// When WestHosted type is choosen 
        /// the West Hosted Client Validation dropdown is displayed
        /// </summary>
        public IDropdown<AnalyticsWestHostedTypeOptions> WestHostedClientValidationDropdown => new Dropdown<AnalyticsWestHostedTypeOptions>(WestHostedClientValidationDropdownLocator);

        /// <summary>
        /// Client Validation Type dropdown
        /// </summary>
        public IDropdown<AnalyticsClientValidationTypeOptions> ClientValidationTypeDropdown { get; } = new Dropdown<AnalyticsClientValidationTypeOptions>(ClientValidationDropDownLocator);

        /// <summary>
        /// KeyCite Flags Option Map
        /// </summary>
        protected EnumPropertyMapper<AnalyticsClientValidationOptions, WebElementInfo> SettingsOptionsMap
            =>
                this.settingsOptionsMap =
                    this.settingsOptionsMap ?? EnumPropertyModelCache.GetMap<AnalyticsClientValidationOptions, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps");

        /// <summary>
        /// Select Client validation Option
        /// </summary>
        /// <param name="selected">
        /// The selected. 
        /// </param>
        /// <param name="option">
        /// Option 
        /// </param>
        /// <returns>
        /// The <see cref="CtsPage"/>.
        /// </returns>
        public CtsPage SelectOption(bool selected, AnalyticsClientValidationOptions option)
        {
            DriverExtensions.SetCheckbox(selected, By.XPath(this.SettingsOptionsMap[option].LocatorString));
            return new CtsPage();
        }

        /// <summary>
        /// Verifies that the option is selected.
        /// </summary>
        /// <param name="option"> The option. </param>
        /// <returns> The <see cref="bool"/>. True if flag is selected </returns>
        public bool IsOptionSelected(AnalyticsClientValidationOptions option)
            => DriverExtensions.IsCheckboxSelected(By.XPath(this.SettingsOptionsMap[option].LocatorString));

        /// <summary>
        /// Gets list of options.
        /// </summary>
        /// <returns> List of options </returns>
        public List<string> GetListOfOptions() => this.SettingsOptionsMap.Select(
                                                          el => DriverExtensions.GetText(
                                                              By.XPath(
                                                                  string.Format(OptionTexLctMask, el.Value.Text))))
                                                      .ToList();

        /// <summary>
        /// Get info message text
        /// </summary>
        /// <returns>
        /// Text <see cref="string"/>.
        /// </returns>
        public string GetInfoMessageText()
        {
            DriverExtensions.WaitForElementDisplayed(InfoMessageLocator);
            return DriverExtensions.GetText(InfoMessageLocator);
        }

        /// <summary>
        /// Click Save button
        /// </summary>
        /// <returns>
        /// The <see cref="CtsPage"/>.
        /// </returns>
        public CtsPage ClickSaveButton()
        {
            DriverExtensions.WaitForElement(SaveButtonLocator).Click();
            return new CtsPage();
        }

        /// <summary>
        /// Click Delete button
        /// </summary>
        /// <returns>
        /// The <see cref="CtsPage"/>.
        /// </returns>
        public ClientValidationSettingsPage ClickDeleteButton()
        {
            DriverExtensions.WaitForElement(DeleteButtonLocator).Click();
            BrowserPool.CurrentBrowser.Driver.SwitchTo().Alert().Accept();
            return this;
        }

        /// <summary>
        /// Click Search button
        /// </summary>
        /// <returns>
        /// The <see cref="CtsPage"/>.
        /// </returns>
        public CtsPage ClickReturnToLink()
        {
            DriverExtensions.WaitForElement(ReturnToLinkLocator).Click();
            return new CtsPage();
        }

        /// <summary>
        /// Is info message "Client Validation Setup saved successfully." displayed
        /// </summary>
        /// <returns>True if message is displayed, false otherwise.</returns>
        public bool IsInfoMessageDisplayed() => DriverExtensions.IsDisplayed(InfoMessageLocator);

        /// <summary>
        /// Is "Delete" button displayed
        /// </summary>
        /// <returns>True if button is displayed, false otherwise.</returns>
        public bool IsDeleteButtonDisplayed() => DriverExtensions.IsDisplayed(DeleteButtonLocator);
    }
}