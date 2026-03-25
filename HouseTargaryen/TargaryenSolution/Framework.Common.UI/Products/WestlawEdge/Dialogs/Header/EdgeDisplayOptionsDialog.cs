namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Header
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.DisplayOptions;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Display Options dialog
    /// </summary>
    public class EdgeDisplayOptionsDialog : BaseModuleRegressionDialog
    {
        private static readonly By SaveButtonLocator = By.XPath(".//*[@id='co_disOpt_Save']");
        private static readonly By RestoreDefaultOptionsButtonLocator = By.XPath(".//button[@id= 'co_disOpt_restoreDefaults']");

        private EnumPropertyMapper<DisplaySizeOptions, WebElementInfo> sizeOptionsMap;
        private EnumPropertyMapper<DisplayFontOptions, WebElementInfo> fontOptionsMap;

        /// <summary>
        /// Display size option Map
        /// </summary>
        protected EnumPropertyMapper<DisplaySizeOptions, WebElementInfo> SizeOptionsMap =>
            this.sizeOptionsMap = this.sizeOptionsMap
                                  ?? EnumPropertyModelCache.GetMap<DisplaySizeOptions, WebElementInfo>(
                                      string.Empty,
                                      @"Resources/EnumPropertyMaps/WestlawEdge/DisplayOptions");

        /// <summary>
        /// Display font option Map
        /// </summary>
        protected EnumPropertyMapper<DisplayFontOptions, WebElementInfo> FontOptionsMap =>
            this.fontOptionsMap = this.fontOptionsMap
                                  ?? EnumPropertyModelCache.GetMap<DisplayFontOptions, WebElementInfo>(
                                      string.Empty,
                                      @"Resources/EnumPropertyMaps/WestlawEdge/DisplayOptions");

        /// <summary>
        /// Set Size option
        /// </summary>
        /// <param name="selected">
        /// The selected. 
        /// </param>
        /// <param name="option">
        /// Option 
        /// </param>
        /// <returns>
        /// The <see cref="EdgeDisplayOptionsDialog"/>.
        /// </returns>
        public EdgeDisplayOptionsDialog SetSizeOption(DisplaySizeOptions option, bool selected = true)
        {
            DriverExtensions.SetCheckbox(selected, By.XPath(this.SizeOptionsMap[option].LocatorString));
            return this;
        }

        /// <summary>
        /// Set Font option
        /// </summary>
        /// <param name="selected">
        /// The selected. 
        /// </param>
        /// <param name="option">
        /// Option 
        /// </param>
        /// <returns>
        /// The <see cref="EdgeDisplayOptionsDialog"/>.
        /// </returns>
        public EdgeDisplayOptionsDialog SetFontOption(DisplayFontOptions option, bool selected = true)
        {
            DriverExtensions.SetCheckbox(selected, By.XPath(this.FontOptionsMap[option].LocatorString));
            return this;
        }

        /// <summary>
        /// Click Save button
        /// </summary>
        /// <returns>
        /// The <see cref="EdgeCommonDocumentPage"/>. 
        /// </returns>
        public EdgeCommonDocumentPage ClickSaveButton() => this.ClickElement<EdgeCommonDocumentPage>(SaveButtonLocator);

        /// <summary>
        /// Click Restore Default Options button
        /// </summary>
        /// <returns>
        /// The <see cref="EdgeDisplayOptionsDialog"/>. 
        /// </returns>
        public EdgeDisplayOptionsDialog ClickRestoreDefaultOptionsButton() =>
            this.ClickElement<EdgeDisplayOptionsDialog>(RestoreDefaultOptionsButtonLocator);

        /// <summary>
        /// Verifies that the size option is selected.
        /// </summary>
        /// <param name="option"> The option. </param>
        /// <returns> The <see cref="bool"/>. True if flag is selected </returns>
        public bool IsSizeOptionSelected(DisplaySizeOptions option)
            => DriverExtensions.IsCheckboxSelected(By.XPath(this.SizeOptionsMap[option].LocatorString));

        /// <summary>
        /// Verifies that the font option is selected.
        /// </summary>
        /// <param name="option"> The option. </param>
        /// <returns> The <see cref="bool"/>. True if flag is selected </returns>
        public bool IsFontOptionSelected(DisplayFontOptions option)
            => DriverExtensions.IsCheckboxSelected(By.XPath(this.FontOptionsMap[option].LocatorString));
    }
}