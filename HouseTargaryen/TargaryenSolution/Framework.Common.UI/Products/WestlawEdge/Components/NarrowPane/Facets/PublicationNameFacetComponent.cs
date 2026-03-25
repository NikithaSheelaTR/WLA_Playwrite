namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
    using Framework.Common.UI.Products.Shared.Dialogs.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Publication Name Facet Component
    /// </summary>
    public class PublicationNameFacetComponent : BaseSearchHierarchyFacetComponent
    {
        private static readonly By SelectPublicationNameLinkLocator = By.CssSelector(
            "#coid_publicationSelectTitle_publication, #coid_publicationSelectTitle_aunzFacetViewSetPublicationNameFacet, #coid_publicationSelectTitle_publicationName");

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicationNameFacetComponent"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public PublicationNameFacetComponent(By componentLocator)
            : base(componentLocator)
        {
        }

        /// <summary>
        /// Click on the 'Select Publication Name' link
        /// </summary>
        /// <returns> The <see cref="PublicationNameDialog"/>. </returns>
        public PublicationNameDialog SelectPublicationNameLinkClick()
        {
            this.ExpandFacet();
            DriverExtensions.Click(SelectPublicationNameLinkLocator);
            return new PublicationNameDialog();
        }
    }
}
