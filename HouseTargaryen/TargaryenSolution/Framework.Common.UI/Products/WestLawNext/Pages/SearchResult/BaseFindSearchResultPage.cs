namespace Framework.Common.UI.Products.WestLawNext.Pages.SearchResult
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Toolbar;
    using Framework.Common.UI.Products.Shared.Components.Toolbar.FooterToolbar;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <inheritdoc />
    /// <summary>
    /// Find Search Result Page
    /// </summary>
    public abstract class BaseFindSearchResultPage : BaseSearchResultPage
    {
        private static readonly By ViewSearchResultsForLinkLocator = By.CssSelector(".co_search_citations_link a");

        private static readonly By PageHeadingLocator = By.XPath("//span[@class='co_search_titleCount']/..");

        /// <summary>
        /// Toolbar component
        /// </summary>
        public Toolbar Toolbar { get; } = new Toolbar();

        /// <summary>
        /// The results list footer, with options for next page, previous page, etc.
        /// </summary>
        public FooterToolbarComponent FooterToolbar { get; } = new FooterToolbarComponent();

        /// <summary>
        /// ClickViewSearchResultsForLink
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>The new instance of T page</returns>
        public T ClickViewSearchResultsForLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementDisplayed(ViewSearchResultsForLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get page's heading (page heading is displayed under global search bar, e.g. Find Results)
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string GetPageHeading() => DriverExtensions.WaitForElement(PageHeadingLocator).GetText();
    }
}