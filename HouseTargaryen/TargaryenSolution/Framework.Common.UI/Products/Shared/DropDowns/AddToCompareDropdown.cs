namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.WestLawNext.Enums.BusinessLawCenter;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.DataModel;

    using OpenQA.Selenium;

    /// <summary>
    /// AddToCompareDropdown
    /// </summary>
    public class AddToCompareDropdown : BaseModuleRegressionCustomDropdown<AddToCompareOptions>
    {
        private static readonly By DropdownLocator = By.ClassName("co-RedlineDropdown");

        private static readonly By DropdownArrowLocator = By.XPath("//div[@class='co-RedlineDropdown']/a");

        private static readonly By DropdownOptionLocator =
            By.XPath(".//li[contains(@class,'co-RedlineDropdown-listItem')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="AddToCompareDropdown"/> class. 
        /// </summary>
        /// <param name="container"> Container: locator of element which contains that dropdown </param>
        public AddToCompareDropdown(By container)
        {
            this.Container = DriverExtensions.WaitForElement(container);
        }

        /// <summary>
        /// Get Selected option
        /// </summary>
        public override AddToCompareOptions SelectedOption
        {
            get
            {
                IWebElement optionElement =
                    DriverExtensions.GetElements(this.Container, DropdownOptionLocator)
                                    .FirstOrDefault(elem => elem.GetAttribute("class").Contains("selected"));
                return
                    DriverExtensions.GetElement(optionElement, By.XPath("./a"))
                                    .Text.GetEnumValueByPropertyModel<AddToCompareOptions, BaseTextModel>(
                                        info => info.Text);
            } 
        }

        /// <summary>
        /// OptionsFromExpandedDropdown
        /// </summary>
        protected override IEnumerable<AddToCompareOptions> OptionsFromExpandedDropdown =>
            DriverExtensions.GetElements(this.Container, DropdownOptionLocator, By.XPath("./a"))
            .Select(elem => elem.Text.GetEnumValueByPropertyModel<AddToCompareOptions, BaseTextModel>(info => info.Text)).ToList();

        /// <summary>
        /// Dropdown
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.GetElement(DropdownLocator);

        private IWebElement Container { get; set; }

        /// <summary>
        /// Is option selected
        /// </summary>
        /// <param name="option"> Option to verify </param>
        /// <returns> True if selected, false otherwise </returns>
        public override bool IsSelected(AddToCompareOptions option) => option.Equals(this.SelectedOption);

        /// <summary>
        /// Select option from expanded dropdown
        /// </summary>
        /// <param name="option"> Option to select </param>
        protected override void SelectOptionFromExpandedDropdown(AddToCompareOptions option)
        {
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.GetElements(this.Container, DropdownOptionLocator, By.XPath("./a"))
                            .FirstOrDefault(elem => elem.Text.Equals(option.GetEnumTextValue()))?.Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Click Dropdown Arrow
        /// </summary>
        protected override void ClickDropdownArrow() => DriverExtensions.WaitForElement(this.Container, DropdownArrowLocator).Click();

        /// <summary>
        /// Verify that dropdown is expanded
        /// </summary>
        /// <returns> True if expanded, false otherwise </returns>
        protected override bool IsDropdownExpanded()
            => DriverExtensions.WaitForElement(this.Container, DropdownArrowLocator).GetAttribute("class").Contains("is_open");
    }
}
