namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Enums.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.DataModel;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// FormTypeFacetComponent
    /// </summary>
    public class FormTypeFacetComponent : BaseFacetCheckboxComponent
    {
        private const string CheckboxLctMask = ".//li[./label[contains(text(),'{0}')]]/input";

        private static readonly By ContainerLocator = By.Id("facet_div_formType");

        private EnumPropertyMapper<FormType, BaseTextModel> formTypeMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the FormType enumeration to BaseTextModel map.
        /// </summary>
        protected EnumPropertyMapper<FormType, BaseTextModel> FormTypeMap
            => this.formTypeMap = this.formTypeMap ?? EnumPropertyModelCache.GetMap<FormType, BaseTextModel>();

        /// <summary>
        /// Apply the specified checkbox
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="formType"> Form type to apply </param>
        /// <param name="setTo">setTo</param>
        /// <returns>The new instance of T page.</returns>
        public T SetCheckbox<T>(FormType formType, bool setTo = true) where T : ICreatablePageObject
            => this.SetCheckbox<T>(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(CheckboxLctMask, this.FormTypeMap[formType].Text))), setTo);

        /// <summary>
        /// Get count from reported checkbox
        /// </summary>
        /// <param name="formType">The form type.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetCheckboxCount(FormType formType)
            => this.GetCheckboxCount(string.Format(CheckboxLctMask, this.FormTypeMap[formType].Text));
    }
}