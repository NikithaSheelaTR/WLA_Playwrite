namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.SearchResultsPageFacets
{
    using Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// ViewFacetComponent for the Search Results Pages
    /// </summary>
    public class ViewFacetComponent : BaseViewFacetComponent
    {
        private static readonly By HeaderLocator = By.XPath("//*[@id='co_website_searchFacets']/div/h3 | //*[@id='co_website_searchFacets']/div/h2");

        private static readonly By ContainerLocator = By.XPath("//div[@id='co_website_searchFacets']/div[@class='co_genericBox'][1]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get Header Text
        /// </summary>
        /// <returns>header text</returns>
        public string GetHeaderText() => DriverExtensions.GetText(HeaderLocator);

        /// <summary>
        /// Verify View label is  present
        /// </summary>
        /// <returns>true if Delivery widget present, false otherwise</returns>
        public bool IsHeaderDisplayed() => DriverExtensions.IsDisplayed(HeaderLocator, 5);
    }
}