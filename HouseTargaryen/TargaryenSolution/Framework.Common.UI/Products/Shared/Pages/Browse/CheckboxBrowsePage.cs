namespace Framework.Common.UI.Products.Shared.Pages.Browse
{
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.WestLawNext.Components.BrowsePage;
    using Framework.Common.UI.Products.WestLawNext.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// This class is the Search Module Regression's implementation of a Browse Page (Category Page) Object
    /// </summary>
    public class CheckboxBrowsePage : CommonBrowsePage
    {
        private const string ContentTypeCheckboxLctMask =
            "//div[@id='co_categoryPageCheckboxContainer']//label[text()='{0}']//preceding-sibling::input";

        private EnumPropertyMapper<ContentType, ContentTypeInfo> contentTypeMap;

        /// <summary>
        /// BrowsePageCheckboxComponent
        /// </summary>
        public BrowsePageCheckboxComponent CheckboxComponent { get; private set; } = new BrowsePageCheckboxComponent();

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<ContentType, ContentTypeInfo> ContentTypeMap
            => this.contentTypeMap = this.contentTypeMap ?? EnumPropertyModelCache.GetMap<ContentType, ContentTypeInfo>();

        /// <summary>
        /// Checks the checkbox for a specific content type
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="contentType"> the content type to select the checkbox for </param>
        public void SelectContentType<T>(ContentType contentType) where T : CheckboxBrowsePage
        {
            DriverExtensions.Click(DriverExtensions.WaitForElementDisplayed(
                    By.XPath(string.Format(ContentTypeCheckboxLctMask, this.ContentTypeMap[contentType].Text))));
            DriverExtensions.WaitForJavaScript();
        }
    }
}