namespace Framework.Common.UI.Products.WestLawNextCanada.Components.Miscellaneous
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.WestlawEdge.Components.Miscellaneous;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Canada Favorites component (homepage)
    /// </summary>
    public class CanadaFavoritesTabComponent : FavoritesTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//li[@id='co_frequentFavoritesContainer']//div[@class='co_dropdownBoxExpanded']");

        private static readonly By ViewAllFavoritesLinkLocator = By.XPath(".//a[text()='View all favourites' or text()='Afficher tous les favoris.']");

        private static readonly By SearchTextBoxLocator = By.XPath(".//textarea[@id='searchInputIdMyLinksFavorites']");

        private static readonly By SearchButtonLocator = By.XPath(".//input[@id='searchButtonMyLinksFavorites']");

        private static readonly By NoFavoritesItemMessageLocator = By.ClassName("Zero-favorites");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// ViewAllFavoritesButton
        /// </summary>
        public ILink ViewAllFavoritesLink => new Link(this.ComponentLocator, ViewAllFavoritesLinkLocator);

        /// <summary>
        /// Add to favorites search text box
        /// </summary>
        public new ITextbox FavoritesSearchTextBox => new Textbox(this.ComponentLocator, SearchTextBoxLocator);

        /// <summary>
        /// Add to favorites search button
        /// </summary>
        public new IButton SearchButton => new Button(this.ComponentLocator, SearchButtonLocator);

        /// <summary>
        /// IsNoFavoritesItemMessageDisplayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsNoFavoritesItemMessageDisplayed() => DriverExtensions.IsDisplayed(NoFavoritesItemMessageLocator);
    }
}
