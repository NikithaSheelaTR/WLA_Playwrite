namespace Framework.Common.UI.Products.WestLawNext.Pages.MigratedTax
{
    using Framework.Common.UI.Products.Shared.Components.HomePage;
    using Framework.Common.UI.Products.Shared.Components.RightPaneComponents;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.Browser;

    /// <summary>
    /// KPMG Tax Page
    /// </summary>
    public class KpmgTaxPage : CommonMigratedTaxPage
    {
        /// <summary>
        /// Favorites Widget
        /// </summary>
        public FavoritesComponent FavoritesWidget { get; set; } = new FavoritesComponent();

        /// <summary>
        /// PWC Customized Libraries
        /// </summary>
        public KpmgCustomizedLibrariesComponent KpmgCustomizedLibraries { get; set; } = new KpmgCustomizedLibrariesComponent();
    }
}