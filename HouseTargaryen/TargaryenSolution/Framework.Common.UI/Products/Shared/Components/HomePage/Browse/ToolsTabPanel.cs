namespace Framework.Common.UI.Products.Shared.Components.HomePage.Browse
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Enums.HomePage;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Tools Tab panel
    /// </summary>
    public class ToolsTabPanel : BaseBrowseTabPanelComponent
    {
        private static readonly By ToolsElementLocator = By.XPath("//div[@id='co_browseWidgetTabPanel5' or @id ='panel6']//li or //li[@id='tab6']");

        private static readonly By ContainerLocator = By.Id("co_browseWidgetTabPanel6");

        private static readonly By FindAndPrintLocator = By.XPath("//span[contains(text(),'Find & Print')]");

        private EnumPropertyMapper<Tool, WebElementInfo> toolMap;

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Tools";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the Tool type enumeration to WebElementInfo map.
        /// </summary>
        private EnumPropertyMapper<Tool, WebElementInfo> ToolMap
            => this.toolMap = this.toolMap ?? EnumPropertyModelCache.GetMap<Tool, WebElementInfo>();

        /// <summary>
        /// Clicks one of the browse category
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="category">The category.</param>
        /// <returns>New instance of T page.</returns>
        public T ClickBrowseCategory<T>(Tool category) where T : ICreatablePageObject
            => this.ClickBrowseCategory<T>(this.ToolMap[category].Text);

        /// <summary>
        /// Get All Tools Items
        /// </summary>
        /// <returns> List of items </returns>
        public IList<ToolsTabItem> GetAllToolsItems()
            => DriverExtensions.GetElements(ToolsElementLocator).Select(element => new ToolsTabItem(element)).ToList();

        /// <summary>
        /// find and Print Link
        /// </summary>
        public ILink FindAndPrintLink => new Link(FindAndPrintLocator);
    }
}