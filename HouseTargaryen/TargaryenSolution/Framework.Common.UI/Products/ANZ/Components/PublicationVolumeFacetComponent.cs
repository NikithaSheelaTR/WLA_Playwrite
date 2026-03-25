namespace Framework.Common.UI.Products.ANZ.Components
{
    using Framework.Common.UI.Products.ANZ.Dialogs;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Publication Volume Facet Component
    /// </summary>
    public class PublicationVolumeFacetComponent : BaseSearchHierarchyFacetComponent
    {
        private const string FilteredOptionLctMask = "//div[contains(@class,'SearchFacet-body')]//li/input[@data-attribute-id='{0}']";

        private static readonly By SelectPublicationVolumeLinkLocator = By.XPath(
                "//*[@id='co_facetHeaderpublicationVolume']//*[text()='Publication Volume']");

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicationVolumeFacetComponent"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public PublicationVolumeFacetComponent(By componentLocator)
            : base(componentLocator)
        {
        }

        /// <summary>
        /// Click on 'Publication Volume' facet link
        /// </summary>
        /// <returns> The <see cref="PublicationVolumeDialog"/>. </returns>
        public PublicationVolumeDialog SelectPublicationVolumeLinkClick()
        {
            DriverExtensions.GetElement(SelectPublicationVolumeLinkLocator).Click();
            return new PublicationVolumeDialog();
        }

        /// <summary>
        /// Is Checkbox Selected
        /// </summary>
        /// <param name="itemName"> Item name</param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsFilteredPublicationVolumeOptionCheckboxSelected(string itemName)
        {
            return DriverExtensions.GetElement(By.XPath(string.Format(FilteredOptionLctMask, itemName))).Selected;            
        }
    }
}
