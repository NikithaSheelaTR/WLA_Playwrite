namespace Framework.Common.UI.Products.WestLawNextCanada.DropDowns
{
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// Sort dropdown on co-cites Ri tab
    /// </summary>
    public class CanadaCoCitesSortDropdown : Dropdown<SearchSortOption>
    {
        private readonly By dropdownLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CanadaCoCitesSortDropdown"/> class.
        /// </summary>
        /// <param name="container">Container</param>
        /// <param name="sortByDropdownLocator">Sort By dropdown element</param>
        public CanadaCoCitesSortDropdown(By container, By sortByDropdownLocator) =>
            this.dropdownLocator = new ByChained(container, sortByDropdownLocator);

        /// <summary>
        /// Verifies if Detail drop down is displayed link displayed
        /// </summary>
        /// <returns>The instance of the page</returns>
        /// <returns></returns>
        public override bool IsDisplayed() => DriverExtensions.GetElement(dropdownLocator).IsDisplayed();
    }
}