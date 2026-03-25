namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
    using Framework.Common.UI.Products.Shared.Dialogs.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Legislation Title Facet Component
    /// </summary>
    public class LegislationTitleFacetComponent : BaseSearchHierarchyFacetComponent
    {
        private static readonly By SelectLegislationTitleLinkLocator = By.XPath("//*[@id='facet_div_Title']");

        /// <summary>
        /// Initializes a new instance of the <see cref="LegislationTitleFacetComponent"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public LegislationTitleFacetComponent(By componentLocator)
            : base(componentLocator)
        {
        }

        /// <summary>
        /// Click on the 'Select Legislation Title' link
        /// </summary>
        /// <returns> The <see cref="LegislationTitleDialog"/>. </returns>
        public LegislationTitleDialog SelectLegislationTitleLinkClick()
        {
            this.ExpandFacet();
            DriverExtensions.Click(SelectLegislationTitleLinkLocator);
            return new LegislationTitleDialog();
        }
    }
}