namespace Framework.Common.UI.Products.Shared.Items.ResultList
{
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Search Result Item
    /// Part of search result list
    /// In addition to items from ResultListItems contains:
    /// - Address
    /// - Phone Number
    /// - DUNS
    /// ToDO could contains other elements as well 
    /// </summary>
    public class CompanyInvestigatorResultListItem : ResultListItem
    {
        /// <summary>
        /// The address locator.
        /// </summary>
        private static readonly By AddressLocator =
            By.XPath(".//div[@class='co_search_detailLevel_1']/h4/following-sibling::div[1]");

        private static readonly By DocumentNumberLocator = By.XPath(".//span[@class='co_searchCount']");

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyInvestigatorResultListItem"/> class. 
        /// </summary>
        /// <param name="containerElement"> container </param>
        public CompanyInvestigatorResultListItem(IWebElement containerElement)
            : base(containerElement)
        {
        }

        /// <summary>
        /// Gets Address
        /// </summary>
        public string Address => this.TryGetText(AddressLocator);

        /// <summary>
        /// Document index (start with 0)
        /// </summary>
        public int DocumentIndex => this.TryGetText(DocumentNumberLocator).ConvertCountToInt() - 1;
    }
}