namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// OfficialHeadnoteFacet
    /// </summary>
    public class OfficialHeadnoteFacetComponent : BaseFacetCheckboxComponent
    {
        private static readonly By ContainerLocator = By.Id("coid_relatedInfo_facet_OfficialHeadnoteAssignment_list");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Verify that the checkbox is selected
        /// </summary>
        /// <param name="checkbox">The checkbox.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsOfficialHeadnoteCheckboxChecked(string checkbox)
            => this.IsCheckboxSelected(DriverExtensions.GetElement(this.ComponentLocator, By.LinkText(checkbox)));
    }
}