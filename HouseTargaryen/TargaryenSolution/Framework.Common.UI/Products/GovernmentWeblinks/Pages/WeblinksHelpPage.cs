namespace Framework.Common.UI.Products.GovernmentWeblinks.Pages
{
    /// <summary>
    /// Help page for Government Weblinks
    /// </summary>
    public class WeblinksHelpPage : BaseGovernmentWeblinksPage
    {
        /// <summary>
        /// Verifies is help page displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsDisplayed() => this.IsTitleDisplayed() && this.GetTitle().Contains("Help");
    }
}
