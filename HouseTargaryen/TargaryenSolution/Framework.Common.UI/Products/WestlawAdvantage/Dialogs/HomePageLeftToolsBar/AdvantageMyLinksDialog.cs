using Framework.Common.UI.Interfaces.Elements;
using Framework.Common.UI.Products.Shared.Elements.Buttons;
using Framework.Common.UI.Products.Shared.Elements.Labels;
using Framework.Common.UI.Products.WestlawAdvantage.Dialogs.HomePageLeftToolsBar;
using OpenQA.Selenium;

namespace Framework.Common.UI.Products.WestlawAdvantage.Dialogs
{
    /// <summary>
    /// Advantage MyLinks Dialog
    /// </summary>
    public class AdvantageMyLinksDialog : AdvantageBaseDialog
    {
        private static readonly By ContentTypeContainerLocator = By.XPath("//div[contains(@class,'__panelContent')]");
        private static readonly By CustomPagesButtonLocator = By.XPath("//span[contains(text(), 'Custom pages')]");
        private static readonly By ELibrariesButtonLocator = By.XPath("//button[contains(@class, '__panelListButton') and span[text()='eLibraries']]");
        private static readonly By FavoritesButtonLocator = By.XPath("//span[contains(text(), 'Favorites')]");
        private static readonly By CustomPagesHeaderLocator = By.XPath("//h2[contains(text(), 'Custom pages')]");
        private static readonly By CreateCustomPagesButtonLocator = By.XPath("//span[contains(text(), 'Create Custom Page')]");
        private static readonly By OpenCustomPagesButtonLocator = By.XPath(".//saf-button[@aria-label='Open all custom pages in new tab']");
        private static readonly By eLibrariesHeaderLocator = By.XPath("//h2[contains(text(), 'eLibraries')]");
        private static readonly By FavoritesHeaderLocator = By.XPath("//h2[contains(text(), 'Favorites')]");
        private static readonly By OpenFavoritesButtonLocator = By.XPath(".//saf-button[@aria-label='View all favorites']");
        private static readonly By OpenCustomPagesHeaderLocator = By.XPath("//h1[contains(text(), 'Custom Pages')]");
        private static readonly By FavoritesPagesHeaderLocator = By.XPath("//h1[contains(text(), 'Favorites')]");

        /// <summary>
        /// Custom Pages Button
        /// </summary>
        public IButton CustomPagesButton => new Button(ContentTypeContainerLocator, CustomPagesButtonLocator);

        /// <summary>
        /// ELibraries Button
        /// </summary>
        public IButton ELibrariesButton => new Button(ContentTypeContainerLocator, ELibrariesButtonLocator);

        /// <summary>
        /// Favorites Button
        /// </summary>
        public IButton FavoritesButton => new Button(ContentTypeContainerLocator, FavoritesButtonLocator);

        /// <summary>
        /// Custom Pages Header
        /// </summary>
        public ILabel CustomPagesHeader => new Label(CustomPagesHeaderLocator);

        /// <summary>
        /// Create Custom Pages Link
        /// </summary>
        public IButton CreateCustomPagesButton => new Button(CreateCustomPagesButtonLocator);

        /// <summary>
        /// Open Custom Pages Button
        /// </summary>
        public IButton OpenCustomPagesButton => new Button(OpenCustomPagesButtonLocator);

        /// <summary>
        /// eLibraries Header Label
        /// </summary>
        public ILabel eLibrariesHeader => new Label(eLibrariesHeaderLocator);

        /// <summary>
        /// Favorites Header Label
        /// </summary>
        public ILabel FavoritesHeader => new Label(FavoritesHeaderLocator);

        /// <summary>
        /// Open Favorites Button
        /// </summary>
        public IButton OpenFavoritesButton => new Button(OpenFavoritesButtonLocator);

        /// <summary>
        /// Custom Pages Header Label
        /// </summary>
        public ILabel OpenCustomPagesHeader => new Label(OpenCustomPagesHeaderLocator);

        /// <summary>
        /// Favorites Pages Header Label
        /// </summary>
        public ILabel FavoritesPagesHeader => new Label(FavoritesPagesHeaderLocator);
    }
}