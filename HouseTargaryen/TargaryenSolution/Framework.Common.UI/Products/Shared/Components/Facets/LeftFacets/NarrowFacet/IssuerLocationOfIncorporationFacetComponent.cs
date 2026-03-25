namespace Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Issuer Location of Incorporation on BLC Agreements page
    /// </summary>
    public class IssuerLocationOfIncorporationFacetComponent : BaseFacetCheckboxComponent
    {
        private const string CheckboxLctMask = ".//li[./*[text()='{0}']]/input";

        private static readonly By ExpandButtonLocator
            = By.XPath(".//a[@role='button' and @class='co_facet_expand' and (contains(@childlistid,'facet_expandable_agreementsIssuerLocation'))]");

        private static readonly By ContainerLocator = By.Id("facet_div_agreementsIssuerLocationOfIncorporation");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

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
            return this.SetCheckbox<T>(DriverExtensions.WaitForElement(DriverExtensions.GetElement(this.ComponentLocator), checkboxLocator), setTo);
        }
    }
}