namespace Framework.Common.UI.Products.WestlawEdge.DropDowns.GradingTool
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The select experiment dropdown on the New Query page.
    /// </summary>
    public class SelectExperimentDropdown : BaseModuleRegressionCustomDropdown<string>
    {
        private static readonly By DropdownLocator = By.XPath("//select[@class='co_experimentOptions']");

        private static readonly By OptionsLocator = By.XPath("//select[@class='co_experimentOptions']/option");

        /// <summary>
        /// Selected option
        /// </summary>
        /// <returns>Text of selected option</returns>
        public override string SelectedOption => DriverExtensions.GetSelectedDropdownOptionText(DropdownLocator);

        /// <summary>
        /// Dropdown element
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.WaitForElement(DropdownLocator);

        /// <summary>
        /// Get all options
        /// </summary>
        /// <returns> Options list </returns>
        protected override IEnumerable<string> OptionsFromExpandedDropdown
            => DriverExtensions.GetElements(OptionsLocator).Select(el => el.GetText()).ToList();

        /// <summary>
        /// Verify that option is selected
        /// </summary>
        /// <param name="option">Option that was selected</param>
        /// <returns>True - if it is selected, false - otherwise</returns>
        public override bool IsSelected(string option) => this.SelectedOption.Equals(option);

        /// <summary>
        /// Click Dropdown Arrow
        /// </summary>
        protected override void ClickDropdownArrow() => this.Dropdown.Click();

        /// <summary>
        /// Select option from expanded dropdown
        /// </summary>
        /// <param name="option">Option to select</param>
        protected override void SelectOptionFromExpandedDropdown(string option) => DriverExtensions
            .GetElements(OptionsLocator).FirstOrDefault(link => link.Text.Trim().Equals(option.Trim())).Click();
    }
}