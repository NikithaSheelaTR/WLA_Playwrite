namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.Toolbar.FooterToolbar
{
    using Framework.Common.UI.Products.Shared.Components.Toolbar.FooterToolbar;
    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// Co-cites pagination component
    /// </summary>
    public class CoCitesPaginationFooterComponent : PaginationFooterComponent
    {
        private const string FooterPageNumberLinkLctMask = ".//a[contains(@aria-label,'Page {0}') or contains(@id,'page{0}')]";
        private static readonly By ContainerLocator = By.XPath(".//ul[@class = 'ToolbarPagination-list']");
        private static readonly By CurrentPageNumberLocator = By.XPath(".//a[contains(@class, 'ToolbarPagination-currentPage')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="CoCitesPaginationFooterComponent"/> class.
        /// </summary>
        /// <param name="footerToolbarContainer">footer toolbar container</param>
        public CoCitesPaginationFooterComponent(By footerToolbarContainer) =>
            this.ComponentLocator = new ByChained(footerToolbarContainer, ContainerLocator);
        
        /// <summary>
        /// Pagination map
        /// </summary>
        protected override EnumPropertyMapper<FooterPaginationOption, WebElementInfo> PaginationMap =>
            EnumPropertyModelCache.GetMap<FooterPaginationOption, WebElementInfo>(
                "CoCitesTab",
                @"Resources/EnumPropertyMaps/WestlawEdgePremium/Footer");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator { get; }

        /// <summary>
        /// Clicks the next, previous, last or first button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="option"> The option. </param>
        /// <returns> A new instance of the co-cites page</returns>
        public override T ClickPageButton<T>(FooterPaginationOption option)
        {
            DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.PaginationMap[option].LocatorString))
                            .Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks the next page button
        /// </summary>
        /// <param name="option"> The option. </param>
        /// <returns> A new instance of the search result page </returns>
        public override bool IsFooterOptionDisplayed(FooterPaginationOption option)
            =>
                DriverExtensions.IsDisplayed(
                    DriverExtensions.WaitForElement(this.ComponentLocator),
                    By.XPath(this.PaginationMap[option].LocatorString));

        /// <summary>
        /// The click page number link in footer.
        /// </summary>
        /// <param name="pageNumber">
        /// The page number.
        /// </param>
        public override void ClickPageNumberLinkInFooter(int pageNumber)
            => DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(FooterPageNumberLinkLctMask, pageNumber))).Click();

        /// <summary>
        /// Get current page number text
        /// </summary>
        /// <returns>Page number</returns>
        public override string GetCurrentPageNumberText() => DriverExtensions.GetText(this.ComponentLocator, CurrentPageNumberLocator);
    }
}