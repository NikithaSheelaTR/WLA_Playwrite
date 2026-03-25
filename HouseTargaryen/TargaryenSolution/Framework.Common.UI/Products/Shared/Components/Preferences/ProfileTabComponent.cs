namespace Framework.Common.UI.Products.Shared.Components.Preferences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Profile Tab Component
    /// </summary>
    public class ProfileTabComponent : BaseTabComponent
    {
        private static readonly By JurisdictionCheckboxesLocator =
            By.XPath(
                "//input[contains(@id, 'coid_userSettingsCountryToSearch_input_')]");

        private static readonly By TimeZoneDropdownLocator = By.Id("coid_userSettingsTimeZoneOption");

        private static readonly By EditHomePageCheckboxesLocator
            = By.XPath("//div[@class='UserSettings-section ' and ./h3[text()='Edit home page']]//label");

        private static readonly By ClearStartPageSectionLocator = By.XPath("//div[./h3[text() = 'Clear start page']]");

        private static readonly By ClearStartPageCheckboxLocator = By.XPath("//input[@type='checkbox' and @name ='useDefaultStartPage']");

        private static readonly By ContainerLocator = By.Id("coid_userSettingsTabPanel1");

        private static readonly By WlcaChecboxLocator = By.XPath("//*[@id='coid_userSettingsCountryToSearch_input_Canada']");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Profile";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Returns the value of the specified dropdown
        /// </summary>
        /// <returns>the selected dropdown option</returns>
        public string GetProfileTimeZoneDropdownSelectedValue()
        {
            return DriverExtensions.GetSelectedDropdownOptionText(TimeZoneDropdownLocator);
        }

        /// <summary>
        /// Select proper jurisdiction checkboxes
        /// </summary>
        /// <param name="jurisdictions"> Jurisdictions to select. Send Empty string to clear all jurisdictions. </param>
        /// <returns> The <see cref="ProfileTabComponent"/>ProfileTabComponent </returns>
        public ProfileTabComponent SelectJurisdiction(params string[] jurisdictions)
        {
            IEnumerable<IWebElement> checkboxesToSelect =
                DriverExtensions.GetElements(JurisdictionCheckboxesLocator)
                                .Where(x => x.GetAttribute("id").ContainsAnyItem(jurisdictions));

            IEnumerable<IWebElement> selectedChechboxes =
                DriverExtensions.GetElements(JurisdictionCheckboxesLocator).Where(x => x.Selected);

            foreach (IWebElement item in checkboxesToSelect.UnionExceptIntersection(selectedChechboxes))
            {
                item.Click();
            }

            return this;
        }

        /// <summary>
        /// Sets the specified dropdown on the profile tab to the specified value.
        /// </summary>
        /// <string name="tabOptionLocatorn">the option to look for</string>
        /// <param name="option"> What to select from the dropdown. </param>
        /// <returns>
        /// The <see cref="ProfileTabComponent"/>ProfileTabComponent </returns>
        public ProfileTabComponent SetProfileTimeZoneTabDropdown(string option)
        {
            DriverExtensions.SetDropdown(option, TimeZoneDropdownLocator);
            return this;
        }

        /// <summary>
        /// Gets edit home page checkbox name.
        /// </summary>
        /// <param name="editHomePageCheckboxName"> The edit home page checkbox name. </param>
        /// <returns> The <see cref="string"/>. Checkbox name </returns>
        public string GetEditHomePageCheckboxName(string editHomePageCheckboxName)
            => DriverExtensions.GetElements(EditHomePageCheckboxesLocator)
            .First(elem => elem.Text.Contains(editHomePageCheckboxName, StringComparison.CurrentCultureIgnoreCase))
            ?.Text ?? string.Empty;

        /// <summary>
        /// Is Clear Start page section displayed
        /// </summary>
        /// <returns></returns>
        public bool IsClearStartPageSectionDisplayed() => DriverExtensions.IsDisplayed(ClearStartPageSectionLocator);

        /// <summary>
        /// Gets Clear Start page section message
        /// </summary>
        /// <returns></returns>
        public string GetClearStartPageSectionMessage() =>
            DriverExtensions.GetText(ClearStartPageSectionLocator);

        /// <summary>
        /// Is Clear Start page section checkbox displayed
        /// </summary>
        /// <returns></returns>
        public bool IsClearStartPageCheckboxDisplayed() => DriverExtensions.WaitForElementDisplayed(ClearStartPageCheckboxLocator).Displayed;

        /// <summary>
        /// Sets Clear Start page section checkbox to given value
        /// </summary>
        /// <param name="value">True or false state of the checkbox</param>
        /// <returns></returns>
        public ProfileTabComponent SetClearStartPageCheckbox(bool value)
        {
            DriverExtensions.WaitForElementDisplayed(ClearStartPageCheckboxLocator).SetCheckbox(value);
            return this;
        }

        /// <summary>
        /// Wlca Checkbox
        /// </summary>
        /// <returns></returns>
        public ICheckBox WlcaCheckBox => new CheckBox(WlcaChecboxLocator);
        
    }
}