using Framework.Common.UI.Interfaces.Elements;
using Framework.Common.UI.Products.Shared.DropDowns;
using Framework.Common.UI.Products.Shared.Elements.Buttons;
using Framework.Common.UI.Products.Shared.Models.EnumProperties;
using Framework.Common.UI.Products.WestlawAdvantage.Enums;
using Framework.Common.UI.Raw.WestlawEdge.Enums.Folders;
using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
using Framework.Core.CommonTypes.Extensions;
using Framework.Core.Utils.Enums;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.UI.Products.WestlawAdvantage.DropDowns.DeepResearch
{
    /// <summary>
    /// Deep research report versions dropdown
    /// </summary>
    public class ReportVersionsDropdown : BaseModuleRegressionCustomDropdown<ReportVersionsOption>
    {
        private static readonly By DropdownLocator = By.XPath("//div[contains(@data-testid, 'answer-version-selector')]");
        private static readonly By VersionSelectorButtonLocator = By.XPath(".//saf-select-v3[contains(@data-testid, 'answer-version-select')]");
        private static readonly By VersionOptionsLocator = By.XPath(".//saf-option-v3[contains(@class, 'AnswerVersionSelector-module')]");
        private static readonly By VersionSelectedOptionLocator = By.XPath(".//saf-option-v3[contains(@class, 'AnswerVersionSelector-module') and contains(@aria-selected, 'true')]");
        private readonly IWebElement container;

        private readonly string sourceFolder = "Resources/EnumPropertyMaps/WestlawAdvantage";

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportVersionsDropdown"/> class.
        /// </summary>
        /// <param name="container">Container element</param>
        public ReportVersionsDropdown(IWebElement container) => this.container = container;

        /// <summary>
        /// Dropdown element
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.SafeGetElement(DropdownLocator);

        /// <summary>
        /// Version Button
        /// </summary>
        private IButton VersionButton => new Button(this.Dropdown, VersionSelectorButtonLocator);

        /// <summary>
        /// Is dropdown displayed
        /// </summary>
        /// <returns>True - if it is displayed, false - otherwise</returns>
        public override bool IsDisplayed() => this.VersionButton.Displayed;

        /// <summary>
        /// Select option from expanded dropdown
        /// </summary>
        /// <param name="option"> The option. </param>
        protected override void SelectOptionFromExpandedDropdown(ReportVersionsOption option) =>
             DriverExtensions.WaitForElement(this.Dropdown, By.XPath(this.VersionsMenuMap[option].LocatorString)).CustomClick();

        /// <summary>
        /// Report versions menu map
        /// </summary>
        protected EnumPropertyMapper<ReportVersionsOption, WebElementInfo> VersionsMenuMap
            => EnumPropertyModelCache.GetMap<ReportVersionsOption, WebElementInfo>(
                                         string.Empty,
                                         @"Resources/EnumPropertyMaps/WestlawAdvantage");

        /// <summary>
        /// Gets Options
        /// </summary>
        protected override IEnumerable<ReportVersionsOption> OptionsFromExpandedDropdown
            => DriverExtensions.GetElements(this.Dropdown, VersionOptionsLocator)
                               .Select(elem => elem.Text.GetEnumValueByText<ReportVersionsOption>(string.Empty,
                                         @"Resources/EnumPropertyMaps/WestlawAdvantage")).ToList();

        /// <summary>
        /// Verify that version is selected
        /// </summary>
        /// <param name="option">Report version option</param>
        /// <returns>True- if selected</returns>
        public override bool IsSelected(ReportVersionsOption option) => DriverExtensions
            .GetElement(this.Dropdown, By.XPath(this.VersionsMenuMap[option].LocatorString)).GetAttribute("area-selected")
            .Contains("true");

        /// <summary>
        /// Get Report Version Text
        /// </summary>
        public string GetReportVersionText()
        {
            IWebElement VersionSelectedOption = DriverExtensions.GetElement(VersionSelectedOptionLocator);
            var textContent = DriverExtensions.ExecuteScript("return arguments[0].textContent;", VersionSelectedOption) as string;
            return textContent;
        }

        /// <summary>
        /// Return Selected Option
        /// </summary>
        /// <returns> Selected option</returns>
        public override ReportVersionsOption SelectedOption =>
                          GetReportVersionText().GetEnumValueByText<ReportVersionsOption>(sourceFolder: this.sourceFolder);

        /// <summary>
        /// ClickDropdownArrow
        /// </summary>
        protected override void ClickDropdownArrow() => DriverExtensions.GetElement(this.Dropdown, VersionSelectorButtonLocator).Click();
    }
}
