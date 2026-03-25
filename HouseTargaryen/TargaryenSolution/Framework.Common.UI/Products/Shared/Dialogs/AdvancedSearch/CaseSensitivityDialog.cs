namespace Framework.Common.UI.Products.Shared.Dialogs.AdvancedSearch
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Enums.Search;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.CommonTypes.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Case-Sensitivity dialog
    /// </summary>
    public class CaseSensitivityDialog : BaseModuleRegressionDialog
    {
        private const string CaseSensitivityOptionsLctMask = "//input[@value={0}]/../../div[@class='co_caseSensitive_value']//option";

        private static readonly By CaseSensitivityDialogDescriptionLocator = By.ClassName("co_caseSensitive_description");

        private static readonly By OkButtonLocator = By.Id("co_advancedSearch_caseSensitiveSave");

        private static readonly By CloseButtonLocator = By.Id("co_searchWidget_advancedSearch_caseSensitive_popupclose");

        /// <summary>
        /// Case sensivity dialog description label
        /// </summary>
        public ILabel CaseSensitivityDialogDescriptionLabel => new Label(CaseSensitivityDialogDescriptionLocator);

        /// <summary>
        /// Ok button
        /// </summary>
        public IButton OkButton => new Button(OkButtonLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(CloseButtonLocator);

        /// <summary>
        /// Get case sensitivity options for query
        /// </summary>
        /// <param name="query"> The query. </param>
        /// <returns> The CaseSensitivityOptions list/ </returns>
        public List<CaseSensitivityOptions> GetCaseSensitivityOptionsForQuery(string query) =>
            DriverExtensions.GetElements(SafeXpath.BySafeXpath(CaseSensitivityOptionsLctMask, query))
            .Select(elem => elem.Text.Trim().GetEnumValueByText<CaseSensitivityOptions>()).ToList();

        /// <summary>
        /// Select case sensitivity option
        /// </summary>
        /// <param name="query"> The query. </param>
        /// <param name="option"> The option. </param>
        public void SelectCaseSensitivityOption(string query, CaseSensitivityOptions option) =>
            DriverExtensions.GetElements(SafeXpath.BySafeXpath(CaseSensitivityOptionsLctMask, query))
            .FirstOrDefault(elem => elem.Text.Trim().GetEnumValueByText<CaseSensitivityOptions>().Equals(option)).Click();
    }
}