namespace Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// BaseFacetComponent
    /// </summary>
    public abstract class BaseFacetComponent : BaseModuleRegressionComponent
    {
        private static readonly By FacetHeaderLocator = By.ClassName("co_facet_header");

        private IWebElement FacetHeaderElement => DriverExtensions.GetElement(this.ComponentLocator, FacetHeaderLocator);

        /// <summary>
        /// IsDisplayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public virtual bool IsHeaderDisplayed() => this.FacetHeaderElement.IsDisplayed();

        /// <summary>
        /// Get title text
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public virtual string GetHeaderText() => this.FacetHeaderElement.GetText();
    }
}