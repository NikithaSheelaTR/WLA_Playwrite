namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.CustomPage;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <inheritdoc />
    /// <summary>
    /// Manage Page Dropdown
    /// </summary>
    public class ManagePageDropdown : BaseModuleRegressionCustomDropdown<ManagePageDropdownOptions>
    {
        private static readonly By DropDownOptionLocator = By.XPath("//div[@id='cp_manage_dropdown']//li//span[@class='a11yDropdown-itemText']");

        private static readonly By DropdownLocator = By.XPath("//div[@id='cp_manage_dropdown']//button");

        private EnumPropertyMapper<ManagePageDropdownOptions, WebElementInfo> managePageMap;

        /// <summary>
        /// Returns Selected Option
        /// </summary>
        /// <returns> Throw exception, because there is no selected option for Manage Page dropdown </returns>
        public override ManagePageDropdownOptions SelectedOption
        {
            get { throw new NotImplementedException("Can't get selected item for the GoTo dropdown"); }
        }

        /// <summary>
        /// Manage Page Dropdown Options Map
        /// </summary>
        protected EnumPropertyMapper<ManagePageDropdownOptions, WebElementInfo> ManagePageMap
            =>
                this.managePageMap =
                    this.managePageMap ?? EnumPropertyModelCache.GetMap<ManagePageDropdownOptions, WebElementInfo>();

        /// <inheritdoc />
        /// <summary>
        /// Dropdown
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.GetElement(DropdownLocator);

        /// <summary>
        /// Get Options
        /// </summary>
        protected override IEnumerable<ManagePageDropdownOptions> OptionsFromExpandedDropdown
            =>
                DriverExtensions.GetElements(DropDownOptionLocator)
                                .Select(
                                    e =>
                                        DriverExtensions.GetImmediateText(e).Trim()
                                                        .GetEnumValueByText<ManagePageDropdownOptions>())
                                .ToList();

        /// <inheritdoc />
        /// <summary>
        /// Verify that Manage Page Dropdown option is selected
        /// </summary>
        /// <param name="option"> Manage Page Dropdown option </param>
        /// <returns> Throw exception, because there is no selected option for Manage Page dropdown</returns>
        public override bool IsSelected(ManagePageDropdownOptions option)
        {
            throw new NotImplementedException("Can't get selected item for the GoTo dropdown");
        }

        /// <summary>
        /// Is Dropdown Displayed
        /// </summary>
        /// <returns>bool</returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(DropdownLocator);

        /// <summary>
        /// The is dropdown expanded.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected override bool IsDropdownExpanded()
        {
            string dropdownArea = this.Dropdown.GetAttribute("aria-expanded");
            return dropdownArea?.Contains("true", StringComparison.InvariantCultureIgnoreCase) ?? false;
        }

        /// <inheritdoc />
        /// <summary>
        /// Select Dropdown Option
        /// </summary>
        /// <param name="option"></param>
        protected override void SelectOptionFromExpandedDropdown(ManagePageDropdownOptions option)
        {
            DriverExtensions.Click(By.XPath(this.ManagePageMap[option].LocatorString));
        }

        /// <summary>
        /// ClickDropdownArrow
        /// </summary>
        protected override void ClickDropdownArrow()
        {
            this.Dropdown.Click();
        }
    }
}