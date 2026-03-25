namespace Framework.Common.UI.Raw.WestlawEdge.Pages.ResultPages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.CategoryPage;
    using Framework.Common.UI.Products.Shared.Components.Facets.RightFacets;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.WestLawNext.Components.SecondarySources;
    using Framework.Common.UI.Products.WestlawEdge.Components;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Interfaces.Elements;

    /// <summary>
    /// Indigo Secondary Sources Category Page
    /// </summary>
    public class EdgeSecondarySourcesCategoryPage : EdgeCommonSearchResultPage
    {
        private const string PublicationCheckboxLctMask = "//input[@class='co_linkCheckBox_checkBox' and contains(@value, {0})]";
        private const string ScopePubIconsLctMask =
            "//div[@id='cobalt_search_commentaryLibrary_results']//button[contains(@class,'co_scopeIcon')][contains(@scopetitle, {0})]";

        private const string PublicationOutOfPlanLabelXLctMask =
            "//input[@class='co_linkCheckBox_checkBox' and contains(@value,{0})]/following-sibling::div[@class='co_outOfPlanLabel']";

        private static readonly By OutOfPlanLabelLocator = By.ClassName("co_outOfPlanLabel");
        private static readonly By LibraryIsFilteredTextLocator = By.Id("co_libraryIsFilteredText");
        private static readonly By SelectedTextsPeriodicalsLocator = By.Id("coid_selected_texts___periodicals");
        private static readonly By ScopeIconLocator = By.XPath("//button[@class='co_scopeIcon']");
        private static readonly By CopyLinkLocator = By.Id("co_linkBuilder");
        private static readonly By SearchTitleCountLabelLocator = By.ClassName("co_search_titleCount");

        /// <summary>
        /// Gets the header.
        /// </summary>
        public new EdgeHeaderComponent Header { get; } = new EdgeHeaderComponent();

        /// <summary>
        /// New Narrow Pane Component (left side of search results page)
        /// </summary>
        public new EdgeNarrowPaneComponent NarrowPane { get; } = new EdgeNarrowPaneComponent();

        /// <summary>
        /// Favorite Publications Library Component
        /// </summary>
        public FavoritePubLibraryComponent FavoritePublications { get; private set; } = new FavoritePubLibraryComponent();

        /// <summary>
        /// Tools And Resources Widget
        /// Might be present on the right hand side for some category pages
        /// </summary>
        public ToolsAndResourcesFacetComponent ToolsAndResourcesComponent { get; private set; } = new ToolsAndResourcesFacetComponent();

        /// <summary>
        /// Favorites Component (Add To and Edit Favorites Links)
        /// </summary>
        public FavoritesComponent Favorites => new FavoritesComponent();

        /// <summary>
        /// SearchTitleLabel
        /// </summary>
        public ILabel SearchTitleCountLabel => new Label(SearchTitleCountLabelLocator);

        /// <summary>
        /// OutOfPlanBanners
        /// </summary>
        public ILabel OutOfPlanBanners() => new Label(OutOfPlanLabelLocator);

        /// <summary>
        /// IsPublicationListed
        /// </summary>
        /// <param name="publicationTitle"></param>
        /// <returns></returns>
        public bool IsPublicationListed(string publicationTitle)
            => DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(PublicationCheckboxLctMask, publicationTitle));

        /// <summary>
        /// IsScopeIconDisplayed
        /// </summary>
        /// <param name="publicationTitle"></param>
        public bool IsScopeIconnForPubInListDisplayed(string publicationTitle)
            => DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(ScopePubIconsLctMask, publicationTitle));

        /// <summary>
        /// ClickScopeIconForPubInList
        /// </summary>
        /// <param name="publicationTitle"></param>
        public ScopeDialog ClickScopeIconForPubInList(string publicationTitle)
        {
            DriverExtensions.Click(SafeXpath.BySafeXpath(ScopePubIconsLctMask, publicationTitle));
            return new ScopeDialog();
        }

        /// <summary>
        /// The click scope icon.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// </returns>
        public T ClickScopeIcon<T>() where T : ICreatablePageObject
        {
            DriverExtensions.Click(ScopeIconLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// The is scope icon displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsScopeIconDisplayed() => DriverExtensions.IsDisplayed(ScopeIconLocator);

        /// <summary>
        /// Gets the library filtered text heading
        /// </summary>
        /// <returns>The filtered text heading</returns>
        public string GetLibraryPageFilteredText() => DriverExtensions.GetText(LibraryIsFilteredTextLocator);

        /// <summary>
        /// IsSelectedTextsPeriodicalsLinkDislayed
        /// </summary>
        /// <returns></returns>
        public bool IsSelectedTextsPeriodicalsLinkDislayed() => DriverExtensions.IsDisplayed(SelectedTextsPeriodicalsLocator);

        /// <summary>
        /// IsOutOfPlanLabelPresentForPublication
        /// </summary>
        /// <param name="publicationName"></param>
        /// <returns></returns>
        public bool IsOutOfPlanLabelPresentForPublication(string publicationName)
            => DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(PublicationOutOfPlanLabelXLctMask, publicationName));
    }
}
