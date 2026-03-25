namespace Framework.Common.UI.Products.WestlawEdge.Components.Preferences
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Preferences;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Indigo Search Tab Component
    /// </summary>
    public class EdgeSearchTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("panel_Search");

        private static readonly By SortContainerLocator = By.XPath("//div[@class='UserSettings-sortContainer']");

        private static readonly By SortDropdownItemLocator = By.XPath("./div[@class='UserSettings-select']//span");

        private static readonly By SortDropdownOptionLocator = By.XPath("./option");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Search";

        /// <summary>
        /// EdgeSearchTab Type Map
        /// </summary>
        private EnumPropertyMapper<EdgeSearchTab, WebElementInfo> searchTabMap;

        /// <summary>
        /// Search Sort Option Map
        /// </summary>
        private EnumPropertyMapper<SearchSortOption, WebElementInfo> searchSortOptionMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the EdgeSearchTab enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<EdgeSearchTab, WebElementInfo> SearchTabMap =>
            this.searchTabMap = this.searchTabMap ?? EnumPropertyModelCache.GetMap<EdgeSearchTab, WebElementInfo>(
                                    string.Empty,
                                    @"Resources/EnumPropertyMaps/WestlawEdge/Preferences");

        /// <summary>
        /// Gets the SearchSortOption enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<SearchSortOption, WebElementInfo> SearchSortOptionMap =>
            this.searchSortOptionMap = this.searchSortOptionMap
                                       ?? EnumPropertyModelCache.GetMap<SearchSortOption, WebElementInfo>(
                                           "Edge",
                                           @"Resources/EnumPropertyMaps/WestlawEdge/Toolbars");

        /// <summary>
        /// Set Search Default Sort Order
        /// </summary>
        /// <param name="tabOption">tab option </param>
        /// <param name="sortType"> Sort Type  </param>
        /// <returns>  The <see cref="EdgeSearchTabComponent"/>SearchTabComponent</returns>
        public EdgeSearchTabComponent SetDefaultSortOrderDropdown(EdgeSearchTab tabOption, SearchSortOption sortType)
        {
            IWebElement dropdownLocator = DriverExtensions.WaitForElement(By.XPath(this.SearchTabMap[tabOption].LocatorString));
            dropdownLocator.SetDropdown(this.SearchSortOptionMap[sortType].Text);

            return this;
        }

        /// <summary>
        /// Verifies if selected option is equal to specified sort type
        /// </summary>
        /// <param name="searchTabOption">tab option</param>
        /// <param name="sortType">sort type</param>
        /// <returns>true if euqal</returns>
        public bool IsSortOrderDropdownOptionsSelected(EdgeSearchTab searchTabOption, SearchSortOption sortType)
        {
            string selectedOption = DriverExtensions.GetSelectedDropdownOptionText(By.XPath(this.SearchTabMap[searchTabOption].LocatorString));
            return selectedOption.Equals(this.SearchSortOptionMap[sortType].Text);
        }

        /// <summary>
        /// GetSortDropdowns
        /// </summary>
        /// <returns>List of items</returns>
        public List<string> GetSortDropdowns()
            => DriverExtensions.GetElements(SortContainerLocator, SortDropdownItemLocator).Select(e => e.Text).ToList();

        /// <summary>
        /// GetSortDropdownOptions
        /// </summary>
        /// <param name="searchTabOption">searchTabOption</param>
        /// <returns>List of options</returns>
        public List<string> GetSortDropdownOptions(EdgeSearchTab searchTabOption) =>
            DriverExtensions.GetElements(
                By.XPath(this.SearchTabMap[searchTabOption].LocatorString),
                SortDropdownOptionLocator).Select(e => e.Text).ToList();

        /// <summary>
        /// Returns true if the specified element on the search tab is selected (checked for checkboxes)
        /// </summary>
        /// <param name="searchTabOption">the option to look for</param>
        /// <returns>true if selected, false otherwise</returns>
        public bool IsCheckboxSelected(EdgeSearchTab searchTabOption)
            => DriverExtensions.WaitForElement(By.XPath(this.SearchTabMap[searchTabOption].LocatorString)).Selected;

        /// <summary>
        /// Sets the specified checkbox option on the search tab to the specified value.
        /// </summary>
        /// <param name="tabOption">
        /// the option to look for
        /// </param>
        /// <param name="setTo"> What to set the checkbox to. True for checked, false for unchecked. </param>
        /// <returns>
        /// The <see cref="EdgeSearchTabComponent"/>SearchTabComponent</returns>
        public EdgeSearchTabComponent SetTabOptionCheckbox(EdgeSearchTab tabOption, bool setTo)
        {
            DriverExtensions.SetCheckbox(setTo, By.XPath(this.SearchTabMap[tabOption].LocatorString));
            return this;
        }
    }
}
