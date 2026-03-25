namespace Framework.Common.UI.Products.WestlawEdge.Components.Miscellaneous
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.WestlawEdge.Items.Favorites;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Favorites component (homepage)
    /// </summary>
    public class FavoritesTabComponent : BaseTabComponent
    {
        private const string FavoritePagesCategoryLinkLocator = "//div[@class='Home-favorites-links']//li /label/ a[@title='{0}']";

        private static readonly By FavoritePagesLinksLocator = By.XPath("//div[@class='Home-favorites-links']//li");

        private static readonly By ContainerLocator = By.Id("panel_FavoritesPaneId");

        private static readonly By SearchTextBoxLocator = By.XPath(".//textarea[@id='searchInputIdFavorites']");

        private static readonly By SearchButtonLocator = By.XPath(".//input[@id='searchButtonFavorites']");

        private static readonly By FavoriteGroupItemLocator = By.XPath(".//h4[@class='Home-favorites-header']");

        private static readonly By ViewAllFavoritesButtonLocator = By.XPath(".//a[contains(@class,'Button-primary')] |.//a[text()='View all favorites']");

        /// <summary>
        /// Add to favorites search text box
        /// </summary>
        public ITextbox FavoritesSearchTextBox => new Textbox(this.ComponentLocator, SearchTextBoxLocator);

        /// <summary>
        /// Add to favorites search button
        /// </summary>
        public IButton SearchButton => new Button(this.ComponentLocator, SearchButtonLocator);

        /// <summary>
        /// ViewAllFavoritesButton
        /// </summary>
        public IButton ViewAllFavoritesButton => new Button(this.ComponentLocator, ViewAllFavoritesButtonLocator);

        /// <summary>
        /// Get Tab name
        /// </summary>
        protected override string TabName => "Favorites";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get favorite groups items
        /// </summary>
        /// <returns>
        /// list of favorite groups items
        /// </returns>
        public List<FavoritesGroupItem> GetFavoriteGroupItems() => DriverExtensions
                                                              .GetElements(this.ComponentLocator, FavoriteGroupItemLocator)
                                                              .Select(item => new FavoritesGroupItem(item)).ToList();

        /// <summary>
        /// Gets all favorites from the SearchHomePage Favorites component and returns them as strings
        /// </summary>
        /// <returns> List of Favorites links </returns>
        public List<string> GetFavoritesList() => DriverExtensions.GetElements(FavoritePagesLinksLocator).Select(item => item.Text).ToList();

        /// <summary>
        /// Click On Category Page
        /// </summary>
        /// <param name="categoryPage"> category Page </param>
        /// <returns> Indigo Common Browse Page </returns>
        public EdgeCommonBrowsePage ClickOnCategoryPage(string categoryPage)
        {
            DriverExtensions.GetElement(By.XPath(string.Format(FavoritePagesCategoryLinkLocator, categoryPage))).Click();
            return new EdgeCommonBrowsePage();
        }
    }
}
