namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Sort By Dropdown
    /// </summary>
    public class CustomSortByDropdown : BaseModuleRegressionCustomDropdown<SearchSortOption>
    {
        private const string DefaultPostfix = "(Default)";

        private const string SearchOptionLctMask = "//ul[@id='co_search_sortOptions']/li[contains(., '{0}')]";

        private static readonly By DropdownLocator = By.XPath("//div[contains(@id,'sortDropDownControl')]");

        private static readonly By DropdownArrowLocator = By.XPath(".//*[@class='icon25 icon_downMenu-gray']");

        private static readonly By SearchOptionsLocator = By.XPath("//ul[@id='co_search_sortOptions']//li");

        private static readonly By SortBySelectElement = By.XPath("//span[@class='sortButtonLabel']/following-sibling::div/button/span[contains(@class,'buttonText')]|//span[text()='Sort by:']/following-sibling::div/button/span[contains(@class,'buttonText')]");

        private EnumPropertyMapper<SearchSortOption, WebElementInfo> dropdownMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomSortByDropdown"/> class.
        /// </summary>
        /// <param name="additionalInfo"> The additional Info. </param>
        /// <param name="sourceFolder"> The source Folder. </param>
        public CustomSortByDropdown(string additionalInfo = "", string sourceFolder = "")
        {
            this.dropdownMap = EnumPropertyModelCache.GetMap<SearchSortOption, WebElementInfo>(additionalInfo, sourceFolder);
        }

        /// <summary>
        /// Retrieve selected option
        /// </summary>
        public override SearchSortOption SelectedOption
        {
            get
            {
                string optionText = DriverExtensions.GetText(DropdownLocator, SortBySelectElement).Replace($" {DefaultPostfix}", string.Empty);
                return this.dropdownMap.Where(x => x.Value.Text.Equals(optionText)).Select(x => x.Key).FirstOrDefault();
            }
        }

        /// <summary>
        /// Dropdown Element
        /// </summary>
        protected override IWebElement Dropdown { get; } = DriverExtensions.SafeGetElement(DropdownLocator);

        /// <summary>
        /// Returns list of available options
        /// </summary>
        /// <returns> List of available options </returns>
        protected override IEnumerable<SearchSortOption> OptionsFromExpandedDropdown => DriverExtensions.GetElements(SearchOptionsLocator)
            .Select(x => this.dropdownMap.First(y => y.Value.Text.Equals(x.Text.Replace($" {DefaultPostfix}", string.Empty))).Key).ToList();

        /// <summary>
        /// Verifies is current sort type default
        /// </summary>
        /// <returns>true or false</returns>
        public bool IsCurrentSortTypeDefault() => DriverExtensions.GetText(DropdownLocator, SortBySelectElement).Contains(DefaultPostfix);

        /// <summary>
        /// Verify that option is selected
        /// </summary>
        /// <param name="option"> Delivery Method to verify </param>
        /// <returns> True if matches, else false </returns>
        public override bool IsSelected(SearchSortOption option) => this.dropdownMap[option].Text?.Equals(DriverExtensions.GetText(SortBySelectElement)) ?? false;

        /// <summary>
        /// Sets the Sort Type on the search results page
        /// </summary>
        /// <param name="optionText"> Option test </param>
        /// <typeparam name="TPage"> New instance of the page </typeparam>
        /// <returns> A new Search Results page, in case the page refreshes </returns>
        public TPage SelectOption<TPage>(string optionText) where TPage : ICreatablePageObject
        {
            this.ExpandIfNotExpanded();
            this.SelectOption(optionText);
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// Select option from expanded drop-down
        /// </summary>
        /// <param name="option"> Option to select</param>
        protected override void SelectOptionFromExpandedDropdown(SearchSortOption option) =>
            DriverExtensions.Click(By.XPath(string.Format(SearchOptionLctMask, this.dropdownMap[option].Text)));

        /// <summary>
        /// ClickDropdownArrow
        /// </summary>
        protected override void ClickDropdownArrow()
        {
            DriverExtensions.GetElement(DropdownLocator, DropdownArrowLocator).Click();
        }

        /// <summary>
        /// Verifies if dropdown is expanded
        /// </summary>
        /// <returns>True if expanded, else false</returns>
        protected override bool IsDropdownExpanded()
        {
            string dropdownClass = DriverExtensions.GetElement(DropdownLocator, By.XPath(".//ul")).GetAttribute("style");
            return dropdownClass.Contains("block", StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Select Option
        /// </summary>
        /// <param name="optionText">The desired option</param>
        private void SelectOption(string optionText) => DriverExtensions.Click(By.XPath(string.Format(SearchOptionLctMask, optionText)));
    }
}