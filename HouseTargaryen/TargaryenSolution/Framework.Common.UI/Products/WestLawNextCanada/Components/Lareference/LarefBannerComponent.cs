namespace Framework.Common.UI.Products.WestLawNextCanada.Components.Lareference
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Components.CategoryPage;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// Laref Banner component
    /// </summary>
    public class LarefBannerComponent : BaseModuleRegressionComponent
    {
        private static readonly By BannerContainerLocator = By.Id("crsw_laRef_banner");
        private static readonly By HeadingLabelLocater = By.XPath("//div[@id='crsw_laRef_banner_heading']/div");
        private static readonly By CopyLinkButtonLocator = By.Id("co_linkBuilder");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => BannerContainerLocator;

        /// <summary>
        /// Heading label of Laref Banner on Laref Home page
        /// </summary>
        public ILabel HeadingLabel => new Label(HeadingLabelLocater);

        /// <summary>
        /// Copy Link Button
        /// </summary>
        public IButton CopyLinkButton => new Button(CopyLinkButtonLocator);

        /// <summary>
        /// Favorites Component (Add To and Edit Favorites Links)
        /// </summary>
        public FavoritesComponent Favorites => new FavoritesComponent();

        /// <summary>
        /// Start Page Component (Add and Remove Page as start after user login)
        /// </summary>
        public StartPageComponent StartPage => new StartPageComponent();
    }
}