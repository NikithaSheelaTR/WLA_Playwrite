namespace Framework.Common.UI.Products.Shared.Components.HomePage.Browse
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.WestLawNext.Models.EnumProperties;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// All Content Tab Panel
    /// </summary>
    public class AllContentTabPanel : BaseBrowseTabPanelComponent
    {
        private static readonly By ContainerLocator = By.Id("co_browseWidgetTabPanel1");

        private EnumPropertyMapper<ContentType, ContentTypeInfo> contentTypeMap;

        /// <summary>
        /// The tab name
        /// </summary>
        protected override string TabName => "All Content";

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
        /// Clicks the category page link for a specified content type
        /// </summary>
        /// <typeparam name="T">the type of the page to return</typeparam>
        /// <param name="contentType">the content type to navigate to</param>
        /// <returns>a browse page for the specified content type</returns>
        public T ClickBrowseCategory<T>(ContentType contentType) where T : ICreatablePageObject
            => this.ClickBrowseCategory<T>(this.ContentTypeMap[contentType].Text);

        /// <summary>
        /// Returns the category page name for a specified content type
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public string GetContentCategoryName(ContentType contentType) 
            => this.ContentTypeMap[contentType].Text;
    }
}