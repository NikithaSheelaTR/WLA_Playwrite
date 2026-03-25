namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// FavoritesFacetComponent
    /// </summary>
    public class FavoritesFacetComponent : BaseFacetCheckboxComponent
    {
        private const string CheckboxLctMask = ".//li[./label[contains(text(),'{0}')]]/input";

        private static readonly By ContainerLocator = By.Id("facet_div_MetaDataFavoritesFacet");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Apply the specified checkbox
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="checkbox"> checkbox to apply </param>
        /// <param name="setTo">setTo</param>
        /// <returns>The new instance of T page.</returns>
        public T SetCheckbox<T>(string checkbox, bool setTo = true) where T : ICreatablePageObject
            => this.SetCheckbox<T>(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(CheckboxLctMask, checkbox))), setTo);
    }
}