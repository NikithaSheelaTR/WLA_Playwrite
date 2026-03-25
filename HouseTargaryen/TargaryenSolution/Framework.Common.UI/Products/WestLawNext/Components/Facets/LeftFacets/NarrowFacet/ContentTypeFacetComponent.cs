namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.WestLawNext.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// ContentTypeFacetComponent
    /// </summary>
    public class ContentTypeFacetComponent : BaseFacetCheckboxComponent
    {
        private const string CheckboxLctMask = ".//li[./label[normalize-space(text())='{0}']]/input";

        private static readonly By ContainerLocator = By.XPath("//div[*/*/span[text()='Content Type']]");

        private static readonly By FirstFiveContentTypesItemsListLocator = By.XPath("//ul//li//label");

        private EnumPropertyMapper<ContentType, ContentTypeInfo> contentTypeMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<ContentType, ContentTypeInfo> ContentTypeMap
            => this.contentTypeMap = this.contentTypeMap ?? EnumPropertyModelCache.GetMap<ContentType, ContentTypeInfo>();

        /// <summary>
        /// Apply the specified checkbox
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="contentType"> contentType to apply </param>
        /// <param name="setTo">setTo</param>
        /// <returns>The new instance of T page.</returns>
        public T SetCheckbox<T>(ContentType contentType, bool setTo = true) where T : ICreatablePageObject
            => this.SetCheckbox<T>(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(CheckboxLctMask, this.ContentTypeMap[contentType].Text))), setTo);

        /// <summary>
        /// Get count from reported checkbox
        /// </summary>
        /// <param name="contentType">The contentType.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetCheckboxCount(ContentType contentType)
            => this.GetCheckboxCount(string.Format(CheckboxLctMask, this.ContentTypeMap[contentType].Text));

        /// <summary>
        /// Get the first five Content Types
        /// </summary>
        /// <returns> List of Content types </returns>
        public List<ContentType> GetTopContentTypeOptions()
            => DriverExtensions.GetElements(this.ComponentLocator, FirstFiveContentTypesItemsListLocator).Select(row => row.Text.GetEnumValueByText<ContentType>()).ToList();
    }
}