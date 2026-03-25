namespace Framework.Common.UI.Products.WestLawNextCanada.Components
{
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Components;
    using Framework.Common.UI.Products.WestLawNextCanada.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// Canada Edge Header component
    /// </summary>
    public class CanadaEdgeHeaderComponent : EdgeHeaderComponent
    {
        private static readonly By ExpandedTabLocator = By.XPath(".//ancestor::div[@class='co_dropdownTabExpanded']");
        private static readonly By InfoMessageLocator = By.XPath("//div[@class='co_infoBox_inner']/div[@class='co_infoBox_message' and not(./a)]");
        private static readonly By SearchFindByLocator = By.Id("queryBuilderLinkId");
        private static readonly By AllContentSearchTabLocator = By.CssSelector("#co_searchWidgetAllWestlawTab button");
        private static readonly By StartPageLinkLocator = By.Id("crsw_startPageLink");

        private EnumPropertyMapper<CanadaEdgeHeaderTabs, WebElementInfo> edgeHeaderTabsMap;

        /// <summary>
        /// Search Find By button
        /// </summary>
        public IButton SearchFindByButton => new Button(SearchFindByLocator);

        /// <summary>
        /// All Content Search tab
        /// </summary>
        public IButton AllContentSearchTab => new Button(AllContentSearchTabLocator);

        /// <summary>
        /// Start Page link
        /// </summary>
        public ILink StartPageLink => new Link(StartPageLinkLocator);

        /// <summary>
        /// Gets the Edge Header tabs enumeration to WebElementInfo map.
        /// </summary>
        private EnumPropertyMapper<CanadaEdgeHeaderTabs, WebElementInfo> EdgeHeaderTabsMap =>
            this.edgeHeaderTabsMap = this.edgeHeaderTabsMap
                                     ?? EnumPropertyModelCache.GetMap<CanadaEdgeHeaderTabs, WebElementInfo>(
                                         string.Empty,
                                         @"Resources/EnumPropertyMaps/WestlawNextCanada");

        /// <summary>
        /// Clicks Westlaw Edge header tab
        /// </summary>
        /// <typeparam name="T">Page returned from clicking the button</typeparam>
        /// <param name="tab">Indigo header tab</param>
        /// <returns>New instance of T</returns>
        public T ClickHeaderTab<T>(CanadaEdgeHeaderTabs tab) where T : ICreatablePageObject
        {
            if (!this.IsHeaderTabExpanded(tab))
            {
                DriverExtensions.Click(DriverExtensions.WaitForElement(By.XPath(this.EdgeHeaderTabsMap[tab].LocatorString)));
            }

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Verify is Westlaw Edge header tab displayed in the page
        /// </summary>
        /// <param name="tab">tab to verify</param>
        /// <returns>trye if displayed</returns>
        public bool IsHeaderTabDisplayed(CanadaEdgeHeaderTabs tab) =>
            DriverExtensions.IsDisplayed(By.XPath(this.EdgeHeaderTabsMap[tab].LocatorString));

        /// <summary>
        /// Verify is Westlaw Edge header tab is expanded
        /// </summary>
        /// <param name="tab">tab to verify</param>
        /// <returns>true if displayed</returns>
        public bool IsHeaderTabExpanded(CanadaEdgeHeaderTabs tab) =>
            DriverExtensions.IsDisplayed(By.XPath(this.EdgeHeaderTabsMap[tab].LocatorString), ExpandedTabLocator);

        /// <summary>
        /// Get the info text message
        /// </summary>
        /// <returns>Text of the info message</returns>
        public override string GetInfoMessage()
        {
            DriverExtensions.WaitForElement(InfoMessageLocator);
            IWebElement messageElement = DriverExtensions.GetElements(InfoMessageLocator).FirstOrDefault(el => el.Displayed);
            return messageElement.GetText();
        }
    }
}