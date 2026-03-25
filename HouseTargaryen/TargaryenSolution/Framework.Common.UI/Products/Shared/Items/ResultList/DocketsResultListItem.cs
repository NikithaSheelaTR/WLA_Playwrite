namespace Framework.Common.UI.Products.Shared.Items.ResultList
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;

    /// <summary>
    /// Dockets Result List Item
    /// </summary>
    public class DocketsResultListItem : ResultListItem
    {
        private static readonly By CaseTypeLocator =
            By.CssSelector(".co_searchResults_citation .co_search_detailLevel_2:nth-child(4)");

        private static readonly By CourtLocator =
            By.CssSelector(".co_searchResults_citation .co_search_detailLevel_1:nth-child(2)");

        private static readonly By DocketNumberLocator =
            By.CssSelector(".co_searchResults_citation .co_search_detailLevel_1:nth-child(3)");

        private static readonly By JusticeLocator =
            By.CssSelector(".co_searchResults_citation .co_search_detailLevel_2:nth-child(5)");

        private static readonly By Metadatalocator = By.ClassName("co_search_detailLevel_3");

        /// <summary>
        /// Initializes a new instance of the <see cref="DocketsResultListItem"/> class. 
        /// </summary>
        /// <param name="containerElement">container Element</param>
        public DocketsResultListItem(IWebElement containerElement)
            : base(containerElement)
        {
        }

        /// <summary>
        /// Docket number for dockets search result
        /// </summary> 
        public string DocketNumber => this.TryGetText(DocketNumberLocator);

        /// <summary>
        /// Case type for dockets search result
        /// </summary> 
        public ILabel CaseType => new Label(CaseTypeLocator);

        /// <summary>
        /// Court for dockets search result
        /// </summary> 
        public ILabel Court => new Label(CourtLocator);

        /// <summary>
        /// Justice for dockets search result
        /// </summary> 
        public ILabel Justice => new Label(JusticeLocator);

        /// <summary>
        /// Metadata for dockets search result
        /// </summary> 
        public ILabel Metadata => new Label(Metadatalocator);
    }
}