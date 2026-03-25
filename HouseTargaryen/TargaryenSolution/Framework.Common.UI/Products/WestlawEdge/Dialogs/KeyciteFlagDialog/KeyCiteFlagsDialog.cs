namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.KeyciteFlagDialog
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Enums.Toolbars;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// The key cite flags dialogs that appears by clicking KeyCite Flag button.
    /// </summary>
    public class KeyCiteFlagsDialog : BaseModuleRegressionDialog
    {
        private static readonly By ApplyButtonLocator =
            By.XPath(".//button[@class='co_buttonApply co_primaryBtn']");

        private static readonly By KeyCiteCrossedIconLocator =
            By.XPath("//div[@id='keyCiteButtonContainer']/button/span[contains(@class,'icon_flagStrikethrough-blue')]");

        private static readonly By OptionsLocator = By.XPath("//ul[@class='optionsContainer']/li");

        private static readonly By DialogContainerLocator =
            By.XPath("//li[@id='co_docToolbarKeyCiteToggle']//div[@class='dropdownContainer']");

        private readonly string sourceFolder = "Resources/EnumPropertyMaps/WestlawEdge/Toolbars";

        private EnumPropertyMapper<KeyCiteFlagOption, WebElementInfo> keyCiteFlagsMap;

        /// <summary>
        /// KeyCite Flags Option Map
        /// </summary>
        protected EnumPropertyMapper<KeyCiteFlagOption, WebElementInfo> KeyCiteFlagsMap =>
            this.keyCiteFlagsMap = this.keyCiteFlagsMap
                                   ?? EnumPropertyModelCache.GetMap<KeyCiteFlagOption, WebElementInfo>(
                                       string.Empty,
                                       @"Resources/EnumPropertyMaps/WestlawEdge/Toolbars");

        /// <summary>
        /// Clicks apply button.
        /// </summary>
        /// <returns> The <see cref="EdgeCommonDocumentPage"/>. </returns>
        public EdgeCommonDocumentPage ClickApplyButton()
            => this.ClickElement<EdgeCommonDocumentPage>(DriverExtensions.GetElement(DialogContainerLocator), ApplyButtonLocator);

        /// <summary>
        /// Select Flag Option
        /// </summary>
        /// <param name="selected"> The selected. </param>
        /// <param name="option"> Option </param> 
        /// <returns> The <see cref="KeyCiteFlagsDialog"/>. </returns>
        public KeyCiteFlagsDialog SelectFlag(bool selected, KeyCiteFlagOption option)
        {
            DriverExtensions.SetCheckbox(selected, By.XPath(this.KeyCiteFlagsMap[option].LocatorString));
            return this;
        }

        /// <summary>
        ///  Gets list of options.
        /// </summary>
        /// <returns>List of KeyCiteFlag options</returns>
        public List<KeyCiteFlagOption> GetListOfOptions() =>
            DriverExtensions.GetElements(OptionsLocator).Select(
                                elem => elem.Text.GetEnumValueByText<KeyCiteFlagOption>(
                                    sourceFolder: this.sourceFolder))
                            .ToList();
        
        /// <summary>
        /// Verifies that apply button is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True is apply button is displayed. </returns>
        public bool IsApplyButtonDisplayed() => DriverExtensions.IsDisplayed(DialogContainerLocator, ApplyButtonLocator);

        /// <summary>
        /// Gets apply button name.
        /// </summary>
        /// <returns> The <see cref="string"/>. Apply button name. </returns>
        public string GetApplyButtonName() => DriverExtensions.GetText(DialogContainerLocator, ApplyButtonLocator);

        /// <summary>
        /// Verifies that the flag is selected.
        /// </summary>
        /// <param name="option"> The option. </param>
        /// <returns> The <see cref="bool"/>. True if flag is selected </returns>
        public bool IsFlagSelected(KeyCiteFlagOption option)
            => DriverExtensions.IsCheckboxSelected(By.XPath(this.KeyCiteFlagsMap[option].LocatorString));

        /// <summary>
        /// Verifies that the key cite crossed icon button is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if crossed icon is displayed. </returns>
        public bool IsKeyCiteCrossedIconButtonDisplayed() => DriverExtensions.IsDisplayed(KeyCiteCrossedIconLocator);
    }
}