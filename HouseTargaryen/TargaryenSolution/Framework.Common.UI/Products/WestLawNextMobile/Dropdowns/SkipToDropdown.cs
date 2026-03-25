namespace Framework.Common.UI.Products.WestLawNextMobile.Dropdowns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.WestLawNext.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Skip To Dropdown
    /// </summary>
    public class SkipToDropdown : BaseModuleRegressionCustomDropdown<ContentType>
    {
        private static readonly By SkipToDropdownLocator = By.Id("co_mobile_jumpToHandle");

        private static readonly By ContentContainerLocator = By.Id("co_mobile_jumpToContentList");

        private static readonly By DropdownOptionLocator = By.XPath("//div[@id='co_mobile_jumpToContentList']/ul/li");

        private EnumPropertyMapper<ContentType, ContentTypeInfo> contentTypeMap;

        /// <summary>
        /// Unable to identify selected option for SkipTo dropdown
        /// </summary>
        public override ContentType SelectedOption
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<ContentType, ContentTypeInfo> ContentTypeMap
            =>
                this.contentTypeMap =
                    this.contentTypeMap ?? EnumPropertyModelCache.GetMap<ContentType, ContentTypeInfo>();

        /// <summary>
        /// OptionsFromExpandedDropdown
        /// </summary>
        protected override IEnumerable<ContentType> OptionsFromExpandedDropdown
            =>
                DriverExtensions.GetElements(DropdownOptionLocator)
                                .Select(elem => this.ConvertOption(elem.Text)).ToList()
                                .Select(elem => elem.GetEnumValueByText<ContentType>()).ToList();

        /// <summary>
        /// Dropdown
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.GetElement(SkipToDropdownLocator);

        /// <summary>
        /// Unable to verify if option is selected
        /// </summary>
        /// <param name="option"> Option </param>
        /// <returns> NotImplementedException </returns>
        public override bool IsSelected(ContentType option)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Verify that Skip To dropdown is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsDropdownDisplayed() => DriverExtensions.IsDisplayed(SkipToDropdownLocator, 5);

        /// <summary>
        /// Verify that dropdown is expanded
        /// </summary>
        /// <returns> True if expanded, false otherwise </returns>
        protected override bool IsDropdownExpanded() => DriverExtensions.IsDisplayed(ContentContainerLocator);

        /// <summary>
        /// SelectOptionFromExpandedDropdown
        /// </summary>
        /// <param name="option"> Option </param>
        protected override void SelectOptionFromExpandedDropdown(ContentType option)
        {
            string optionName = this.ContentTypeMap[option].Text;
            DriverExtensions.GetElements(DropdownOptionLocator).FirstOrDefault(elem => elem.Text.Contains(optionName))?.Click();
        }

        private string ConvertOption(string option) => option.Substring(0, option.IndexOf('(')).Trim();
    }
}
