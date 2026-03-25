namespace Framework.Common.UI.Products.Shared.Pages
{
    using Framework.Common.UI.Utils.Browser;

    /// <summary>
    /// PDF Viewer Page
    /// </summary>
    public class PdfViewerPage : BaseModuleRegressionPage
    {
        /// <summary>
        /// Gets the plugin type and checks if the type is valid for a PDF.
        /// </summary>
        /// <returns> True if valid, false otherwise </returns>
        public bool IsValidPdfType() => BrowserPool.CurrentBrowser.Url.Contains("pdf");
    }
}
