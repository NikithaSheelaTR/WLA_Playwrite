namespace Framework.Common.UI.Products.WestlawEdge.DropDowns
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The base custom string dropdown.
    /// </summary>
    public class CustomStringDropdown : BaseCustomDropdown<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomStringDropdown"/> class. 
        /// </summary>
        /// <param name="locatorBys">
        /// The locator bys
        /// </param>
        public CustomStringDropdown(params By[] locatorBys) : base(locatorBys)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomStringDropdown"/> class.
        /// </summary>
        /// <param name="outerContainer">The outer container.</param>
        /// <param name="locatorBys">The locator bys.</param>
        public CustomStringDropdown(IWebElement outerContainer, params By[] locatorBys) : base(outerContainer, locatorBys)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomStringDropdown"/> class.
        /// </summary>
        /// <param name="elementContainer">The element container.</param>
        public CustomStringDropdown(IWebElement elementContainer) : base(elementContainer)
        {
        }

        /// <summary>
        /// The selected option.
        /// </summary>
        public override string SelectedOption => this.GetContainer().Text;

        /// <summary>
        /// The options from expanded dropdown.
        /// </summary>
        protected override IEnumerable<string> OptionsFromExpandedDropdown =>
            DriverExtensions.GetElements(this.GetContainer(), this.DropdownOptionsLocator).Select(option => option.Text)
                            .ToList();

        /// <summary>
        /// The is selected.
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsSelected(string option) => this.SelectedOption.Contains(option);

        /// <summary>
        /// The is enabled.
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsEnabled(string option) =>
            DriverExtensions.GetElements(this.GetContainer(), this.DropdownOptionsLocator)
                            .First(dropdownOption => dropdownOption.Text.Contains(option)).Enabled;

        /// <summary>
        /// The select option from expanded dropdown.
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        protected override void SelectOptionFromExpandedDropdown(string option)
        {
            DriverExtensions.GetElements(this.GetContainer(), this.DropdownOptionsLocator)
                            .First(dropdownOption => dropdownOption.Text.Contains(option)).Click();
        }
    }
}