namespace Framework.Common.UI.Products.WestLawNext.Components.AdvancedSearchTemplate
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Enums.Search;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Exclude Document Types Field Box Component
    /// </summary>
    public class ExcludeDocumentTypesFieldBoxComponent : BaseModuleRegressionComponent
    {
        private const string AreaLctMask = "//label[text()={0}]";

        private static readonly By ContainerLocator = By.Id("co_search_advancedSearchDocumentFieldsBox_0");

        /// <summary>
        /// Exclude Document Type Alert Map
        /// </summary>
        private static readonly EnumPropertyMapper<ExcludeDocumentTypeOptions, WebElementInfo> ExcludeDocumentTypeAlertMap =
                EnumPropertyModelCache.GetMap<ExcludeDocumentTypeOptions, WebElementInfo>();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Is exclude document type displayed
        /// </summary>
        /// <param name="option"> option  </param>
        /// <returns>True if option is displayed</returns>
        public bool IsExcludedDocumentTypeOptionDisplayed(ExcludeDocumentTypeOptions option)
            => DriverExtensions.GetElement(By.Id(ExcludeDocumentTypeAlertMap[option].Id)).Displayed;

        /// <summary>
        /// Verifies that title of checkbox is displayed
        /// </summary>
        /// <param name="option"> The checkbox Option. </param>
        /// <returns>True if title is present </returns>
        public bool IsExcludedDocumentTypeTitleDisplayed(ExcludeDocumentTypeOptions option)
            => DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(AreaLctMask, ExcludeDocumentTypeAlertMap[option].Text)).Displayed;

        /// <summary>
        /// Set Exclude Document Type Checkbox
        /// </summary>
        /// <param name="option"> The option. </param>
        /// <param name="state"> The state. </param>
        /// <returns> this component  </returns>
        public ExcludeDocumentTypesFieldBoxComponent SetCheckbox(ExcludeDocumentTypeOptions option, bool state)
        {
            DriverExtensions.SetCheckbox(By.Id(ExcludeDocumentTypeAlertMap[option].Id), state);
            return this;
        }
    }
}