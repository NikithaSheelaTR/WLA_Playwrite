namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Subsections Facet Component
    /// </summary>
    public class SubsectionsFacetComponent : BaseFacetCheckboxComponent
    {
        private const string CheckboxLctMask =
            "//a[@role='checkbox' and contains(@id,'coid_relatedInfo_Subsection_') and contains(text(),'{0}')]";

        private static readonly By ExpandButtonLocator = By.XPath(".//a[@class='co_relatedInfo_Subsection_facet co_facet_expand']");

        private static readonly By ContainerLocator = By.Id("facet_div_Subsection");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get count from reported checkbox
        /// </summary>
        /// <param name="checkbox">The locator.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public new int GetCheckboxCount(string checkbox)
        {
            this.ExpandParentFacet(By.XPath(string.Format(CheckboxLctMask, checkbox)), ExpandButtonLocator);
            return base.GetCheckboxCount(string.Format(CheckboxLctMask, checkbox));
        }

        /// <summary>
        /// Apply the specified checkbox
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="checkbox">The checkbox.</param>
        /// <param name="setTo">setTo</param>
        /// <returns>The new instance of T page.</returns>
        public T SetCheckbox<T>(string checkbox, bool setTo = true) where T : ICreatablePageObject
        {
            By checkboxLocator = By.XPath(string.Format(CheckboxLctMask, checkbox));
            this.ExpandParentFacet(checkboxLocator, ExpandButtonLocator);
            return this.SetCheckbox<T>(DriverExtensions.WaitForElement(DriverExtensions.WaitForElement(this.ComponentLocator), checkboxLocator), setTo);
        }

        /// <summary>
        /// Verify that 'Subsections' facet is enabled.
        /// </summary>
        /// <returns> True if 'Subsections' facet is enabled, false otherwise </returns>
        public bool IsSubsectionsFacetEnabled() => !DriverExtensions.GetAttribute("class", this.ComponentLocator).Contains("co_disabled");
    }
}