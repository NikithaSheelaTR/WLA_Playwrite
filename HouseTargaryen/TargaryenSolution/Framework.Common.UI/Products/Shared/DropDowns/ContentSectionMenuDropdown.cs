namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using Framework.Common.UI.Products.Shared.Enums.CustomPage;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Content Section Menu Dropdown
    /// </summary>
    public class ContentSectionMenuDropdown : BaseModuleRegressionCustomDropdown<ContentSectionMenuDropdownOptions>
    {
        private static readonly By DropDownOptionLocator = By.XPath("//*[@class='CP-card-menu co_dropDownButton a11yDropdown cp_showForEditOnly']//li//span[@class='a11yDropdown-itemText']");

        private static readonly By DropdownLocator = By.XPath("//*[@class='CP-card-menu co_dropDownButton a11yDropdown cp_showForEditOnly']");

        private EnumPropertyMapper<ContentSectionMenuDropdownOptions, WebElementInfo> contentMenuMap;

        /// <summary>
        /// Returns Selected Option
        /// </summary>
        public override ContentSectionMenuDropdownOptions SelectedOption
        {
            get { throw new NotImplementedException("Can't get selected item for the GoTo dropdown"); }
        }

        /// <summary>
        /// Content Menu Options Map
        /// </summary>
        protected EnumPropertyMapper<ContentSectionMenuDropdownOptions, WebElementInfo> ContentMenuMap
            =>
                this.contentMenuMap =
                    this.contentMenuMap ?? EnumPropertyModelCache.GetMap<ContentSectionMenuDropdownOptions, WebElementInfo>();

        /// <inheritdoc />
        /// <summary>
        /// Dropdown
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.GetElement(DropdownLocator);

        /// <summary>
        /// Get Options
        /// </summary>
        protected override IEnumerable<ContentSectionMenuDropdownOptions> OptionsFromExpandedDropdown
            =>
                DriverExtensions.GetElements(DropDownOptionLocator)
                                .Select(
                                    e =>
                                        DriverExtensions.GetImmediateText(e).Trim()
                                                        .GetEnumValueByText<ContentSectionMenuDropdownOptions>())
                                .ToList();

        /// <inheritdoc />
        /// <summary>
        /// Verify that Content Menu option is selected
        /// </summary>
        /// <param name="option"> Content Menu option </param>
        public override bool IsSelected(ContentSectionMenuDropdownOptions option)
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
        protected override void SelectOptionFromExpandedDropdown(ContentSectionMenuDropdownOptions option)
        {
            DriverExtensions.Click(By.XPath(this.ContentMenuMap[option].LocatorString));
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



    

