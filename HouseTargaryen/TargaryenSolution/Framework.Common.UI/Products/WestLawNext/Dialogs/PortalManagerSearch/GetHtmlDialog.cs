namespace Framework.Common.UI.Products.WestLawNext.Dialogs.PortalManagerSearch
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Get Html Dialog
    /// </summary>
    public class GetHtmlDialog : BaseModuleRegressionDialog
    {
        private static readonly By CloseButtonLocator = By.XPath("//div[@id='getHTMLLightbox']//input[@value='Close']");

        private static readonly By DownloadButtonLocator = By.Id("co_download");

        /// <summary>
        /// verifies that the Get HTML dialog is displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCloseButtonDisplayed => DriverExtensions.IsDisplayed(CloseButtonLocator, 5);

        /// <summary>
        /// Clicks on the download button on the get html dialog
        /// </summary>
        public void ClickDownload() => this.ClickElement(DownloadButtonLocator);

        /// <summary>
        /// Clicks on the close button on the get html dialog
        /// </summary>
        public void ClickClose() => this.ClickElement(CloseButtonLocator);
    }
}