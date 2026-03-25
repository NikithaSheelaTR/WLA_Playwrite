namespace Framework.Common.UI.Products.WestLawNext.DropDowns
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Common.UI.Interfaces.Elements;

    using OpenQA.Selenium;

    /// <summary>
    /// Compartment drop down inside header section near the logo
    /// </summary>
    public class CompartmentDropdown : BaseModuleRegressionCustomDropdown<CompartmentType>
    {
        private static readonly By DropdownLocator = By.Id("co_compartment");

        private static readonly By CompartmentDropdownArrowButtonLocator = By.XPath("//button[contains(@class, 'co_dropDownButton')]");
        
        private static readonly By OptionLocator = By.XPath("//div[@id='co_compartment']/ul/li");

        private static readonly By SelectedOptionLocator =
            By.XPath("//div[@id='co_compartment']//li[contains(@class,'selected')]/a");

        private EnumPropertyMapper<CompartmentType, WebElementInfo> compartmentTypeMap;

        /// <summary>
        /// Compartment Dropdown Arrow Button
        /// </summary>
        public IButton ArrowButton => new Button(CompartmentDropdownArrowButtonLocator);

        /// <summary>
        /// Selected Option Link
        /// </summary>
        public ILink SelectedOptionLink => new Link(SelectedOptionLocator);

        /// <summary>
        /// Return Selected Option
        /// </summary>
        public override CompartmentType SelectedOption => this.SelectedOptionLink.Text.Replace("Selected\r\n", string.Empty).GetEnumValueByText<CompartmentType>();

        /// <summary>
        /// Get Options
        /// </summary>
        protected override IEnumerable<CompartmentType> OptionsFromExpandedDropdown =>       
             DriverExtensions.GetElements(OptionLocator)
            .Select(elem => elem.Text.Replace("Selected\r\n", string.Empty)
            .GetEnumValueByPropertyModel<CompartmentType, WebElementInfo>(info => info.Text.Trim()))
            .ToList();
                       
        /// <summary>
        /// Dropdown
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.GetElement(DropdownLocator);

        /// <summary>
        /// Gets the Compartment Type Map
        /// </summary>
        private EnumPropertyMapper<CompartmentType, WebElementInfo> CompartmentTypeMap
            => this.compartmentTypeMap = this.compartmentTypeMap ?? EnumPropertyModelCache.GetMap<CompartmentType, WebElementInfo>();

        /// <summary>
        /// Verify whether CompartmentType option is selected
        /// </summary>
        /// <param name="option">Option to compare with</param>
        /// <returns>True if option is equal, otherwise - false</returns>
        public override bool IsSelected(CompartmentType option) => this.SelectedOption.Equals(option);

        /// <summary>
        /// Get Link Text
        /// </summary>
        /// <param name="option"> Option to select </param>
        /// <returns> The <see cref="string"/> </returns>
        public string GetLinkText(CompartmentType option)
            => DriverExtensions.GetAttribute("data-lightbox", By.Id(this.CompartmentTypeMap[option].Id))
            ?? DriverExtensions.GetAttribute("href", By.XPath(this.CompartmentTypeMap[option].LocatorString));

        /// <summary>
        /// Select Dropdown Option
        /// </summary>
        /// <param name="option">Option to select</param>
        protected override void SelectOptionFromExpandedDropdown(CompartmentType option)
            => DriverExtensions.Click(By.XPath(this.CompartmentTypeMap[option].LocatorString));
    }
}