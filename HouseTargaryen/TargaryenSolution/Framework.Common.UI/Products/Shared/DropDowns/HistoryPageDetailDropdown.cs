namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// This is DetailDropdown in the toolbar for history page
    /// </summary>
    public class HistoryPageDetailDropdown : DetailDropdown
    {
        private static readonly By DropdownOptionLocator = By.XPath(".//li");

        private static readonly By DropdownLocator = By.XPath("//li[@id='co_docToolbarDetailWidget']");
        private static readonly By DropdownExpanded = By.XPath("//li[@id='co_docToolbarDetailWidget']//ul");

        private static readonly By DetailLevelIconLocator =
            By.XPath(".//button/span[contains(@class, 'icon25 icon_downMenu-gray')]");

        /// <summary>
        /// Return Selected Option.
        /// </summary>
        public override DetailLevel SelectedOption
        {
            get
            {
                IWebElement checkedElement = DriverExtensions.GetElements(this.Dropdown, DropdownOptionLocator).FirstOrDefault(
                    elem => elem.GetAttribute("class").Contains("checked")
                            || elem.GetAttribute("class")
                                   .Contains("selected"));
                string checkedElementText = checkedElement.Text;

                return checkedElementText.GetEnumValueByText<DetailLevel>();
            }
        }


        /// <summary>
        /// Gets Options
        /// </summary>
        protected override IEnumerable<DetailLevel> OptionsFromExpandedDropdown =>
            DriverExtensions.GetElements(DropdownLocator, DropdownOptionLocator).Select(
                elem => elem.Text.GetEnumValueByText<DetailLevel>()).ToList();

        /// <inheritdoc />
        /// <summary>
        /// The dropdown container element 
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.GetElement(DropdownLocator);

        /// <summary>
        /// Verifies if Detail drop down is displayed link displayed
        /// </summary>
        /// <returns>The instance of the page</returns>
        /// <returns></returns>
        public override bool IsDisplayed() => DriverExtensions.GetElement(DropdownLocator).IsDisplayed();

        /// <summary>
        /// Verifies if dropdown is expanded
        /// </summary>
        /// <returns>bool</returns>
        protected override bool IsDropdownExpanded()
        {         
            string dropdownStyle = DriverExtensions.GetElement(DropdownExpanded).GetAttribute("style");           
            return dropdownStyle.Contains("block", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
