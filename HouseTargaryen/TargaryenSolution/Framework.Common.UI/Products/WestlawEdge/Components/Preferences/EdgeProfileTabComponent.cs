namespace Framework.Common.UI.Products.WestlawEdge.Components.Preferences
{
    using System;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components.Preferences;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Preferences;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Indigo Profile Tab Component
    /// </summary>
    public class EdgeProfileTabComponent : ProfileTabComponent
    {
        private const string EditHomePageCheckboxLctMask = "//div[contains(@class,'UserSettings-section')]//label/span[text()='News']/preceding-sibling::input";
        
        private static readonly By ClearStartPageCheckboxLocator = By.XPath("//input[@type='checkbox' and ./parent::label/span[text()='Clear']]");

        private EnumPropertyMapper<EdgeProfileJurisdictionTab, WebElementInfo> profileMap;       

        /// <summary>
        /// Tool Mapper
        /// </summary>
        protected EnumPropertyMapper<EdgeProfileJurisdictionTab, WebElementInfo> ProfileMap =>
            this.profileMap = this.profileMap
                              ?? EnumPropertyModelCache.GetMap<EdgeProfileJurisdictionTab, WebElementInfo>(
                                  string.Empty,
                                  @"Resources/EnumPropertyMaps/WestlawEdge/Preferences");


        /// <summary>
        /// Select proper jurisdiction checkboxes
        /// </summary>
        /// <param name="jurisdictions"> Jurisdictions to select. Send Empty string to clear all jurisdictions. </param>
        /// <returns> The <see cref="EdgeProfileTabComponent"/>ProfileTabComponent </returns>
        public EdgeProfileTabComponent SelectJurisdiction(params EdgeProfileJurisdictionTab[] jurisdictions)
        {
            foreach (EdgeProfileJurisdictionTab item in Enum.GetValues(typeof(EdgeProfileJurisdictionTab)))
            {
                DriverExtensions.WaitForElementDisplayed(By.XPath(this.ProfileMap[item].LocatorString)).SetCheckbox(jurisdictions.Contains(item));
            }

            return this;
        }

        /// <summary>
        /// Is Clear Start page section checkbox displayed
        /// </summary>
        /// <returns></returns>
        public new bool IsClearStartPageCheckboxDisplayed() =>
            DriverExtensions.WaitForElementDisplayed(ClearStartPageCheckboxLocator).Displayed;

        /// <summary>
        /// Sets Clear Start page section checkbox to given value
        /// </summary>
        /// <param name="value">True or false state of the checkbox</param>
        /// <returns> The <see cref="EdgeProfileTabComponent"/>EdgeProfileTabComponent </returns>
        public new EdgeProfileTabComponent SetClearStartPageCheckbox(bool value)
        {
            DriverExtensions.WaitForElementDisplayed(ClearStartPageCheckboxLocator).SetCheckbox(value);
            return this;
        }

        /// <summary>
        /// Select proper EditHomePage checkboxe
        /// </summary>
        /// <param name="checkboxName"> checkbox text </param>
        /// <param name="checkedState"> true/false </param>
        /// <returns> The <see cref="EdgeProfileTabComponent"/>EdgeProfileTabComponent </returns>
        public EdgeProfileTabComponent SelectEditHomePageCheckBox(string checkboxName, bool checkedState)
        {
            DriverExtensions.GetElement(By.XPath(string.Format(EditHomePageCheckboxLctMask, checkboxName))).SetCheckbox(checkedState);
            return this;
        }
    }
}