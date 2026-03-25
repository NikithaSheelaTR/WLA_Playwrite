namespace Framework.Common.UI.Products.WestLawNextMobile.Dropdowns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.Mobile;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Mobile Tools dropdown
    /// </summary>
    public class ToolsDropdown : BaseModuleRegressionCustomDropdown<MobileDocMenuTool>
    {
        private static readonly By ToolsDropdownLocator = By.Id("coid_website_toolsLinksExpandCollapse");

        private static readonly By ToolLinksContainerLocator = By.Id("coid_website_toolsLinks");

        private static readonly By DropdownOptionLocator = By.XPath("//div[@id='coid_website_toolsLinks']/ul/li/a");

        /// <summary>
        /// The tools map.
        /// </summary>
        private EnumPropertyMapper<MobileDocMenuTool, WebElementInfo> toolsMap;

        /// <summary>
        /// Unable to identify selected option for MobileToolDropdown
        /// </summary>
        public override MobileDocMenuTool SelectedOption
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// OptionsFromExpandedDropdown
        /// </summary>
        protected override IEnumerable<MobileDocMenuTool> OptionsFromExpandedDropdown
            =>
                DriverExtensions.GetElements(DropdownOptionLocator)
                                .Select(elem => this.ConvertMobileOption(elem.Text).GetEnumValueByText<MobileDocMenuTool>())
                                .ToList();

        /// <summary>
        /// Dropdown
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.GetElement(ToolsDropdownLocator);

        /// <summary>
        /// Gets the tabs Map.
        /// </summary>
        protected EnumPropertyMapper<MobileDocMenuTool, WebElementInfo> ToolsMap
            => this.toolsMap = this.toolsMap ?? EnumPropertyModelCache.GetMap<MobileDocMenuTool, WebElementInfo>();

        /// <summary>
        /// Unable to verify if option is selected
        /// </summary>
        /// <param name="option"> Option </param>
        /// <returns> NotImplementedException </returns>
        public override bool IsSelected(MobileDocMenuTool option)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Verify that dropdown is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsDropdownDisplayed() => DriverExtensions.IsDisplayed(ToolsDropdownLocator, 5);

        /// <summary>
        /// SelectOptionFromExpandedDropdown
        /// </summary>
        /// <param name="option"> Option to select </param>
        protected override void SelectOptionFromExpandedDropdown(MobileDocMenuTool option)
            =>
                DriverExtensions.WaitForElement(
                    DriverExtensions.WaitForElement(ToolLinksContainerLocator),
                    By.XPath(string.Format(this.ToolsMap[option].LocatorString))).Click();

        /// <summary>
        /// Verify that dropdown is expanded
        /// </summary>
        /// <returns> True if expanded, false otherwise </returns>
        protected override bool IsDropdownExpanded() => DriverExtensions.IsDisplayed(ToolLinksContainerLocator);

        /// <summary>
        /// Possible options for matters: "Save to Firm Central Matters", "Save to Matters"
        /// Possible options for 'Save to Research': "Save to {user name}'s Research"
        /// </summary>
        /// <param name="text"> Text to convert </param>
        /// <returns> Converted mobile option </returns>
        private string ConvertMobileOption(string text)
        {
            const string SaveToResearch = "Save to Research";
            const string SaveToMatters = "Save to Matters";

            string convertedString = text.Trim();
            if (text.Contains("Research") && text.Contains("Save to"))
            {
                convertedString = SaveToResearch;
            }

            if (text.Contains("Matters") && text.Contains("Save to"))
            {
                convertedString = SaveToMatters;
            }

            return convertedString;
        }
    }
}
