namespace Framework.Common.UI.Products.Shared.Pages.Footer
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.Browser;

    /// <summary>
    /// if the page is used for other footer pages (which don't have a Westlaw view), it can be renamed to External Page
    /// </summary>
    public class TrainingAndSupportExternalPage: ICreatablePageObject
    {
        /// <summary>
        /// Get Current Url for page
        /// </summary>
        /// <returns>Current Url</returns>
        public string Url => BrowserPool.CurrentBrowser.Url;
    }
}
