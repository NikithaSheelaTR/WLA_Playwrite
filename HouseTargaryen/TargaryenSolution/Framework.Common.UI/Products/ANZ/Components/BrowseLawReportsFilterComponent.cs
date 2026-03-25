namespace Framework.Common.UI.Products.ANZ.Components
{
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using OpenQA.Selenium;

    /// <summary>
    /// Browse Law Reports Filter component
    /// </summary>
    public class BrowseLawReportsFilterComponent : EdgeSearchFacetsFilterComponent
    {
        private static readonly By PublicationNameFacetLocator = By.XPath("//*[@id='co_facetHeaderpublicationVolume']");

        private static readonly By VolumeIssueFacetLocator = By.XPath("//*[@id='facet_div_publicationVolumeIssue']");

        /// <summary>
        /// Publication Volume Facet
        /// </summary>
        public PublicationVolumeFacetComponent PublicationVolumeFacet => new PublicationVolumeFacetComponent(PublicationNameFacetLocator);

        /// <summary>
        /// Volume/Issue Facet
        /// </summary>
        public AnzBaseFacetWithAppearingDialogComponent VolumeIssueFacet => new AnzBaseFacetWithAppearingDialogComponent(VolumeIssueFacetLocator);
    }
}
