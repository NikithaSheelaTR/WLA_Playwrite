namespace Framework.Common.UI.Products.WestLawNextMobile.Pages
{
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.WestLawNext.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Browse content page
    /// </summary>
    public class BrowseContentPage : MobileBasePageWithHeader
    {
        private const string ContentTypeLctMask = "//a[@class='docLink' and text()={0}]";

        private static readonly By ContentLocator = By.ClassName("docLink");

        private static readonly By PageHeaderLocator = By.XPath("//div[@class='hdr noBrd']");

        private EnumPropertyMapper<ContentType, ContentTypeInfo> contentTypeMap;

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<ContentType, ContentTypeInfo> ContentTypeMap
            => this.contentTypeMap = this.contentTypeMap ?? EnumPropertyModelCache.GetMap<ContentType, ContentTypeInfo>();

        /// <summary>
        /// Clicks the links for the specified Content type
        /// </summary>
        /// <typeparam name="T">The type of the page-object to return</typeparam>
        /// <param name="contentType">Content type  as a enum</param>
        /// <returns>The page-object of the next page</returns>
        public T ClickContentType<T>(ContentType contentType) where T : MobileBasePage
            => this.ClickBySearchResult<T>(this.ContentTypeMap[contentType].Text);

        /// <summary>
        /// Click by search result
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="linkToOpen"> The link To Open. </param>
        /// <returns> New instance of the page </returns>
        public T ClickBySearchResult<T>(string linkToOpen) where T : MobileBasePage
        {
            DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(ContentTypeLctMask, linkToOpen)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Verify content is displayed on the page
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsContentDisplayed() => DriverExtensions.IsDisplayed(ContentLocator);

        /// <summary>
        /// Get text from page header
        /// </summary>
        /// <returns> Page header text </returns>
        public string GetPageHeaderText() => DriverExtensions.WaitForElement(PageHeaderLocator).Text;
    }
}