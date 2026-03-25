namespace Framework.Common.UI.Products.WestlawEdge.DropDowns
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Folders;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Date dropdown in the folder grid
    /// </summary>
    public class DateDropdown : BaseModuleRegressionCustomDropdown<DateDropdownOption>
    {
        private static readonly By DropdownLocator = By.XPath(".//div[contains(@id, dropdown)]");
        private static readonly By DropdownOptionLocator = By.XPath(".//li/span");
        private static readonly By SelectedOptionLocator = By.XPath(".//li[contains(@class,'item_selected')]/span");

        private readonly IWebElement container;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateDropdown"/> class.
        /// </summary>
        /// <param name="container">Container element</param>
        public DateDropdown(IWebElement container) => this.container = container;

        /// <summary>
        /// Return Selected Option
        /// </summary>
        /// <returns> Selected option</returns>
        public override DateDropdownOption SelectedOption =>
            DriverExtensions.GetElement(this.Dropdown, SelectedOptionLocator).Text
                            .GetEnumValueByText<DateDropdownOption>();

        /// <summary>
        /// Date dropdown map
        /// </summary>
        protected EnumPropertyMapper<DateDropdownOption, WebElementInfo> DateDropdownMap
            => EnumPropertyModelCache.GetMap<DateDropdownOption, WebElementInfo>(
                string.Empty,
                @"Resources/EnumPropertyMaps/WestlawEdge/Folders");

        /// <summary>
        /// Gets Options
        /// </summary>
        protected override IEnumerable<DateDropdownOption> OptionsFromExpandedDropdown
            => DriverExtensions.GetElements(this.Dropdown, DropdownOptionLocator)
                               .Select(elem => elem.Text.GetEnumValueByText<DateDropdownOption>(string.Empty,
                                   @"Resources/EnumPropertyMaps/WestlawEdge/Folders")).ToList();

        /// <summary>
        /// Dropdown
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.GetElement(this.container, DropdownLocator);

        /// <summary>
        /// Verify that date dropdown option is selected
        /// </summary>
        /// <param name="option">date dropdown option</param>
        /// <returns></returns>
        public override bool IsSelected(DateDropdownOption option)
            => DriverExtensions.GetElement(this.Dropdown, By.XPath(this.DateDropdownMap[option].LocatorString)).GetAttribute("class")
                               .Contains("item_selected");

        /// <summary>
        /// Select dropdown option
        /// </summary>
        /// <param name="option"></param>
        protected override void SelectOptionFromExpandedDropdown(DateDropdownOption option)
        => DriverExtensions.WaitForElement(this.Dropdown, By.XPath(this.DateDropdownMap[option].LocatorString)).Click();
    }
}