namespace Framework.Common.UI.Products.WestLawNextCanada.Components.QuickCheck.Toolbar
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNextCanada.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents the Canada Quick Check Sort Dropdown component.
    /// </summary>
    public class CanadaQuickCheckSortDropdown : BaseModuleRegressionCustomDropdown<SortByOptions>
    {
        private static readonly By DropdownOptionLocator = By.XPath("//ul[@aria-label='Sort by']/li/span");
        private EnumPropertyMapper<SortByOptions, WebElementInfo> sortOptionsMap;

        /// <summary>
        /// The doc icons map.
        /// </summary>
        private EnumPropertyMapper<SortByOptions, WebElementInfo> SortOptionsMap =>
            this.sortOptionsMap = this.sortOptionsMap ?? EnumPropertyModelCache.GetMap<SortByOptions, WebElementInfo>(
                                   string.Empty,
                                   @"Resources\EnumPropertyMaps\WestlawNextCanada");

        /// <summary>
        /// Initializes a new instance of the <see cref="CanadaQuickCheckSortDropdown"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public CanadaQuickCheckSortDropdown(IWebElement container)
        {
            this.Container = container;
        }

        /// <summary>
        /// Select an option from the Sort By dropdown and return a new page instance.
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="option"></param>
        /// <returns></returns>
        public new TPage SelectOption<TPage>(SortByOptions option) where TPage : ICreatablePageObject
        {
            DriverExtensions.WaitForPageLoad();
            this.SelectOptionFromExpandedDropdown(option);
            DriverExtensions.WaitForAnimation();
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// Verifies is Sort By option is selected.
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public override bool IsSelected(SortByOptions option)
        {
            return DriverExtensions.GetElement(this.Container, By.XPath(this.SortOptionsMap[option].LocatorString))
                                   .GetAttribute("aria-checked").Equals("true");
        }

        /// <summary>
        /// Gets the selected option.
        /// </summary>
        public override SortByOptions SelectedOption
        {
            get
            {
                this.ExpandIfNotExpanded();
                string checkedElementText = DriverExtensions.GetElements(this.Dropdown, DropdownOptionLocator).FirstOrDefault(
                    elem => elem.GetAttribute("aria-checked").Contains("true")).Text;
                return checkedElementText.GetEnumValueByText<SortByOptions>(
                    string.Empty,
                    @"Resources\EnumPropertyMaps\WestlawNextCanada");
            }
        }

        /// <summary>
        /// Options from Sort By expanded dropdown.
        /// </summary>
        protected override IEnumerable<SortByOptions> OptionsFromExpandedDropdown
        {
            get
            {
                return DriverExtensions.GetElements(this.Dropdown, DropdownOptionLocator).Select(
                    elem => elem.Text.GetEnumValueByText<SortByOptions>(
                    string.Empty,
                    @"Resources\EnumPropertyMaps\WestlawNextCanada")).ToList();
            }
        }
          

        /// <summary>
        /// Select option from the expanded dropdown.
        /// </summary>
        /// <param name="option"></param>
        protected override void SelectOptionFromExpandedDropdown(SortByOptions option)
        {
            this.ExpandIfNotExpanded();
            DriverExtensions.WaitForElement(By.XPath(this.SortOptionsMap[option].LocatorString)).CustomClick();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// The dropdown.
        /// </summary>
        protected override IWebElement Dropdown => this.Container;

        /// <summary>
        /// Gets the container.
        /// </summary>
        private IWebElement Container { get; }
    }
}