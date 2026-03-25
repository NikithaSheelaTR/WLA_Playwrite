namespace Framework.Common.UI.Products.WestLawNextCanada.DropDowns
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.RI;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// Sort dropdown in Judicial Consideration page
    /// </summary>
    public class JcSortDropdown : BaseModuleRegressionCustomDropdown<JudicialConsiderationSortByOptions>
    {
        private static readonly By DropdownLocator = By.XPath("//div[@id='co_sortDropDownControl']/button");

        private static readonly By DropdownOptionsLocator = By.XPath("//ul[@id='co_search_sortOptions']/li/span");

        private static readonly By SelectedOptionLocator = By.XPath(".//li[contains(@class,'item_selected')]/span");

        private readonly IWebElement container;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateDropdown"/> class.
        /// </summary>
        /// <param name="container">Container element</param>
        public JcSortDropdown(IWebElement container) => this.container = container;

        /// <summary>
        /// Return Selected Option
        /// </summary>
        /// <returns> Selected option</returns>
        public override JudicialConsiderationSortByOptions SelectedOption =>
            DriverExtensions.GetElement(this.Dropdown, SelectedOptionLocator).Text
                            .GetEnumValueByText<JudicialConsiderationSortByOptions>();

        /// <summary>
        /// Date dropdown map
        /// </summary>
        protected EnumPropertyMapper<JudicialConsiderationSortByOptions, WebElementInfo> JcSortDropdownMap
            => EnumPropertyModelCache.GetMap<JudicialConsiderationSortByOptions, WebElementInfo>(
                string.Empty);

        /// <summary>
        /// Gets Options
        /// </summary>
        protected override IEnumerable<JudicialConsiderationSortByOptions> OptionsFromExpandedDropdown =>
            DriverExtensions.GetElements(this.Dropdown, DropdownOptionsLocator).Select(
                elem => elem.Text.GetEnumValueByText<JudicialConsiderationSortByOptions>(string.Empty)).ToList();

        /// <summary>
        /// Dropdown
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.GetElement(DropdownLocator);

        /// <summary>
        /// Verify that date dropdown option is selected
        /// </summary>
        /// <param name="option">date dropdown option</param>
        /// <returns></returns>
        public override bool IsSelected(JudicialConsiderationSortByOptions option)
            => DriverExtensions.GetElement(this.Dropdown, By.XPath(this.JcSortDropdownMap[option].LocatorString)).GetAttribute("class")
                               .Contains("item_selected");

        /// <summary>
        /// Select dropdown option
        /// </summary>
        /// <param name="option"></param>
        protected override void SelectOptionFromExpandedDropdown(JudicialConsiderationSortByOptions option)
            => DriverExtensions.WaitForElement(this.Dropdown, By.XPath(this.JcSortDropdownMap[option].LocatorString)).Click();

        /// <summary>
        /// Verifies if dropdown is expanded
        /// </summary>
        /// <returns></returns>
        protected override bool IsDropdownExpanded()
        {
            string dropdownClass = DriverExtensions.GetElement(this.Dropdown).GetAttribute("aria-expanded");
            return dropdownClass.Equals("true");
        }
    }
}
