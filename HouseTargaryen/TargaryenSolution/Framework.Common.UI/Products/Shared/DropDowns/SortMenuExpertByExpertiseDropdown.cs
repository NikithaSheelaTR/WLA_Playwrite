namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.SortingTypes;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Sort By Dropdown
    /// </summary>
    public class SortMenuExpertByExpertiseDropdown : BaseModuleRegressionCustomDropdown<SortMenuExpertByExpertise>
    {
        private const string SearchOptionLctMask = "//ul[contains(@class,'a11yDropdown-menu')]/li[contains(., '{0}')]";

        private static readonly By DropdownLocator = By.XPath("//div[contains(@id,'co_SortByCounty')]");

        private static readonly By DropdownArrowLocator = By.XPath(".//*[contains(@class,'icon_down_blue_arrow')]");

        private static readonly By SearchOptionsLocator = By.XPath(".//ul[contains(@class,'a11yDropdown-menu')]/li");

        private static readonly By SortBySelectElement = By.XPath(".//span[contains(@class,'a11yDropdown-buttonText')]");

        private EnumPropertyMapper<SortMenuExpertByExpertise, WebElementInfo> dropdownMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="SortMenuExpertByExpertiseDropdown"/> class.
        /// </summary>
        /// <param name="additionalInfo"> The additional Info. </param>
        /// <param name="sourceFolder"> The source Folder. </param>
        public SortMenuExpertByExpertiseDropdown(string additionalInfo = "", string sourceFolder = "")
        {
            this.dropdownMap = EnumPropertyModelCache.GetMap<SortMenuExpertByExpertise, WebElementInfo>(additionalInfo, sourceFolder);
        }

        /// <summary>
        /// Retrieve selected option
        /// </summary>
        public override SortMenuExpertByExpertise SelectedOption
        {
            get
            {
                return this.dropdownMap.Where(x => x.Value.Text.Equals(DriverExtensions.GetText(DropdownLocator, SortBySelectElement)))
                           .Select(x => x.Key).FirstOrDefault();
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
        protected override IEnumerable<SortMenuExpertByExpertise> OptionsFromExpandedDropdown => DriverExtensions.GetElements(SearchOptionsLocator)
            .Select(x => this.dropdownMap.First(y => y.Value.Text.Equals(x.Text)).Key).ToList();

        /// <summary>
        /// Verify that option is selected
        /// </summary>
        /// <param name="option"> Delivery Method to verify </param>
        /// <returns> True if matches, else false </returns>
        public override bool IsSelected(SortMenuExpertByExpertise option) => this.dropdownMap[option].Text?.Equals(DriverExtensions.GetText(SortBySelectElement)) ?? false;

        /// <summary>
        /// Select option from expanded drop-down
        /// </summary>
        /// <param name="option"> Option to select</param>
        protected override void SelectOptionFromExpandedDropdown(SortMenuExpertByExpertise option) =>
            DriverExtensions.Click(By.XPath(string.Format(SearchOptionLctMask, this.dropdownMap[option].Text)));

        /// <summary>
        /// ClickDropdownArrow
        /// </summary>
        protected override void ClickDropdownArrow()
        {
            DriverExtensions.GetElement(this.Dropdown, DropdownArrowLocator).CustomClick();
        }

        /// <summary>
        /// Verifies if dropdown is expanded
        /// </summary>
        /// <returns>True if expanded, else false</returns>
        protected override bool IsDropdownExpanded()
        {
            string dropdownClass = DriverExtensions.GetElement(this.Dropdown, By.XPath(".//ul")).GetAttribute("style");
            return dropdownClass.Contains("block", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}