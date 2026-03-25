namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.TaxnetPro.Enums.Toolbar;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <inheritdoc />
    /// <summary>
    /// This is SubscribedContentDropdown in the toolbar
    /// </summary>
    public class SubscribedContentDropdown : BaseModuleRegressionCustomDropdown<SubscribedContentOptions>
    {
        private static readonly By DropdownLocator = By.Id("co_searchPPVToolLink1");

        private static readonly By SelectedOptionLocator = By.XPath("//li[@class='checked']//span");

        private static readonly By SubscribedContentOptionLocator = By.CssSelector("li > a");

        private static readonly By DropdownArrowLocator = By.XPath(".//*[contains(@class,'icon_down_blue_arrow')]");

        private static readonly By ExpandedDropdownLocator = By.CssSelector(".expanded");

        private EnumPropertyMapper<SubscribedContentOptions, WebElementInfo> dropdownMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscribedContentDropdown"/> class.
        /// </summary>
        /// <param name="additionalInfo"> The additional Info. </param>
        /// <param name="sourceFolder"> The source Folder. </param>
        public SubscribedContentDropdown(string additionalInfo = "", string sourceFolder = "Resources/EnumPropertyMaps/TaxnetPro")
        {
            this.dropdownMap = EnumPropertyModelCache.GetMap<SubscribedContentOptions, WebElementInfo>(additionalInfo, sourceFolder);
        }

        /// <summary>
        /// Subscribed Content Option Map
        /// </summary>
        protected EnumPropertyMapper<SubscribedContentOptions, WebElementInfo> DropdownMap =>
            this.dropdownMap = this.dropdownMap
                                   ?? EnumPropertyModelCache.GetMap<SubscribedContentOptions, WebElementInfo>(
                                       string.Empty,
                                       @"Resources/EnumPropertyMaps/TaxnetPro");

        /// <summary>
        /// Retrieve selected option
        /// </summary>
        public override SubscribedContentOptions SelectedOption
        {
            get
            {
                return this.DropdownMap
                    .Where(x => x.Value.Text.Equals(DriverExtensions.GetText(DropdownLocator, SelectedOptionLocator)))
                    .Select(x => x.Key)
                    .FirstOrDefault();
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
        protected override IEnumerable<SubscribedContentOptions> OptionsFromExpandedDropdown => 
            DriverExtensions.GetElements(DropdownLocator, SubscribedContentOptionLocator)
            .Select(x => this.dropdownMap.First(y => y.Value.Text.Equals(x.Text)).Key).ToList();

        /// <summary>
        /// Verify that option is selected
        /// </summary>
        /// <param name="option"> Subscribed Content </param>
        /// <returns> True if matches, else false </returns>
        public override bool IsSelected(SubscribedContentOptions option) => option.Equals(this.SelectedOption);

        /// <summary>
        /// Select option from expanded drop-down
        /// </summary>
        /// <param name="option"> Option to select</param>
        protected override void SelectOptionFromExpandedDropdown(SubscribedContentOptions option) =>
            DriverExtensions.Click(By.XPath(this.dropdownMap[option].LocatorString));

        /// <summary>
        /// ClickDropdownArrow
        /// </summary>
        protected override void ClickDropdownArrow()
        {
            DriverExtensions.GetElement(DropdownLocator, DropdownArrowLocator).CustomClick();
        }

        /// <summary>
        /// Verifies if dropdown is expanded
        /// </summary>
        /// <returns>True if expanded, else false</returns>
        protected override bool IsDropdownExpanded()
        {
            return DriverExtensions.IsDisplayed(this.Dropdown, ExpandedDropdownLocator);
        }
    }
}