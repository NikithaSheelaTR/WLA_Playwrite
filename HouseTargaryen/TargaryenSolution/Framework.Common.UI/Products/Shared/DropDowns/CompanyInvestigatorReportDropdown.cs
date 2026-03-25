namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.Reports;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Contains all methods pertaining to the Company Investigator Report dropdown
    /// </summary>
    public class CompanyInvestigatorReportDropdown
        : BaseModuleRegressionCustomDropdown<CompanyInvestigatorReportOptions>
    {
        private static readonly By DropdownLocator = By.Id("ReportTab");

        private static readonly By OptionsLocator = By.XPath(".//ul[contains(@class,'a11yDropdown-menu') and contains(@style,'block')]//a");

        private EnumPropertyMapper<CompanyInvestigatorReportOptions, WebElementInfo> companyInvestigatorReportsMap;
        
        /// <summary>
        /// Company Investigator Report Options Map
        /// </summary>
        protected EnumPropertyMapper<CompanyInvestigatorReportOptions, WebElementInfo> CompanyInvestigatorReportsMap =>
            this.companyInvestigatorReportsMap = this.companyInvestigatorReportsMap
                                                 ?? EnumPropertyModelCache
                                                     .GetMap<CompanyInvestigatorReportOptions, WebElementInfo>();

        /// <summary>
        /// Returns Selected Option
        /// </summary>
        /// <returns> Throw exception, because there is no selected option for this dropdown</returns>
        public override CompanyInvestigatorReportOptions SelectedOption
        {
            get
            {
                throw new NotImplementedException(
                    "Can't get selected item for the CompanyInvestigatorReportDropdown");
            }
        }

        /// <summary>
        /// Gets Options
        /// </summary>
        protected override IEnumerable<CompanyInvestigatorReportOptions> OptionsFromExpandedDropdown
        {
            get
            {
                return DriverExtensions.GetElements(this.Dropdown, OptionsLocator).Select(
                    elem => DriverExtensions
                            .GetElement(elem, By.TagName("a")).GetAttribute("title")
                            .GetEnumValueByText<CompanyInvestigatorReportOptions>()).ToList();
            }
        }

        /// <summary>
        /// Returns Selected Option
        /// </summary>
        /// <returns> Throw exception, because there is no selected option for this dropdown</returns>
        public override bool IsSelected(CompanyInvestigatorReportOptions option)
        {
            throw new NotImplementedException(
                "Can't get selected item for the CompanyInvestigatorReportDropdown");
        }

        /// <summary>
        /// Dropdown element
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.WaitForElement(DropdownLocator);

        
        /// <summary>
        /// Select Dropdown Option
        /// </summary>
        /// <param name="option">CompanyInvestigatorReportOptions</param>
        protected override void SelectOptionFromExpandedDropdown(CompanyInvestigatorReportOptions option)
        {
            Dropdown.Hover();
            DriverExtensions.WaitForElement(this.Dropdown,
                SafeXpath.BySafeXpath(
                    ".//ul//a[contains(text(), {0})]",
                    this.CompanyInvestigatorReportsMap[option].Text)).Click();
        }

        /// <summary>
        /// Verifies if dropdown is expanded
        /// </summary>
        /// <returns></returns>
        protected override bool IsDropdownExpanded() => DriverExtensions.GetElement(this.Dropdown, By.TagName("ul")).GetAttribute("style").Contains("block");


        /// <summary>
        /// Click DropdownArrow
        /// </summary>
        protected override void ClickDropdownArrow() => DriverExtensions.GetElement(this.Dropdown, By.TagName("button")).Click();
    }
}
