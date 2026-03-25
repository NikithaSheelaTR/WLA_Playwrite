namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Header
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Indigo Favorites Dialog
    /// </summary>
    public class EdgeFavoritesDialog : BaseEdgeHeaderDialog
    {
        private static readonly By ContainerLocator = By.Id("co_frequentFavoritesContainer");
        private static readonly By NoFavoritesItemMessageLocator = By.ClassName("Zero-favorites");

        /// <summary>
        /// Container
        /// </summary>
        protected override IWebElement Container => DriverExtensions.WaitForElement(ContainerLocator);

        /// <summary>
        /// IsNoFavoritesItemMessageDisplayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsNoFavoritesItemMessageDisplayed() => DriverExtensions.IsDisplayed(NoFavoritesItemMessageLocator);
    }
}
