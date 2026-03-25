namespace Framework.Common.UI.Products.ANZ.DropDowns
{
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Other Versions Dropdown
    /// </summary>
    public class EdgeOtherVersionsDropdown : BaseModuleRegressionCustomDropdown<string>
    {

        private static readonly By DropdownLocator = By.ClassName("rv_dropDownMenu");
        private static readonly By DropdownArrowLocator = By.XPath("//a[@title='View Reported Versions']//span[@class='icon_down_arrow']");
        private static readonly By LinksLocator = By.XPath("//li[@class='co_renditionOther']/a");
        private static readonly By CaseVersionLocatpr = By.XPath("//*[@class='CaseVersion-item']");

        /// <inheritdoc />
        /// <summary>
        /// Returns Selected Option
        /// </summary>
        /// <returns> Throw exception, because there is no selected option for GoTo dropdown</returns>
        public override string SelectedOption
        {
            get { throw new NotImplementedException("Can't get selected item for the GoTo dropdown"); }
        }

        /// <summary>
        /// Dropdown element
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.WaitForElement(DropdownLocator);

        /// <summary>
        /// Get Options
        /// </summary>
        protected override IEnumerable<string> OptionsFromExpandedDropdown =>
            DriverExtensions.GetElements(LinksLocator).Select(el => el.GetText()).ToList();

        /// <summary>
        /// Verify that option is selected
        /// </summary>
        /// <param name="option"> Option to select </param>
        /// <returns> Throw exception, because there is no selected option for GoTo dropdown</returns>
        public override bool IsSelected(string option)
        {
            throw new NotImplementedException("Can't get selected item for the GoTo dropdown");
        }

        /// <summary>
        /// Returns true if 'other versions' dropdown is displayed on a page
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(DropdownLocator, 1);

        /// <summary>
        /// ClickDropdownArrow
        /// </summary>
        protected override void ClickDropdownArrow()
        {
            DriverExtensions.GetElement(DropdownArrowLocator).Click();
        }

        /// <summary>
        /// The select option from expanded dropdown.
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        protected override void SelectOptionFromExpandedDropdown(string option) =>
            DriverExtensions.GetElements(LinksLocator).FirstOrDefault(link => link.Text.Equals(option)).Click();

        /// <summary>
        /// The select option from Case Version.
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        public void SelectOptionFromCaseVersion(string option) =>
            DriverExtensions.GetElements(CaseVersionLocatpr).FirstOrDefault(link => link.Text.Equals(option)).Click();
    }

}
