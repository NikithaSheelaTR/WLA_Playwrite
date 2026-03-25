namespace Framework.Common.UI.Raw.WestlawEdge.Pages.Document
{
    using Framework.Common.UI.Products.Shared.Components;

    /// <summary>
    /// Document page with chunk pagination
    /// </summary>
    public class EdgeChunkingDocumentPage: EdgeCommonDocumentPage
    {
        /// <summary> Chunk Navigation component  </summary>
        public ChunkNavigationComponent ChunkNavigation { get; protected set; } = new ChunkNavigationComponent();
    }
}