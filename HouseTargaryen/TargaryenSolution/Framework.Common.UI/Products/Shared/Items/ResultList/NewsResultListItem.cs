namespace Framework.Common.UI.Products.Shared.Items.ResultList
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// News Result List Item
    /// todo: make this class internal when Search Manager is implemented
    /// </summary>
    public sealed class NewsResultListItem : ResultListItem
    {
        private static readonly By DuplicateCountLocator = By.CssSelector(".co_searchContent h4 span");

        private static readonly By DuplicateLinkLocator = By.XPath(".//a[contains(@id,'cobalt_result_news_duplicate')]");

        private static readonly By WordCountLocator = By.XPath(".//span[starts-with(normalize-space(text()),'Word')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsResultListItem"/> class. 
        /// </summary>
        /// <param name="containerElement">container Element</param>
        public NewsResultListItem(IWebElement containerElement)
            : base(containerElement)
        {
        }

        /// <summary>
        /// Gets Word Count field figure
        /// </summary>
        public int WordCount
            => DriverExtensions.GetElement(this.Container, WordCountLocator).GetText().ConvertCountToInt();

        /// <summary>
        /// Get list duplicate items
        /// </summary>
        /// <returns> list of duplicate titles </returns>
        public List<string> GetListDuplicateItems()
            => DriverExtensions.GetElements(this.Container, DuplicateLinkLocator).Select(el => el.Text).ToList();

        /// <summary>
        /// Is duplicate count text displayed
        /// </summary>
        /// <returns> true if duplicate count text is displayed </returns>
        public bool IsDuplicateCountTextDisplayed()
            => DriverExtensions.GetElement(this.Container, DuplicateCountLocator).Displayed;

        /// <summary>
        /// Open duplicate item
        /// </summary>
        /// <param name="duplicateIndex"> The duplicate index. </param>
        /// <returns> The <see cref="CommonDocumentPage"/>. </returns>
        public CommonDocumentPage OpenDuplicateItem(int duplicateIndex)
        {
            List<IWebElement> docLinkElements =
                DriverExtensions.GetElements(this.Container, DuplicateLinkLocator).ToList();
            DriverExtensions.Click(docLinkElements[duplicateIndex]);
            return new CommonDocumentPage();
        }
    }
}