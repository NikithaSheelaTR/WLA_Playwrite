namespace Framework.Common.UI.Products.Shared.Components.Alerts.NewsEnterSearchTermComponents
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.Search;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Exclude Document Types Component from WestClip Alert Enter search term section
    /// </summary>
    public class ExcludeTheseDocumentTypesComponent : BaseAlertComponent
    {
        private static readonly By ExcludeTheseDocumentTypesComponentLocator =
            By.XPath("//div[@id='co_search_advancedSearch_ExcludeDocumentTypes']");

        /// <summary>
        /// Exclude Document Type Alert Map
        /// </summary>
        private static readonly EnumPropertyMapper<ExcludeDocumentTypeOptions, WebElementInfo> ExcludeDocumentTypeAlertMap =
                EnumPropertyModelCache.GetMap<ExcludeDocumentTypeOptions, WebElementInfo>("Alert");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ExcludeTheseDocumentTypesComponentLocator;

        /// <summary>
        /// Get Exclude Document Types Checkboxes State
        /// </summary>
        /// <param name="options"> The options. </param>
        /// <returns> Checkboxes states from the 'Exclude these document types' component </returns>
        public Dictionary<string, bool> GetCheckboxesState(params ExcludeDocumentTypeOptions[] options) =>
            options.ToDictionary(
                option => ExcludeDocumentTypeAlertMap[option].Text,
                option => DriverExtensions.IsCheckboxSelected(By.Id(ExcludeDocumentTypeAlertMap[option].Id)));

        /// <summary>
        /// Set Exclude Document Type Checkbox
        /// </summary>
        /// <param name="option"> The option. </param>
        /// <param name="state"> The state. </param>
        /// <returns> this component  </returns>
        public ExcludeTheseDocumentTypesComponent SetCheckbox(ExcludeDocumentTypeOptions option, bool state)
        {
            DriverExtensions.SetCheckbox(By.Id(ExcludeDocumentTypeAlertMap[option].Id), state);
            return this;
        }

        /// <summary>
        /// Verify that 'Exclude These Document Types' component is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, 5);
    }
}