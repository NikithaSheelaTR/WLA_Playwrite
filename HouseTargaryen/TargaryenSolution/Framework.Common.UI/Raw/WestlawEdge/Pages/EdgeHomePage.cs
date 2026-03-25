namespace Framework.Common.UI.Raw.WestlawEdge.Pages
{
    using System;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using Framework.Common.UI.Products.Shared.Dialogs.Favorites;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Products.WestlawEdge.Components.EdgeResponsive;
    using Framework.Common.UI.Products.WestlawEdge.Components.HomePage;
    using Framework.Common.UI.Products.WestlawEdge.Components.Miscellaneous;
    using Framework.Common.UI.Products.WestlawEdge.Components.TourComponent;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.BrowseComponent;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Content;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.MyContent;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Tours;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;
    using ContentTypesTabPanel = Products.WestlawEdge.Components.BrowseWidget.ContentTypesTabPanel;
    using BrowseTabPanel = Products.WestlawEdge.Components.BrowseWidget.BrowseTabPanel;

    /// <summary>
    /// The common search home page.
    /// </summary>
    public class EdgeHomePage : EdgeCommonAuthenticatedWestlawNextPage, ICommonSearchHomePage
    {
        private const string TabNameOfPage = "Westlaw Edge";

        private static readonly By HomeTourLocator = By.Id("coid_confirmButton");
        private static readonly By HomeBodyLocator = By.CssSelector("body.co_homepage");
        private static readonly By SaoMessageLocator = By.XPath("//div[@id='co_subHeader']//div[@class='co_infoBox_message']");
        private static readonly By DynamicMessageLocator = By.Id("co_dynamicMessageContainer");
        private static readonly By QuickCheckLocator = By.XPath("//div[@class='Access-point Access-docAnalyzer']/a");

        private EnumPropertyMapper<ContentTypeEdge, WebElementInfo> contentTypeMap;

        /// <summary>
        /// Gets the Miscellaneous component
        /// </summary>
        public MiscellaneousComponent MiscellaneousComponent { get; } = new MiscellaneousComponent();

        /// <summary>
        /// Get the Browse Component
        /// </summary>
        public BrowseTabPanel BrowseTabPanel { get; } = new BrowseTabPanel();

        /// <summary>
        /// Home tour options- take the tour, maybe later, don't show me this
        /// </summary>
        public TakeTheTourComponent TakeTheTourComponent { get; } = new TakeTheTourComponent();

        /// <summary>
        /// Home tour card component
        /// </summary>
        public TourCardComponent<HomeTourCards> TourCardComponent { get; } = new TourCardComponent<HomeTourCards>();

        /// <summary>
        /// home tour - tour notification
        /// </summary>
        public TourNotificationComponent TourNotificationComponent { get; } = new TourNotificationComponent();

        /// <summary>
        /// Tray component
        /// </summary>
        public TrayComponent TrayComponent { get; } = new TrayComponent();

        /// <summary>
        /// Hot Documents Component
        /// </summary>
        public HotDocumentsComponent HotDocumentsComponent { get; } = new HotDocumentsComponent();

        /// <summary>
        /// Browse Content Right Pane Component
        /// </summary>
        public BrowseContentRightPaneComponent BrowseContentRightPaneComponent { get; } = new BrowseContentRightPaneComponent();

        /// <summary>
        /// Quick Check Link
        /// </summary>
        public ILink QuickCheckLink => new Link(QuickCheckLocator);

        /// <summary>
        /// Gets the BrowseTab enumeration to BaseTextModel map.
        /// </summary>
        protected EnumPropertyMapper<ContentTypeEdge, WebElementInfo> EdgeContentTypeMap =>
            this.contentTypeMap = this.contentTypeMap ?? EnumPropertyModelCache.GetMap<ContentTypeEdge, WebElementInfo>(
                                      string.Empty,
                                      @"Resources/EnumPropertyMaps/WestlawEdge/Content");

        /// <summary>
        /// Gets the confirmation message for acceptance and declines for SAO on the Search Home Page
        /// </summary>
        /// <returns>confirmation message</returns>
        public string GetSaoConfirmationMessage() => DriverExtensions.WaitForElement(SaoMessageLocator).GetText();

        /// <summary>
        /// Adds Category Page To Favorite Widget And Returns the <see cref="EdgeHomePage"/> page instance.
        /// </summary>
        /// <param name="homePage"> home Page </param>
        /// <param name="categoryPageName">The category Page Name.</param>
        /// <returns>The <see cref="EdgeHomePage"/>.</returns>
        public EdgeHomePage AddCategoryPageToFavoriteWidgetAndReturn(EdgeHomePage homePage, ContentTypeEdge categoryPageName)
        {
            var favoritesTabComponent = homePage.BrowseTabPanel.SetActiveTab<MyContentTabPanel>(BrowseTab.MyContent).ClickMyContentsTab<FavoritesTabComponent>(MyContent.Favorites);

            if (!favoritesTabComponent.GetFavoritesList().Contains(this.EdgeContentTypeMap[categoryPageName].Text))
            {
                var categoryPage = homePage.BrowseTabPanel.SetActiveTab<ContentTypesTabPanel>(BrowseTab.ContentTypes).ClickBrowseCategory<EdgeCommonBrowsePage>(categoryPageName);
                AddToFavoritesDialog addToFavorites = categoryPage.Favorites.ClickAddToFavoritesLink();
                addToFavorites.SaveToSpecificFavoritesGroup<CommonBrowsePage>("My Favorites");
                categoryPage.Header.ClickLogo<EdgeHomePage>();
            }

            return this;
        }

        /// <summary>
        /// Remove From Favorite
        /// </summary>
        /// <param name="categoryPageName"> category Page Name </param>
        /// <returns> homw page </returns>
        public EdgeHomePage RemoveFromFavorite(string categoryPageName)
        {
            var favoritesTabComponent = this.BrowseTabPanel.SetActiveTab<MyContentTabPanel>(BrowseTab.MyContent).ClickMyContentsTab<FavoritesTabComponent>(MyContent.Favorites);

            if (favoritesTabComponent.GetFavoritesList().Contains(categoryPageName))
            {
                return favoritesTabComponent.ClickOnCategoryPage(categoryPageName)
                                     .Favorites.ClickEditFavoritesLink()
                                     .UnselectAllGroups().ClickSaveButton<EdgeCommonBrowsePage>()
                                     .Header.ClickLogo<EdgeHomePage>();
            }

            return this;
        }

        /// <summary>
        /// Determines if the Dynamic Messaging promo is displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsDynamicMessagingPromoDisplayed() => DriverExtensions.IsDisplayed(DynamicMessageLocator);

        /// <summary>
        /// home tour - true if tour is displayed, false otherwise
        /// </summary>
        /// <returns> true if displayed</returns>
        public bool IsHomeTourDisplayed() => DriverExtensions.IsDisplayed(HomeTourLocator, 5);

        /// <summary>
        /// Checks whether or not the user has navigated to the Westlaw home page
        /// </summary>
        /// <returns> true if present</returns>
        public bool IsHomePageDisplayed()
            => BrowserPool.CurrentBrowser.Title.Equals(TabNameOfPage, StringComparison.InvariantCultureIgnoreCase)
               && DriverExtensions.IsDisplayed(HomeBodyLocator);
    }
}
