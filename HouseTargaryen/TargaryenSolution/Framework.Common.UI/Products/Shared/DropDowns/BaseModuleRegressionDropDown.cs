namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Pages;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// Base drop-down object for module regression suites
    /// todo: Delete
    /// </summary>
    /// <typeparam name="T"> Type of Enumeration </typeparam>
    public abstract class BaseModuleRegressionDropdown<T> : BaseWebObject, IDropdown<T> where T : struct
    {
        private EnumPropertyMapper<T, WebElementInfo> map;

        /// <summary>
        /// Get all options
        /// </summary>
        /// <returns> Options list </returns>
        public IEnumerable<T> Options
            => this.DropdownElement.Options.Select(elem => this.GetOptionByText(elem.Text.Trim())).ToList();

        /// <summary>
        /// Return Selected Option
        /// </summary>
        /// <returns> Selected option</returns>
        public virtual T SelectedOption => this.GetOptionByText(this.DropdownElement.SelectedOption.Text);

        /// <summary>
        /// Return Selected Option Text
        /// </summary>
        /// <returns>Text of Selected option</returns>
        public string SelectedOptionText => this.DropdownElement.SelectedOption.Text;

        /// <summary>
        /// Dropdown locator
        /// </summary>
        protected abstract By DropdownLocator { get; }

        /// <summary>
        /// Additional Info for JSON map
        /// </summary>
        protected string AdditionalInfo { get; set; } = string.Empty;

        /// <summary>
        /// Additional Info for JSON map
        /// </summary>
        protected string SourceFolder { get; set; } = string.Empty;

        /// <summary>
        /// Annotation Options Map 
        /// </summary>
        /// <returns> The map </returns>
        protected EnumPropertyMapper<T, WebElementInfo> Map =>
            this.map = this.map ?? EnumPropertyModelCache.GetMap<T, WebElementInfo>(this.AdditionalInfo, this.SourceFolder);

        /// <summary>
        /// Dropdown Element
        /// </summary>
        private SelectElement DropdownElement => new SelectElement(DriverExtensions.WaitForElement(this.DropdownLocator));

        /// <summary>
        /// Verify that option is enabled
        /// </summary>
        /// <param name="option">The option.</param>
        /// <returns>
        /// True if option is enabled, false otherwise
        /// </returns>
        public bool IsEnabled(T option) =>
            this.DropdownElement.Options.First(dropdownOption => dropdownOption.Text.Equals(this.GetOptionText(option)))
                .Enabled;

        /// <summary>
        /// Verify that option is selected
        /// </summary>
        /// <param name="option">Option to verify</param>
        /// <returns> True if option is selected, false otherwise </returns>
        public bool IsSelected(T option) => option.Equals(this.SelectedOption);

        /// <summary>
        /// Select option
        /// </summary>
        /// <typeparam name="TPage"> Page Type </typeparam>
        /// <param name="option"> Option to select </param>
        /// <returns> New instance of the page </returns>
        public TPage SelectOption<TPage>(T option) where TPage : ICreatablePageObject
        {
            this.SelectOption(option);
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// Select option
        /// </summary>
        /// <param name="option"> Option to select </param>
        public virtual void SelectOption(T option) => this.DropdownElement.SelectByText(this.GetOptionText(option));

        /// <summary>
        /// Verify that dropdown is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsDisplayed() => DriverExtensions.IsDisplayed(this.DropdownLocator, 5);

        /// <summary>
        /// Get text of the option
        /// </summary>
        /// <param name="option"> Option </param>
        /// <returns> Option's text </returns>
        protected virtual string GetOptionText(T option) => this.Map[option].Text;

        /// <summary>
        /// Get option by name
        /// </summary>
        /// <param name="text"> Option's text </param>
        /// <returns> Option </returns>
        protected virtual T GetOptionByText(string text) => text.GetEnumValueByText<T>(this.AdditionalInfo, this.SourceFolder);
    }
}