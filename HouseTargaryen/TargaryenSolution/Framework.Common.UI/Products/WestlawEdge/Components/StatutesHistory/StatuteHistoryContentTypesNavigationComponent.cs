namespace Framework.Common.UI.Products.WestlawEdge.Components.StatutesHistory
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Content;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Statutes History Content Types Navigation Component
    /// </summary>
    public class StatuteHistoryContentTypesNavigationComponent : BaseModuleRegressionComponent
    {
        private const string ContentTypeLctMask = "//li[@id='{0}']/a";
        private const string FacetItemLctMask = "//div[@id='co_contentTypeLinksBox']//li[./*[contains(text(),\"{0}\")]]";

        private static readonly By ContainerLocator = By.Id("co_contentTypeLinksBox");

        private EnumPropertyMapper<StatuteHistoryContentTypes, WebElementInfo> contentTypeMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map
        /// </summary>
        protected EnumPropertyMapper<StatuteHistoryContentTypes, WebElementInfo> ContentTypeMap =>
            this.contentTypeMap = this.contentTypeMap
                                  ?? EnumPropertyModelCache.GetMap<StatuteHistoryContentTypes, WebElementInfo>(
                                      string.Empty,
                                      @"Resources/EnumPropertyMaps/WestlawEdge/Content");

        /// <summary>
        /// Returns Content Type Header text
        /// </summary>
        /// <param name="contentType">Statute history content type</param>
        /// <returns>Content type header</returns>
        public string GetContentTypeHeader(StatuteHistoryContentTypes contentType) => DriverExtensions
            .GetText(By.XPath(string.Format(ContentTypeLctMask, this.ContentTypeMap[contentType].Id))).Trim();

        /// <summary>
        /// Determine if a facet element is selected by the given facet name.
        /// </summary>
        /// <param name="contentType"> Name displayed for the facet </param>
        /// <returns> True if the facet is selected, false otherwise </returns> 
        public bool IsContentTypeSelected(StatuteHistoryContentTypes contentType)
            => DriverExtensions.WaitForElement(By.Id(this.ContentTypeMap[contentType].Id)).GetAttribute("aria-selected").Contains("true");

        /// <summary>
        /// Click Content Type Link by Content Type
        /// </summary>
        /// <typeparam name="T"> Page Type </typeparam>
        /// <param name="contentType"> Content type </param>
        /// <returns> New instance of the page </returns>
        public T ClickContentTypeLink<T>(StatuteHistoryContentTypes contentType)
            where T : ICreatablePageObject => this.ClickContentTypeLink<T>(this.ContentTypeMap[contentType].Text);

        /// <summary>
        /// Click Content Type Link by link text
        /// </summary>
        /// <param name="contentType"> The content Type. </param>
        /// <typeparam name="T"> Page Type  </typeparam>
        /// <returns> New instance of the page  </returns>
        public T ClickContentTypeLink<T>(string contentType)
            where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(FacetItemLctMask, contentType))).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}