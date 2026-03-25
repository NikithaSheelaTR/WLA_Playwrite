namespace Framework.Common.UI.Products.WestLawNextCanada.Components.Facets
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Subject Area Facet
    /// </summary>
    public class SubjectAreaFacetComponent : BaseFacetCheckboxComponent
    {
        private const string CheckboxLctMask = "//label[contains(text(),'{0}')]/parent::li/input[@data-parent-id='subjectArea']";
        private static readonly By ContainerLocator = By.CssSelector("#facet_div_subjectArea");

        /// <summary>
        /// Component locatorsub
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
        
        /// <summary>
        /// Apply the specified checkbox
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="subjectArea">The Subject area.</param>
        /// <param name="setTo">setTo</param>
        /// <returns>The new instance of T page.</returns>
        public T SetCheckbox<T>(string subjectArea, bool setTo = true) where T : ICreatablePageObject
        {
            By checkboxLocator = By.XPath(string.Format(CheckboxLctMask, subjectArea));
            return this.SetCheckbox<T>(DriverExtensions.GetElement(this.ComponentLocator, checkboxLocator), setTo);
        }

        /// <summary>
        /// Get count from reported checkbox
        /// </summary>
        /// <param name="subjectArea">The Subject area.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public new int GetCheckboxCount(string subjectArea)
            => base.GetCheckboxCount(string.Format(CheckboxLctMask, subjectArea));
    }
}