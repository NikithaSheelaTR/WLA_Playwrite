
namespace Framework.Common.UI.Products.Shared.Components.Alerts.NarrowByContent
{
    using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Products.WestLawNext.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Content Types Tab Panel
    /// </summary>
    public class ContentTypesTabComponent : BaseTabComponent
    {
        private const string CheckboxLctMask = "//ul[@id='content_type_list']/li/a[text()='{0}']";
        private static readonly By ContainerLocator = By.Id("keycite_alerts_lightbox");

        private EnumPropertyMapper<ContentType, ContentTypeInfo> contentTypeMap;

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Content Types";

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
        /// Select content type
        /// </summary>
        /// <param name="contentType">the content type to navigate to</param>
        /// <param name="isSet">True to check, false to uncheck</param>
        /// <returns> The <see cref="NarrowByContentDialog"/>. </returns>
        public NarrowByContentDialog SetCheckboxByContentName(ContentType contentType, bool isSet = true)
        {
            DriverExtensions.SetCheckbox(
                By.XPath(string.Format(CheckboxLctMask, this.ContentTypeMap[contentType].Text)),
                isSet);
            return new NarrowByContentDialog();
        }
    }
}
