namespace Framework.Common.UI.Products.WestlawEdge.DropDowns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Header;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.DataModel;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    using EnumExtension = Framework.Core.CommonTypes.Extensions.EnumExtension;

    /// <summary>
    /// Region Toggle on the Header 
    /// </summary>
    public class EdgeRegionDropdown : BaseModuleRegressionCustomDropdown<EdgeRegions>
    {
        private static readonly By DropdownContainerLocator = By.XPath("//*[@id='coid_countryDropdown']//div[@class='co_dropDownMenuContent']");

        private static readonly By DropdownOptionsLocator = By.XPath(
            "//*[@id='coid_countryDropdown']//div[@class='co_dropDownMenuContent']//span[not(@class)]");

        private static readonly By DropdownArrowLocator = By.XPath("//div[@id='regions-dropdown']//a");

        private static readonly string DropdownOptionLctMask = "//ul[@class='co_dropDownMenuList hasIcons']//span[text()='{0}']";

        private EnumPropertyMapper<EdgeRegions, BaseTextModel> regionsMap;

        /// <summary>
        /// Returns Selected Option
        /// </summary>
        /// <returns> Selected Region</returns>
        public override EdgeRegions SelectedOption
        {
            get
            {
                return this.RegionsMap.Where(x => x.Value.Text.Equals(DriverExtensions.GetText(DropdownContainerLocator, DropdownArrowLocator)))
                           .Select(x => x.Key).FirstOrDefault();
            }
        }

        /// <summary>
        /// Indigo Region Map
        /// </summary>
        protected EnumPropertyMapper<EdgeRegions, BaseTextModel> RegionsMap =>
            this.regionsMap = this.regionsMap ?? EnumPropertyModelCache.GetMap<EdgeRegions, BaseTextModel>(
                                  string.Empty,
                                  @"Resources/EnumPropertyMaps/WestlawEdge/Header");

        /// <summary>
        /// Dropdown element
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.GetElement(DropdownContainerLocator);

        /// <summary>
        /// Get Region Options
        /// </summary>
        protected override IEnumerable<EdgeRegions> OptionsFromExpandedDropdown
           => DriverExtensions.GetElements(DropdownOptionsLocator)
                    .Select(elem => EnumExtension.GetEnumValueByText<EdgeRegions>(elem.Text,
                       string.Empty, @"Resources/EnumPropertyMaps/WestlawEdge/Header")).ToList();

        //=> DriverExtensions.GetElements(this.Dropdown,DropdownOptionsLocator)
        // .Select(e => e.Text.GetEnumValueByText<EdgeRegions>(string.Empty,
        //  @"Resources/EnumPropertyMaps/WestlawEdge/Header")).ToList();

        /// <summary>
        /// Verify that option is selected
        /// </summary>
        /// <param name="region"> Option to select </param>
        /// <returns> true or false</returns>
        public override bool IsSelected(EdgeRegions region) => 
            this.RegionsMap[region].Text?.Equals(DriverExtensions.GetText(DropdownContainerLocator, DropdownArrowLocator)) ?? false;
        

        /// <summary>
        /// Select Dropdown Option
        /// </summary>
        /// <param name="region">The region</param>
        protected override void SelectOptionFromExpandedDropdown(EdgeRegions region)
        {
            DriverExtensions.GetElement(By.XPath(string.Format(DropdownOptionLctMask, this.RegionsMap[region].Text))).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Click Dropdown arrow
        /// </summary>
        protected override void ClickDropdownArrow() => DriverExtensions.GetElement(DropdownArrowLocator).Click();
    }
}
