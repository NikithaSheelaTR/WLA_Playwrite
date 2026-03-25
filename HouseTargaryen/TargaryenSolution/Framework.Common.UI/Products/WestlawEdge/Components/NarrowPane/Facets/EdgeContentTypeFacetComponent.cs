namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.WestLawNext.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// ContentType Facet
    /// </summary>
    public class EdgeContentTypeFacetComponent : BaseSearchHierarchyFacetComponent
    {
        private const string ContentTypeCheckboxLctMask = "//span[label/span[text()='{0}']]/input";

        private const string ContentTypeFacetCountLctMask =
            "//div[contains(@class,'SearchFacet-listItem')]/div[.//span[text()='{0}']]//span[@class = 'SearchFacet-outputTextValue']";

        private static readonly By ContentTypeListLocator = By.XPath("//span[@class = 'SearchFacet-labelText']");

        private EnumPropertyMapper<ContentType, ContentTypeInfo> contentTypeMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeContentTypeFacetComponent"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public EdgeContentTypeFacetComponent(By componentLocator) : base(componentLocator)
        {
        }

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<ContentType, ContentTypeInfo> ContentTypeMap
            =>
                this.contentTypeMap =
                    this.contentTypeMap ?? EnumPropertyModelCache.GetMap<ContentType, ContentTypeInfo>();

        /// <summary>
        /// Select Content type
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="contentType"> Facet content type to select </param>
        /// <returns> New instance of the page</returns>
        public T SelectContentType<T>(ContentType contentType) where T : ICreatablePageObject
        {
            this.ExpandFacet();
            DriverExtensions.WaitForElement(By.XPath(string.Format(ContentTypeCheckboxLctMask, this.ContentTypeMap[contentType].Text))).Click();

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get Content Type Facet count
        /// </summary>
        /// <param name="contentTypeFacet"> Facet Content Type </param>
        /// <returns> Count for Content Type Facet </returns>
        public int GetContentTypeFacetCount(ContentType contentTypeFacet)
        {
            this.ExpandFacet();

            return
                DriverExtensions.WaitForElement(
                    By.XPath(string.Format(ContentTypeFacetCountLctMask, this.ContentTypeMap[contentTypeFacet].Text)))
                                .Text.ConvertCountToInt();
        }

        /// <summary>
        /// The get content type facet options.
        /// </summary>
        /// <returns> List of Content types </returns>
        public List<ContentType> GetContentTypeFacetOptions()
            => DriverExtensions.GetElements(ContentTypeListLocator).Select(row => row.Text.GetEnumValueByText<ContentType>()).ToList();
    }
}