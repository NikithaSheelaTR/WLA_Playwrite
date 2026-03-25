namespace Framework.Common.UI.Products.WestLawNext.Pages.SecondarySources
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Components.ResultList;
    using Framework.Common.UI.Products.Shared.Components.Toolbar;
    using Framework.Common.UI.Products.Shared.Components.Toolbar.FooterToolbar;
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Products.WestLawNext.Components.SecondarySources;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Secondary Sources Category Page
    /// </summary>
    public class SecondarySourcesPage : CommonBrowsePage
    {
        private static readonly By NoPubsFoundMessageLocator = By.XPath("//div[@id='cobalt_search_no_results']");

        private static readonly By AddToFavoritesButtonLocator = By.Id("co_foldering_categoryPage");

        private static readonly By LibraryIsFilteredTextLocator = By.Id("co_libraryIsFilteredText");

        /// <summary>
        /// Favorite Pub Library Component
        /// </summary>
        public FavoritePubLibraryComponent FavoritePublicationsComponent { get; set; } = new FavoritePubLibraryComponent();

        /// <summary>
        /// Toolbar component
        /// </summary>
        public Toolbar Toolbar { get; set; } = new Toolbar();

        /// <summary>
        /// The Narrow Pane (left side of search results page)
        /// </summary>
        public NarrowPaneComponent NarrowPane { get; set; } = new NarrowPaneComponent();

        /// <summary>
        /// The results list footer, with options for next page, previous page, etc.
        /// </summary>
        public FooterToolbarComponent FooterToolbar { get; set; } = new FooterToolbarComponent();

        /// <summary>
        /// ResultList component
        /// </summary>
        public SecondarySourcesResultListComponent SecondarySourcesResultList { get; set; } = new SecondarySourcesResultListComponent();

        /// <summary>
        /// Click Favorites Button
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>The new instance of T page.</returns>
        public T ClickFavoritesButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(AddToFavoritesButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Gets the library filtered text heading
        /// </summary>
        /// <returns>The filtered text heading</returns>
        public string GetLibraryPageFilteredText()
            => DriverExtensions.WaitForElement(LibraryIsFilteredTextLocator).GetText();

        /// <summary>
        /// Checks whether the page has No Publication Found Message
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetNoPublicationsFoundMessage() =>
            DriverExtensions.WaitForElement(NoPubsFoundMessageLocator).GetText();
    }
}