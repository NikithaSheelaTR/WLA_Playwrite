namespace Framework.Common.UI.Products.WestlawEdge.Elements.QuickCheck
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.WestlawEdge.DropDowns;
    using Framework.Common.UI.Products.WestlawEdge.Items.QuickCheck;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The select report drop down.
    /// </summary>
    public class SelectReportDropdown : CustomStringDropdown, IDropdownWithItems<string, SelectReportDropdownItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectReportDropdown"/> class.
        /// </summary>
        /// <param name="dropdownLocator">
        /// The drop down locator.
        /// </param>
        public SelectReportDropdown(By dropdownLocator) : base(dropdownLocator)
        {
        }

        /// <inheritdoc />
        public IEnumerable<SelectReportDropdownItem> OptionItems
        {
            get
            {
                this.ExpandIfNotExpanded();
                IList<IWebElement> itemContainers = DriverExtensions.GetElements(this.GetContainer(), this.DropdownOptionsLocator);
                return itemContainers.Select(container => new SelectReportDropdownItem(container));
            }
        }

        /// <inheritdoc />
        public SelectReportDropdownItem SelectedOptionItem
        {
            get
            {
                this.ExpandIfNotExpanded();
                return this.OptionItems.First(item => item.IsSelected);
            }
        }

        /// <summary>
        /// The selected option.
        /// </summary>
        public override string SelectedOption => this.SelectedOptionItem.OptionText;

        /// <inheritdoc />
        protected override IEnumerable<string> OptionsFromExpandedDropdown => this.OptionItems.Select(item => item.OptionText);

        /// <summary>
        /// The drop down button.
        /// </summary>
        private IButton DropdownButton => new Button(this.GetContainer(), this.DropdownArrowLocator);

        /// <summary>
        /// The is selected.
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsSelected(string option) => this.SelectedOption.Equals(option);

        /// <inheritdoc />
        protected override void SelectOptionFromExpandedDropdown(string option)
        {
            SelectReportDropdownItem item = this.OptionItems.First(
                optionItem =>
                    optionItem.OptionText.Equals(
                        option,
                        StringComparison.OrdinalIgnoreCase));

            item.Link.Click();
        }

        /// <inheritdoc />
        protected override bool IsDropdownExpanded() => this.DropdownButton.GetAttribute("aria-expanded").Equals("true");

        /// <inheritdoc />
        protected override void ClickDropdownArrow() => this.DropdownButton.Click();
    }
}