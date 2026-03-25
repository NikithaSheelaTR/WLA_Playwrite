namespace Framework.Common.UI.Products.Shared.Components.CategoryPage
{
    using Framework.Common.UI.Products.Shared.Dialogs.Favorites;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Favorites Component
    /// Locations - Browse pages, Some Advanced search template pages
    /// </summary>
    public class FavoritesComponent : BaseModuleRegressionComponent
    {
        private static readonly By AddToFavoritesLinkLocator = By.XPath("//*[@id = 'co_foldering_categoryPage' and contains(.,'Add to') or contains(@oldtitle,'Ajouter aux')]");

        private static readonly By EditLinkLocator = By.XPath("//*[@id = 'co_foldering_categoryPage' and contains(.,'Edit') or contains(@oldtitle,'Modifier')]");

        private static readonly By ContainerLocator = By.Id("coid_website_browsePageAddToFavorites");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click on the 'Add to Favorites' Link
        /// </summary>
        /// <returns> The <see cref="AddToFavoritesDialog"/>. </returns>
        public AddToFavoritesDialog ClickAddToFavoritesLink()
        {
            DriverExtensions.WaitForElement(AddToFavoritesLinkLocator).Click();
            return new AddToFavoritesDialog();
        }

        /// <summary>
        /// Click on the Edit Favorites Link
        /// </summary>
        /// <returns> The <see cref="AddToFavoritesDialog"/>. </returns>
        public AddToFavoritesDialog ClickEditFavoritesLink()
        {
            DriverExtensions.WaitForElement(EditLinkLocator).Click();
            return new AddToFavoritesDialog();
        }

        /// <summary>
        /// Verify that 'Add To Favorites' Link is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsAddToFavoritesLinkDisplayed() => DriverExtensions.IsDisplayed(AddToFavoritesLinkLocator, 5);

        /// <summary>
        /// Verify that Edit Favorites Link Displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsEditFavoritesLinkDisplayed() => DriverExtensions.IsDisplayed(EditLinkLocator, 5);
    }
}