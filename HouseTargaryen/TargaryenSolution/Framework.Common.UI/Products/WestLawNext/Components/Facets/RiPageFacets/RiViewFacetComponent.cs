namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.RiPageFacets
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// ViewFacetComponent for the Related Info pages
    /// </summary>
    public class RiViewFacetComponent : BaseViewFacetComponent
    {
        private const string ChildContentTypeLctMask
            = "//div[@id='co_contentTypeLinksBox']//li[./div[@class='co_indent'] and .//*[contains(text(),'{0}')]]";

        private static readonly By ContainerLocator = By.Id("co_viewResultsBy");

        private static readonly By ChildFacetCountLocator = By.XPath("//div[@id='co_contentTypeLinksBox']//li[./div[@class='co_indent']]/span");

        private static readonly By ChildFacetLocator = By.XPath("//div[@id='co_contentTypeLinksBox']//*[contains(@id, 'coid_relatedinfo_DocumentType')] | //div[@class='co_indent']//a");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Return a list of child facet counts in the order in which they are displayed.
        /// </summary>
        /// <returns> List of counts </returns>//stay
        public List<int> GetChildFacetListCount() => DriverExtensions.GetElements(ChildFacetCountLocator).Select(e => e.Text.ConvertCountToInt()).ToList();

        /// <summary>
        /// Return a list of elements for each child facet in the body of the page in the order in which they are displayed.
        /// </summary>
        /// <returns> List of child facet elements </returns>
        public List<string> GetChildFacetsNames()
        {
            DriverExtensions.WaitForElement(ChildFacetLocator);
            return DriverExtensions.GetElements(ChildFacetLocator).Select(facet => facet.Text).ToList();
        }

        /// <summary>
        /// Click Content Type Link by link text - View Pane
        /// </summary>
        /// <param name="contentType"> The content Type. </param>
        /// <typeparam name="T"> Page Type  </typeparam>
        /// <returns> New instance of the page  </returns>
        public T ClickChildContentTypeLink<T>(string contentType) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(ChildContentTypeLctMask, contentType))).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get the number of results for a given content type
        /// </summary>
        /// <param name="contentType"> Content type </param>
        /// <returns> Number of results </returns>
        public int GetChildContentTypeCount(string contentType)
            => DriverExtensions.GetElement(DriverExtensions.WaitForElement(By.XPath(string.Format(ChildContentTypeLctMask, contentType))), By.TagName("span")).Text.ConvertCountToInt();
    }
}