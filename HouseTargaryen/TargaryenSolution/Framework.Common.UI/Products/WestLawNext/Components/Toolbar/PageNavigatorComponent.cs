namespace Framework.Common.UI.Products.WestLawNext.Components.Toolbar
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;

    /// <summary>
    /// Page Navigator Component in Toolbar
    /// </summary>
    public class PageNavigatorComponent : BaseModuleRegressionComponent
    {
        private static readonly By NextPageButtonLocator =
            By.XPath("//li[@id='co_paginationListItem_id']/a[@class='co_next'] | //a[@id='co_search_header_pagination_next'] | //*[contains(@class, 'co_next')] | //button[@id='co_search_header_pagination_next']");

        private static readonly By PageRangeLocator =
            By.XPath("//li[@id='co_paginationListItem_id']//strong | //ol/li/span | //div[contains(@id, 'pagination-range-info-header')] | //nav[@class='co_navPages']//span");

        private static readonly By PreviousPageButtonLocator =
            By.XPath("//li[@id='co_paginationListItem_id']/a[@class='co_prev'] | //a[@id='co_search_header_pagination_prev'] | //*[contains(@class,'co_prev')]");

        private static readonly By ContainerLocator = By.ClassName("co_navPages");

        /// <summary>
        /// Next button
        /// </summary>
        public IButton NextPageButton => new Button(NextPageButtonLocator);

        /// <summary>
        /// Previous button
        /// </summary>
        public IButton PreviousPageButton => new Button(PreviousPageButtonLocator);

        /// <summary>
        /// PageRange label
        /// </summary>
        public ILabel PageRangeLabel => new Label(PageRangeLocator);

        /// <summary>
        /// Component locator
        /// </summary>s
        protected override By ComponentLocator => ContainerLocator;
    }
}