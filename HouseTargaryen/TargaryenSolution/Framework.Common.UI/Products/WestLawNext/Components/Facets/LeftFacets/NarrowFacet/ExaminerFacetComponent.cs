
namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using OpenQA.Selenium;

    /// <summary>
    /// Examiner Facet Component
    /// </summary>
    public class ExaminerFacetComponent: BaseLinkFacetComponent
    {
        private static readonly By ContainerLocator = By.Id("facet_div_patentsExaminer");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
