namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.Foldering;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Component representing foldering options widget in folder tree component
    /// </summary>
    public class FolderOptionsDropdown : BaseModuleRegressionCustomDropdown<FolderOptions>
    {
        private static readonly By DropdownLocator =
            By.XPath("//li[@id='co_ro_folder_options']//button[@class='a11yDropdown-button']");

        private static readonly By DropdownOptionLocator = By.XPath("//li[@id='co_ro_folder_options']/ul/li");

        private EnumPropertyMapper<FolderOptions, WebElementInfo> folderOptionsMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="FolderOptionsDropdown"/> class. 
        /// Constructor
        /// </summary>
        public FolderOptionsDropdown()
        {
            DriverExtensions.WaitForElement(DropdownLocator);
        }

        /// <summary>
        /// Get selected option
        /// </summary>
        public override FolderOptions SelectedOption
        {
            get { throw new NotImplementedException("Can't get selected item for the FolderOptions dropdown"); }
        }

        /// <summary>
        /// Get Options From dropdown
        /// </summary>
        protected override IEnumerable<FolderOptions> OptionsFromExpandedDropdown =>
            DriverExtensions.GetElements(DropdownOptionLocator)
            .Select(elem => DriverExtensions.GetElement(elem, By.TagName("span"))
            .Text
            .GetEnumValueByText<FolderOptions>())
            .ToList();

        /// <summary>
        /// Dropdown element
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.WaitForElement(DropdownLocator);

        /// <summary>
        /// Gets the FolderOptions Map
        /// </summary>
        private EnumPropertyMapper<FolderOptions, WebElementInfo> FolderOptionsMap
            =>
                this.folderOptionsMap =
                    this.folderOptionsMap ?? EnumPropertyModelCache.GetMap<FolderOptions, WebElementInfo>();

        /// <summary>
        /// Is the Move Folder Option Enabled
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsOptionDisabled(FolderOptions option) =>
            bool.Parse(DriverExtensions.WaitForElement(By.Id(this.FolderOptionsMap[option].Id)).GetAttribute("aria-disabled"));

        /// <summary>
        /// Verify that annotations option is selected
        /// </summary>
        /// <param name="option"> FolderOptions option </param>
        /// <returns> True if option is selected, false otherwise </returns>
        public override bool IsSelected(FolderOptions option)
        {
            throw new NotImplementedException("Folder Options dropdown has no 'IsSelected' functionality");
        }

        /// <summary>
        /// Verify that Folder Options dropdown is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(DropdownLocator, 5);

        /// <summary>
        /// Select Dropdown Option
        /// </summary>
        /// <param name="option">
        /// option from FolderOptions enum
        /// </param>
        protected override void SelectOptionFromExpandedDropdown(FolderOptions option)
        {
            DriverExtensions.WaitForElement(By.Id(this.FolderOptionsMap[option].Id)).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// ClickDropdownArrow
        /// </summary>
        protected override void ClickDropdownArrow()
        {
            DriverExtensions.Click(this.Dropdown);
        }
    }
}