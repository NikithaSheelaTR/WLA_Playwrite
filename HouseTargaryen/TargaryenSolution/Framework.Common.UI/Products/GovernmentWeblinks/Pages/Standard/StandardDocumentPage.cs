namespace Framework.Common.UI.Products.GovernmentWeblinks.Pages.Standard
{
    using Framework.Common.UI.Products.GovernmentWeblinks.Components;

    /// <summary>
    /// Standard Document Page
    /// </summary>
    public class StandardDocumentPage : WeblinksDocumentPage
    {
        /// <summary>
        /// Header component
        /// </summary>
        public new WeblinksStandardHeaderComponent Header { get; } = new WeblinksStandardHeaderComponent();
    }
}
