namespace Framework.Common.UI.Products.Shared.Items.HotDocuments
{
    using OpenQA.Selenium;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using System.Collections.Generic;

    //TODO: Refactor GetHotDocsItems method when content will be shows up
    /// <summary>
    /// Describe items in the 'Hot Docs' widget on the Home/Custom page.
    /// </summary>
    public class HotDocumentsItem
    {
        private static readonly By HotDocItemLocator = By.XPath(
            "//div[contains(@class,'co_genericBox co_expandedState co_dataFeedWidget styled')]//div[@class='co_genericBoxContent']//li");

        private const string HotDocItemHeaderLctMask
            = "//div[contains(@class,'co_genericBox co_expandedState co_dataFeedWidget styled')]//li[{0}]/div[@class='co_searchContent']//h3/a";

        private const string HotDocItemMetadataLctMask
            = "//div[contains(@class,'co_genericBox co_expandedState co_dataFeedWidget styled')]//li[{0}]/div[@class='co_searchContent']//h4";

        private const string HotDocItemDocDescriptionLctMask
            = "//div[contains(@class,'co_genericBox co_expandedState co_dataFeedWidget styled')]//li[{0}]/div[@class='co_searchContent']//div[@class='co_searchResults_summary']";

        /// <summary>
        /// A hyperlink to the source document
        /// </summary>
        public IWebElement DocLink { get; set; }

        /// <summary>
        /// Object metadata(date, court) 
        /// </summary>
        public string Metadata { get; set; }

        /// <summary>
        /// Object description/excerpt text – 250 character limit
        /// </summary>
        public string DocDescprition { get; set; }

        /// <summary>
        /// Gets list of Hot Documents 
        /// </summary>
        /// <returns> List list of Hot Documents </returns>
        public List<HotDocumentsItem> GetHotDocsItems()
        {
            var listOfItems = new List<HotDocumentsItem>();
            for (int i = 1; i < DriverExtensions.GetElements(HotDocItemLocator).Count + 1; i++)
            {
                var item = new HotDocumentsItem
                {
                    DocLink = DriverExtensions.GetElement(By.XPath(string.Format(HotDocItemHeaderLctMask, i))),
                    Metadata = DriverExtensions.GetText(By.XPath(string.Format(HotDocItemMetadataLctMask, i))),
                    DocDescprition = DriverExtensions.GetText(By.XPath(string.Format(HotDocItemDocDescriptionLctMask, i)))
                };
                listOfItems.Add(item);
            }

            return listOfItems;
        }
    }
}
