namespace Framework.Common.UI.Products.Shared.Dialogs.Dockets
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The dialog that appears after clicking a PDF Download link for a Docket entry
    /// </summary>
    public class MultiPdfDialog : BaseModuleRegressionDialog
    {
        private static readonly By LoadingSpinnerLocator = By.CssSelector("#co_loading, .co_loading");
        private static readonly By ViewButtonLocator = By.CssSelector(".co_pdfButton");

        /// <summary>
        /// Waits for the dialog to not longer show the processing spinner
        /// </summary>
        /// <returns> page object </returns>
        public T WaitForProcessingComplete<T>(string newTabName, int timeOut = 30000)
            where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementNotDisplayed(timeOut, LoadingSpinnerLocator);
            DriverExtensions.WaitForJavaScript();
            BrowserPool.CurrentBrowser.CreateTab(newTabName);
            BrowserPool.CurrentBrowser.ActivateTab(newTabName);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// check if multipartpdf dialog has view links
        /// </summary>
        /// <returns>boolean is view links shown</returns>
        public bool AreViewLinksShown(int timeOut = 30000)
        {
            DriverExtensions.WaitForElementNotDisplayed(timeOut, LoadingSpinnerLocator);
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.IsDisplayed(ViewButtonLocator);
        }
    }
}