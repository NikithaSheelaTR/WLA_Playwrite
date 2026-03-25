namespace Framework.Common.UI.Products.Shared.Components.HomePage.Browse
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Practice Areas Tab Panel
    /// </summary>
    public class PracticeAreasTabPanel : BaseBrowseTabPanelComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class='Browse-widget']//div[contains(@class,'Tab-panel') and @aria-hidden='false']");

        private static readonly By PracticeAreasLinksLocator = By.XPath(".//li/a[@data-link-type='category-page']");

        private EnumPropertyMapper<PracticeArea, WebElementInfo> practiceAreaMap;

        /// <summary>
        /// PracticeAreasLinks
        /// </summary>
        public IReadOnlyCollection<ILink> PracticeAreasLinks =>
            new ElementsCollection<Link>(ContainerLocator, PracticeAreasLinksLocator);

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Practice areas";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the PracticeArea enumeration to WebElementInfo map.
        /// </summary>
        private EnumPropertyMapper<PracticeArea, WebElementInfo> PracticeAreaMap
            => this.practiceAreaMap = this.practiceAreaMap ?? EnumPropertyModelCache.GetMap<PracticeArea, WebElementInfo>();

        /// <summary>
        /// Clicks one of the browse category
        /// </summary>
        /// <typeparam name="T">The type of page that will be navigated to</typeparam>
        /// <param name="category">Category to click</param>
        /// <returns>A new instance of type T</returns>
        public T ClickBrowseCategory<T>(PracticeArea category) where T : ICreatablePageObject
            => this.ClickBrowseCategory<T>(this.PracticeAreaMap[category].Text);
    }
}