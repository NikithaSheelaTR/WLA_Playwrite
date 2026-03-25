namespace Framework.Common.UI.Products.WestLawNext.Pages.Document
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Pages.Document;

    /// <summary>
    /// Document Page with Chunk component
    /// </summary>
    public class ChunkingDocumentPage : CommonDocumentPage
    {
        /// <summary> Chunk Navigation component  </summary>
        public ChunkNavigationComponent ChunkNavigation { get; protected set; } = new ChunkNavigationComponent();
    }
}
