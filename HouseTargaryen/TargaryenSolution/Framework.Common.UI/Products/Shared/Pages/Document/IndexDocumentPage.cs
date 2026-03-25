namespace Framework.Common.UI.Products.Shared.Pages.Document
{
    using Framework.Common.UI.Products.Shared.Components.AlphabeticalIndex;

    /// <summary>
    /// Index document page
    /// </summary>
    public class IndexDocumentPage : CommonDocumentPage
    {
        /// <summary>
        /// Alphabetical index component
        /// </summary>
        public AlphabeticalIndexComponent AlphabeticalIndex { get; } = new AlphabeticalIndexComponent();
    }
}
