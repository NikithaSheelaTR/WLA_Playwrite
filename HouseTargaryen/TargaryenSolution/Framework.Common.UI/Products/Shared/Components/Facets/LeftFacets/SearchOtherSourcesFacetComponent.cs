namespace Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Search Other Sources Links
    /// </summary>
    public sealed class SearchOtherSourcesFacetComponent : BaseFacetComponent
    {
        private static readonly By ContainerLocator = By.CssSelector("div#co_searchResults_externalLinks");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click Search Other Sources Link By Text
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="linkText"> Link text </param>
        /// <returns> New instance of the page </returns>
        public override T ClickLinkByText<T>(string linkText)
        {
            DriverExtensions.GetElements(this.ComponentLocator, By.TagName("a")).First(el => el.Text.Equals(linkText)).CustomClick();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}