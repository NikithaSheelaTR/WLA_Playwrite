namespace Framework.Common.UI.Products.WestlawEdge.Components.BrowseWidget
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Content;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// Instance of Content Types tab
    /// </summary>
    public class ContentTypesTabPanel : BaseTabComponent
    {
        private const string BrowseCategoryLinkLctMask = "//div[contains(@class, 'Tab-container')]//*[starts-with(text(), {0})]";

        private static readonly By QuickCheckLocator = By.ClassName("Access-docAnalyzer");

        private static readonly By StartPrecisionSearchButtonLocator = By.XPath("//*[@class='Browse-widget-accessPoints']//*[contains(@class, 'Button-primary')]");

        private static readonly By LearnMoreLinkLocator = By.XPath("//*[@class='Browse-widget-accessPoints']//*[contains(@class, 'co_linkBlue')]");

        private static readonly By ContainerLocator = By.Id("co_browseWidgetTab1");

        private EnumPropertyMapper<ContentTypeEdge, WebElementInfo> contentTypeMap;

        /// <summary>
        /// Precision Research button
        /// </summary>
        public IButton StartPrecisionSearchButton => new Button(StartPrecisionSearchButtonLocator);

        /// <summary>
        /// Learn more link
        /// </summary>
        public ILink LearnMoreLink => new Link(LearnMoreLinkLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Content types";

        /// <summary>
        /// ContentTypes Mapper
        /// </summary>
        protected EnumPropertyMapper<ContentTypeEdge, WebElementInfo> ContentTypeMap =>
            this.contentTypeMap = this.contentTypeMap ?? EnumPropertyModelCache.GetMap<ContentTypeEdge, WebElementInfo>(
                                      string.Empty,
                                      @"Resources/EnumPropertyMaps/WestlawEdge/Content");

        /// <summary>
        /// Clicks one of the browse category
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="category">The category.</param>
        /// <returns>New instance of T page.</returns>
        public T ClickBrowseCategory<T>(ContentTypeEdge category) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementDisplayed(By.LinkText(this.ContentTypeMap[category].Text)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click Quick Check Widget
        /// </summary>
        /// <typeparam name="T">page to return</typeparam>
        /// <returns>Upload page</returns>
        public T ClickQuickCheckWidget<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementDisplayed(QuickCheckLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks one of the browse category
        /// </summary>
        /// <typeparam name="T">The type of page that will be navigated to</typeparam>
        /// <param name="category">Category to click</param>
        /// <returns>A new instance of an object of type T</returns>
        public T ClickBrowseCategory<T>(string category) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementDisplayed(SafeXpath.BySafeXpath(BrowseCategoryLinkLctMask, category));
            DriverExtensions.ClickUsingJavaScript(SafeXpath.BySafeXpath(BrowseCategoryLinkLctMask, category));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// The is category present override
        /// </summary>
        /// <param name="category"> category </param>
        /// <returns> true if present </returns>
        public bool IsCategoryDisplayed(string category) => DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(BrowseCategoryLinkLctMask, category)).Displayed;
    }
}