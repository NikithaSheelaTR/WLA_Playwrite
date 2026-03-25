namespace Framework.Common.UI.Products.WestlawEdge.DropDowns
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Constants;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The base custom dropdown.
    /// </summary>
    /// <typeparam name="T">Options Type</typeparam>
    public abstract class BaseCustomDropdown<T> : BaseContainerWrapper, IDropdown<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCustomDropdown{T}"/> class. 
        /// </summary>
        /// <param name="locatorBys">
        /// The locator bys
        /// </param>
        protected BaseCustomDropdown(params By[] locatorBys) : base(locatorBys)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCustomDropdown{T}"/> class.
        /// </summary>
        /// <param name="outerContainer">The outer container.</param>
        /// <param name="locatorBys">The locator bys.</param>
        protected BaseCustomDropdown(IWebElement outerContainer, params By[] locatorBys) : base(outerContainer, locatorBys)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCustomDropdown{T}"/> class.
        /// </summary>
        /// <param name="elementContainer">The element container.</param>
        protected BaseCustomDropdown(IWebElement elementContainer) : base(elementContainer)
        {
        }

        /// <summary>
        /// Gets all options 
        /// </summary>
        /// <returns> Options list </returns>
        public IEnumerable<T> Options
        {
            get
            {
                this.ExpandIfNotExpanded();
                return this.OptionsFromExpandedDropdown;
            }
        }

        /// <summary>
        /// Return Selected Option
        /// </summary>
        /// <returns> Selected option</returns>
        public abstract T SelectedOption { get; }

        /// <summary>
        /// Gets the selected option text.
        /// </summary>
        public virtual string SelectedOptionText => this.SelectedOption.ToString();

        /// <summary>
        /// Gets or sets the dropdown arrow locator.
        /// </summary>
        public By DropdownArrowLocator { get; set; } = By.XPath(".//a | .//button");

        /// <summary>
        /// Gets or sets the dropdown arrow locator.
        /// </summary>
        public By DropdownOptionsLocator { get; set; } = By.XPath(".//ul/li");

        /// <summary>
        /// Get dropdown options
        /// </summary>
        protected abstract IEnumerable<T> OptionsFromExpandedDropdown { get; }

        /// <summary>
        /// Verify that option is selected
        /// </summary>
        /// <param name="option">Option to verify</param>
        /// <returns> True if option is selected, false otherwise </returns>
        public abstract bool IsSelected(T option);

        /// <inheritdoc />
        public abstract bool IsEnabled(T option);

        /// <inheritdoc />
        public virtual bool IsDisplayed() => this.GetContainer(WebDriverTimeouts.ElementDisplayed)?.Displayed ?? false;

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
        public void SelectOption(T option)
        {
            this.ExpandIfNotExpanded();
            this.SelectOptionFromExpandedDropdown(option);
        }

        /// <summary>
        /// Select option
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        protected abstract void SelectOptionFromExpandedDropdown(T option);

        /// <summary>
        /// Expand dropdown
        /// </summary>
        protected void ExpandIfNotExpanded()
        {
            if (!this.IsDropdownExpanded())
            {
                this.ClickDropdownArrow();
            }
        }

        /// <summary>
        /// Verifies if dropdown is expanded
        /// </summary>
        /// <returns>True if expanded</returns>
        protected virtual bool IsDropdownExpanded()
        {
            string dropdownClass = this.GetContainer().GetAttribute("class");
            return dropdownClass.Contains("expanded", StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// ClickDropdownArrow
        /// </summary>
        protected virtual void ClickDropdownArrow() => DriverExtensions.Click(this.GetContainer(), this.DropdownArrowLocator);
    }
}