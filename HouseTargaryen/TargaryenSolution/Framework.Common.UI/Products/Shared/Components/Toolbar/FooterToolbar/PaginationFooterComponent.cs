namespace Framework.Common.UI.Products.Shared.Components.Toolbar.FooterToolbar
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Pagination Footer component
    /// </summary>
    public class PaginationFooterComponent : BaseModuleRegressionComponent
    {
        private const string FooterPageNumberLinkLctMask = "//a[contains(@aria-label,'Page {0}') or contains(@id,'page{0}')]";
        private static readonly By CurrentPageNumberLocator = By.XPath(".//span[@id='co_search_footer_pagination_current']");
        private static readonly By FooterComponentLocator = By.XPath("//ul[contains(@class,'co_navFooter')]");
        private static readonly By ContainerLocator = By.ClassName("co_navFooter_pagination");

        private EnumPropertyMapper<FooterPaginationOption, WebElementInfo> paginationMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// PaginationMap
        /// </summary>
        protected virtual EnumPropertyMapper<FooterPaginationOption, WebElementInfo> PaginationMap =>
            this.paginationMap =
                this.paginationMap ?? EnumPropertyModelCache.GetMap<FooterPaginationOption, WebElementInfo>();

        /// <summary>
        /// Clicks the next page button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="option"> The option. </param>
        /// <returns> A new instance of the search result page </returns>
        public virtual T ClickPageButton<T>(FooterPaginationOption option) where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(
                DriverExtensions.WaitForElement(FooterComponentLocator),
                By.ClassName(this.PaginationMap[option].ClassName)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks the next page button
        /// </summary>
        /// <param name="option"> The option. </param>
        /// <returns> A new instance of the search result page </returns>
        public virtual bool IsFooterOptionDisplayed(FooterPaginationOption option)
            =>
                DriverExtensions.IsDisplayed(
                    DriverExtensions.WaitForElement(FooterComponentLocator),
                    By.ClassName(this.PaginationMap[option].ClassName));

        /// <summary>
        /// The click page number link in footer.
        /// </summary>
        /// <param name="pageNumber">
        /// The page number.
        /// </param>
        public virtual void ClickPageNumberLinkInFooter(int pageNumber)
            => DriverExtensions.Click(By.XPath(string.Format(FooterPageNumberLinkLctMask, pageNumber)));

        /// <summary>
        /// Get current page number text
        /// </summary>
        /// <returns>Page number</returns>
        public virtual string GetCurrentPageNumberText() =>
            DriverExtensions.GetText(CurrentPageNumberLocator);

        /// <summary>
        /// Verify that component is displayed
        /// </summary>
        /// <returns> True if component is displayed, false otherwise </returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, 10);
    }
}