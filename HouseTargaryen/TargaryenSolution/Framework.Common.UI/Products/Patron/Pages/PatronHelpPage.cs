namespace Framework.Common.UI.Products.Patron.Pages
{
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Help page unique to patron users
    /// </summary>
    public class PatronHelpPage : BaseModuleRegressionPage
    {
        /// <summary>
        /// Get Page Title
        /// </summary>
        /// <returns> Page Title </returns>
        public string GetPageTitle() =>BrowserPool.CurrentBrowser.Title;
    }
}