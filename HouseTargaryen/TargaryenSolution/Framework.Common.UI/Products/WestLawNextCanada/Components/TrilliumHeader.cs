namespace Framework.Common.UI.Products.WestLawNextCanada.Components
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// TrilliumHeaderComponent
    /// </summary>
    public class TrilliumHeader : BaseModuleRegressionComponent
    {
        private static readonly By TrilliumHeaderLocator = By.XPath(".//*[@id='co_libraryFavotieWidget']");
        private static readonly By FavouritesToggleLocator = By.XPath(".//a[@id='co_foldering_favorites_expansion_toggle']");
        private static readonly By PreviousButtonLocator = By.XPath(".//button[@id='co_libraryFavoriteWidget_prev']");
        private static readonly By NextButtonLocator = By.XPath(".//button[@id='co_libraryFavoriteWidget_next']");
        private static readonly By PaginationLocator = By.XPath(".//ul[@id='co_libraryFavoritesWidget_pagination']");

        /// <summary>
        /// Header locator
        /// </summary>
        protected override By ComponentLocator => TrilliumHeaderLocator;

        /// <summary>
        /// Favourites Toggle Button
        /// </summary>
        public IButton FavouritesToggleButton => new Button(this.ComponentLocator, FavouritesToggleLocator);

        /// <summary>
        /// Previous Button
        /// </summary>
        public IButton PreviousButton => new Button(this.ComponentLocator, PreviousButtonLocator);

        /// <summary>
        /// Next button
        /// </summary>
        public IButton NextButton => new Button(this.ComponentLocator, NextButtonLocator);

        /// <summary>
        /// Favourites Border
        /// </summary>
        public ILabel PaginationLabel => new Label(this.ComponentLocator, PaginationLocator);
    }
}
