namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.Widgets;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <inheritdoc />
    /// <summary>
    /// The Quick Check Edit documetn details Court level dropdown.
    /// </summary>
    public sealed class QuickCheckDocumentInformationCourtLevelDropdown : BaseModuleRegressionCustomDropdown<CourtLevel>
    {
        private static readonly By DropdownOptionLocator = By.XPath("//*[@aria-label='Court level:']/li");

        private static readonly By DropdownExpanderLocator = By.XPath("./button");

        private static readonly By CourtLevelContainer = By.XPath("//div[@id = 'DA-MenuSelectorContainer']");

        private EnumPropertyMapper<CourtLevel, WebElementInfo> courtLevelMap;

        /// <summary>
        /// Gets the selected option.
        /// </summary>
        public override CourtLevel SelectedOption
        {
            get
            {
                IWebElement checkedElement = DriverExtensions.GetElements(this.Dropdown, DropdownOptionLocator).FirstOrDefault(
                    elem => elem.GetAttribute("aria-checked").Contains("true"));
                string checkedElementText = checkedElement.Text;

                return checkedElementText.GetEnumValueByText<CourtLevel>();
            }
        }

        /// <summary>
        /// The options from expanded dropdown.
        /// </summary>
        protected override IEnumerable<CourtLevel> OptionsFromExpandedDropdown =>
            DriverExtensions.GetElements(this.Dropdown, DropdownOptionLocator)
                            .Select(elem => elem.Text.GetEnumValueByText<CourtLevel>()).ToList();

        /// <inheritdoc />
        /// <summary>
        /// The dropdown.
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.GetElement(CourtLevelContainer);

        /// <summary>
        /// Court Level Map
        /// </summary>
        private EnumPropertyMapper<CourtLevel, WebElementInfo> CourtLevelMap =>
            this.courtLevelMap = this.courtLevelMap ?? EnumPropertyModelCache.GetMap<CourtLevel, WebElementInfo>(
                                      "",
                                      @"Resources\EnumPropertyMaps\WestlawEdge\QuickCheck");

        /// <summary>
        /// The is selected.
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsSelected(CourtLevel option)
        {
            this.ExpandIfNotExpanded();
            return DriverExtensions.GetElement(this.Dropdown, By.XPath(this.CourtLevelMap[option].LocatorString))
                                   .GetAttribute("aria-checked").Equals("true");
        }

        /// <summary>
        /// The select option from expanded dropdown.
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        protected override void SelectOptionFromExpandedDropdown(CourtLevel option)
        {
            var dropdownOption = DriverExtensions.WaitForElement(By.XPath(this.CourtLevelMap[option].LocatorString));
            dropdownOption.CustomClick();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// The is dropdown expanded.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected override bool IsDropdownExpanded()
        {
            string dropdownArea = DriverExtensions.GetElement(this.Dropdown, DropdownExpanderLocator).GetAttribute("aria-expanded");
            return dropdownArea?.Contains("true", StringComparison.InvariantCultureIgnoreCase) ?? false;
        }

        /// <summary>
        /// custom click on dropdown arrow.
        /// </summary>
        protected override void ClickDropdownArrow() => DriverExtensions.GetElement(this.Dropdown, DropdownExpanderLocator).CustomClick();
    }
}