namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Constants;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// The dropdown.
    /// </summary>
    public class Dropdown : BaseContainerWrapper, IDropdown<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Dropdown"/> class.
        /// </summary>
        /// <param name="locatorBys">The locator bys</param>
        public Dropdown(params By[] locatorBys) : base(locatorBys)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dropdown"/> class.
        /// </summary>
        /// <param name="outerContainer">The outer container.</param>
        /// <param name="locatorBys">The locator bys.</param>
        public Dropdown(IWebElement outerContainer, params By[] locatorBys) : base(outerContainer, locatorBys)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dropdown"/> class.
        /// </summary>
        /// <param name="elementContainer">The element container.</param>
        public Dropdown(IWebElement elementContainer) : base(elementContainer)
        {
        }

        /// <inheritdoc />
        public IEnumerable<string> Options => this.DropdownElement.Options.Select(option => option.Text);

        /// <inheritdoc />
        public string SelectedOption => this.DropdownElement.SelectedOption.Text;

        /// <summary>
        /// The selected option text.
        /// </summary>
        public string SelectedOptionText => this.SelectedOption;
        
        /// <summary>
        /// Dropdown Element
        /// </summary>
        private SelectElement DropdownElement => new SelectElement(this.GetContainer());

        /// <inheritdoc />
        public bool IsSelected(string option) => this.SelectedOption.Equals(option);

        /// <inheritdoc />
        public bool IsEnabled(string option) =>
            this.DropdownElement.Options.First(dropdownOption => dropdownOption.Text.Equals(option)).Enabled;

        /// <inheritdoc />
        public bool IsDisplayed() => this.GetContainer(WebDriverTimeouts.ElementDisplayed)?.Displayed ?? false;

        /// <inheritdoc />
        public TPage SelectOption<TPage>(string option)
            where TPage : ICreatablePageObject
        {
            this.DropdownElement.SelectByText(option);
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <inheritdoc />
        public void SelectOption(string option) => this.DropdownElement.SelectByText(option);
    }
}