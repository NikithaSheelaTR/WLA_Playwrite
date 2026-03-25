namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Constants;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// The dropdown.
    /// </summary>
    /// <typeparam name="TEnum">Enum for options </typeparam>
    public class Dropdown<TEnum> : BaseContainerWrapper, IDropdown<TEnum> where TEnum : struct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Dropdown{TEnum}"/> class.
        /// </summary>
        /// <param name="locatorBys">The locator bys</param>
        public Dropdown(params By[] locatorBys) : base(locatorBys)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dropdown{TEnum}"/> class.
        /// </summary>
        /// <param name="outerContainer">The outer container.</param>
        /// <param name="locatorBys">The locator bys.</param>
        public Dropdown(IWebElement outerContainer, params By[] locatorBys) : base(outerContainer, locatorBys)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dropdown{TEnum}"/> class.
        /// </summary>
        /// <param name="elementContainer">The element container.</param>
        public Dropdown(IWebElement elementContainer) : base(elementContainer)
        {
        }

        /// <inheritdoc />
        public IEnumerable<TEnum> Options
            => this.DropdownElement.Options.Select(elem => this.GetOptionByText(elem.Text.Trim())).ToList();

        /// <inheritdoc />
        public virtual TEnum SelectedOption => this.GetOptionByText(this.DropdownElement.SelectedOption.Text);

        /// <summary>
        /// Returns Selected Option Text
        /// </summary>
        /// <returns>Text of Selected option</returns>
        public string SelectedOptionText => this.DropdownElement.SelectedOption.Text;

        /// <summary>
        /// Gets or sets Additional Info for JSON map
        /// </summary>
        public string AdditionalInfo { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets source folder for JSON map
        /// </summary>
        public string SourceFolder { get; set; } = string.Empty;

        /// <summary>
        /// Annotation Options Map 
        /// </summary>
        /// <returns> The map </returns>
        protected EnumPropertyMapper<TEnum, WebElementInfo> Map => EnumPropertyModelCache.GetMap<TEnum, WebElementInfo>(this.AdditionalInfo, this.SourceFolder);

        /// <summary>
        /// Dropdown Element
        /// </summary>
        private SelectElement DropdownElement => new SelectElement(this.GetContainer());

        /// <inheritdoc />
        public bool IsEnabled(TEnum option) =>
            this.DropdownElement.Options.First(dropdownOption => dropdownOption.Text.Equals(this.GetOptionText(option)))
                .Enabled;

        /// <inheritdoc />
        public bool IsSelected(TEnum option) => option.Equals(this.SelectedOption);

        /// <inheritdoc />
        public TPage SelectOption<TPage>(TEnum option) where TPage : ICreatablePageObject
        {
            this.SelectOption(option);
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <inheritdoc />
        public virtual void SelectOption(TEnum option)
        {
            this.DropdownElement.SelectByText(this.GetOptionText(option));
            DriverExtensions.WaitForJavaScript();
        }

        /// <inheritdoc />
        public virtual bool IsDisplayed() => this.GetContainer(WebDriverTimeouts.ElementDisplayed)?.Displayed ?? false;

        /// <summary>
        /// Get text of the option
        /// </summary>
        /// <param name="option"> Option </param>
        /// <returns> Option's text </returns>
        protected virtual string GetOptionText(TEnum option) => this.Map[option].Text;

        /// <summary>
        /// Get option by name
        /// </summary>
        /// <param name="text"> Option's text </param>
        /// <returns> Option </returns>
        protected virtual TEnum GetOptionByText(string text) => text.GetEnumValueByText<TEnum>(this.AdditionalInfo, this.SourceFolder);
    }
}