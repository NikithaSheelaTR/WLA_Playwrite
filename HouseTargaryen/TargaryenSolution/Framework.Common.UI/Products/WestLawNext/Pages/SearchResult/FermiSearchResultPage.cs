namespace Framework.Common.UI.Products.WestLawNext.Pages.SearchResult
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Toolbar;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Products.WestLawNext.Pages.SearchResult.ContentTypeSearchResultPages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <inheritdoc />
    /// <summary>
    /// Search result page for Fermi searches
    /// </summary>
    public sealed class FermiSearchResultPage : BaseSearchResultPageWithResultList<ResultListItem>
    {
        private static readonly By MoreLinkLocator = By.Id("co_flipToFermiAllLink");

        private static readonly By PageHeaderLocator = By.ClassName("co_search_result_heading_content");

        private static readonly By BackToBooleanTermsLinkLocator = By.XPath("//*[@id ='co_flipToTNCBox']//a[@id ='infoBoxContent']");

        /// <summary>
        /// Back To Boolean Terms and Connectors Link
        /// </summary>
        public ILink BackToBooleanLink => new Link(BackToBooleanTermsLinkLocator);

        /// <summary>
        /// Toolbar component
        /// </summary>
        public Toolbar Toolbar { get; } = new Toolbar();

        /// <summary>
        /// Click on the More link
        /// </summary>
        /// <returns>
        /// The <see cref="ContentTypeSearchResultsPage"/>.
        /// </returns>
        public ContentTypeSearchResultsPage ClickMoreLink()
        {
            DriverExtensions.WaitForElement(MoreLinkLocator).Click();
            return new ContentTypeSearchResultsPage();
        }

        /// <summary>
        /// Get page's heading (page heading is displayed under global search bar, e.g. Find Results)
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>        
        public override string GetPageHeading() => DriverExtensions.WaitForElement(PageHeaderLocator).GetText();

        /// <summary>
        /// Click on the Back to Boolean terms link
        /// </summary>
        /// <returns> New instance of T </returns>
        public T BackToBooleanTermsLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(BackToBooleanTermsLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}