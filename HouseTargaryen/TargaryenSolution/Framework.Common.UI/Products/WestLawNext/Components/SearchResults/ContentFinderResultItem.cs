namespace Framework.Common.UI.Products.WestLawNext.Components.SearchResults
{
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Content finder result item
    /// </summary>
    public class ContentFinderResultItem : BaseItem
    {
        private static readonly By TitleLocator = By.XPath("//a[contains(@id,'cobalt_result_duplicates:')]");

        private static readonly By SummaryLocator = By.XPath("//div[contains(@id,'co_searchResults_summary_')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentFinderResultItem"/> class. 
        /// </summary>
        /// <param name="container">
        /// </param>
        public ContentFinderResultItem(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// Title
        /// </summary>
        public string Title => DriverExtensions.WaitForElement(this.Container, TitleLocator).Text;

        /// <summary>
        /// Summary
        /// </summary>
        public string Summary => DriverExtensions.WaitForElement(this.Container, SummaryLocator).Text;
    }
}
