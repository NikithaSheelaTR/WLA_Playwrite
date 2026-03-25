namespace Framework.Common.UI.Products.WestLawNextCanada.Components.BrowseWidget
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Components.BrowseWidget;
    using Framework.Common.UI.Products.WestLawNextCanada.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Instance of Canada Content Types tab
    /// </summary>
    public class CanadaContentTypesTabPanel : ContentTypesTabPanel
    {
        private EnumPropertyMapper<CanadaContentType, WebElementInfo> canadaContentTypeMap;

        /// <summary>
        /// ContentTypes Mapper
        /// </summary>
        protected EnumPropertyMapper<CanadaContentType, WebElementInfo> CanadaContentTypeMap =>
            this.canadaContentTypeMap = this.canadaContentTypeMap ?? EnumPropertyModelCache.GetMap<CanadaContentType, WebElementInfo>(
                                            string.Empty,
                                            @"Resources/EnumPropertyMaps/WestlawNextCanada");

        /// <summary>
        /// Clicks one of the browse category
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="category">The category.</param>
        /// <returns>New instance of T page.</returns>
        public T ClickBrowseCategory<T>(CanadaContentType category) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementDisplayed(By.LinkText(this.CanadaContentTypeMap[category].Text)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
