namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Sort By Dropdown
    /// </summary>
    public class AccessibilitySortByDropdown : BaseModuleRegressionCustomDropdown<SearchSortOption>
    {
        private const string SearchOptionLctMask = "//ul[@id='co_search_sortOptions']//li[contains(., '{0}')]";

        private static readonly By SearchOptionsLocator = By.XPath("//ul[@id='co_search_sortOptions']//li");

        private static readonly By SortByDropdownLocator = By.XPath("//div[@id='co_search_sortDropDownControl']//span[@class='a11yDropdown-buttonText']");

        /// <summary>
        /// Retrieve selected option
        /// </summary>
        public override SearchSortOption SelectedOption
        {
            get
            {
                return this.DropdownMap.Where(x => x.Value.Text.Equals(DriverExtensions.GetText(SortByDropdownLocator)))
                           .Select(x => x.Key).FirstOrDefault();
            }
        }

        /// <summary>
        /// IWebElement
        /// </summary>
        protected override IWebElement Dropdown { get; } = DriverExtensions.SafeGetElement(SortByDropdownLocator);

        /// <summary>
        /// Returns list of available options
        /// </summary>
        /// <returns> List of available options </returns>
        protected override IEnumerable<SearchSortOption> OptionsFromExpandedDropdown => DriverExtensions.GetElements(SearchOptionsLocator)
            .Select(x => this.DropdownMap.First(y => y.Value.Text.Equals(x.Text)).Key).ToList();

        private EnumPropertyMapper<SearchSortOption, WebElementInfo> DropdownMap { get; } = EnumPropertyModelCache.GetMap<SearchSortOption, WebElementInfo>();

        /// <summary>
        /// Verify that option is selected
        /// </summary>
        public override bool IsSelected(SearchSortOption option) => this.DropdownMap[option].Text?.Equals(DriverExtensions.GetText(SortByDropdownLocator)) ?? false;

        /// <summary>
        /// Select option from expanded drop-down
        /// </summary>
        /// <param name="option"> Option to select</param>
        protected override void SelectOptionFromExpandedDropdown(SearchSortOption option) =>
            DriverExtensions.Click(By.XPath(string.Format(SearchOptionLctMask, this.DropdownMap[option].Text)));
    }
}