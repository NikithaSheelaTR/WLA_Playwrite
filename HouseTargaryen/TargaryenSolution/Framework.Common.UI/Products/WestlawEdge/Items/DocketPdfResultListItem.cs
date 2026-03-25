namespace Framework.Common.UI.Products.WestlawEdge.Items
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Products.WestlawEdge.Components.ResultList;

    using OpenQA.Selenium;

    /// <summary>
    /// Docket PDFs item
    /// </summary>
    public class DocketPdfResultListItem : DocketsResultListItem
    {
        private static readonly By DocketPdfLinkLocator = By.XPath(".//span[@class = 'SPDF-titleWrapper']/a");
        private static readonly By DocketLinkLocator = By.XPath(".//span[@class = 'co_search_detailLevel_1']/a");
        private static readonly By EnteredInfoLocatorLocator = By.XPath(".//span[@class = 'SPDF-titleWrapper']/span");
        private static readonly By DocketTitleLocator = By.XPath(".//div[@class = 'SPDF-details']");
        private static readonly By DocketFiledDateLocator = By.XPath(".//span[contains(text(), 'Docket Filed:')]");

        /// <summary>
        ///  Initializes a new instance of the <see cref="DocketPdfResultListItem"/> class.
        /// </summary>
        /// <param name="containerElement"> container element of Docket PDFs item</param>
        public DocketPdfResultListItem(IWebElement containerElement)
            : base(containerElement)
        {
        }

        /// <summary>
        /// Snippet navigation component
        /// </summary>
        public EdgeSnippetNavigationComponent SnippetNavigation => new EdgeSnippetNavigationComponent(this.Container);
        
        /// <summary>
        /// Docket PDF link
        /// </summary>
        public ILink DocketPdfLink => new Link(this.Container, DocketPdfLinkLocator);

        /// <summary>
        /// Docket link
        /// </summary>
        public ILink DocketLink => new Link(this.Container, DocketLinkLocator);

        /// <summary>
        /// Docket Filed date
        /// </summary> 
        public ILabel DocketFiledDate => new Label(this.Container, DocketFiledDateLocator);

        /// <summary>
        /// Docket title
        /// </summary> 
        public ILabel DocketTitle => new Label(this.Container, DocketTitleLocator);

        /// <summary>
        /// Entered info
        /// </summary> 
        public ILabel EnteredInfo => new Label(this.Container, EnteredInfoLocatorLocator);
    }
}