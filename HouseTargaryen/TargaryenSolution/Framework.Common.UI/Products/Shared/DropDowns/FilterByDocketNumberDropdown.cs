namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Filter By Docket Number Dropdown
    /// </summary>
    public class FilterByDocketNumberDropdown : BaseModuleRegressionCustomDropdown<string>
    {
        private const string DropdownLinksLctMask = "//span[@class = 'a11yDropdown-itemText' and text() ='{0}']";

        private const string SelectedLinkLctMask = "//li[contains(@class, 'selected')]/span[text() ='{0}']";

        private static readonly By DropdownLocator = By.ClassName("a11yDropdown-menu");

        private static readonly By DropdownLinksLocator = By.XPath("//div[@id='co_docFilterWidget']//li/span");

        private static readonly By SelectedOptionLocator = By.XPath("//div[@id='co_docFilterWidget']/button/span[1]");

        private static readonly By DropdownArrowLocator = By.XPath("//div[@id='co_docFilterWidget']/button/span[2]");

        private static readonly By FilterByDocketNumberTextLocator = By.XPath("//div[@class='co_docketBatchFilter']/h4");

        /// <summary>
        /// Return Selected Option
        /// </summary>
        public override string SelectedOption => DriverExtensions.GetText(SelectedOptionLocator);

        /// <summary>
        /// Dropdown Element
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.GetElement(DropdownLocator);

        /// <summary>
        /// Get Options from Dropdown
        /// </summary>
        protected override IEnumerable<string> OptionsFromExpandedDropdown => DriverExtensions.GetElements(DropdownLinksLocator).Select(el => el.Text);
        
        /// <summary>
        /// Is option selected
        /// </summary>
        /// <param name="option">Option to verify</param>
        /// <returns> True if displayed, false otherwise  </returns>
        public override bool IsSelected(string option) => DriverExtensions.IsDisplayed(By.XPath(string.Format(SelectedLinkLctMask, option)));

        /// <summary>
        /// Is Filter By Docket Number Text Displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise  </returns>
        public bool IsFilterByDocketNumberTextDisplayed() => DriverExtensions.IsDisplayed(FilterByDocketNumberTextLocator);

        /// <summary>
        /// Get Filter By Docket Number Text
        /// </summary>
        /// <returns>Filter By Docket Number Text</returns>
        public string GetFilterByDocketNumberText() => DriverExtensions.GetElement(FilterByDocketNumberTextLocator).Text;

        /// <summary>
        /// Click Dropdown arrow
        /// </summary>
        protected override void ClickDropdownArrow()
        {
            if (this.IsDropdownArrowDisplayed())
            {
                DriverExtensions.GetElement(DropdownArrowLocator).Click();
            }
        }

        /// <summary>
        /// Select Option From Expanded Dropdown
        /// </summary>
        /// <param name="option">Docket number</param>
        protected override void SelectOptionFromExpandedDropdown(string option) =>
            DriverExtensions.GetElement(By.XPath(string.Format(DropdownLinksLctMask, option))).Click();

        private bool IsDropdownArrowDisplayed() => DriverExtensions.IsDisplayed(DropdownArrowLocator);
    }
}